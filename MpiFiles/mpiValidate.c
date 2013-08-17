#define _GNU_SOURCE
#include <stdio.h>
#include <stdarg.h>
#include <string.h>
#include <stdlib.h>
#include "Headers/DADParser.h"
#include "Headers/mpiValidate.h"
#include "Headers/collections.h"

void validateIntField(char* fieldName, XMLNode* expectedParams, int actualValue, charList* resultBuffer)
{
  int expectedValue = atoi(xmlGetText(xmlGetChildNode(expectedParams, fieldName))); 

  if(actualValue != expectedValue)
  {
    char* intToStrBuff = NULL;
	  int allocatedSize = asprintf(&intToStrBuff, "%d", expectedValue);
	  AddChars(resultBuffer, intToStrBuff, allocatedSize);
	  free(intToStrBuff);
	}
}


void validateDatatypeField(XMLNode* expectedParams, MPI_Datatype actualValue, charList* resultBuffer)
{
  MPI_Datatype expectedValue  = strToMPIType(xmlGetText(xmlGetChildNode(expectedParams, DATA_TYPE_ELEMENT)));

  if(actualValue != expectedValue)
  {
    char* dataTypeBuff = NULL;
	  int allocatedSize = asprintf(&dataTypeBuff, "%s", mpiTypeToStr(expectedValue));
	  AddChars(resultBuffer, dataTypeBuff, allocatedSize);
	  free(dataTypeBuff);
	}
}

void validateStatusField(XMLNode* expectedParams, 
	MPI_Status *actualStatus,charList* resultBuffer)
{
  XMLNode *statusNode = xmlGetChildNode(expectedParams, STATUS_ELEMENT);   

  int expectedSource = atoi(xmlGetText(xmlGetChildNode(statusNode, MPI_SOURCE_STR))); 
  int expectedTag = atoi(xmlGetText(xmlGetChildNode(statusNode, MPI_TAG_STR))); 
    
  if(actualStatus->MPI_SOURCE != expectedSource || 
  	 actualStatus->MPI_TAG != expectedTag)
  {
    char* statusToStrBuff = NULL;
	  int allocatedSize = asprintf(&statusToStrBuff, "%d,%d,", 
	  									expectedSource, expectedTag);	  									
	  AddChars(resultBuffer, statusToStrBuff, allocatedSize);
	  free(statusToStrBuff);    	
	}
}


void validateBufField(XMLNode *expectedParams, MPI_Datatype expectedDatatype, 
	int expectedCount, void *actualBuf, charList* result, 
	char* sohReplace, char* partitionReplace, char* eotReplace)
{
	//Assume that it is going to be unencoded
	AddChars(result, "X", 1);
  int replacementIndicatorIndex = result->ItemCount - 1;
	
  XMLNode *expectedBufNode;

  if(isFloatingPointType(expectedDatatype) == TRUE)
    expectedBufNode = xmlGetChildNode(expectedParams, BUF_BYTES_ELEMENT);
  else
    expectedBufNode = xmlGetChildNode(expectedParams, BUF_ELEMENT);   


  int bufferMemorySize = - 1;
  void *expectedBuf = NULL;
  int allEqual = TRUE;

  if (expectedDatatype == MPI_CHAR){
    bufferMemorySize = expectedCount * sizeof(char);
    expectedBuf = (char*)malloc(bufferMemorySize);
  } else if (expectedDatatype == MPI_SHORT){
    bufferMemorySize = expectedCount * sizeof(short);
    expectedBuf = (short*)malloc(bufferMemorySize);
  } else if (expectedDatatype == MPI_INT) {
    bufferMemorySize = expectedCount * sizeof(int);
    expectedBuf = (int*)malloc(bufferMemorySize);
  } else if (expectedDatatype == MPI_LONG) {
    bufferMemorySize = expectedCount * sizeof(long);
    expectedBuf = (long*)malloc(bufferMemorySize);
  } else if (expectedDatatype == MPI_UNSIGNED_CHAR || expectedDatatype == MPI_BYTE){
    bufferMemorySize = expectedCount * sizeof(unsigned char);
    expectedBuf = (unsigned*)malloc(bufferMemorySize);
  } else if (expectedDatatype == MPI_UNSIGNED_SHORT){
    bufferMemorySize = expectedCount * sizeof(unsigned short);
    expectedBuf = (unsigned short*)malloc(bufferMemorySize);
  } else if (expectedDatatype == MPI_UNSIGNED){
    bufferMemorySize = expectedCount * sizeof(unsigned int);
    expectedBuf = (unsigned int*)malloc(bufferMemorySize);
  } else if (expectedDatatype == MPI_UNSIGNED_LONG){
    bufferMemorySize = expectedCount * sizeof(unsigned long);
    expectedBuf = (unsigned long*)malloc(bufferMemorySize);
  } 


  if(expectedBuf != NULL){
    xmlToMPIBuf(expectedBufNode, expectedBuf, expectedDatatype, expectedCount);
    if(memcmp(expectedBuf, actualBuf, bufferMemorySize) != 0){
      allEqual = FALSE;
    }

    free(expectedBuf);
  }
  else
  {
    int i = 0;
    if (expectedDatatype == MPI_FLOAT){
      bufferMemorySize = expectedCount * sizeof(float);
      float* expectedFloatBuf = (float*)malloc(bufferMemorySize);
      xmlToMPIBuf(expectedBufNode, expectedFloatBuf, expectedDatatype, expectedCount);
      
      float* actualFloatBuf = (float*)actualBuf;
      
      for(i = 0; i < expectedCount; i++){
        if(expectedFloatBuf[i] != actualFloatBuf[i]){
          allEqual = FALSE;
          break;
        }
      }

      free(expectedFloatBuf);
    }else if (expectedDatatype == MPI_DOUBLE){
      bufferMemorySize = expectedCount * sizeof(double);
      double* expectedDoubleBuf = (double*)malloc(bufferMemorySize);
      xmlToMPIBuf(expectedBufNode, expectedDoubleBuf, expectedDatatype, expectedCount);
      
      double* actualDoubleBuf = (double*)actualBuf;
      
      for(i = 0; i < expectedCount; i++){
        if(expectedDoubleBuf[i] != actualDoubleBuf[i]){
          allEqual = FALSE;
          break;
        }
      }

      free(expectedDoubleBuf);
    } else if (expectedDatatype == MPI_LONG_DOUBLE){
      bufferMemorySize = expectedCount * sizeof(long double);
      long double* expectedLongDoubleBuf = (long double*)malloc(bufferMemorySize);
      xmlToMPIBuf(expectedBufNode, expectedLongDoubleBuf, expectedDatatype, expectedCount);
      
      long double* actualLongDoubleBuf = (long double*)actualBuf;
      
      for(i = 0; i < expectedCount; i++){
        if(expectedLongDoubleBuf[i] != actualLongDoubleBuf[i]){
          allEqual = FALSE;
          break;
        }
      }

      free(expectedLongDoubleBuf);
    }
  }
  
  if(allEqual == FALSE)
  {
  	result->Items[replacementIndicatorIndex] = 'U';
  	
  	if(EncodeMpiBuff(actualBuf, expectedDatatype,expectedCount, 
  			sohReplace, partitionReplace, eotReplace, result) == TRUE)
  	{				
  		result->Items[replacementIndicatorIndex] = 'E';
		}
	}
}

