#define _GNU_SOURCE
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include "Headers/mpiXML.h"
#include "Headers/mpiUtils.h"
#include "Headers/dictionary.h"

int asyncRequestCounter = 0;

char* getTime()
{
  time_t rawtime;
  struct tm * timeinfo;
  
  time ( &rawtime );
  timeinfo = localtime (&rawtime);

  return asctime(timeinfo);
}

XMLNode* createGenericCommandNode(char* elementName, int rank, int commandId)
{
  XMLNode *genericCommandNode = xmlCreateNode(ELEMENT_NODE, elementName, NULL);
  
  char* rankBuffer;
  char* commandIdBuffer;
  int bufferSize;
  bufferSize = asprintf(&rankBuffer, "%d", rank);
  bufferSize = asprintf(&commandIdBuffer, "%d", commandId);
  

  xmlAddAttribute(genericCommandNode, RANK_ATTRIBUTE, rankBuffer);
  xmlAddAttribute(genericCommandNode, COMMAND_ID_ATTRIBUTE, commandIdBuffer);
  xmlAddAttribute(genericCommandNode, DATETIME_ATTRIBUTE, getTime());

  free(rankBuffer);
  free(commandIdBuffer);

  return genericCommandNode;
}

XMLNode* createRequestElementNode(int src, int dest, void* buf, int count, MPI_Datatype datatype, MPI_Request *request)
{
  XMLNode *elementNode = createIntElementNode(REQUEST_ELEMENT, asyncRequestCounter);
  AsyncBuf* asyncBuf = (AsyncBuf*)malloc(sizeof(AsyncBuf));
  asyncBuf->id = asyncRequestCounter;
  asyncBuf->src = src;
  asyncBuf->dest = dest;
  asyncBuf->datatype = datatype;
  asyncBuf->count = count;
  asyncBuf->request = request;
  asyncBuf->buf = buf;

  DictionaryAdd(request, asyncBuf);
  asyncRequestCounter++;

  return elementNode;
}

XMLNode* createStatusNode(MPI_Status* status)
{
  XMLNode *statusNode = xmlCreateNode(ELEMENT_NODE, STATUS_ELEMENT, NULL);
  xmlAddChildNode(statusNode, createIntElementNode(MPI_SOURCE_STR, status->MPI_SOURCE));
  xmlAddChildNode(statusNode, createIntElementNode(MPI_TAG_STR, status->MPI_TAG));  
  xmlAddChildNode(statusNode, createIntElementNode(MPI_ERROR_STR, status->MPI_ERROR)); 

  return statusNode;
}

void createXMLValues(XMLNode* bufNode, XMLNode* bufByteNode, void* values, MPI_Datatype datatype, int count)
{
  int i = 0;
  for(i=0; i < count; i++)
  {
    XMLNode *valueNode = xmlCreateNode(ELEMENT_NODE, VALUE_ELEMENT, NULL);


    char *value;
    char* byteCode = NULL;

    int length = 0;
    if(datatype == MPI_CHAR){
      length = asprintf(&value, "%c", ((char*)values)[i]);    
    } 
      else if(datatype == MPI_BYTE || datatype == MPI_UNSIGNED_CHAR){
      length = asprintf(&value, "%c", ((unsigned char*)values)[i]);    
    } else if(datatype == MPI_SHORT){
      length = asprintf(&value, "%hi", ((short*)values)[i]);    
    } else if(datatype == MPI_INT){
      length = asprintf(&value, "%d", ((int*)values)[i]);    
    } else if(datatype == MPI_LONG){
      length = asprintf(&value, "%ld", ((long int*)values)[i]);    
    } else if(datatype == MPI_FLOAT){
      byteCode = (char*)malloc(sizeof(float));
      memcpy(byteCode, &(((float*)values)[i]), sizeof(float));
      floatingStrToReadableStr(byteCode, (int)sizeof(float));  
      length = asprintf(&value, "%f", ((float*)values)[i]);
    } else if(datatype == MPI_DOUBLE){
      length = asprintf(&value, "%f", ((double*)values)[i]);  
      byteCode = (char*)malloc(sizeof(double));
      memcpy(byteCode, &(((double*)values)[i]), sizeof(double));
      floatingStrToReadableStr(byteCode, (int)sizeof(double));  
    } else if(datatype == MPI_UNSIGNED_SHORT){
      length = asprintf(&value, "%hu", ((unsigned short*)values)[i]);   
    } else if(datatype == MPI_UNSIGNED){
      length = asprintf(&value, "%u", ((unsigned int*)values)[i]);   
    } else if(datatype == MPI_UNSIGNED_LONG){
      length = asprintf(&value, "%lu", ((unsigned long*)values)[i]);   
    } else if(datatype == MPI_LONG_DOUBLE){
      length = asprintf(&value, "%Lf", ((long double*)values)[i]);

      //copy off the byte code of the floating point value too, to make sure we store it correctly
      byteCode = (char*)malloc(sizeof(long double));
      memcpy(byteCode, &(((long double*)values)[i]), sizeof(long double));
      floatingStrToReadableStr(byteCode, (int)sizeof(long double));

      //long double *ldBuf = (long double *)values;
      //memcpy(byteCode, &(ldBuf[i]), sizeof(long double));
      
      
      
      //if(ldBuf[i] == convertedBuf[0])
      //  printf("Equal for %Lf, byte code is %s\n", ldBuf[i], byteCode);
      //else
      //  printf("Not Equal for %Lf, byte code is %s\n", ldBuf[i], byteCode);
      
    
      //free(convertedBuf);
    }

    xmlAddChildNode(valueNode, xmlCreateNode(TEXT_NODE, TEXT_NAME, value));
    xmlAddChildNode(bufNode, valueNode);
    free(value);

    if(byteCode != NULL)
    {
      //Create a 'byte code' node and add its value as the text of the field
      XMLNode *byteValueNode = xmlCreateNode(ELEMENT_NODE, VALUE_ELEMENT, NULL);
      xmlAddChildNode(byteValueNode, xmlCreateNode(TEXT_NODE, TEXT_NAME, byteCode));

      //Add the byte code to the parent too
      xmlAddChildNode(bufByteNode, byteValueNode);

      //free up the byte code buffer
      free(byteCode);
    }
  }
}

