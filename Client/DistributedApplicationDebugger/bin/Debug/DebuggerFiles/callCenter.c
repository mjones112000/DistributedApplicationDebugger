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
#include "XML/xml.h"
#include "Headers/booleanLogic.h"
#include "Headers/DADParser.h"
#include "Headers/communication.h"
#include "Headers/collections.h"
#include "Headers/callCenter.h"
#include "Headers/mpiUtils.h"

#define STRING_EQUALS_INDICATOR 0

char* GenerateEnvironmentData();
void GenerateBufferData();
clusterCommunication* GeneratePlaySession();
clusterCommunication* GenerateRecordOrReplaySession(int recordMode);
int CheckForFile(const char* exeLocation);
void* ProcessGDBMessages(void* value);
void IssueGdbCommand();
					
int _sessionRunning = FALSE;
int _mpiRunning = FALSE;

int _incomingPort = FALSE;
int _connectionEndPoint;
struct sockaddr_in _incomingAddress;

int _clientSocket;

FILE* _mpiFD = FALSE;
queue* _clusterNodeList = NULL;

sem_t clusterQueueLock;

queue* _outgoingMessageList = NULL;
sem_t outgoingQueueLock;
sem_t outgoingQueueNotification;
sem_t outgoingThreadCompleteNotification;

char* _sohReplace =  NULL;
char* _partitionReplace =  NULL;
char* _eotReplace =  NULL;



int main(int argc, char**argv)
{	
  _sessionRunning = TRUE;

  //Wait for a connection from the client
  _incomingPort = atoi(argv[1]);
  ListenForClient();

  return 0;
}

/*Establishes an incoming connection on the specified port 
		and pipes all data from it to stdout*/
void ListenForClient()
{
  _connectionEndPoint = socket(AF_INET, SOCK_STREAM, 0);
  SetSocketOptions(&_connectionEndPoint);

	//Bind to the port passed in
  InitializeSockAddr(&_incomingAddress,INADDR_ANY,_incomingPort);  
	if(bind(_connectionEndPoint, (struct sockaddr *) &_incomingAddress,sizeof(_incomingAddress)) < 0)
 		return;

	//Listen for 1 connection on the incoming port	
  listen(_connectionEndPoint,1);
  
  //Accept the connection
  socklen_t clilen = sizeof(_incomingAddress);
  _clientSocket = accept(_connectionEndPoint, (struct sockaddr *) &_incomingAddress, &clilen);
 // SetSocketOptions(&_clientSocket);

  _clusterNodeList = (queue*)malloc(sizeof(queue));
  InitializeQueue(_clusterNodeList);

  _outgoingMessageList = (queue*)malloc(sizeof(queue));
  InitializeQueue(_outgoingMessageList);

  //Create an input buffer to store the incoming byte stream
  charList* inputBuffer = (charList*)malloc(sizeof(charList));;
  InitializeCharList(inputBuffer);

  //Create a storing the processed messages
  queue* messages = (queue*)malloc(sizeof(queue));
  InitializeQueue(messages);  

	//Post for just 1 thread to be allowed to add to the cluster queue
	sem_init(&clusterQueueLock, 0, 1);

  //Post for just 1 thread to be allowed to add and remove from the outgoing client queue
	sem_init(&outgoingQueueLock, 0, 1);

  //Post for the notification lock to initially be empty till the outgoing queue has something in it
  sem_init(&outgoingQueueNotification, 0, 0);

  //Post for the comleted lock to initially be empty so that we will know when it has actually completed
  sem_init(&outgoingThreadCompleteNotification, 0, 0);

  //Start the outgoing message thread
  pthread_t threadId;
  pthread_create(&threadId, NULL, WriteToClient, NULL);

	//Create a buffer to read data into
  int bufferSize = 1048;
  char readBuffer[bufferSize];    

	int bytesRead = 0;
  while(_sessionRunning == TRUE)
  {	
    //Clear the reading buffer before each read
    memset(readBuffer, '\0', bufferSize);

    //Read in from the input side
    bytesRead = read(_clientSocket, readBuffer, bufferSize);
   
    if(bytesRead <= 0)
    	break;
	
    //Copy the input buffer to the input stream
    AddChars(inputBuffer,  readBuffer, bytesRead);

    //Parse the input buffer for whole messagses from the client and trim off the EOT and SOH
    ParseInputBuffer(inputBuffer, messages, TRUE);    

    //Process the messages from the Client's side
    ProcessClientMessages(messages);
  }

  _sessionRunning = FALSE;

  //Notify the outogoing queue thread to stop waiting and close
  sem_post(&outgoingQueueNotification);
  //Wait for the outgoing thread to complete before we close it all down
  sem_wait(&outgoingThreadCompleteNotification);

  //Close out the file descriptor from mpirun
  if(_mpiFD != FALSE)
    pclose(_mpiFD);

  //clean up the connection created.
  close(_clientSocket);

  CleanUpCharList(inputBuffer);
  CleanUpQueue(messages);

  //CleanUpQueue(_clusterNodeList);
  CleanUpQueue(_outgoingMessageList);
}

void CreateDirectory(char* directory)
{
	struct stat sb;
	
	if(stat(directory, &sb) != 0)
	{
		mkdir(directory, S_IRWXU);
	}
}

int StrEqual(char* str1, char* str2)
{
	if(strcmp(str1, str2) == STRING_EQUALS_INDICATOR)
		return TRUE;
	else
		return FALSE;	
}

