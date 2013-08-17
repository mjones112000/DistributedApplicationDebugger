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
	mpicc -g -s -Wall -O0 -c -o obj/testCases.o -DMPIDEBUG testCases.c
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

