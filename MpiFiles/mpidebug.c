#define _GNU_SOURCE
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <sys/stat.h>
#include <unistd.h>
#include <sys/wait.h>
#include <sys/types.h>
#include <netdb.h> 
#include <semaphore.h>
#include <memory.h>
#include <pthread.h>

#include "Headers/mpiXML.h"
#include "Headers/mpiValidate.h"
#include "Headers/mpiUtils.h"
#include "Headers/dictionary.h"
#include "Headers/mpiSerialize.h"
#include "Headers/booleanLogic.h"
#include "Headers/communication.h"
#include "XML/xml.h"
#include "Headers/mpidebug.h"
#include "Headers/gdbAttach.h"
#include "Headers/DADParser.h"
#include "Headers/collections.h"

#define UNKNOWN_NODE 0
#define NORMAL_NODE 1
#define RECORD_NODE 2
#define PLAYBACK_NODE 3

#define NON_VERBOSE_MODE 0
#define VERBOSE_MODE 1


int _rank = 0;
int _size = 1;
int _linecounter = 1;
int _role = NORMAL_NODE;
int _traceMode = NON_VERBOSE_MODE;
int _currentNodeIndex = -1;
int* _playbackRoleList = NULL;
int _mixedModePlayback = FALSE;
int _socketOut = FALSE;
char* _callCenterAddress = NULL;
int _callCenterPort = 0;
int _out_pipe[2];
int _gdbNode = FALSE;
char* _fileLocation = NULL;

char* _sohReplace =  NULL;
char* _partitionReplace =  NULL;
char* _eotReplace =  NULL;


FILE *_outputFile;
XMLNode *_inputDoc;
char* _messageBuf;

pthread_t _stdOutThreadId = -1;
sem_t _outgoingLock;
sem_t _finalizedNotification;
int _finalized = FALSE;

int CommunicationConfigured()
{
	if(_callCenterAddress != NULL && _callCenterPort > 0)
		return TRUE;
	else
		return FALSE; 
}

void RedirectStdOut()
{
  //Fork off with stdout redirectoed
  int stdOutDupResult = dup(STDOUT_FILENO);

  //Exit out of the original one
  if( pipe(_out_pipe) != 0 ) {
      exit(1);
    }

  //Redirect the write side
  dup2(_out_pipe[1], STDOUT_FILENO);

  close(_out_pipe[1]);
  //Set the stdout buffer to autoflush
  setvbuf(stdout, NULL, _IONBF, 0);
}

void writeToClient(charList* serializedMessage)
{
	  sem_wait(&_outgoingLock);
	

    if(_socketOut == FALSE)
    {
      //Create outgoing connection
      _socketOut = CreateOutgoingConnection(_callCenterAddress,_callCenterPort);
      if(_socketOut == FALSE)
    	  return;
    }
  
    //Write to the output side
    if(write (_socketOut, serializedMessage->Items, serializedMessage->ItemCount) != serializedMessage->ItemCount)
      _socketOut = FALSE; 
 
	  sem_post(&_outgoingLock);
	
	  CleanUpCharList(serializedMessage);
}

void logAndDispose(XMLNode* node)
{
  //xmlWrite the XML node to file 
  xmlWrite(node, _outputFile); 
  fflush(_outputFile);       

  //Free up the memory
  xmlFree(node);
}

void open_outputFile(char *directoryPath)
{
  //Determine the name of the file to record data to    
  char *_outputFileName;
  int allocatedSize; 

  if(directoryPath == NULL){     
    allocatedSize = asprintf(&_outputFileName, "Node%d.xml", _rank);
  } else{
    mkdir(directoryPath,0777);
    allocatedSize = asprintf(&_outputFileName, "%s/Node%d.xml", directoryPath, _rank);
  }

   //first delete the old file 
  remove(_outputFileName);
  
  //Open up the file to record to
  _outputFile = fopen(_outputFileName, "a");
  free(_outputFileName);
}

void xmlReadInputFile(char *directoryPath)
{
  char *inputFileName;
  int length = 0;
  if(directoryPath == NULL){
    length = asprintf(&inputFileName, "Node%d.xml", _rank);
  } else{
    length = asprintf(&inputFileName, "%s/Node%d.xml", directoryPath, _rank);
  }

  _inputDoc = xmlRead(inputFileName);
  free(inputFileName);
}