//Handles Play, Record, Replay commands from the Client
void ProcessClientMessages(queue* messages)
{
  //Keep processing until the queue is empty
  while(IsQueueEmpty(messages) == FALSE)
  {
    //Dequeue the messages and process them in order
    char* processedMessage = NULL;
    Dequeue((void**)(&processedMessage), messages);

    //The message is 
  	char* token = strtok(processedMessage, PARTITION_STR); 	
		
  	//Make sure we got something
    if(token != NULL)
    {
    	if(StrEqual(token, "ENVIRONMENT") == TRUE)
    	{
		  	//get the outgoing queue's lock
		  	sem_wait(&outgoingQueueLock);
		  	
		    //Add a record of the history to the outgoing queu
		    Enqueue(_outgoingMessageList, (void**)GenerateEnvironmentData());
				
		    //Let the otugoing thread know that there is queued up data
		  	sem_post(&outgoingQueueNotification);
          
        //Release the ougoing queue
      	sem_post(&outgoingQueueLock);
			}
			else if(strcmp(token, "BUFFER REQUEST") == STRING_EQUALS_INDICATOR)
			{
				GenerateBufferData();
		 	}
      else if(StrEqual(token, "PLAY") == TRUE)
      {
      	clusterCommunication* session = GeneratePlaySession();
      	if(session != NULL)
      	{
      		_mpiRunning = TRUE;
	      	pthread_t minionThread;
	      	pthread_create(&minionThread, NULL, ListenToCluster, session);
	      }
      }  
		  else if(StrEqual(token, "RECORD") == TRUE || 
		  				StrEqual(token, "REPLAY") == TRUE)
		 	{			 		
		 		int recordMode = StrEqual(token, "RECORD");
		 		
				clusterCommunication* session = GenerateRecordOrReplaySession(recordMode);
      	if(session != NULL)
      	{
      		_mpiRunning = TRUE;
		 			pthread_t minionThread;
  				pthread_create(&minionThread, NULL, ListenToCluster, session);	
		 		}
			}
			else if(StrEqual(token, "GDB INPUT") == TRUE)
			{
				IssueGdbCommand();												
			}
			else if(StrEqual(token, "MPI COMPLETE") == TRUE)
			{		
				_mpiRunning = FALSE;
				
				//LOOP THROUGH AND CLOSE CLUSTER CONNECTIONS
				while(IsQueueEmpty(_clusterNodeList) == FALSE)
      	{
        	//Dequeue the messages and drop them on the outgoing message queue
        	clusterNode* completedClusterNode = NULL;
        	Dequeue((void**)(&completedClusterNode), _clusterNodeList);
        	sem_post(&(completedClusterNode->messageNotification));
        	if(completedClusterNode->gdbSocket != FALSE)
        	{
        		close(completedClusterNode->gdbSocket);
        	}     		
      	}
      					
			  //Close out the file descriptor from mpirun
			  if(_mpiFD != FALSE)
			  {
    			pclose(_mpiFD);
					_mpiFD = FALSE;
				}
			}
			
		  free(processedMessage);
		}
  }
}

char* GenerateEnvironmentData()
{
  //Get the session history
 	charList* environmentDataList = (charList*)malloc(sizeof(charList));
	InitializeCharList(environmentDataList);

  AddChars(environmentDataList, "ENVIRONMENT DATA|",17);

  char* configInfo = GetConfigInfo();
  AddChars(environmentDataList, configInfo, strlen(configInfo));
  free(configInfo);
  
  AddChars(environmentDataList, "|", 1);

  //The next token should be the location of the session folder to request history
	GetSessionHistory(strtok(NULL, PARTITION_STR), environmentDataList);
  
  //Copy the message
  char* environmentData = (char*)malloc((environmentDataList->ItemCount + 2) * sizeof(char));
  environmentData[0] = SOH;
  memcpy(environmentData+1, environmentDataList->Items, environmentDataList->ItemCount);
  environmentData[environmentDataList->ItemCount + 1] = EOT;
	CleanUpCharList(environmentDataList);
	
	return environmentData;
}

clusterCommunication* GeneratePlaySession()
{
	int clusterSize = atoi(strtok(NULL, PARTITION_STR));
	
	char* exeLocation = strtok(NULL, PARTITION_STR);
	if(CheckForFile(exeLocation) == FALSE)
		return NULL;	
	
	
	char* hostFileLocation = strtok(NULL, PARTITION_STR);
	char* parameters = strtok(NULL, PARTITION_STR);
	
		//The next three should be the replacement soh, partition, and eot chars
	char* sohReplace = strtok(NULL, PARTITION_STR);
	char* barReplace = strtok(NULL, PARTITION_STR);
	char* eotReplace = strtok(NULL, PARTITION_STR);
	StoreReplacementChars(sohReplace, barReplace, eotReplace); 


  int playCommandSize = 500;
	char* mpiPlayRequest = (char*)malloc(playCommandSize*sizeof(char));
	memset(mpiPlayRequest, '\0', playCommandSize);
	
	CreateMPIRequest(mpiPlayRequest, clusterSize, 
		exeLocation,hostFileLocation,parameters, strtok(NULL, PARTITION_STR));	
			
  clusterCommunication* clusterComm = 
  	(clusterCommunication*)malloc(sizeof(clusterCommunication));
  	
  clusterComm->clusterSize = clusterSize;
  clusterComm->clusterCommandLine = mpiPlayRequest;

	return clusterComm;
}

