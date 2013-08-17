#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <netdb.h>
#include <semaphore.h>
#include <memory.h>
#include <pthread.h>
#include <string.h>
#include <dirent.h>
#include <sys/types.h>
#include <sys/stat.h>
#include "Headers/booleanLogic.h"
#include "Headers/communication.h"


void* StdInThread(void* value);
void* CallBackThread1(void* value);
void* CallBackThread2(void* value);

int _callBackSocket1 = FALSE;
int _callBackSocket2 = FALSE;

sem_t quitingSemaphore;
FILE* _mpiFD = FALSE;


int main(int argc, char**argv)
{	
  if(argc < 4)
  {
    printf("Exiting. Not all parameters supplied, expected callback port, application path, and gdb port.\n");
    exit(0);
  }

  int callbackPort = atoi(argv[1]);
  char* applicationPath = argv[2];
  int gdbPort = atoi(argv[3]);

  //Wait for a connection from the client
	sem_init(&quitingSemaphore, 0, 0);

	pthread_t stdInThreadId, callBackThread1Id, callBackThread2Id;

	pthread_create(&stdInThreadId, NULL, (void *)StdInThread, NULL);
  pthread_create(&callBackThread1Id, NULL, (void *)CallBackThread1, &callbackPort);
	pthread_create(&callBackThread2Id, NULL, (void *)CallBackThread2, &gdbPort);  

  char* localAddress = (char*)malloc(50*sizeof(char));      
  GetPrimaryIp(localAddress, 50) ;

  char applicationCommandLine[255];
  sprintf(applicationCommandLine, "%s %s %d %s %d", applicationPath, localAddress, callbackPort, applicationPath, gdbPort);

  _mpiFD = popen(applicationCommandLine, "w");
	sem_wait(&quitingSemaphore);
  return 0;
}


void* StdInThread(void* value)
{
	//Create a buffer to read data into
  int bufferSize = 8192;
  char inputBuffer[bufferSize];    
	
	int bytesRead = 0;

  int stdInSocket = fileno(stdin);

  while(1)
  {	
    //Read in from the input side
    bytesRead = read(stdInSocket, inputBuffer, bufferSize);
    
    if(bytesRead <= 0 || strstr(inputBuffer, "exit") != NULL)
    {
      break;
    }   		

		//Write to the output side
		if(write (_callBackSocket2, inputBuffer, bytesRead) < 0)
    {
	      break;
    }
  }

	//Notify the sempahore that we are no longer piping
  sem_post(&quitingSemaphore);

  return NULL;
}

void* CallBackThread1(void* value)
{
  int bufferSize = 8192;
  char inputBuffer[bufferSize];    
	
	int bytesRead = 0;

  _callBackSocket1 = CreateIncomingConnection(*((int*)value));
  int stdOutSocket = fileno(stdout);

  while(1)
  {	
    //Read in from the input side
    bytesRead = read(_callBackSocket1, inputBuffer, bufferSize);
    if(bytesRead <= 0)
    {
      close(_callBackSocket1);
      close(_callBackSocket2);
      break;
    }

	  //Write to the output side
	  write (stdOutSocket, inputBuffer, bytesRead);
  }

  if(_mpiFD != FALSE)
    pclose(_mpiFD);
  return NULL;
}

void* CallBackThread2(void* value)
{
  int bufferSize = 8192;
  char inputBuffer[bufferSize];    
	int bytesRead = 0;
  _callBackSocket2 = CreateIncomingConnection(*((int*)value));

  int attached = FALSE;
  int resumed = FALSE;

  int stdOutSocket = fileno(stdout);

  while(1)
  {	
    memset(inputBuffer, '\0', bufferSize);

    //Read in from the input side
    bytesRead = read(_callBackSocket2, inputBuffer, bufferSize);
    if(bytesRead <= 0)
    {
      close(_callBackSocket2);
      break;
    }

    if(attached == FALSE)
    {   
      char attachBuffer[100];
      int attachBufferLen = sprintf(attachBuffer, "attach %s\n", inputBuffer);
      write (_callBackSocket2, attachBuffer, attachBufferLen);
      attached = TRUE;
    }
    else
    {
     if(resumed == FALSE && strstr(inputBuffer, "(gdb)") != NULL)
     {
        resumed = TRUE;
        write(_callBackSocket1, "GO", 2);
        write(_callBackSocket2, "n\n", 2);
        write(_callBackSocket2, "n\n", 2);
     }
     else
     {
		    //Write to the output side
		    write (stdOutSocket, inputBuffer, bytesRead);      
      }
    }
  }

  return NULL;
}

