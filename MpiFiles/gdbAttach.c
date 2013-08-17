#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>
#include <semaphore.h>
#include <string.h>
#include "Headers/booleanLogic.h"
#include "Headers/gdbAttach.h"
#include "Headers/communication.h"
#include "Headers/DADParser.h"

void AttachGDB(char* applicationLocation, int mainSocket, char* callbackAddress, int callbackPort, int nodeId)
{
  int mainPid = getpid();
  int pid = fork (); 
  if (pid == (pid_t)0) 
	{	
    //The ends of the pipes for communication from the child to parent
  	//and parent to child
	  int fromChildPipe[2];
  	int fromParentPipe[2];
	
	  int childPipeResult = pipe (fromChildPipe); 
  	int parentPipeResult = pipe (fromParentPipe);

    //This is the child process
    //Fork into the GDB section and the controller process
    int pid2 = fork();
    if(pid2 == (pid_t)0)
    {
      //Overwrite stdin, stdout, and stderror
	    close(0);
	    int stdInDupResult = dup(fromParentPipe[0]);	

	    close(1);
	    int stdOutDupResult = dup(fromChildPipe[1]);

	    close(2);
	    int stdErrResult = dup(fromChildPipe[1]);

	    //create the gdb command for the test file
	    char* gdbCommand[4];
	    gdbCommand[0] = "gdb";
      gdbCommand[1] = applicationLocation;
      gdbCommand[2] = NULL;

      //Shell out the gdb command
      execvp(gdbCommand[0], gdbCommand);
    }   
    else
    {
      //Make a second connection to the call center for control
      int gdbSocketOut = CreateOutgoingConnection(callbackAddress, callbackPort); 
	
      gdbAttachInfo *attachInfo = (gdbAttachInfo*)malloc(sizeof(gdbAttachInfo));
      InitializeAttachInfo(attachInfo);
      sem_init(&(attachInfo->quitingSemaphore), 0, 0);

      attachInfo->streamIncoming = fromChildPipe[0];        
      attachInfo->streamOutgoing = fromParentPipe[1];
      attachInfo->gdbSocketOut = gdbSocketOut;


		  //Launch threads for the child listen and the tcp listen
		  pthread_t childThreadId, tcpListenThreadId;
		  pthread_create(&childThreadId, NULL, (void *)gdbProcessListener, attachInfo);
      pthread_create(&tcpListenThreadId, NULL, (void *)gdbProcessWriter, attachInfo);

			SendIdData(gdbSocketOut, nodeId, mainPid);

      sem_wait(&(attachInfo->quitingSemaphore));

      free(attachInfo);
      exit(0);
    }

    return;
	} 
	else 
	{
    int bufferSize = 8192;
    char inputBuffer[bufferSize];    
    //This will force us to wait for the call center to acknowlege the gdb callback
   	if(read(mainSocket, inputBuffer, bufferSize) < 0)
   	{
   		exit(0);
		}
   }
}

void* gdbProcessListener(void* val)
{
  gdbAttachInfo *attachInfo = (gdbAttachInfo*)val;

  int bufferSize = 8192;
	//Read one char at a time from child infinitly.
	char* inputBuffer = (char*)malloc(bufferSize*sizeof(char));

  int bytesRead = 0;
	while(1)
	{  
    //Read in from the input side of the gdb process
    bytesRead = read(attachInfo->streamIncoming, inputBuffer, bufferSize);
		
    if(bytesRead <= 0)
    {
      close(attachInfo->gdbSocketOut);
    	break;
    }

		//Write to the output side
		if(write (attachInfo->gdbSocketOut, inputBuffer, bytesRead) <= 0)
			break;
	}	

	//Notify the sempahore that we are no longer piping
  sem_post(&(attachInfo->quitingSemaphore));
  return NULL;
}

//Thread for listen to the tcp port
void* gdbProcessWriter(void* val)
{ 
  gdbAttachInfo *attachInfo = (gdbAttachInfo*)val;

  int bufferSize = 8192;
	//Read one char at a time from child infinitly.
	char* inputBuffer = (char*)malloc(bufferSize*sizeof(char));

  int bytesRead = 0;
	while(1)
	{  
    //read from the call center
    bytesRead = read(attachInfo->gdbSocketOut, inputBuffer, bufferSize);

    if(bytesRead <= 0)
    {
      close(attachInfo->gdbSocketOut);
    	break;
    }

		//Write to the output side
		if(write (attachInfo->streamOutgoing, inputBuffer, bytesRead) <= 0)
			break;
	}	

  //shut down gdb
  if(write(attachInfo->streamOutgoing, "quit\n", 5)<0)
  {
  	//no big deal, ignore	
	}

	//Notify the sempahore that we are no longer piping
  sem_post(&(attachInfo->quitingSemaphore));
  return NULL;
}


void InitializeAttachInfo(gdbAttachInfo* newGdbAttachInfo)
{
  sem_init(&(newGdbAttachInfo->quitingSemaphore), 0, 0);
  newGdbAttachInfo->streamIncoming = FALSE;
  newGdbAttachInfo->streamOutgoing = FALSE;
  newGdbAttachInfo->gdbSocketOut = FALSE;
}