clusterCommunication* GenerateRecordOrReplaySession(int recordMode)
{
	int clusterSize = atoi(strtok(NULL, PARTITION_STR));

	char* exeLocation, *hostFileLocation, *parameters, *sessionsFolderLocation;
	char* sessionName, *timeBuffer, *sessionFolder, *sessionInfoFolder;
	int timeBufferSize = 50;
	timeBuffer = (char*)malloc(timeBufferSize*sizeof(char));
				
	exeLocation = strtok(NULL, PARTITION_STR);
	if(CheckForFile(exeLocation) == FALSE)
	{
		free(exeLocation);
		return NULL;
	}
	
	hostFileLocation = strtok(NULL, PARTITION_STR);

	parameters = strtok(NULL, PARTITION_STR);

  //The next three should be the replacement soh, partition, and eot chars
	char* sohReplace = strtok(NULL, PARTITION_STR);
	char* barReplace = strtok(NULL, PARTITION_STR);
	char* eotReplace = strtok(NULL, PARTITION_STR);
	StoreReplacementChars(sohReplace, barReplace, eotReplace); 

	
	//Creeate the initial mpi request
	int commandSize = 500;
	char* mpiRequest = (char*)malloc(commandSize*sizeof(char));
	memset(mpiRequest, '\0', commandSize);
		
	CreateMPIRequest(mpiRequest, 
		clusterSize,exeLocation,hostFileLocation, parameters, strtok(NULL, PARTITION_STR));

	//Get the session specific part of the command
	sessionsFolderLocation = strtok(NULL, PARTITION_STR);
	
	sessionName = strtok(NULL, PARTITION_STR);
	
	asprintf(&sessionFolder, "%s/%s", sessionsFolderLocation, sessionName);
	
	if(recordMode == TRUE)
	{
		getDateTime(&timeBuffer, timeBufferSize);
		asprintf(&sessionInfoFolder, "%s/%s", sessionFolder, timeBuffer);

		//Get the session history
	 	charList* recordSessionData = (charList*)malloc(sizeof(charList));
		InitializeCharList(recordSessionData);

		//Log the session parameters
		LogSessionInfo(sessionName, timeBuffer, clusterSize, 
			hostFileLocation, exeLocation, parameters, 
			sessionsFolderLocation, sessionFolder, sessionInfoFolder, 
			recordSessionData);
	
		//append record and the directory to the end
		sprintf(mpiRequest + strlen(mpiRequest), " -r -d %s", sessionInfoFolder);								
	
		//get the outgoing queue's lock
		sem_wait(&outgoingQueueLock);
		
		//Copy the message
		char* recordSession = (char*)malloc((recordSessionData->ItemCount + 2) * sizeof(char));
		recordSession[0] = SOH;
		memcpy(recordSession +1, recordSessionData->Items, recordSessionData->ItemCount);
		recordSession[recordSessionData->ItemCount + 1] = EOT;
		
		//Send back data with the record session name
		Enqueue(_outgoingMessageList, (void**)(recordSession));
	
		//clean up
		CleanUpCharList(recordSessionData);
	
		//Let the otugoing thread know that there is queued up data
		sem_post(&outgoingQueueNotification);
		  
		//Release the ougoing queue
		sem_post(&outgoingQueueLock);
	
	}
	else
	{
		//Get the speicifc session time from the request
		asprintf(&timeBuffer, "%s", strtok(NULL, PARTITION_STR));
		asprintf(&sessionInfoFolder, "%s/%s", sessionFolder, timeBuffer);	
		
		//Get the comman delimited list of nodes in 'Playback' mode
		char* playbackNodeList;
		playbackNodeList = strtok(NULL, PARTITION_STR);

		//append replay, the node list and the directory to the end
		sprintf(mpiRequest + strlen(mpiRequest), 
			" -p %s -d %s", playbackNodeList, sessionInfoFolder);	
			
	}
							
	//Create a communication structure for Recording	
  clusterCommunication* clusterComm = 
    (clusterCommunication*)malloc(sizeof(clusterCommunication));

  clusterComm->clusterSize = clusterSize;
  clusterComm->clusterCommandLine = mpiRequest;

	free(timeBuffer);
	free(sessionFolder);
	free(sessionInfoFolder);

	return clusterComm;
}

void IssueGdbCommand()
{
	int nodeId = atoi(strtok(NULL, PARTITION_STR));
	char* gdbCommand = strtok(NULL, PARTITION_STR);
	
 	sem_wait(&clusterQueueLock);

 	StartIterateQueue(_clusterNodeList);
	clusterNode* connectedNode = NULL;
	IterateQueue((void**)&connectedNode, _clusterNodeList);
	
	while(connectedNode != NULL)
	{
		if(connectedNode->nodeId == nodeId)
		{
			//we found the node, write to the gdb side
			Write (connectedNode->gdbSocket, gdbCommand, strlen(gdbCommand));
     	break;
		}
		IterateQueue((void**)&connectedNode, _clusterNodeList);
	}
	
	//Release the cluster	
	sem_post(&clusterQueueLock);
}

void LogSessionInfo(char* sessionName, char* timeBuffer, int clusterSize, 
	char* hostFileLocation, char* exeLocation, char* parameters, char* sessionsFolderLocation, 
	char* sessionFolder, char* sessionInfoFolder, charList* recordSessionData)
{
		AddChars(recordSessionData, "RECORD SESSION|",15);
		AddChars(recordSessionData, timeBuffer, strlen(timeBuffer));
		AddChars(recordSessionData, "|", 1);

	//Make the base 'session info' node
	XMLNode *sessionInfoNode = xmlCreateNode(ELEMENT_NODE, "SessionInfo", NULL);

	//Add its children
	xmlAddChildNode(sessionInfoNode, createStringElementNode("SessionName", sessionName));
	xmlAddChildNode(sessionInfoNode, createStringElementNode("StartTime", timeBuffer));
	xmlAddChildNode(sessionInfoNode, createIntElementNode("Nodes", clusterSize));
	xmlAddChildNode(sessionInfoNode, createStringElementNode("HostFile", hostFileLocation));
	xmlAddChildNode(sessionInfoNode, createStringElementNode("Location", exeLocation));
	xmlAddChildNode(sessionInfoNode, createStringElementNode("Parameters", parameters));

	//Make sure the Session folder is around
	CreateDirectory(sessionsFolderLocation);

	//Now make sure that there is a folder for this session
	CreateDirectory(sessionFolder);

	//Now make the folder for this session run
	CreateDirectory(sessionInfoFolder);			

	char* fileName;
	asprintf(&fileName, "%s/session.info", sessionInfoFolder);
	FILE* outputFile = fopen(fileName, "w");
	xmlWrite(sessionInfoNode, outputFile); 
	fflush(outputFile);       
	fclose(outputFile);

	//Read in the logged session info
	ReadChars(fileName, recordSessionData);

	//Free up the memory
	xmlFree(sessionInfoNode);	
	free(fileName);

}

