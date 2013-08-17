#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <unistd.h>
#include "Headers/booleanLogic.h"
#include "Headers/DADParser.h"

void EncodePacket(char* body, int bodyLength, char* result)
{
	result = (char*)malloc((bodyLength + 2) * sizeof(char));
	result[0] = SOH;
	memmove(body, result + 1, bodyLength);
	result[bodyLength +1] = EOT;
}

void SendIdData(int writeSocket, int nodeId, int processId)
{
	int nameSize = 100;
	char* name = (char*)malloc(nameSize * sizeof(char));
	memset(name, '\0', nameSize);
		
	gethostname(name, nameSize);

  char ackBuffer[200];
	memset(ackBuffer, '\0', 200);

	//write back the id and name  
  int ackBufferLen = sprintf(ackBuffer, "%c%s%c%d%c%d%c%s%c",
  	SOH, NODE_ID_HEADER_STR,PARTITION_CHR, nodeId, 
  	PARTITION_CHR, processId, PARTITION_CHR, name, EOT); 

  free(name);
  //send back what the main process's id wa
  if(write (writeSocket, ackBuffer, ackBufferLen) < 0)
  {
  	exit(0);
	}
}

void ParseInputBuffer(charList* buffer, queue* result, int trimControlChars)
{
  int startIndex, endIndex;

  char* startPtr = NULL;
  char* endPtr = NULL;
  char* message = NULL;


  startPtr = (char*)memchr(buffer->Items, SOH, buffer->ItemCount);

  if(startPtr != NULL)
  {
    startIndex = startPtr - buffer->Items;

    //Remove everything up to the first SOH
    RemoveChars(NULL, buffer, startIndex);

    endPtr = (char*)memchr(buffer->Items, EOT, buffer->ItemCount);

    while(startPtr != NULL && endPtr != NULL)
    {
      //Add one to the ptr because we are 1 based
      endIndex = (endPtr - buffer->Items) + 1;

      //char* message = NULL;
      message = NULL;
      if(trimControlChars == TRUE)
      {
        endIndex = endIndex -2;

        message = (char*)malloc((endIndex + 1) * sizeof(char));
        memset(message, '\0', endIndex + 1);

        //Remove the SOH
        RemoveChars(NULL, buffer, 1);
        
        //Get the message
        RemoveChars(message, buffer, endIndex);


        //Remove the EOT
        RemoveChars(NULL, buffer, 1);
      }
      else
      {
        message = (char*)malloc((endIndex + 1) * sizeof(char));
        memset(message, '\0', endIndex + 1);

        RemoveChars(message, buffer, endIndex);
      }
      Enqueue(result, message);

      startPtr = (char*)memchr(buffer->Items, SOH, buffer->ItemCount);

      if(startPtr != NULL)
      {
        //Remove everything up to the first SOH
        startIndex = startPtr - buffer->Items;
        RemoveChars(NULL, buffer, startIndex);

        endPtr = (char*)memchr(buffer->Items, EOT, buffer->ItemCount);
      }
    }    
  }
}

