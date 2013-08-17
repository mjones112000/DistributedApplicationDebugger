#include <stdio.h>
#include <stdlib.h>
#include <time.h>


void getDateTime(char** resultBuff, int buffLength, int *resultLength)
{
	time_t rawtime;
  struct tm * timeinfo;
		
	time(&rawtime);
	timeinfo = localtime(&rawtime);
  *resultLength = strftime(*resultBuff, buffLength, "%Y-%m-%d-T%H:%M:%S\n", timeinfo);
}

void main(){
/*
	char buffer [100];
	time_t rawtime;
  struct tm * timeinfo;
		
	time(&rawtime);
	timeinfo = localtime(&rawtime);
  int length = strftime(buffer, 100, "%Y-%m-%d-T%H:%M:%S\n", timeinfo);
  */
  
  char* buffer = (char*)malloc(100*sizeof(char));
  int resultLength = 0;
  getDateTime(&buffer, 100, &resultLength);
  printf("Length was %d, %s\n", resultLength, buffer);
  free(buffer);
}
