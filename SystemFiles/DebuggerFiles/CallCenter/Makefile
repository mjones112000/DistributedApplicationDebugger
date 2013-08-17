# usage: make clean
#	 make FILE=test-file-name 
#	 make run FILE=test-file-name DATA=data-file-name

all:
	gcc -Wall -O3 -Wno-unused -c -o ../XML/obj/xmlDoc.o ../XML/xmlDoc.c
	gcc -Wall -O3 -Wno-unused -c -o ../XML/obj/xmlReader.o ../XML/xmlReader.c
	gcc -Wall -O3 -Wno-unused -c -o ../XML/obj/xmlWriter.o ../XML/xmlWriter.c
	`which gcc` -Wall -O3 -g -c ../communication.c
	`which gcc` -Wall -O3 -g -c ../collections.c
	`which gcc` -Wall -O3 -g -c ../DADParser.c
	`which mpicc` -Wall -O3 -Wno-int-to-pointer-cast -Wno-implicit-function-declaration -Wno-format-security -g -c ../callCenter.c
	`which mpicc` -Wall -O3 -g -o callCenter ../XML/obj/* callCenter.o communication.o collections.o DADParser.o -lpthread
	rm -f *.o
	rm -f *~
run:
	./$(FILE) $(DATA)

clean:
	rm -f *.o
	rm -f *~