//Returns the next XML node from the input doc
XMLNode * getNextNode()
{
  _currentNodeIndex++;
  return _inputDoc->ChildNodes[_currentNodeIndex];
}

//Parses the arguments passed in
void parseInputParameters(int argc, char **argv)
{
  int i;
  char* directoryPath = NULL;

  //Loop through all of the arguments
  for(i = 0; i < argc; i++)
  {
    //Make sure this is a two character parameter startinging with a hyphen
    if((int)strlen(argv[i]) == 2 && argv[i][0] == '-')
    {
      //Check what the value parameter was
      switch(argv[i][1])
      {
        //Verbose Mode
        case 'v':
          _traceMode = VERBOSE_MODE;
          break;
        //Normal Node
        case 'n':
          _role = NORMAL_NODE;
          break;
        //Record Node
        case 'r':
          _role = RECORD_NODE;
          break;
        //Playback Node
        case 'p':
        {
          //Initialize the playback role list
          _playbackRoleList = (int*)malloc(_size*sizeof(int));

          //Assume that we are a play back node
          _role = PLAYBACK_NODE;
          
          //Check if there was a playlist supplied on the command line
          if(i + 1 != argc && argv[i+1][0] != '-')
          { 
            //Assume that everyone node is a normal node, fill in the playback nodes later
            int j = 0;            
            for(j=0; j < _size; j++)
            {
              if(_size == 1)
                _playbackRoleList[j] = PLAYBACK_NODE;
              else
                _playbackRoleList[j] = NORMAL_NODE;
            }

           
            //get the node tokens from the comma delimited string 
            int nodeId = 0;
            char *nodeToken = NULL;
            nodeToken = strtok(argv[i+1], ",");

            //Keep track if we were found in the playback list or not
            int includeInPlayback = FALSE;
            
            //Examine the node id            
            while(nodeToken != NULL) 
            {
              nodeId = atoi(nodeToken);

              if(_size > 1 && nodeId >= _size)
                continue;
              
              _playbackRoleList[nodeId] = PLAYBACK_NODE;

              //Playback mode if only node in the cluster or
              //we are a node in the commad delimited list
              if(nodeId == _rank || _size == 1)
              {
                //Set in case we came here for for a size of 1
                _rank = nodeId;
                includeInPlayback = TRUE;
              }
              
              //Get the next node and check if that is ours
              nodeToken = strtok(NULL, ",");        
            }

            //Check if we ever read our rank from the command line
            if(includeInPlayback == FALSE)
            {
              //We were not in the list, run as normal node
              _role = NORMAL_NODE;
            }

            i++;
          }
          else
          {
            //Everybody in the list is a playback node
            int j = 0;
            for(j=0; j < _size; j++)
              _playbackRoleList[j] = PLAYBACK_NODE;
          }
        }
        break;

				//GDB Nodes
				case 'g':
        {
          //Check if there was a playlist supplied on the command line
          if(i + 1 != argc && argv[i+1][0] != '-')
          {     
            //get the node tokens from the comma delimited string 
            int nodeId = 0;
            char *nodeToken = NULL;
            nodeToken = strtok(argv[i+1], ",");

            //Examine the node id            
            while(nodeToken != NULL) 
            {
              nodeId = atoi(nodeToken);
							if(nodeId == _rank)
							{
							  _gdbNode = TRUE;
								break;
              }
              
              //Get the next node and check if that is ours
              nodeToken = strtok(NULL, ",");        
            }

            i++;
          }
        }
        break;

				//directory path
        case 'd':
        {          
          //Check if there was a playlist supplied on the command line
          if(i + 1 != argc && argv[i+1][0] != '-')
          { 
            directoryPath = (char*)malloc(strlen(argv[i+1]) * sizeof(char));
            directoryPath = strcpy(directoryPath, argv[i+1]);
            i++;
          }
        }        
        break;

				//callcenter address:port
        case 'c':
        {
          //Check if the ipaddress and port were supplied
          if(i + 1 != argc && argv[i+1][0] != '-')
          { 
            char *nodeToken = NULL;
            nodeToken = strtok(argv[i+1], ":");
            if(nodeToken != NULL)
            {
              _callCenterAddress = (char*)malloc(strlen(nodeToken) *sizeof(char));
              _callCenterAddress = strcpy(_callCenterAddress, nodeToken);
              nodeToken = strtok(NULL, ":");
              if(nodeToken != NULL)
              {
                _callCenterPort = atoi(nodeToken);
              }
              else
              {
                _callCenterAddress = NULL;
              }
            }
          }
        }
        break;
        
        //file location of executable
        case 'f':
        {
        	if(i + 1 != argc && argv[i+1][0] != '-')
          {
       			_fileLocation = (char*)malloc((strlen(argv[i+1]) + 1) *sizeof(char));
       			memset(_fileLocation, '\0', strlen(argv[i+1]) + 1);
          	_fileLocation = strcpy(_fileLocation, argv[i+1]);
            i++;
          }
				}
				break;
				
				//swap values SOH,Partition,EOT values
				case 's':
        {
       		if(i + 1 != argc && argv[i+1][0] != '-')
          {          	
        		char** splitArray = NULL;
						int itemCount = 0;
						
						//The swap values are comma delimited
						itemCount = Split(argv[i+1], ",", &splitArray, 0, strlen(argv[i+1]));
						
						_sohReplace = splitArray[0];
						_partitionReplace = splitArray[1];
						_eotReplace = splitArray[2];
						
						splitArray = NULL;
						free(splitArray);	
          }
				}
				break;
      }
    }
  }

  if(_role == PLAYBACK_NODE)
  {
    //No input node list given, just playback as the node assigned
    xmlReadInputFile(directoryPath);
    if(_playbackRoleList != NULL)
    {
      //Check if there is at least one node that is in Normal Mode
      //If they are the nwe are in a 'mixed mode' playback
      for(i = 0; i < _size; i++)
      {
        if(_playbackRoleList[i] == NORMAL_NODE){
          _mixedModePlayback = TRUE;
          break;
        }
      }
    }
  } 
  else if(_role == RECORD_NODE)
  {
    open_outputFile(directoryPath);
  }

  free(directoryPath);
  //Set up a thread to read from the standard out
  if(CommunicationConfigured() == TRUE && _stdOutThreadId == -1) 
  {
    //The outgoing lock keeps the console and application messages from colliding
    sem_init(&_outgoingLock, 0, 1);  
    //Finalize notification is initialized to zero since finalized will wait on it before std out listener finishes
    sem_init(&_finalizedNotification, 0, 0);
    //Redirect std out so we can listen to it
    RedirectStdOut();
    //Start the thread to listen to std out
    pthread_create(&_stdOutThreadId, NULL, StdOutRedirectThread, NULL);
  }
}