int CheckForFile(const char* exeLocation)
{
	struct stat sb;
	if(!(stat(exeLocation, &sb) == 0 && S_ISREG(sb.st_mode)))
	{			  	
		//get the outgoing queue's lock
  	sem_wait(&outgoingQueueLock);
    //Copy the message
    char* fileNotFoundMessage;
    asprintf(&fileNotFoundMessage, 
    	"%cFILE NOT FOUND|%s%c", SOH, exeLocation, EOT);		    
  
  	//Add a record of the history to the outgoing queu
  	Enqueue(_outgoingMessageList, (void**)(fileNotFoundMessage));
					
  	//Let the otugoing thread know that there is queued up data
		sem_post(&outgoingQueueNotification);
    
  	//Release the ougoing queue
		sem_post(&outgoingQueueLock);
		return FALSE;
	}
	
	return TRUE;
}

void CreateMPIRequest(char* mpiRequest, int clusterSize, 
	char* exeLocation,char* hostFileLocation, char* parameters, char* gdbNodeList)
{
	if(StrEqual(hostFileLocation, " ") == FALSE &&
		 StrEqual(parameters, " ") == FALSE)
	{
		sprintf(mpiRequest, "mpirun -np %d -machinefile %s %s %s",
			clusterSize, hostFileLocation, exeLocation, parameters);							
	}
	else if(StrEqual(hostFileLocation, " ") == FALSE)
	{
		sprintf(mpiRequest, "mpirun -np %d -machinefile %s %s",
			clusterSize, hostFileLocation, exeLocation);	
	}
	else if(StrEqual(parameters, " ") == FALSE)
	{
		sprintf(mpiRequest, "mpirun -np %d %s %s",
			clusterSize, exeLocation, parameters);
	}
	else
	{
		sprintf(mpiRequest, "mpirun -np %d %s",
			clusterSize, exeLocation);
	}
	
	if(StrEqual(gdbNodeList, " ") == FALSE)
	{
		//append the gdb node list
		sprintf(mpiRequest + strlen(mpiRequest), " -g %s", gdbNodeList);	
	}
	
	//append the executable file location for attaching gdb
	sprintf(mpiRequest + strlen(mpiRequest), " -f %s -s %s,%s,%s", 
		exeLocation, _sohReplace, _partitionReplace, _eotReplace);	
}

char* RetrieveIdInformation(int readSocket, int* nodeId, int* processId)
{
	//Create a buffer to read data into
  int bufferSize = 100;
  char readBuffer[bufferSize];    

	int bytesRead = 0;	
		
  //Create an input buffer to store the incoming byte stream
  charList* localInputBuffer = (charList*)malloc(sizeof(charList));;
  InitializeCharList(localInputBuffer);

  queue* localMessageQueue = (queue*)malloc(sizeof(queue));
  InitializeQueue(localMessageQueue);  

	//Read the message as an id command
	while(IsQueueEmpty(localMessageQueue) == TRUE)
	{
		//zero out the read buffer
		memset(readBuffer, '\0', bufferSize);
		//Read in from the gdb connection
		bytesRead = read(readSocket, readBuffer, bufferSize);

		if(bytesRead <= 0)
			break;

		//Copy the input buffer to the input stream
		AddChars(localInputBuffer, readBuffer, bytesRead);

		//Parse the input buffer for whole messagses
		ParseInputBuffer(localInputBuffer, localMessageQueue, FALSE);
	}

	//we got at least one message, the first one should be process id of a node
	char* headerNode = NULL;
	Dequeue((void**)(&headerNode), localMessageQueue);
	
	char** splitArray = NULL;
	int itemCount = 0;
	itemCount = Split(headerNode, "|", &splitArray, 1, strlen(headerNode) - 1);
	*nodeId = atoi(splitArray[1]);
	*processId = atoi(splitArray[2]);
	
	CleanUpCharList(localInputBuffer);
	CleanUpQueue(localMessageQueue);
	CleanupStringArray(&splitArray, itemCount);	
	
	return headerNode;
}