XMLNode* xmlMPIInit(int rank, int returnValue, int commandId)
{
  //Create a node with the node for the 'rank' command with the standard attributes
  XMLNode *initNode = createGenericCommandNode(MPI_INIT_STR, rank, commandId);

  xmlAddChildNode(initNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return initNode;
}

XMLNode* xmlMPIRank(int rank, MPI_Comm comm, int rankValue, int returnValue, int commandId)
{
  //Create a node with the node for the 'rank' command with the standard attributes
  XMLNode *rankNode = createGenericCommandNode(MPI_RANK_STR, rank, commandId);

  //Add the Parameters Node
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(rankNode, parametersNode);

  //Add the rest of the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(COMM_ELEMENT, (int)comm));

  //Add the return value to the message
  xmlAddChildNode(rankNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return rankNode;
}

XMLNode* xmlMPISize(int rank, MPI_Comm comm, int sizeValue, int returnValue, int commandId)
{
  //Create a node with the node for the 'size' command with the standard attributes
  XMLNode *sizeNode = createGenericCommandNode(MPI_SIZE_STR, rank, commandId);

  //Add the Parameters Node
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(sizeNode, parametersNode);

  //Add the rest of the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(COMM_ELEMENT, (int)comm));
 

  //Add the return value to the message
  xmlAddChildNode(sizeNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return sizeNode;
}

XMLNode* xmlMPIFinalize(int rank, int returnValue, int commandId)
{
  //Create a node with the node for the 'rank' command with the standard attributes
  XMLNode *finalizeNode = createGenericCommandNode(MPI_FINALIZE_STR, rank, commandId);

  xmlAddChildNode(finalizeNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return finalizeNode;
}

XMLNode* xmlMPISend(int rank, void *buf, int count, MPI_Datatype datatype, 
										int dest, int tag, MPI_Comm comm, int returnValue, int commandId)
{
  //Create a node with the node for the 'send' command with the standard attributes
  XMLNode *sendNode = createGenericCommandNode(MPI_SEND_STR, rank, commandId);

  //Add the Parameters Node
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(sendNode, parametersNode);
  
  //Add the buff array parameter
  XMLNode *bufNode = xmlAddChildNode(parametersNode, xmlCreateNode(ELEMENT_NODE, BUF_ELEMENT, NULL));  
  XMLNode *bufByteNode = NULL;

  if(isFloatingPointType(datatype) == TRUE){
     bufByteNode = xmlAddChildNode(parametersNode, xmlCreateNode(ELEMENT_NODE, BUF_BYTES_ELEMENT, NULL));
  }

  createXMLValues(bufNode, bufByteNode, buf, datatype, count);


  //Add the rest of the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(COUNT_ELEMENT, count));
  xmlAddChildNode(parametersNode, createStringElementNode(DATA_TYPE_ELEMENT, mpiTypeToStr(datatype)));
  xmlAddChildNode(parametersNode, createIntElementNode(DEST_ELEMENT, dest));
  xmlAddChildNode(parametersNode, createIntElementNode(TAG_ELEMENT, tag));
  xmlAddChildNode(parametersNode, createIntElementNode(COMM_ELEMENT, (int)comm));
 

  //Add the return value to the message
  xmlAddChildNode(sendNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 
  
  return sendNode;
}



XMLNode* xmlMPIRecv(int rank, void *buf, int count, 
	      MPI_Datatype datatype, int src, int tag, MPI_Comm comm, 
	      MPI_Status *status, int returnValue, int commandId)
{
  //Create a node with the node for the 'receive' command with the standard attributes
  XMLNode *recvNode = createGenericCommandNode(MPI_RECV_STR, rank, commandId);

  //Add the Parameters Node
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(recvNode, parametersNode);

  XMLNode *bufNode = xmlAddChildNode(parametersNode, xmlCreateNode(ELEMENT_NODE, BUF_ELEMENT, NULL));  
  XMLNode *bufByteNode = NULL;

  if(isFloatingPointType(datatype) == TRUE){
     bufByteNode = xmlAddChildNode(parametersNode, xmlCreateNode(ELEMENT_NODE, BUF_BYTES_ELEMENT, NULL));
  }

  createXMLValues(bufNode, bufByteNode, buf, datatype, count);

  //Add the rest of the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(COUNT_ELEMENT, count));
  xmlAddChildNode(parametersNode, createStringElementNode(DATA_TYPE_ELEMENT, mpiTypeToStr(datatype)));
  xmlAddChildNode(parametersNode, createIntElementNode(SRC_ELEMENT, src));
  xmlAddChildNode(parametersNode, createIntElementNode(TAG_ELEMENT, tag));
  xmlAddChildNode(parametersNode, createIntElementNode(COMM_ELEMENT, (int)comm));

  //Add the status node to the parameters
  xmlAddChildNode(parametersNode, createStatusNode(status));

  //Add the return value to the message
  xmlAddChildNode(recvNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return recvNode;
}

XMLNode* xmlMPIISend(int rank, void *buf, int count, 
  MPI_Datatype datatype, int dest, int tag, MPI_Comm comm, MPI_Request *request, int returnValue, int commandId)
{
  //Create a node with the node for the 'iSend' command with the standard attributes
  XMLNode *iSendNode = createGenericCommandNode(MPI_ISEND_STR, rank, commandId);

  //Add the Parameters Node
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(iSendNode, parametersNode);

 
  //Add the buff array parameter
  XMLNode *bufNode = xmlAddChildNode(parametersNode, xmlCreateNode(ELEMENT_NODE, BUF_ELEMENT, NULL));  
  XMLNode *bufByteNode = NULL;

  if(isFloatingPointType(datatype) == TRUE){
     bufByteNode = xmlAddChildNode(parametersNode, xmlCreateNode(ELEMENT_NODE, BUF_BYTES_ELEMENT, NULL));
  }

  createXMLValues(bufNode, bufByteNode, buf, datatype, count);

  //Add the rest of the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(COUNT_ELEMENT, count));
  xmlAddChildNode(parametersNode, createStringElementNode(DATA_TYPE_ELEMENT, mpiTypeToStr(datatype)));
  xmlAddChildNode(parametersNode, createIntElementNode(DEST_ELEMENT, dest));
  xmlAddChildNode(parametersNode, createIntElementNode(TAG_ELEMENT, tag));
  xmlAddChildNode(parametersNode, createIntElementNode(COMM_ELEMENT, (int)comm));

  xmlAddChildNode(parametersNode, createRequestElementNode(-1, dest, buf, count, datatype, request));
  //Add the return value to the message
  xmlAddChildNode(iSendNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return iSendNode;
}

XMLNode* xmlMPIIRecv(int rank, void *buf,  int count, 
	      MPI_Datatype datatype, int src, int tag, MPI_Comm comm, MPI_Request *request, int returnValue, int commandId)
{

  //Create a node with the node for the 'iRecv' command with the standard attributes
  XMLNode *iRecvNode = createGenericCommandNode(MPI_IRECV_STR, rank, commandId);

  //Add the Parameters Node
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(iRecvNode, parametersNode);


  //Add the rest of the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(COUNT_ELEMENT, count));
  xmlAddChildNode(parametersNode, createStringElementNode(DATA_TYPE_ELEMENT, mpiTypeToStr(datatype)));
  xmlAddChildNode(parametersNode, createIntElementNode(SRC_ELEMENT, src));
  xmlAddChildNode(parametersNode, createIntElementNode(TAG_ELEMENT, tag));
  xmlAddChildNode(parametersNode, createIntElementNode(COMM_ELEMENT, (int)comm));
  xmlAddChildNode(parametersNode, createRequestElementNode(src, -1, buf, count, datatype, request));

  //Add the return value to the message
  xmlAddChildNode(iRecvNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return iRecvNode;
}

XMLNode* xmlMPIWait(int rank, MPI_Request *request, MPI_Status *status, int returnValue, int commandId)
{
  //Get the buffer that should now be filled from the request
  AsyncBuf* asyncBuf = DictionaryRemove(request);     

  //Create a node with the node for the 'wait' command with the standard attributes
  XMLNode *waitNode = createGenericCommandNode(MPI_WAIT_STR, rank, commandId);

  //Add the Parameters Node
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(waitNode, parametersNode);

  //Add the request and status node to the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(REQUEST_ELEMENT, asyncBuf->id));
  xmlAddChildNode(parametersNode, createStatusNode(status));

  //If this was a recieve node, the results should be stored in the buffer, serialize those here
  if(asyncBuf->src >= 0){
    XMLNode *bufNode = xmlAddChildNode(waitNode, xmlCreateNode(ELEMENT_NODE, BUF_ELEMENT, NULL));  
    XMLNode *bufByteNode = NULL;

    if(isFloatingPointType(asyncBuf->datatype) == TRUE){
      bufByteNode = xmlAddChildNode(waitNode, xmlCreateNode(ELEMENT_NODE, BUF_BYTES_ELEMENT, NULL));
    }

    createXMLValues(bufNode, bufByteNode, asyncBuf->buf, asyncBuf->datatype, asyncBuf->count);
  }

  //Clear up the send or receive record from this request now
//  free(asyncBuf);

  //Add the return value to the message
  xmlAddChildNode(waitNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return waitNode;  
}

XMLNode* xmlMPIBarrier(int rank,  MPI_Comm comm, int returnValue, int commandId)
{
  //Create a node with the node for the 'barrier' command with the standard attributes
  XMLNode *barrierNode = createGenericCommandNode(MPI_BARRIER_STR, rank, commandId);

  //Create and add a parameters node to the root
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(barrierNode, parametersNode);
  
  //Add the Comm value to the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(COMM_ELEMENT, (int)comm));

  //Add the return value to the root of the node
  xmlAddChildNode(barrierNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return barrierNode;
}

XMLNode* xmlMPIProbe(int rank,  int src, int tag, MPI_Comm comm, MPI_Status *status, int returnValue, int commandId)
{
  //Create a node with the node for the 'probe' command with the standard attributes
  XMLNode *probeNode = createGenericCommandNode(MPI_PROBE_STR, rank, commandId);

  //Add the Parameters Node
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(probeNode, parametersNode);
  
  //Add the rest of the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(SRC_ELEMENT, src));
  xmlAddChildNode(parametersNode, createIntElementNode(TAG_ELEMENT, tag));
  xmlAddChildNode(parametersNode, createIntElementNode(COMM_ELEMENT, (int)comm));

  //Add the status node to the parameters
  xmlAddChildNode(parametersNode, createStatusNode(status)); 

  //Add the return value to the message
  xmlAddChildNode(probeNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return probeNode;
}

//Nonblocking Probe Command
XMLNode* xmlMPIIProbe(int rank, int src, int tag, MPI_Comm comm, int *flag, MPI_Status *status, int returnValue, int commandId)
{
  //Create a node with the node for the 'iprobe' command with the standard attributes
  XMLNode *iProbeNode = createGenericCommandNode(MPI_IPROBE_STR, rank, commandId);

  //Add the Parameters Node
  XMLNode *parametersNode = xmlCreateNode(ELEMENT_NODE, PARAMETERS_ELEMENT, NULL);
  xmlAddChildNode(iProbeNode, parametersNode);
  
  //Add the rest of the parameters
  xmlAddChildNode(parametersNode, createIntElementNode(SRC_ELEMENT, src));
  xmlAddChildNode(parametersNode, createIntElementNode(TAG_ELEMENT, tag));
  xmlAddChildNode(parametersNode, createIntElementNode(COMM_ELEMENT, (int)comm));
  xmlAddChildNode(parametersNode, createIntElementNode(FLAG_ELEMENT, flag[0]));//TODO Best way to serialize the flag??

  //Add the status node to the parameters
  xmlAddChildNode(parametersNode, createStatusNode(status)); 

  //Add the return value to the message
  xmlAddChildNode(iProbeNode, createIntElementNode(RETURN_VALUE_ELEMENT, returnValue)); 

  return iProbeNode;
}