//Initiliaze the input and parse the input parameters
int _MPI_Init(char pname[100], int line, int *argc, char ***argv) {
  int commandId = 0;
  int returnValue = 0;

  //Initialize and figure out what size and rank we are
  returnValue = MPI_Init(argc, argv);

	//We must get the size and rank now so we can report what node got
	//initialized 
  MPI_Comm_size(MPI_COMM_WORLD, &_size);
  MPI_Comm_rank(MPI_COMM_WORLD, &_rank);    


  //Parse the input parameters to determine what type of node we are
  parseInputParameters(*argc, *argv);

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();

  if(CommunicationConfigured() == TRUE)
  {
   	//Make connection back to the call center
 		if(_socketOut == FALSE)
   		_socketOut = CreateOutgoingConnection(_callCenterAddress,_callCenterPort);
   		
		//Send our id back to the call center
		SendIdData(_socketOut, _rank, getpid());
    int bufferSize = 255;
  	char readBuffer[bufferSize];   
    	
 		//Now wait for the signal to continue
  	int bytesRead = read(_socketOut, readBuffer, bufferSize);
	  //We got something from the call center, assume it is the signal start
	
  	//Send a 'PRE' message that we are processing this command   
  	writeToClient(preSerializeMPIInit(_rank, line, &commandId, expectedValues));
  }

  //No need to do anything since we already initialized, record if necessary	
	if(_role == RECORD_NODE)
	{
		XMLNode *xmlNode = xmlMPIInit(_rank, returnValue, commandId);
		logAndDispose(xmlNode);
  }
      
  //Send a 'POST' message with the actual return value
  if(CommunicationConfigured() == TRUE)
	  writeToClient(postSerializeMPIInit(_rank, commandId, returnValue, expectedValues));
		
	//If we're a gdb node, allow the gdb mode to take over	
	if(_gdbNode == TRUE)
		AttachGDB(_fileLocation, _socketOut, _callCenterAddress, _callCenterPort, _rank);
	
  return returnValue;
}