void* ListenToCluster(void* value)
{
  clusterCommunication* clusterComm = (clusterCommunication*)value;
  int clusterSize = clusterComm->clusterSize;

	listen(_connectionEndPoint,clusterSize);

  //Now Launch the minions
  LaunchCluster(clusterComm);

	int connectedNodes = 0;
	
  //Accept the connection
  socklen_t clilen = sizeof(_incomingAddress);

	fd_set rfds;
 	struct timeval tv;
 	int retval;
  
  while(SessionIsRunning() == TRUE && connectedNodes < clusterSize) 
  {
   	FD_ZERO(&rfds);
   	FD_SET(_connectionEndPoint, &rfds);

   	/* Wait up to one seconds. */
   	tv.tv_sec = 1;
   	tv.tv_usec = 0;

   	retval = select(_connectionEndPoint + 1, &rfds, NULL, NULL, &tv);

		if (retval == -1)
   	{
 			break;
	 	}
		else if (retval)
		{
			int acceptedSocket = 
					accept(_connectionEndPoint, (struct sockaddr *) &_incomingAddress, &clilen);
	
			int* nodeId = (int*)malloc(sizeof(int));
			int* processId = (int*)malloc(sizeof(int));
			char* nodeInfo = RetrieveIdInformation(acceptedSocket, nodeId, processId);
		  //get the outgoing queue's lock
		  sem_wait(&outgoingQueueLock);
		  	
		  //Add a record of the history to the outgoing queu
		  Enqueue(_outgoingMessageList, nodeInfo);
				
		  //Let the otugoing thread know that there is queued up data
		  sem_post(&outgoingQueueNotification);
          
      //Release the ougoing queue
      sem_post(&outgoingQueueLock);			
			
			clusterNode* newClusterNode = (clusterNode*)malloc(sizeof(clusterNode));
			InitializeClusterNode(newClusterNode, acceptedSocket, *nodeId, *processId);
			free(nodeId);
			free(processId);

			//Add our minion to the queue
			sem_wait(&clusterQueueLock);
			Enqueue(_clusterNodeList, (void**)newClusterNode);
			sem_post(&clusterQueueLock);

			//Start a thread to deal with each minion
			pthread_t readThread, processThread;
			pthread_create(&readThread, NULL, ReadFromMinion, newClusterNode);
			pthread_create(&processThread, NULL, ProcessMinionMessages, newClusterNode);
			connectedNodes++;
		}
		else if(SessionIsRunning() == FALSE)
		{
			break;
		}
  }

  //Now issue 'continue' to each node
 	if(SessionIsRunning() == TRUE)
 	{
 		//Lock the cluster list
 		sem_wait(&clusterQueueLock);
 		StartIterateQueue(_clusterNodeList);
		clusterNode* connectedNode = NULL;
		IterateQueue((void**)&connectedNode, _clusterNodeList);

		while(connectedNode != NULL)
		{
    	if(write (connectedNode->clientSocket, "CONTINUE", 8)<0)
     		break;

    	IterateQueue((void**)&connectedNode, _clusterNodeList);

		}
		//Release the cluster	
		//if (retval == -1)
		sem_post(&clusterQueueLock);
	}
  	
  //now listen for any call backs 
  while(SessionIsRunning() == TRUE)
  {
   	FD_ZERO(&rfds);
   	FD_SET(_connectionEndPoint, &rfds);

   	/* Wait up to one seconds. */
   	tv.tv_sec = 1;
   	tv.tv_usec = 0;

   	retval = select(_connectionEndPoint + 1, &rfds, NULL, NULL, &tv);
    
		if (retval == -1)
   	{
 			break;
	 	}
		else if (retval)
		{
			//We got a gdb call back
			int* gdbSocket = (int*)malloc(sizeof(int));
			
			*gdbSocket =	accept(_connectionEndPoint, (struct sockaddr *) &_incomingAddress, &clilen);
			//Start a thread to deal with the gdb side of this node
			pthread_t gdbThread;
			//pthread_create(&gdbThread, NULL, ProcessGDBMessages, &gdbSocket);
			pthread_create(&gdbThread, NULL, ProcessGDBMessages, gdbSocket);
		}
		else if(SessionIsRunning() == FALSE)
		{
			break;
		}
  }
  
  return NULL;
}

char* serializeGDB(char* message, int messageLength, int sourceId)
{
	charList* tempBuffer = (charList*)malloc(sizeof(charList));;
	InitializeCharList(tempBuffer);
	
	//create wrappers around the SOH, Partition, and EOT chars
	char* soh = (char*)malloc(2*sizeof(char));
	soh[0] = SOH;
	soh[1] = '\0';

	char* partition = (char*)malloc(2*sizeof(char));
	partition[0] = PARTITION_CHR;
	partition[1] = '\0';	
	
	char* eot = (char*)malloc(2*sizeof(char));
	eot[0] = EOT;
	eot[1] = '\0';
	
	char replacementIndicator = 'U';
	
	//add the message passed in into a charlist to do the replace with
	AddChars(tempBuffer, message, messageLength);
	
	//replace SOH
	if(ReplaceChars(tempBuffer, soh, _sohReplace) == TRUE)
		replacementIndicator = 'E';
		
	//replace the partition char
	if(ReplaceChars(tempBuffer, partition, _partitionReplace) == TRUE)
		replacementIndicator = 'E';
		
	//replace the EOT
	if(ReplaceChars(tempBuffer, eot, _eotReplace) == TRUE)
		replacementIndicator = 'E';

  char headerBuffer[255];  
  int headerLength = 0;
  headerLength = sprintf(headerBuffer, "%c%s%c%d%c%c%c", 
        SOH,GDB_HEADER_STR, PARTITION_CHR, 
        sourceId, PARTITION_CHR, replacementIndicator, PARTITION_CHR);

  //Create an input buffer to store the incoming byte stream
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

  AddChars(commandBuffer, headerBuffer, headerLength);  
  AddChars(commandBuffer, tempBuffer->Items,  tempBuffer->ItemCount);
  AddChars(commandBuffer, eot, 1);;

  commandBuffer->Items[commandBuffer->ItemCount] = '\0';
  
  char* result = (char*)malloc(commandBuffer->ItemCount*sizeof(char));
  memcpy(result,commandBuffer->Items, commandBuffer->ItemCount);

	free(soh);
	free(partition);
	free(eot);
	CleanUpCharList(tempBuffer);
  
  //Now free up the char list
  CleanUpCharList(commandBuffer);

  return result;
}

