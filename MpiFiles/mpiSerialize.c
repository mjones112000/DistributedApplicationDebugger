 #include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <memory.h>
#include "Headers/booleanLogic.h"
#include "Headers/mpiSerialize.h"
#include "Headers/DADParser.h"
#include "Headers/mpiValidate.h"

int commandIdCounter = 0;

/****************************************PRE COMMANDS************************************************************************/
charList* wrapPreCommand(int nodeId, int lineNum, char* commandName, charList* commandInfo, int *commandId)
{
  (*commandId) = commandIdCounter;
	
  int footerLength = 1;
  char footerBuffer[footerLength];
  footerBuffer[0] = EOT;

  //SOH|PRE|NODEID|COMMANDID|LINENUM|COMMAND_NAME|
  char headerBuffer[255];  
  int headerLength = 0;
  headerLength = sprintf(headerBuffer, "%c%s%c%d%c%d%c%d%c%s", 
        SOH,PRE_HEADER_STR, PARTITION_CHR, nodeId, PARTITION_CHR, *commandId, PARTITION_CHR, lineNum, PARTITION_CHR, commandName);


  //Create an input buffer to store the incoming byte stream
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

  //SOH|PRE|NODEID|COMMANDID|LINENUM|COMMAND_NAME|
  AddChars(commandBuffer, headerBuffer, headerLength);

  //COMMAND_INFO
  AddChars(commandBuffer, commandInfo->Items,  commandInfo->ItemCount);
  CleanUpCharList(commandInfo);
  
  //|EOT
  AddChars(commandBuffer, footerBuffer, footerLength);

  commandBuffer->Items[commandBuffer->ItemCount] = '\0';

  commandIdCounter++;
  return commandBuffer;
}

charList* createPreCommCommand(int nodeId, int lineNum, char* commandName, MPI_Comm comm, int *commandId)
{
  //Create a buffer for the command's specific data
  int commBuffMaxLength = 50;
  char commBuff[commBuffMaxLength];
  //zero out the buffer
  memset(commBuff, '\0', commBuffMaxLength);

  //Write the specific's for this command's data to the buffer
  int commBuffLength =  sprintf(commBuff, "%c%s",PARTITION_CHR, mpiCommToStr(comm));
  
	//Create a buffer list  
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
  AddChars(commandBuffer, commBuff, commBuffLength);
  
  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, commandName, commandBuffer, commandId);
}

//MPIINIT:  SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_INIT||EOT
charList* preSerializeMPIInit(int nodeId, int lineNum, int *commandId, XMLNode *expectedValues)
{
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
  
  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, MPI_INIT_STR, commandBuffer, commandId);
}

//RANK:     SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_RANK|COMM|EOT
charList* preSerializeMPIRank(int nodeId, int lineNum, MPI_Comm comm, int *commandId)
{
  return createPreCommCommand(nodeId, lineNum, MPI_RANK_STR, comm, commandId);
}

//SIZE:     SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_SIZE|COMM|EOT
charList* preSerializeMPISize(int nodeId, int lineNum, MPI_Comm comm, int *commandId)
{
  return createPreCommCommand(nodeId, lineNum, MPI_SIZE_STR, comm, commandId);
}

void ValidateMessageCommand(charList *commandBuffer, XMLNode *expectedValues, 
int count, MPI_Datatype datatype, int tag, char* addressTypeName, int addressTypeValue)
{
 //Validate
 if(expectedValues == NULL)
 {
	 AddChars(commandBuffer, "||||", 4);
 }
 else
 {
 	//Get the parameters section of the node
 	XMLNode *paramValues = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);  
  
  //Validate the Count
  AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
	validateIntField(COUNT_ELEMENT, paramValues, count, commandBuffer);
  
  //Validate the data type
  AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
  validateDatatypeField(paramValues, datatype, commandBuffer);

  //Validate the destination
  AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
  validateIntField(addressTypeName, paramValues, addressTypeValue, commandBuffer);

  //Validate the tag
  AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
  validateIntField(TAG_ELEMENT, paramValues, tag, commandBuffer);
 }
}

void ValidateReturnValue(charList *commandBuffer, XMLNode *expectedValues, int returnValue)
{
 	//Validate
 	if(expectedValues == NULL)
 	{
	  AddChars(commandBuffer, "|", 1);
 	}
 	else
 	{  
  	//Validate the return value
  	AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
		validateIntField(RETURN_VALUE_ELEMENT, expectedValues, returnValue, commandBuffer);
	}
}