//Get the rank
int _MPI_Comm_rank(char pname[100], int line, MPI_Comm comm, int *rank) {
  int commandId = 0;
  int returnValue = 0;

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();

  //Send a 'PRE' message that we are processing this command
  if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPIRank(_rank, line, comm, &commandId));

  switch(_role)
  {
    case NORMAL_NODE:
    	//Issue the rank command and get the return value
      returnValue = MPI_Comm_rank(MPI_COMM_WORLD, rank);
      break;

    case RECORD_NODE:
      {
        //Send the message as normal and serialize it to XML
        returnValue = MPI_Comm_rank(MPI_COMM_WORLD, rank);
        _rank = *rank;
        XMLNode *xmlNode = xmlMPIRank(_rank, comm, *rank, returnValue, commandId);

        //xmlWrite the XML node to file 
        logAndDispose(xmlNode);
      }
      break;

    case PLAYBACK_NODE:
      {
        //If the size is 1, we have already determined our rank
        if(_size == 1)
        {
          *rank = _rank;
        }
        else
        {
          //Normal process, determine the rank
          returnValue = MPI_Comm_rank(MPI_COMM_WORLD, rank);
          _rank = *rank;
        }
      }
      break;
  }
  
  //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
	  writeToClient(postSerializeMPIRank(_rank, commandId, *rank, returnValue, expectedValues));

  return returnValue;
}


//Get the size
int _MPI_Comm_size(char pname[100], int line, MPI_Comm comm, int *size) {
  int commandId = 0;
  int returnValue = 0;
  
	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();
  
  //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPISize(_rank, line, comm, &commandId));

  switch(_role)
  {
    case NORMAL_NODE:
      returnValue = MPI_Comm_size(comm, size);
      _size = *size;
      break;

    case RECORD_NODE:
      {
        //Send the message as normal and serialize it to XML
        returnValue = MPI_Comm_size(comm, size);
        _size = *size;

        XMLNode *xmlNode = xmlMPISize(_rank, comm, *size, returnValue, commandId);

        //xmlWrite the XML node to file 
        logAndDispose(xmlNode);
      }
      break;

    case PLAYBACK_NODE:
      {
        //If the size is 1, we have already determined our size
        returnValue = MPI_Comm_size(comm, size);
        _size = *size;
      }
      break;
  }


  //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
		writeToClient(postSerializeMPISize(_rank, commandId, *size, returnValue, expectedValues));

  return returnValue;
}

//MPI Send implementation
int _MPI_Send(char pname[100], int line, void *buf, int count,
              MPI_Datatype datatype, int dest, int tag, MPI_Comm comm) {
  int commandId = 0;
  int returnValue = 0;
  
	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();
	  
   //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPISend(_rank, line, count, 
  			datatype, dest, tag, comm, &commandId, expectedValues));
  
  switch(_role)
  {
    case NORMAL_NODE:
      //Just send as normal if the mode is normal
      returnValue = MPI_Send(buf, count, datatype, dest, tag, comm);   
      break;

    case RECORD_NODE:
      {
        //Send the message as normal and serialize it to XML
        returnValue = MPI_Send(buf, count, datatype, dest, tag, comm);
        XMLNode *xmlNode = xmlMPISend(_rank, buf, count, datatype, dest, tag, comm, returnValue, commandId);

        //xmlWrite the XML node to file 
        logAndDispose(xmlNode);
      }
      break;

    case PLAYBACK_NODE:
      {
        if(_playbackRoleList[dest] == NORMAL_NODE)
        {
          returnValue = MPI_Send(buf, count, datatype, dest, tag, comm);
        }
        else
        {
          returnValue = atoi(xmlGetText(
          	xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT)));
        }
      }
      break;
  }
  //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
		writeToClient(postSerializeMPISend(_rank, datatype, count, buf, 
		 							commandId, returnValue, expectedValues, 
		 							_sohReplace, _partitionReplace, _eotReplace));  
  
  return returnValue;
}

