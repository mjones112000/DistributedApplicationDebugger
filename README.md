This section deals with the four step process needed to compile an MPI project with the correct libraries in order 
to allow for The Distributed Application Debugger's Client and Call Center to be able to interact with it. 



1. Copy the MpiFiles directory to your root directory.

The MpiFiles directory contains all of the files needed in order to compile your project.  This directory just needs to be copied to your
root directory one time and then, in order to inject The Distributed Application Debugger's Runtime component, 
you just need to put your project files in this folder.  Inside of this folder is a bin directory which will
be the destination of all the binaries complied using the Makefile in step 4.
 
2. Create .distributedApplicationDebugger.conf.

There is a hidden file called .distributedApplicationDebugger.conf which The Call Center looks for 
in order to know where to find the MpiFiles/bin directory created in Step 1.  This file needs to just be created once
and is expected to be placed in the user's root directory.  For example, for user mjones the file would
be placed in /home/mjones and contain the following line:
/home/mjones/MpiFiles/bin/

3. Include mpi.h.

The user needs to include the local mpi.h in their mpi files instead of the installed real framework mpi.h.  
This file will inject The Runtime into the code and then do all of the
pre and post processing used to utilize the tool.  In order to organize the header files, the MpiFiles includes the needed
mpi.h in a Headers subdirectory, so if the files are included in the root MpiFiles directory, the included
file would be "Headers/mpi.h".  The file text.c included in the MpiFiles directory can be used as a sample 
for reference.

4. Run the Makefile from the command line.

After including the mpi.h file included in the MpiFiles directory, the user needs to compile their application with the 
Distributed Application Debugger's assemblies.  A Makefile to compile with is included within this directory too and its contents are shown below.
In order indicate the file to compile with the MPI runtime, the user must place the file in the MpiFiles directory and then run 
the make command with the name of the file to include.  Assuming that the user is compiling from their root directory, 
the MpiFiles directory is located there and the file to debug is called testParDev.c the command line to make the file would be:

make -C MpiFiles/ File=testParDev


Note that the .c extension is NOT included in the command line, just the file name. The Makefile compiles all of the 
needed assemblies, including the XML, collection, parsing, and validation libraries and then compiles the user's code.  
The important line of the Makefile is line 21 which reads:

mpicc -g -s -Wall -O0 -c -o \$(FILE).o -DMPIDEBUG \$(FILE).c 

The -DMPIDEBUG token means that when the header information from the mpi.h file is compiled, that the 
debug.h and mpidebug.h files will be included as well.  These files
 redirect the calls to the MPI library to intermediary libraries prefaced with underscores.  

5. When running the GUI make sure that the transfer directory is set to ./callCenter in the configuration part.

The Makefile included with the MPI runtime files.


# usage: make clean
#	 make FILE=test-file-name 
#	 make run FILE=test-file-name DATA=data-file-name

all:
	gcc -Wall -O3 -Wno-unused -c -o XML/obj/xmlDoc.o XML/xmlDoc.c
	gcc -Wall -O3 -Wno-unused -c -o XML/obj/xmlReader.o XML/xmlReader.c
	gcc -Wall -O3 -Wno-unused -c -o XML/obj/xmlWriter.o XML/xmlWriter.c
	gcc -Wall -O3 -Wno-unused -c -o obj/dictionary.o dictionary.c
	gcc -Wall -O3 -Wno-unused -c -o obj/DADParser.o DADParser.c
	gcc -Wall -O3 -Wno-unused -c -o obj/communication.o communication.c
	gcc -Wall -O3 -Wno-unused -c -o obj/collections.o collections.c
	mpicc -Wall -O3 -Wno-unused -c -o obj/mpiUtils.o mpiUtils.c
	mpicc -Wall -O3 -Wno-unused -c -o obj/mpiXML.o mpiXML.c
	mpicc -Wall -O0 -Wno-unused -c -o obj/mpiValidate.o mpiValidate.c
	mpicc -Wall -O0 -Wno-unused -c -o obj/mpiSerialize.o mpiSerialize.c
	mpicc -Wall -O0 -Wno-unused -c -o obj/gdbAttach.o gdbAttach.c
	mpicc -Wall -O0 -Wno-unused -c -o obj/mpidebug.o mpidebug.c
	strip XML/obj/*.o -S
	strip obj/*.o -S
	mpicc -g -s -Wall -O0 -c -o $(FILE).o -DMPIDEBUG $(FILE).c 
	mpicc -o bin/$(FILE) $(FILE).o XML/obj/*.o obj/*.o -lpthread
run:
	./$(FILE) $(DATA)

clean:
	rm -f *.o
	rm -f *~
	rm -f obj/*.o
	rm -f obj/*.~
	rm -f XML/obj/*.o
	rm -f XML/obj/*.~
	rm -f Headers/*.*~