void* ProcessGDBMessages(void* value)
{
	//All the GDB process to continue
	int gdbSocket = *((int*)value);

	int* nodeId = (int*)malloc(sizeof(int));
	int* processId = (int*)malloc(sizeof(int));
	char* nodeInfo = RetrieveIdInformation(gdbSocket, nodeId, processId);	
	free(nodeInfo);
	
	//Loop through the cluster and match our gdb socket with the main one
	sem_wait(&clusterQueueLock);
	StartIterateQueue(_clusterNodeList);
	clusterNode* connectedNode = NULL;
	IterateQueue((void**)&connectedNode, _clusterNodeList);

	clusterNode* matchingNode = NULL;
	while(connectedNode != NULL)
	{
		if(connectedNode->nodeId == *nodeId)
		{
			connectedNode->gdbSocket = gdbSocket;
			matchingNode = connectedNode;
			break;
		}
		
  	IterateQueue((void**)&connectedNode, _clusterNodeList);
	}

	sem_post(&clusterQueueLock);

	free(nodeId);
	free(processId);

  int bufferSize = 8192;
  char inputBuffer[bufferSize];    
	int bytesRead = 0;


  int attached = FALSE;
  int resumed = FALSE;

  //Create an input buffer to store the incoming byte stream
  charList* localInputBuffer = (charList*)malloc(sizeof(charList));;
  InitializeCharList(localInputBuffer);

  //Create a storing the processed messages
  queue* localMessageQueue = (queue*)malloc(sizeof(queue));
  InitializeQueue(localMessageQueue);  

  while(1)
  {	
    memset(inputBuffer, '\0', bufferSize);
    //Read in from the input side
    bytesRead = read(gdbSocket, inputBuffer, bufferSize);
    if(bytesRead <= 0)
    {
      close(gdbSocket);
      break;
    }

    if(attached == FALSE)
    {   
      char attachBuffer[100];
      int attachBufferLen = sprintf(attachBuffer, "attach %d\n", matchingNode->processId);
      Write (gdbSocket, attachBuffer, attachBufferLen);
      	attached = TRUE;
    }
    else
    {
			if(resumed == FALSE && strstr(inputBuffer, "(gdb)") != NULL)
			{
        resumed = TRUE;
        Write(matchingNode->clientSocket, "GO", 2);
        Write(matchingNode->gdbSocket, "n\n", 2);
        Write(matchingNode->gdbSocket, "n\n", 2);	      
	      Write(matchingNode->gdbSocket, "n\n", 2);
 	    }
 	    else
 			{
		    //Write to the output side
		    char* message = serializeGDB(inputBuffer, bytesRead, matchingNode->nodeId);
		    
	      //Lock the minion's queue
	      sem_wait(&(matchingNode->clusterNodeLock));

	      Enqueue(matchingNode->messages, (void**)message);

	      //Release the lock on the minion queue
	    	sem_post(&(matchingNode->clusterNodeLock));

	      //Notify the processing thread that there is something	
	    	sem_post(&(matchingNode->messageNotification));

			}
 		}
	}

	CleanupClusterNode(matchingNode);
  return NULL;
}

int SessionIsRunning()
{
	if(_sessionRunning == TRUE && _mpiRunning == TRUE)
		return TRUE;
	else
		return FALSE;
}

void LaunchCluster(clusterCommunication* clusterComm)
{	
  char myIpAddress[50];
  GetPrimaryIp(myIpAddress, 50);

  int LaunchCommandSize = 500;
  char launchCommand[LaunchCommandSize];
  memset(launchCommand, '\0', LaunchCommandSize);
  
  sprintf(launchCommand,"%s -c %s:%d", clusterComm->clusterCommandLine,
    myIpAddress, _incomingPort);
	
  _mpiFD = popen(launchCommand, "w");
  
  
 	free(clusterComm->clusterCommandLine);
 	free(clusterComm);
}

void* ReadFromMinion(void* value)
{
  clusterNode* myClusterNode = (clusterNode*)value;

  //Create an input buffer to store the incoming byte stream
  charList* localInputBuffer = (charList*)malloc(sizeof(charList));;
  InitializeCharList(localInputBuffer);

  //Create a storing the processed messages
  queue* localMessageQueue = (queue*)malloc(sizeof(queue));
  InitializeQueue(localMessageQueue);  

	//Create a buffer to read data into
  int bufferSize = 255;
  char readBuffer[bufferSize];    

	int bytesRead = 0;
	while(SessionIsRunning() == TRUE)
  {	
    //Clear the reading buffer before each read
    memset(readBuffer, '\0', bufferSize);

    //Read in from the input side
    bytesRead = read(myClusterNode->clientSocket, readBuffer, bufferSize);
		
    if(bytesRead <= 0)
    	break;
		
    //Copy the input buffer to the input stream
    AddChars(localInputBuffer, readBuffer, bytesRead);

    //Parse the input buffer for whole messagses
    ParseInputBuffer(localInputBuffer, localMessageQueue, FALSE);    

    //Check if we got any whole messages
    if(IsQueueEmpty(localMessageQueue) == FALSE)
    {
      //Lock the minion's queue
      sem_wait(&(myClusterNode->clusterNodeLock));

      while(IsQueueEmpty(localMessageQueue) == FALSE)
      {
        //Dequeue the messages and drop them on the outgoing message queue
        char* processedMessage = NULL;
        Dequeue((void**)(&processedMessage), localMessageQueue);
        Enqueue(myClusterNode->messages, (void**)processedMessage);
      }

      //Release the lock on the minion queue
    	sem_post(&(myClusterNode->clusterNodeLock));
      
      //Notify the processing thread that there is something
    	sem_post(&(myClusterNode->messageNotification));
    }
  }
  
  CleanUpCharList(localInputBuffer);
  CleanUpQueue(localMessageQueue);
  return NULL;
}