//MPI Recv implementation
int _MPI_Recv(char pname[100], int line, void *buf, int count,
              MPI_Datatype datatype, int src, int tag, MPI_Comm comm, MPI_Status *status) {
  int commandId = 0;
  int returnValue = 0;

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();
	  
   //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPIRecv(_rank, line, count, 
  			datatype, src, tag, comm, &commandId, expectedValues));

  switch(_role)
  {
    case NORMAL_NODE:
        //Just recv as normal
      returnValue = MPI_Recv(buf, count, datatype, src, tag, comm, status);
      break;

    case RECORD_NODE:
      {
        //Receive the message and serialize it to XML
        returnValue = MPI_Recv(buf, count, datatype, src, tag, comm, status);
        XMLNode *xmlNode = xmlMPIRecv(_rank, buf, count, datatype, src, tag, comm, status, returnValue, commandId);

        //Write the XML node to file 
        logAndDispose(xmlNode);
      }
      break;

    case PLAYBACK_NODE:
      {
        if(_mixedModePlayback == TRUE &&
          (src == MPI_ANY_SOURCE || _playbackRoleList[src] == NORMAL_NODE))
        {
          returnValue = MPI_Recv(buf, count, datatype, src, tag, comm, status);
        }
        else
        {
          //Get the parameters, buf, and size nodes
          XMLNode *parameterNode = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);
          XMLNode *bufNode;

          if(isFloatingPointType(datatype) == TRUE)
            bufNode = xmlGetChildNode(parameterNode, BUF_BYTES_ELEMENT);
          else
            bufNode = xmlGetChildNode(parameterNode, BUF_ELEMENT);

          XMLNode *sizeNode = xmlGetChildNode(parameterNode, COUNT_ELEMENT);

          //Read in the values and reload the buffer
          xmlToMPIBuf(bufNode, buf, datatype, atoi(xmlGetText(sizeNode)));

          //Load the Status Element
          XMLNode *statusNode = xmlGetChildNode(parameterNode, STATUS_ELEMENT);
          xmlToMPIStatus(statusNode, status);

          //Set the return value
          returnValue = atoi(xmlGetText(xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT)));
        }
      }
      break;
  }

  //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
		writeToClient(postSerializeMPIRecv(_rank, datatype, status, count, buf,
										commandId, returnValue, expectedValues, 
										_sohReplace, _partitionReplace, _eotReplace));  


  return returnValue;
}

//MPI Send implementation
int _MPI_ISend(char pname[100], int line, void *buf, int count,
              MPI_Datatype datatype, int dest, int tag, MPI_Comm comm, MPI_Request *request){
  int commandId = 0;
  int returnValue = 0;

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();
	  
   //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPIISend(_rank, line, count, datatype, dest, tag, comm, request,&commandId, expectedValues));

  switch(_role)
  {
    case NORMAL_NODE:
      //Just send as normal if the mode is normal
      returnValue = MPI_Isend(buf, count, datatype, dest, tag, comm, request);
      break;

    case RECORD_NODE:
      {
        //Send the message as normal and serialize it to XML
        returnValue = MPI_Isend(buf, count, datatype, dest, tag, comm, request);

        //serialize the values
        XMLNode *xmlNode = xmlMPIISend(_rank, buf, count, datatype, dest, tag, comm, request, returnValue, commandId);

        //xmlWrite the XML node to file 
        logAndDispose(xmlNode);
      }
      break;

    case PLAYBACK_NODE:
      {
        if(_playbackRoleList[dest] == NORMAL_NODE)
        {
          returnValue = MPI_Isend(buf, count, datatype, dest, tag, comm, request);
        }
        else
        {
          returnValue = atoi(xmlGetText(xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT)));
        }

        XMLNode *parameterNode = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);
        int* requestId  = (int*)malloc(sizeof(int));
        requestId[0] = atoi(xmlGetText(xmlGetChildNode(parameterNode, REQUEST_ELEMENT)));

        AsyncBuf* asyncBuf = (AsyncBuf*)malloc(sizeof(AsyncBuf));

        asyncBuf->id = requestId[0];
        asyncBuf->src = -1;
        asyncBuf->dest = dest;
        asyncBuf->datatype = datatype;
        asyncBuf->count = count;
        asyncBuf->request = request;
        asyncBuf->buf = buf;

        DictionaryAdd(requestId, asyncBuf);

      }

      break;
  }

  //Send a 'POST' message with the actual return value
  if(CommunicationConfigured() == TRUE)
		writeToClient(postSerializeMPIISend(_rank, datatype, count, buf, 
		 							commandId, returnValue, expectedValues, 
		 							_sohReplace, _partitionReplace, _eotReplace));

  return returnValue;
}