int EncodeMpiBuff(void* values, MPI_Datatype datatype, int count, 
		 char* sohReplace, char* partitionReplace, char* eotReplace, charList* result)
{
  //flag indicating if any characters where encoded
  int encoded = FALSE;
  
	
	//create a temporary buffer which will be resused to encode the buffer
  charList* tempBuffer = (charList*)malloc(sizeof(charList));;
  InitializeCharList(tempBuffer);

  //Createa string versions of the values to encode
  char* soh = (char*)malloc(2*sizeof(char));
	soh[0] = SOH;
	soh[1] = '\0';

	char* partition = (char*)malloc(2*sizeof(char));
	partition[0] = PARTITION_CHR;
	partition[1] = '\0';	
	
	char* eot = (char*)malloc(2*sizeof(char));
	eot[0] = EOT;
	eot[1] = '\0';

	//Send back the code to decode on the client side	
	char encodeKeys[255];
	memset(encodeKeys, '\0', 255);
			
	int encodeKeyLen = sprintf(encodeKeys, "%c%s%c%s%c%s", PARTITION_CHR, sohReplace, 
			PARTITION_CHR, partitionReplace, PARTITION_CHR, eotReplace);
	AddChars(result, encodeKeys, encodeKeyLen);
	
  int i = 0;	
  for(i=0; i < count; i++)
  {
    char *value;

    int length = 0;
    if(datatype == MPI_CHAR){
      length = asprintf(&value, "%c", ((char*)values)[i]);    
    } else if(datatype == MPI_BYTE || datatype == MPI_UNSIGNED_CHAR){
      length = asprintf(&value, "%c", ((unsigned char*)values)[i]);    
    } else if(datatype == MPI_SHORT){
      length = asprintf(&value, "%hi", ((short*)values)[i]);    
    } else if(datatype == MPI_INT){
      length = asprintf(&value, "%d", ((int*)values)[i]);    
    } else if(datatype == MPI_LONG){
      length = asprintf(&value, "%ld", ((long int*)values)[i]);    
    } else if(datatype == MPI_FLOAT){  
      length = asprintf(&value, "%f", ((float*)values)[i]);
    } else if(datatype == MPI_DOUBLE){
      length = asprintf(&value, "%f", ((double*)values)[i]);  
    } else if(datatype == MPI_UNSIGNED_SHORT){
      length = asprintf(&value, "%hu", ((unsigned short*)values)[i]);   
    } else if(datatype == MPI_UNSIGNED){
      length = asprintf(&value, "%u", ((unsigned int*)values)[i]);   
    } else if(datatype == MPI_UNSIGNED_LONG){
      length = asprintf(&value, "%lu", ((unsigned long*)values)[i]);   
    } else if(datatype == MPI_LONG_DOUBLE){
      length = asprintf(&value, "%Lf", ((long double*)values)[i]);
    }

   	AddChars(tempBuffer, value, length);
    free(value);
    
		if(ReplaceChars(tempBuffer, soh, sohReplace) == TRUE)
			encoded = TRUE;
	
		if(ReplaceChars(tempBuffer, partition, partitionReplace) == TRUE)
			encoded = TRUE;

		if(ReplaceChars(tempBuffer, eot, eotReplace) == TRUE)
			encoded = TRUE;
  
  	AddChars(result, PARTITION_STR, 1);				
		AddChars(result, tempBuffer->Items, tempBuffer->ItemCount);
	
		ClearChars(tempBuffer);
  }
	
	free(soh);
	free(partition);
	free(eot);

	CleanUpCharList(tempBuffer);
	
	return encoded;
}