//SEND:     SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_SEND|COUNT|MPI_INT|DEST|TAG|COMM|EOT
charList* preSerializeMPISend(int nodeId, int lineNum, int count, MPI_Datatype datatype,
							 int dest, int tag, MPI_Comm comm, int *commandId, XMLNode *expectedValues)
{
  //Create a buffer for the command's specific data
  int sendBuffMaxLength = 512;
  char sendBuff[sendBuffMaxLength];
  //zero out the buffer
  memset(sendBuff, '\0', sendBuffMaxLength);

  //Write the specific's for this command's data to the buffer
  int sendBuffLength =  sprintf(sendBuff, "%c%d%c%s%c%d%c%d%c%s",
  	PARTITION_CHR,count, PARTITION_CHR, mpiTypeToStr(datatype), 
  	PARTITION_CHR, dest, PARTITION_CHR, tag, PARTITION_CHR, mpiCommToStr(comm));

	charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
	AddChars(commandBuffer, sendBuff, sendBuffLength);

	ValidateMessageCommand(commandBuffer, expectedValues, 
		count, datatype, tag, DEST_ELEMENT,dest);

  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, MPI_SEND_STR, commandBuffer, commandId);
}

//ISEND:    SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_ISEND|Count|MPI_INT|DEST|TAG|COMM|REQUEST|EOT
charList* preSerializeMPIISend(int nodeId, int lineNum, int count, 
			 MPI_Datatype datatype, int dest, int tag, MPI_Comm comm, 
			 MPI_Request *request, int *commandId, XMLNode *expectedValues)
{
  //Create a buffer for the command's specific data
  int isendBuffMaxLength = 512;
  char isendBuff[isendBuffMaxLength];
  //zero out the buffer
  memset(isendBuff, '\0', isendBuffMaxLength);

  //Write the specific's for this command's data to the buffer
  int isendBuffLength =  sprintf(isendBuff, "%c%d%c%s%c%d%c%d%c%s%c%p",
   PARTITION_CHR, count, PARTITION_CHR, mpiTypeToStr(datatype), PARTITION_CHR, 
	 dest, PARTITION_CHR, tag, PARTITION_CHR, mpiCommToStr(comm), PARTITION_CHR, request);

	charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
	AddChars(commandBuffer, isendBuff, isendBuffLength);

	ValidateMessageCommand(commandBuffer, expectedValues, 
		count, datatype, tag, DEST_ELEMENT,dest);

  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, MPI_ISEND_STR , commandBuffer, commandId);
}

//RECV:     SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_RECV|COUNT|MPI_INT|SRC|TAG|COMM|EOT
charList* preSerializeMPIRecv(int nodeId, int lineNum, int count,
              MPI_Datatype datatype, int src, int tag, MPI_Comm comm, 
              int *commandId, XMLNode *expectedValues)
{
  //Create a buffer for the command's specific data
  int recvBuffMaxLength = 512;
  char recvBuff[recvBuffMaxLength];
  //zero out the buffer
  memset(recvBuff, '\0', recvBuffMaxLength);
    
 int recvBuffLength =  sprintf(recvBuff, "%c%d%c%s%c%d%c%d%c%s",
	PARTITION_CHR, count, PARTITION_CHR, mpiTypeToStr(datatype), PARTITION_CHR, 
	src, PARTITION_CHR, tag, PARTITION_CHR, mpiCommToStr(comm));

	charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
	AddChars(commandBuffer, recvBuff, recvBuffLength);

	ValidateMessageCommand(commandBuffer, expectedValues, 
		count, datatype, tag, SRC_ELEMENT,src);
	
  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, MPI_RECV_STR, commandBuffer, commandId);
}