//MPI Recv implementation
int _MPI_IRecv(char pname[100], int line, void *buf, int count, 
	      MPI_Datatype datatype, int src, int tag, MPI_Comm comm, MPI_Request *request) {
  int commandId = 0;
  int returnValue = 0;

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();

  //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPIIRecv(_rank, line, count, datatype, src, tag, comm, request, &commandId, expectedValues));

  switch(_role)
  { 
    case NORMAL_NODE:
        //Just recv as normal
      returnValue = MPI_Irecv(buf, count, datatype, src, tag, comm, request);
      break;

    case RECORD_NODE: 
      {   
        //Receive the message and serialize it to XML
        returnValue = MPI_Irecv(buf, count, datatype, src, tag, comm, request); 
          
        //Write the XML node to file 
        logAndDispose(xmlMPIIRecv(_rank, buf, count, datatype, src, tag, comm, request, returnValue, commandId));
      }
      break;

    case PLAYBACK_NODE:
      {
        if(_mixedModePlayback == TRUE &&
           (src == MPI_ANY_SOURCE || _playbackRoleList[src] == NORMAL_NODE))
        {
          returnValue = 
          	MPI_Irecv(buf, count, datatype, src, tag, comm, request);           
        }
        else
        {           
          returnValue = 
          	atoi(xmlGetText(xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT)));
        }

        XMLNode *parameterNode = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);
        int* requestId  = (int*)malloc(sizeof(int));
        requestId[0] = atoi(xmlGetText(xmlGetChildNode(parameterNode, REQUEST_ELEMENT)));

        AsyncBuf* asyncBuf = (AsyncBuf*)malloc(sizeof(AsyncBuf));

        asyncBuf->id = requestId[0];
        asyncBuf->src = src;
        asyncBuf->dest = -1;
        asyncBuf->datatype = datatype;
        asyncBuf->count = count;
        asyncBuf->request = request;
        asyncBuf->buf = buf;


        DictionaryAdd(requestId, asyncBuf);
      }
      break;
  }
  
  //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
		writeToClient(postSerializeMPIIRecv(_rank, commandId, returnValue, expectedValues));

  return returnValue;
}

int _MPI_Wait(char pname[100], int line, MPI_Request *request, MPI_Status *status) {
  int commandId = 0;
  int returnValue = 0;

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;
  AsyncBuf* asyncBuf = NULL;
  if(_role == PLAYBACK_NODE)
  {
	  expectedValues = getNextNode();
	}

  //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPIWait(_rank, line, request, &commandId));

  switch(_role)
  { 
    case NORMAL_NODE:
        //Just recv as normal
      returnValue = MPI_Wait(request, status);
      break;

    case RECORD_NODE: 
      {   
        //Receive the message and serialize it to XML
        returnValue = MPI_Wait(request, status);
        logAndDispose(xmlMPIWait(_rank, request, status, returnValue, commandId));
      }
      break;

    case PLAYBACK_NODE:
      {      
        returnValue = atoi(xmlGetText(xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT))); 

        //Get the parameters, buf, and size nodes
        XMLNode *parameterNode = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);
  			int requestId = atoi(xmlGetText(
  				xmlGetChildNode(parameterNode, REQUEST_ELEMENT)));
  			  
  			asyncBuf = (AsyncBuf*)DictionaryRemoveByInt(requestId);
  			     
        //Check if this is a Wait from a Receive command
        if(asyncBuf->src >= 0)
        {
          if(_playbackRoleList[asyncBuf->src] == NORMAL_NODE)
          {
            returnValue = MPI_Wait(request, status);
          }
          else
          { 
            XMLNode *bufNode;
            if(isFloatingPointType(asyncBuf->datatype) == TRUE)
            {
              bufNode = xmlGetChildNode(expectedValues, BUF_BYTES_ELEMENT);
            }
            else
            {
              bufNode = xmlGetChildNode(expectedValues, BUF_ELEMENT); 
            }

            xmlToMPIBuf(bufNode, asyncBuf->buf, asyncBuf->datatype, asyncBuf->count);

            //Load the Status Element
            XMLNode *statusNode = xmlGetChildNode(parameterNode, STATUS_ELEMENT);
            xmlToMPIStatus(statusNode, status);

            //playback the return value
            returnValue = atoi(xmlGetText(xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT)));
          }
        }
        else
        {
          //This is the request from a Send
          if(_playbackRoleList[asyncBuf->dest] == NORMAL_NODE)
          {
            returnValue = MPI_Wait(request, status);
          }
          else
          {
            //Load the Status Element
            XMLNode *statusNode = xmlGetChildNode(parameterNode, STATUS_ELEMENT);
            xmlToMPIStatus(statusNode, status);

            //playback the return value
            returnValue = atoi(xmlGetText(xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT)));
          }
        }

      }
      break;
  }
  
  //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
		writeToClient(postSerializeMPIWait(_rank, status, asyncBuf, 
		 							commandId, returnValue, expectedValues, 
		 							_sohReplace, _partitionReplace, _eotReplace));
	
	//Clear up the send or receive record from this request now
	if(asyncBuf != NULL)
	  free(asyncBuf);

  return returnValue;
}  