void* ProcessMinionMessages(void* value)
{
  clusterNode* myClusterNode = (clusterNode*)value;
  int queueCleared = TRUE;

  while(SessionIsRunning() == TRUE)
  {	
    sem_wait(&(myClusterNode->messageNotification));
  	
  	if(SessionIsRunning() == FALSE)
      break;
      
    queueCleared = FALSE;

    while(queueCleared == FALSE)
    {
      if(sem_trywait(&outgoingQueueLock) == 0)
      {
        //we were able to aquire the outgoing lock
        if(sem_trywait(&(myClusterNode->clusterNodeLock)) == 0)
        {
          if(IsQueueEmpty(myClusterNode->messages) == FALSE)
          {
            char* processedMessage = NULL;
            Dequeue((void**)(&processedMessage), myClusterNode->messages);
            Enqueue(_outgoingMessageList, (void**)processedMessage);
            
            //Let the otugoing thread know that there is queued up data
          	sem_post(&outgoingQueueNotification);
          }

          if(IsQueueEmpty(myClusterNode->messages) == TRUE)
          {
            //Minion's queue is cleared now
            queueCleared = TRUE;
          }
          
          //Release the minion lock
        	sem_post(&(myClusterNode->clusterNodeLock));        
        }
        
        //Release the ougoing queue
      	sem_post(&outgoingQueueLock);
      }
    }
  }
  
  if(myClusterNode->gdbSocket == FALSE)
	  CleanupClusterNode(myClusterNode);
	  
  return NULL;
}

void* WriteToClient(void* value)
{
  int messageLength = 0;
  char* eotPtr = NULL;
  char* message = NULL;

  while(_sessionRunning == TRUE)
  {
    //Wait to be notified that there is actually something to write out
    sem_wait(&outgoingQueueNotification);

    if(_sessionRunning == FALSE)
      break;

    //Confirm that there actually are messages to process
    if(IsQueueEmpty(_outgoingMessageList) == TRUE)
      continue;

    //Lock the outgoing queue
    sem_wait(&outgoingQueueLock);
    
    //Loop through all of the messages
    while(IsQueueEmpty(_outgoingMessageList) == FALSE)
    {
      //Dequeue the messages and process them in order
      Dequeue((void**)(&message), _outgoingMessageList);

      //find the EOT character
      eotPtr = (char*)rawmemchr(message, EOT);
      
      //Determine the length of the message
      messageLength = (eotPtr - message) + 1;
			
      //Write the message out to the client
      if(write (_clientSocket, message, messageLength) < 0)
        break;

      //free up the memory of the message
      free(message);
    }
    
    //release the outgoing queue lock so the user can add more
   	sem_post(&outgoingQueueLock);
  }

  //Let the main thread know that the outgoing thread completed
  sem_post(&outgoingThreadCompleteNotification);

  return NULL;
}

void InitializeClusterNode(clusterNode* newClusterNode, int clientSocket, int nodeId, int processId)
{
  sem_init(&(newClusterNode->clusterNodeLock), 0, 1);
  sem_init(&(newClusterNode->messageNotification), 0, 0);

  newClusterNode->clientSocket = clientSocket;
  newClusterNode->nodeId = nodeId;
  newClusterNode->processId = processId;
  newClusterNode->gdbSocket = FALSE;
  newClusterNode->messages = (queue*)malloc(sizeof(queue));

  InitializeQueue(newClusterNode->messages);
}

void CleanupClusterNode(clusterNode* disposingClusterNode)
{
  //clean up the connection created.
	close(disposingClusterNode->clientSocket);
	close(disposingClusterNode->gdbSocket);
	free(disposingClusterNode->messages);
	disposingClusterNode->messages = NULL;
	free(disposingClusterNode);
	disposingClusterNode = NULL;
}

void GetSessionHistory(char* dirLocation, charList* resultList)
{	
	DIR *dir;
	struct dirent *ent;
	
	//dir = opendir ("c:\\src\\");
	dir = opendir (dirLocation);

	if (dir != NULL) 
	{
		/* print all the files and directories within directory */
		while ((ent = readdir (dir)) != NULL) 
		{
			switch(ent->d_type)
			{
				case DT_REG:
				{
					if(strcmp(ent->d_name, "session.info") != STRING_EQUALS_INDICATOR)
						continue;
						
					char* fileName;
					asprintf(&fileName, "%s/%s", dirLocation, ent->d_name);

					ReadChars(fileName, resultList);
					
					free(fileName);
				}  
					break;
				case DT_DIR:
				{
					if(ent->d_name[0] != '.')
					{
					 	char* dirBuffer;
					  asprintf(&dirBuffer, "%s/%s", dirLocation, ent->d_name);
						GetSessionHistory(dirBuffer, resultList);
						free(dirBuffer);
					}
				}
					break;
				default:
					break;
			}
		}
		closedir (dir);
	} 	
}