//IRECV:    SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_IRECV|Count|MPI_INT|SRC|TAG|COMM|REQUEST|EOT
charList* preSerializeMPIIRecv(int nodeId, int lineNum, int count, 
			MPI_Datatype datatype, int src, int tag, MPI_Comm comm, MPI_Request 
			*request, int *commandId, XMLNode *expectedValues)
{
  //Create a buffer for the command's specific data
  int irecvBuffMaxLength = 512;
  char irecvBuff[irecvBuffMaxLength];
  //zero out the buffer
  memset(irecvBuff, '\0', irecvBuffMaxLength);

  int irecvBuffLength =  sprintf(irecvBuff, "%c%d%c%s%c%d%c%d%c%s%c%p",
		PARTITION_CHR, count, PARTITION_CHR, mpiTypeToStr(datatype), 
		PARTITION_CHR, src, PARTITION_CHR,tag, PARTITION_CHR, 
		mpiCommToStr(comm), PARTITION_CHR, request);

	charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
	AddChars(commandBuffer, irecvBuff, irecvBuffLength);

	ValidateMessageCommand(commandBuffer, expectedValues, 
		count, datatype, tag, SRC_ELEMENT,src);
		
  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, MPI_IRECV_STR, commandBuffer, commandId);
}

//WAIT:     SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_WAIT|REQUEST|EOT
charList* preSerializeMPIWait(int nodeId, int lineNum, MPI_Request *request, int *commandId)
{
  //Create a buffer for the command's specific data
  int waitBuffMaxLength = 512;
  char waitBuff[waitBuffMaxLength];
  //zero out the buffer
  memset(waitBuff, '\0', waitBuffMaxLength);

  int waitBuffLength =  sprintf(waitBuff, "%c%p", PARTITION_CHR, request);

	charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
	AddChars(commandBuffer, waitBuff, waitBuffLength);

  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, MPI_WAIT_STR, commandBuffer, commandId);
}

//BARRIER:  SOH|PREV|NODEID|COMMANDID|LINENUM|COMM|EOT
charList* preSerializeMPIBarrier(int nodeId, int lineNum, MPI_Comm comm, int *commandId)
{
    return createPreCommCommand(nodeId, lineNum, MPI_BARRIER_STR, comm, commandId);
}

//PROBE:    SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_PROBE|SRC|Tag|COMM|EOT
charList* preSerializeMPIProbe(int nodeId, int lineNum,  int src, int tag, MPI_Comm comm, int *commandId, XMLNode *expectedValues)
{
  //Create a buffer for the command's specific data
  int probeBuffMaxLength = 512;
  char probeBuff[probeBuffMaxLength];
  //zero out the buffer
  memset(probeBuff, '\0', probeBuffMaxLength);

  int probeBuffLength =  sprintf(probeBuff, "%c%d%c%d%c%s",
    PARTITION_CHR, src, PARTITION_CHR, tag, PARTITION_CHR, mpiCommToStr(comm));
  
 	charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
	AddChars(commandBuffer, probeBuff, probeBuffLength);

	if(expectedValues == NULL)
	{
		AddChars(commandBuffer, "||", 2);
	}
	else
	{
		//Get the parameters section of the node
		XMLNode *paramValues = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);

		//Validate the source
		AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
		validateIntField(SRC_ELEMENT, paramValues, src, commandBuffer);

		//Validate the tag
		AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
		validateIntField(TAG_ELEMENT, paramValues, tag, commandBuffer);
	}

  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, MPI_PROBE_STR, commandBuffer, commandId);
}

//IPROBE:   SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_IPROBE|SRC|TAG|COMM|EOT
charList* preSerializeMPIIProbe(int nodeId, int lineNum, int src, int tag, MPI_Comm comm, int *commandId, XMLNode *expectedValues)
{
  //Create a buffer for the command's specific data
  int iprobeBuffMaxLength = 512;
  char iprobeBuff[iprobeBuffMaxLength];
  //zero out the buffer
  memset(iprobeBuff, '\0', iprobeBuffMaxLength);

  int iprobeBuffLength =  sprintf(iprobeBuff, "%c%d%c%d%c%s",
    PARTITION_CHR, src, PARTITION_CHR, tag, PARTITION_CHR, mpiCommToStr(comm));
  
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
	AddChars(commandBuffer, iprobeBuff, iprobeBuffLength);

	if(expectedValues == NULL)
	{
		AddChars(commandBuffer, "||", 2);
	}
	else
	{
		//Get the parameters section of the node
		XMLNode *paramValues = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);

		//Validate the source
		AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
		validateIntField(SRC_ELEMENT, paramValues, src, commandBuffer);

		//Validate the tag
		AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
		validateIntField(TAG_ELEMENT, paramValues, tag, commandBuffer);
	}
  
  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, MPI_IPROBE_STR, commandBuffer, commandId);
}