int _MPI_Barrier(char pname[100], int line, MPI_Comm comm)
{
  int commandId = 0;
  int returnValue = 0;

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();

  //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPIBarrier(_rank, line, comm, &commandId));

  switch(_role)
  { 
    case NORMAL_NODE:
        //Just run as normal
      returnValue = MPI_Barrier(comm);
      break;

    case RECORD_NODE: 
      {   
        //Wait like normal and log the result
        returnValue = MPI_Barrier(comm);
        logAndDispose(xmlMPIBarrier(_rank, comm, returnValue, commandId));
      }
      break;

    case PLAYBACK_NODE:
      {
        //Check if there are any nodes that running in NORMAL mode
        if(_mixedModePlayback == TRUE)
        {
          //At least one node is running in Normal node, all nodes need to wait
          returnValue = MPI_Barrier(comm);
        } 
        else
        {
          //Everyone is running in playback mode, no need to wait
          returnValue = atoi(xmlGetText(xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT))); 
        }
      }
  }

  //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
		writeToClient(postSerializeMPIBarrier(_rank, commandId, returnValue, expectedValues));

  return returnValue;
}

int _MPI_Probe(char pname[100], int line, int src, int tag, MPI_Comm comm, MPI_Status *status)
{
  int commandId = 0;
  int returnValue = 0;

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();

  //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPIProbe(_rank, line, src, tag, comm, &commandId, expectedValues));

  switch(_role)
  { 
    case NORMAL_NODE:
      //Call like normal 
      returnValue = MPI_Probe(src, tag, comm, status);
      break;

    case RECORD_NODE:      
      {          
        returnValue = MPI_Probe(src, tag, comm, status);
        
        //Write the XML node to file 
        logAndDispose(xmlMPIProbe(_rank, src, tag, comm, status, returnValue, commandId));
        
      }
      break;

    case PLAYBACK_NODE:
      {
        //Check if the node we were probing from is a Normal Node
        
        if(_mixedModePlayback == TRUE &&
           (src == MPI_ANY_SOURCE || _playbackRoleList[src] == NORMAL_NODE))
        {
          //We need to actually perform the Probe because they are waiting on us
          returnValue = MPI_Probe(src, tag, comm, status);
        } 
        else
        {
          //Load the Status Element
          XMLNode *parameterNode = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);
          XMLNode *statusNode = xmlGetChildNode(parameterNode, STATUS_ELEMENT);
          xmlToMPIStatus(statusNode, status);

          //Just read the response from the XML
          returnValue = atoi(xmlGetText(xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT))); 
        }
      }
      break;
  } 

  //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
		writeToClient(postSerializeMPIProbe(_rank, commandId, status, returnValue, expectedValues));

  return returnValue;
}

int _MPI_IProbe(char pname[100], int line, int src, int tag, MPI_Comm comm, int *flag, MPI_Status *status)
{
  int commandId = 0;
  int returnValue = 0;

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();

  //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPIIProbe(_rank, line, src, tag, comm, &commandId, expectedValues));

  switch(_role)
  { 
    case NORMAL_NODE:
      //Call like normal
      returnValue = MPI_Iprobe(src, tag, comm, flag, status );
      break;

    case RECORD_NODE:      
      {          
        returnValue = MPI_Iprobe(src, tag, comm, flag, status );
        
        //Write the XML node to file 
        logAndDispose(xmlMPIIProbe(_rank, src, tag, comm, flag, status, returnValue, commandId));
        
      }
      break;

    case PLAYBACK_NODE:
      {
        //Check if the node we were probing from is a Normal Node
        if(_mixedModePlayback == TRUE &&
           (src == MPI_ANY_SOURCE || _playbackRoleList[src] == NORMAL_NODE))
        {
          //Actually perform the Iprobe but ignore the results, just in case
          //MPI_Iprobe has side effects
          MPI_Status tempStatus;
          MPI_Iprobe(src, tag, comm, flag, &tempStatus);
        }
        
        XMLNode *parameterNode = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);

        //Set the flag
        flag[0] = atoi(xmlGetText(xmlGetChildNode(parameterNode, FLAG_ELEMENT)));

        //set the Status Element
        XMLNode *statusNode = xmlGetChildNode(parameterNode, STATUS_ELEMENT);
        xmlToMPIStatus(statusNode, status);

        //set the return value
        returnValue = atoi(xmlGetText(xmlGetChildNode(expectedValues, RETURN_VALUE_ELEMENT))); 
      }
      break;
  } 

  //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
		writeToClient(postSerializeMPIIProbe(_rank, commandId, flag, status, returnValue));

  return returnValue;
}