void GenerateBufferData()
{	
	char* nodeId = strtok(NULL, PARTITION_STR);
	char* commandIds = strtok(NULL, PARTITION_STR);
	char* fileLocation = strtok(NULL, PARTITION_STR);

 	charList* bufferValues = (charList*)malloc(sizeof(charList));
	InitializeCharList(bufferValues);
	
	//Read the file into an XML doc node
	XMLNode* docNode = xmlRead(fileLocation);
	char* commandIdPair = strtok(commandIds, ",");
  int nodeCount = 0;
	XMLNode* commandNode = NULL;

	char* soh = (char*)malloc(2*sizeof(char));
	soh[0] = SOH;
	soh[1] = '\0';

	char* partition = (char*)malloc(2*sizeof(char));
	partition[0] = PARTITION_CHR;
	partition[1] = '\0';	
	
	char* eot = (char*)malloc(2*sizeof(char));
	eot[0] = EOT;
	eot[1] = '\0';

	while(commandIdPair != NULL)
	{
		char* periodPtr = strstr(commandIdPair, ".");
		int commandIdLen = periodPtr - commandIdPair;
		char* commandId = (char*)malloc((commandIdLen + 1)* sizeof(char));
		
		int commandBufIdLen = strlen(commandIdPair) - (commandIdLen + 1);
		char* commandBufferId = (char*)malloc((commandBufIdLen + 1) * sizeof(char));
		memmove(commandId, commandIdPair, commandIdLen);	
		memmove(commandBufferId, commandIdPair + commandIdLen + 1, 
										commandBufIdLen);

		commandId[commandIdLen] = '\0';
		commandBufferId[commandBufIdLen] = '\0';
	
		//Loop through all the nodes
		while(nodeCount < docNode->ChildrenCount)
		{		
			Attribute* commandAttribute = 
				xmlGetAttribute(docNode->ChildNodes[nodeCount], COMMAND_ID_ATTRIBUTE);

			//Check if the command is the one we are looking for
			if(commandAttribute != NULL && StrEqual(commandBufferId,commandAttribute->Value) == TRUE)
			{			
				//This is our node
				commandNode = docNode->ChildNodes[nodeCount];
				break;
			}
			nodeCount++;
		}
		if(commandNode != NULL)
		{
			AddChars(bufferValues, soh, 1);
			AddChars(bufferValues, "BUFFER VALUE|",13);
			AddChars(bufferValues, nodeId, strlen(nodeId));
			AddChars(bufferValues, "|",1);
			AddChars(bufferValues, commandId, strlen(commandId));  
			AddChars(bufferValues, "|U",2);

			int replacementIndicatorIndex = bufferValues->ItemCount - 1;

			XMLNode* bufNode = NULL;
			if(StrEqual(commandNode->Name, MPI_WAIT_STR) == TRUE)
			{
				bufNode = xmlGetChildNode(commandNode, BUF_ELEMENT);
			}
			else
			{
				XMLNode* paramNode = xmlGetChildNode(commandNode, PARAMETERS_ELEMENT);
				if(paramNode != NULL)
				{
					bufNode = xmlGetChildNode(paramNode, BUF_ELEMENT);
				}
			}

			if(bufNode != NULL)
			{
				int bufCounter = 0;
				charList* tempBuffer = (charList*)malloc(sizeof(charList));;
				InitializeCharList(tempBuffer);
				while(bufCounter < bufNode->ChildrenCount)
				{
					char* value  = xmlGetText(bufNode->ChildNodes[bufCounter]);
					AddChars(tempBuffer, value, strlen(value));
					if(ReplaceChars(tempBuffer, soh, _sohReplace) == TRUE)
						bufferValues->Items[replacementIndicatorIndex] = 'E';
						
					if(ReplaceChars(tempBuffer, partition, _partitionReplace) == TRUE)
						bufferValues->Items[replacementIndicatorIndex] = 'E';
					
					if(ReplaceChars(tempBuffer, eot, _eotReplace) == TRUE)
						bufferValues->Items[replacementIndicatorIndex] = 'E';
						
					AddChars(bufferValues, PARTITION_STR, 1);				
					AddChars(bufferValues, tempBuffer->Items, tempBuffer->ItemCount);
			
					ClearChars(tempBuffer);
					bufCounter++;
				}
		
				CleanUpCharList(tempBuffer);
			}
			AddChars(bufferValues, eot, 1);		

			char* bufferMessage = (char*)malloc(bufferValues->ItemCount * sizeof(char));
			memcpy(bufferMessage, bufferValues->Items, bufferValues->ItemCount);			
	    
	    //get the outgoing queue's lock
			sem_wait(&outgoingQueueLock);
	    //Add a record of the history to the outgoing queue
			Enqueue(_outgoingMessageList, (void**)bufferMessage);
	    ClearChars(bufferValues);
			
			//Release the ougoing queue
			sem_post(&outgoingQueueLock);
		}
		free(commandId);
		free(commandBufferId);
				
		commandNode = NULL;	
		commandIdPair = strtok(NULL, ","); 
		//Let the otugoing thread know that there is queued up data
		sem_post(&outgoingQueueNotification); 
  } 

  free(soh);
  free(partition);
	free(eot);
	xmlFree(docNode);  
	
	//clean up the history list
	CleanUpCharList(bufferValues);
}

char* GetConfigInfo()
{
  FILE* fp = fopen(".distributedApplicationDebugger.conf", "r");
 	
 	int maxBufferLength = 256;
 	
  char* configline = (char*)malloc(sizeof(char) * maxBufferLength);
  memset(configline, '\0', maxBufferLength);
  
  if(fp!=NULL)
 	{
 		if(fgets(configline, maxBufferLength, fp) != NULL)
			configline[strlen(configline) - 1] = '\0';
		
		fclose(fp);
	}

	
	return configline;
}

void StoreReplacementChars(char* sohChars, char* partitionChars, char* eotChars)
{	
  if(_sohReplace != NULL)
  	free(_sohReplace);
  
  if(_partitionReplace != NULL)
  	free(_partitionReplace);
  
  if(_eotReplace != NULL)
  	free(_eotReplace);
  
  _sohReplace = (char*)malloc(strlen(sohChars)*sizeof(char));
  _partitionReplace = (char*)malloc(strlen(partitionChars)*sizeof(char));
  _eotReplace = (char*)malloc(strlen(eotChars)*sizeof(char));
  
  strcpy(_sohReplace, sohChars);
  strcpy(_partitionReplace, partitionChars);
  strcpy(_eotReplace, eotChars);
}

void getDateTime(char** resultBuff, int buffLength)
{
	time_t rawtime;
  struct tm * timeinfo;
		
	time(&rawtime);
	timeinfo = localtime(&rawtime);
  strftime(*resultBuff, buffLength, "%Y-%m-%dT%H:%M:%S", timeinfo);
}