//FINALIZE: SOH|PRE|NODEID|COMMANDID|LINENUM|MPI_FINALIZE||EOT
charList* preSerializeMPIFinalize(int nodeId, int lineNum, int *commandId)
{
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
  
  //Create a general wrapper around the command
  return wrapPreCommand(nodeId, lineNum, MPI_FINALIZE_STR, commandBuffer, commandId);
}

/****************************************POST COMMANDS***********************************************************************/
charList* wrapPostCommand(int nodeId, int commandId, charList* commandInfo, int returnValue)
{
  int footerLength = 1;
  char footerBuffer[footerLength];
  footerBuffer[0] = EOT;

  //SOH|POST|NODEID|COMMANDID|RETURNVALUE|
  char headerBuffer[255];  
  int headerLength = 0;
  headerLength = sprintf(headerBuffer, "%c%s%c%d%c%d%c%d", 
        SOH, POST_HEADER_STR, PARTITION_CHR, nodeId, 
        PARTITION_CHR , commandId, PARTITION_CHR, returnValue);


  //Create an input buffer to store the incoming byte stream
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

  //SOH|POST|SOURCEID|COMMANDID|LINENUM|COMMAND_NAME|
  AddChars(commandBuffer, headerBuffer, headerLength);

  //COMMAND_INFO
  AddChars(commandBuffer, commandInfo->Items,  commandInfo->ItemCount);
  CleanUpCharList(commandInfo);
  
  //|EOT
  AddChars(commandBuffer, footerBuffer, footerLength);

  commandBuffer->Items[commandBuffer->ItemCount] = '\0';

  return commandBuffer;
}

//MPIINIT:  SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
charList* postSerializeMPIInit(int nodeId, int commandId, int returnValue, XMLNode *expectedValues)
{
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

	//Validate
  ValidateReturnValue(commandBuffer, expectedValues, returnValue);
  return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//RANK:  SOH|POST|NODEID|COMMANDID|RETURNVALUE|RANK|EOT
charList* postSerializeMPIRank(int nodeId, int commandId, int rank, int returnValue, XMLNode *expectedValues)
{
	//Create a buffer list  
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

	//Validate
  ValidateReturnValue(commandBuffer, expectedValues, returnValue);

  //Create a buffer for the command's specific data
  int rankBuffMaxLength = 50;
  char rankBuff[rankBuffMaxLength];
  //zero out the buffer
  memset(rankBuff, '\0', rankBuffMaxLength);

  //Write the specific's for this command's data to the buffer
  int rankBuffLength =  sprintf(rankBuff, "|%d", rank);
  
  AddChars(commandBuffer, rankBuff, rankBuffLength);

  return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//SIZE:  SOH|POST|NODEID|COMMANDID|RETURNVALUE|SIZE|EOT
charList* postSerializeMPISize(int nodeId, int commandId, int size, int returnValue, XMLNode *expectedValues)
{
  //Create a buffer list  
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
    
	//Validate
  ValidateReturnValue(commandBuffer, expectedValues, returnValue);

  //Create a buffer for the command's specific data
  int sizeBuffMaxLength = 50;
  char sizeBuff[sizeBuffMaxLength];
  //zero out the buffer
  memset(sizeBuff, '\0', sizeBuffMaxLength);

  //Write the specific's for this command's data to the buffer
  int sizeBuffLength =  sprintf(sizeBuff, "|%d", size);
  AddChars(commandBuffer, sizeBuff, sizeBuffLength);
    
  //Create a general wrapper around the command
  return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//Send:  SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
charList* postSerializeMPISend(int nodeId, MPI_Datatype datatype, int count, void *buf,
			int commandId, int returnValue, XMLNode *expectedValues, 
			char* sohReplace, char* partitionReplace, char* eotReplace)
{
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
	
	//Validate the return value
	ValidateReturnValue(commandBuffer, expectedValues, returnValue);
	AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));		 		          
 	
 	//Validate
 	if(expectedValues == NULL)
 		AddChars(commandBuffer, "X", 1);
 	else
		validateBufField(xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT),
				datatype, count, buf, commandBuffer, sohReplace, partitionReplace, eotReplace);
	
	return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//ISend:  SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
charList* postSerializeMPIISend(int nodeId, MPI_Datatype datatype, 
			int count, void *buf,
			int commandId, int returnValue, XMLNode *expectedValues, 
			char* sohReplace, char* partitionReplace, char* eotReplace)
{
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

  //Validate the return value
	ValidateReturnValue(commandBuffer, expectedValues, returnValue);
	AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
		
	//Validate
 	if(expectedValues == NULL)
 		AddChars(commandBuffer, "X", 1);
 	else
		validateBufField(xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT),
				datatype, count, buf, commandBuffer, sohReplace, partitionReplace, eotReplace);

	return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//RECV:  SOH|POST|NODEID|COMMANDID|RETURNVALUE|Status.Source, Status.Tag, Status.Error|EOT
charList* postSerializeMPIRecv(int nodeId, MPI_Datatype datatype, 
										MPI_Status *status, int count, void *buf,
										int commandId, int returnValue, XMLNode *expectedValues, 
										char* sohReplace, char* partitionReplace, char* eotReplace)
{
  //Create a buffer list  
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

  //Validate the return value
	ValidateReturnValue(commandBuffer, expectedValues, returnValue);

  //Create a buffer for the command's specific data
  int recvBuffMaxLength = 512;
  char recvBuff[recvBuffMaxLength];
  //zero out the buffer
  memset(recvBuff, '\0', recvBuffMaxLength);
    
  int recvBuffLength =  sprintf(recvBuff, "|%d,%d,%s",
    status->MPI_SOURCE, status->MPI_TAG, mpiErrToStr(status->MPI_ERROR));

	//Add the status info
  AddChars(commandBuffer, recvBuff, recvBuffLength);
  AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
 	
 	if(expectedValues == NULL)
 	{
 		AddChars(commandBuffer, "|X", 2);
 	}
 	else
 	{  
  	//Validate the status buffer
		validateStatusField(xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT), status,commandBuffer);
		
		//Validate the buffer
		AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
		validateBufField(xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT),
			datatype, count, buf, commandBuffer, sohReplace, partitionReplace, eotReplace);
	}

  //Create a general wrapper around the command
  return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//IRECV: SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
