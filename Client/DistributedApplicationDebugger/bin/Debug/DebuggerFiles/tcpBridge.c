#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <netdb.h> 
#include <semaphore.h>
#include <pthread.h>
#include "Headers/communication.h"
#include "Headers/booleanLogic.h"

#define STRING_EQUALS_INDICATOR 0
#define CLIENT_MODE "-c"
#define BRIDGE_MODE "-b"
#define SERVER_MODE "-s"

//Structure to pair an fd to read from and an fd to write to
struct pipePair_item
{
  int In;
  int Out;
};
typedef struct pipePair_item pipePair;

//Helper Methods
void PipeMessages(void *value);
void RunClientMode(char* outgoingPath, int outgoingPort);
void RunServerMode(int incomingPort);
void RunBridgeMode(int incomingPort, char* outgoingPath, int outgoingPort);
void SetSocketOptions(int *socketPtr);
void InitializeSockAddr(struct sockaddr_in *address, in_addr_t path, int port);
pipePair* CreatePipePair(int in, int out);

//Semaphore to wait on to close out the application
sem_t quitingSemaphore;


int main(int argc, char**argv)
{
	//First argument is the mode -c for client, -b for bridge, -s for server
  if(strcmp(argv[1], CLIENT_MODE) == STRING_EQUALS_INDICATOR)
  {
  	//Client mode - arg 2 is the port to connect to, arg 3 is the ip address
    //RunClientMode(argv[3], atoi(argv[2]));
    if(argc == 3)
    {
      //they did not include the server ipaddress, assume we want to use our own
      char* ipAddress = (char*)malloc(50*sizeof(char));      
      GetPrimaryIp(ipAddress, 50) ;
      printf("running alternate version, ipaddress is %s\n", ipAddress);
      RunClientMode(ipAddress, atoi(argv[2]));      
      free(ipAddress);
    }
    else
    {
      RunClientMode(argv[3], atoi(argv[2]));
    }
  }
  else if(strcmp(argv[1], SERVER_MODE) == STRING_EQUALS_INDICATOR)
  {
  	//Server mode, arg 2 is the port to listen to
    RunServerMode(atoi(argv[2]));
  }
  else if(strcmp(argv[1], BRIDGE_MODE) == STRING_EQUALS_INDICATOR)
  {
  	//Bridge mode, arg 2 is the incoming port, arg 3 & 4 are the outgoing ip address and port
    RunBridgeMode(atoi(argv[2]), argv[3], atoi(argv[4]));
  }

  return 0;
}

/*Establishes an outgoing connection on the specified 
	port and address and pipes all data from stdout to it*/
void RunClientMode(char* outgoingPath, int outgoingPort)
{	
	//Create outgoing connection
	int socketOut = CreateOutgoingConnection(outgoingPath,outgoingPort);
  if(socketOut == FALSE)
  	return;
  
  //Start a background thread to pipe messages from stdin to the outgoing port
  pthread_t threadId;
  pthread_create(&threadId, NULL, (void *)PipeMessages, CreatePipePair(fileno(stdin), socketOut));
	
	//Post for just 1 thread to release before we finish
	sem_init(&quitingSemaphore, 0, 0);

	//Wait until the piping thread releases to return
	sem_wait(&quitingSemaphore);

	//clean up the connection created.
  close(socketOut);
}

/*Establishes an incoming connection on the specified port 
		and pipes all data from it to stdout*/
void RunServerMode(int incomingPort)
{
	//Create an incoming connection
	int socketIn = CreateIncomingConnection(incomingPort);
	if(socketIn == FALSE)
		return;

	//Start a background thread to pipe messages from the incoming port to stdout
  pthread_t threadId;
  pthread_create(&threadId, NULL, (void *)PipeMessages, CreatePipePair(socketIn, fileno(stdout)));
	
	//Post for just 1 thread to release before we finish
	sem_init(&quitingSemaphore, 0, 0);

	//Wait until the piping thread releases to return
	sem_wait(&quitingSemaphore);
  
  //clean up the connection created.
  close(socketIn);
}

/*Establishes an incoming connection on the specified port and an outgoing
	connection on port and ipaddress specified and ports messages between them.*/ 
void RunBridgeMode(int incomingPort, char* outgoingPath, int outgoingPort)
{
	//Create an incoming connection
	int socketIn = CreateIncomingConnection(incomingPort);
	if(socketIn == FALSE)
		return;

	//Create an outgoing connection
	int socketOut = CreateOutgoingConnection(outgoingPath,outgoingPort);
	if(socketOut == FALSE)
	{
		close(socketIn);
		return;
	}
	
  pthread_t clientThreadId, serverThreadId;

	//Start 2 threads to pipe incoming data from either direction to the other connection
  pthread_create(&clientThreadId, NULL, (void *)PipeMessages, CreatePipePair(socketIn , socketOut));
  pthread_create(&serverThreadId, NULL, (void *)PipeMessages, CreatePipePair(socketOut, socketIn));

	
	sem_init(&quitingSemaphore, 0, 0);

	//Wait until both piping threads releases to return
	sem_wait(&quitingSemaphore);

	//Clean up the connections created.
  close(socketOut);
  close(socketIn);
}

//Creates a structure pairing an input fd with an output fd
pipePair* CreatePipePair(int in, int out)
{
	//Create the pair
  pipePair* pair = (pipePair*)malloc(sizeof(pipePair));
        
  //Assign their values
  pair->In = in;
  pair->Out = out;       

	//Return it
  return pair; 
}

//Takes in a pipePair and cycles through reading from the input
//side and writing that data to its output side.
void PipeMessages(void *value)
{
	//The input value is expedted to be a pipe pair
  pipePair *pair = (pipePair *)value;

	//Create a buffer to read data into
  int bufferSize = 8192;
  char inputBuffer[bufferSize];    
	
	int bytesRead = 0;
  while(1)
  {	
    //Read in from the input side
    bytesRead = read(pair->In, inputBuffer, bufferSize);
    
    if(bytesRead < 0)
    	break;
		
		//Write to the output side
		if(write (pair->Out, inputBuffer, bytesRead) < 0)
			break;
  }

	//Notify the sempahore that we are no longer piping
  sem_post(&quitingSemaphore);

	//Clean up the value passed in.
  free(pair);
}

