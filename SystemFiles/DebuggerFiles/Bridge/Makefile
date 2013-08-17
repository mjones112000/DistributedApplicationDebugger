# usage: make clean
#	 make FILE=test-file-name 
#	 make run FILE=test-file-name DATA=data-file-name

all:
	`which gcc` -Wall -O3 -Wno-unused -c ../communication.c
	`which gcc` -Wall -O3 -c ../collections.c
	`which gcc` -Wall -O3 -c ../tcpBridge.c
	`which gcc` -Wall -O3 -o tcpBridge tcpBridge.o communication.o -lpthread

	rm -f *.o
	rm -f *~
run:
	./$(FILE) $(DATA)

clean:
	rm -f *.o
	rm -f *~