charList* postSerializeMPIIRecv(int nodeId, int commandId, int returnValue, XMLNode *expectedValues)
{
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
  
  //Validate the return value
	ValidateReturnValue(commandBuffer, expectedValues, returnValue);
  
	return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//WAIT: SOH|POST|NODEID|COMMANDID|RETURNVALUE|Status.Source, Status.Tag, Status.Error|EOT		 										
charList* postSerializeMPIWait(int nodeId, MPI_Status *status, AsyncBuf* asyncBuf,
			int commandId, int returnValue, XMLNode *expectedValues, 
			char* sohReplace, char* partitionReplace, char* eotReplace)
{
  //Create a buffer list  
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
  
  //Validate the return value
  ValidateReturnValue(commandBuffer, expectedValues, returnValue);
  	
  //Create a buffer for the command's specific data
  int waitBuffMaxLength = 512;
  char waitBuff[waitBuffMaxLength];
  //zero out the buffer
  memset(waitBuff, '\0', waitBuffMaxLength);
    
  int waitBuffLength =  sprintf(waitBuff, "|%d,%d,%s",
    status->MPI_SOURCE, status->MPI_TAG, mpiErrToStr(status->MPI_ERROR));
    
  //Add the status buffer
  AddChars(commandBuffer, waitBuff, waitBuffLength);
 	AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
 	
 	if(expectedValues == NULL)
 	{
 		//Add leave the invalid status & invalid buffer spots empty
 		AddChars(commandBuffer, "|X", 2);
	}
	else
	{ 
		XMLNode* paramsNode = xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT);         		
		validateStatusField(paramsNode, status,commandBuffer);

		AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));        	
		
		if(asyncBuf == NULL)
		{
			AddChars(commandBuffer, "X", 1);
		}
		else
		{
			validateBufField(expectedValues, asyncBuf->datatype, asyncBuf->count, asyncBuf->buf, 
				commandBuffer, sohReplace, partitionReplace, eotReplace);
		}
		
	}

  //Create a general wrapper around the command
  return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//BARRIER: SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