//MPI_Finalize implementation
int _MPI_Finalize(char pname[100], int line) {

  int commandId = 0;

	//Get the next node if this is a playback situation
  XMLNode *expectedValues = NULL;  
  if(_role == PLAYBACK_NODE)
	  expectedValues = getNextNode();


  //Send a 'PRE' message that we are processing this command
	if(CommunicationConfigured() == TRUE)
  	writeToClient(preSerializeMPIFinalize(_rank, line, &commandId));


  int returnValue = MPI_Finalize();  
  
   //Send a 'POST' message with the actual return value
	if(CommunicationConfigured() == TRUE)
	  writeToClient(postSerializeMPIFinalize(_rank, commandId, returnValue, expectedValues));

 	switch(_role)
  { 
    case NORMAL_NODE:
      //No need to do anything else, we already finalized  
      break;

    case RECORD_NODE:      
      {  
        //Write the XML node to file 
        logAndDispose(xmlMPIFinalize(_rank, returnValue, commandId));
        
        //We can also close the output file since we are done with MPI    
        fclose(_outputFile);
      }
      break;

    case PLAYBACK_NODE:
      {
        xmlFree(_inputDoc);  

        //No more need for the list of the cluster's roles, we are finalized now
        if(_playbackRoleList != NULL)
          free(_playbackRoleList);
      }
      break;
  } 

  //Set finalized to true
  _finalized = TRUE;

  if(CommunicationConfigured() == TRUE && _stdOutThreadId != -1) 
  {	
    //Wait for the stdout listen thread to finish
		sem_wait(&_finalizedNotification);
    //just to be safe, guarentee that the thread has exited
 		pthread_join(_stdOutThreadId, NULL);
  }
	
	//Cleanup the replacement values
	if(_sohReplace !=  NULL)
		free(_sohReplace);
	
	if(_partitionReplace != NULL)
		free(_partitionReplace);
	
	if(_eotReplace != NULL)
		free(_eotReplace);
		
	_sohReplace = NULL;
	_partitionReplace = NULL;	
	_eotReplace = NULL;
	
	close(_socketOut);
	
  return returnValue;
}


//Listens to std out and sends its contents out as a serialized message
void* StdOutRedirectThread(void *value)
{
  //setup structures to peek at the read queue from 
  fd_set rfds;
  struct timeval tv;

  int retval;
  int bytesRead;
  int bufferSize = 8192;
  char readBuffer[bufferSize];   

  while(1)
  {
    FD_ZERO(&rfds);
    FD_SET(_out_pipe[0], &rfds);

    tv.tv_sec = 2;
    tv.tv_usec = 0;
    //wait up to 2 seconds to read
    retval = select(_out_pipe[0] + 1, &rfds, NULL, NULL, &tv);

    //Check if there was anything in the output buffer
    if(retval > 0)
    {
      bytesRead = read(_out_pipe[0], readBuffer, bufferSize);
      if(bytesRead > 0)//It better be greater then 1!
      {
        //Write out a console message to the users.
        writeToClient(serializeConsole(readBuffer, bytesRead, _rank,
        	_sohReplace, _partitionReplace, _eotReplace));   
      }
    }
    //Nothing read, check if we finalized yet
    else if( _finalized == TRUE)
    {  
      FD_ZERO(&rfds);
      FD_SET(_out_pipe[0], &rfds);
      tv.tv_sec = 2;
      tv.tv_usec = 0;

      retval = select(_out_pipe[0] + 1, &rfds, NULL, NULL, &tv);
      if(retval > 0)
        continue;
      else
        //Nothing in the stdout buff and we finalized, time to finish this thread
        break;
    }
  }

  sem_post(&_finalizedNotification);
  return 0;
}