charList* postSerializeMPIBarrier(int nodeId, int commandId, int returnValue, XMLNode *expectedValues)
{
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
  
 	//Validate the return value
  ValidateReturnValue(commandBuffer, expectedValues, returnValue);
  
	return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//PROBE: SOH|POST|NODEID|COMMANDID|RETURNVALUE|Status.Source, Status.Tag, Status.Error|EOT
charList* postSerializeMPIProbe(int nodeId, int commandId, MPI_Status *status, int returnValue, XMLNode *expectedValues)
{
  //Create a buffer list  
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

 	//Validate the return value
  ValidateReturnValue(commandBuffer, expectedValues, returnValue);

  //Create a buffer for the command's specific data
  int probeBuffMaxLength = 512;
  char probeBuff[probeBuffMaxLength];
  //zero out the buffer
  memset(probeBuff, '\0', probeBuffMaxLength);
    
  int probeBuffLength =  sprintf(probeBuff, "|%d,%d,%s",
    status->MPI_SOURCE, status->MPI_TAG, mpiErrToStr(status->MPI_ERROR));
  
  //Add the status buffer
  AddChars(commandBuffer, probeBuff, probeBuffLength);
  AddChars(commandBuffer, PARTITION_STR, strlen(PARTITION_STR));
  
 	if(expectedValues != NULL)
		validateStatusField(xmlGetChildNode(expectedValues, PARAMETERS_ELEMENT), status,commandBuffer);

  //Create a general wrapper around the command
  return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//IPROBE: SOH|POST|NODEID|COMMANDID|RETURNVALUE|FLAG|Status.Source, Status.Tag, Status.Error|EOT
charList* postSerializeMPIIProbe(int nodeId, int commandId, int *flag, MPI_Status *status, int returnValue)
{
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

  //Not Validating Return value
  AddChars(commandBuffer, "|", 1);

  //Create a buffer for the command's specific data
  int iprobeBuffMaxLength = 512;
  char iprobeBuff[iprobeBuffMaxLength];
  //zero out the buffer
  memset(iprobeBuff, '\0', iprobeBuffMaxLength);
    
  int iprobeBuffLength =  sprintf(iprobeBuff, "|%d|%d,%d,%s", flag[0],
    status->MPI_SOURCE, status->MPI_TAG, mpiErrToStr(status->MPI_ERROR));
    
  //Add the status buffer
  AddChars(commandBuffer, iprobeBuff, iprobeBuffLength);

  //Create a general wrapper around the command
  return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//FINALIZE: SOH|POST|NODEID|COMMANDID|RETURNVALUE||EOT
charList* postSerializeMPIFinalize(int nodeId, int commandId, int returnValue, XMLNode *expectedValues)
{
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);
  
 	//Validate the return value
  ValidateReturnValue(commandBuffer, expectedValues, returnValue);
 
  //Create a general wrapper around the command
  return wrapPostCommand(nodeId, commandId, commandBuffer, returnValue);
}

//Serialize a console command
charList* serializeConsole(char* message, int messageLength, int sourceId, 
									char* sohReplace, char* partitionReplace, char* eotReplace)
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
	if(ReplaceChars(tempBuffer, soh, sohReplace) == TRUE)
		replacementIndicator = 'E';
		
	//replace the partition char
	if(ReplaceChars(tempBuffer, partition, partitionReplace) == TRUE)
		replacementIndicator = 'E';
		
	//replace the EOT
	if(ReplaceChars(tempBuffer, eot, eotReplace) == TRUE)
		replacementIndicator = 'E';

  //SOH|SourceId|CommandId|LineNum|CommandName|CommandInfo|EOT
  char headerBuffer[255];  
  int headerLength = 0;
  
  headerLength = sprintf(headerBuffer, "%c%s%c%d%c%c%c", 
        SOH, CONSOLE_HEADER_STR, PARTITION_CHR, sourceId, 
        PARTITION_CHR, replacementIndicator, PARTITION_CHR);

  //Create an input buffer to store the incoming byte stream
  charList* commandBuffer = (charList*)malloc(sizeof(charList));
  InitializeCharList(commandBuffer);

  AddChars(commandBuffer, headerBuffer, headerLength);  
  AddChars(commandBuffer, tempBuffer->Items,  tempBuffer->ItemCount);
  AddChars(commandBuffer, eot, 1);

  commandBuffer->Items[commandBuffer->ItemCount] = '\0';

	free(soh);
	free(partition);
	free(eot);
	CleanUpCharList(tempBuffer);

  return commandBuffer;
}


