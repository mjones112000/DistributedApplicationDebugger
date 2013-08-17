#define _GNU_SOURCE
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "Headers/mpiUtils.h"

void xmlToMPIBuf(XMLNode* xmlBuf, void* resultBuf, MPI_Datatype datatype, int count)
{
  int i = 0;
  
  for(i=0; i < count; i++)
  {
    if(datatype == MPI_CHAR){
      ((char*)resultBuf)[i] = xmlGetText(xmlBuf->ChildNodes[i])[0];
    } else if (datatype == MPI_BYTE || datatype == MPI_UNSIGNED_CHAR) {
      ((unsigned char*)resultBuf)[i] = (unsigned char)(xmlGetText(xmlBuf->ChildNodes[i])[0]);
    } else if (datatype == MPI_SHORT){
      ((short*)resultBuf)[i] =  (short)atoi(xmlGetText(xmlBuf->ChildNodes[i]));
    } else if (datatype == MPI_INT) {
      ((int*)resultBuf)[i] = atoi(xmlGetText(xmlBuf->ChildNodes[i]));
    } else if (datatype == MPI_LONG){
      ((long*)resultBuf)[i] =  strtoul(xmlGetText(xmlBuf->ChildNodes[i]),NULL,10);
    } else if (datatype == MPI_FLOAT){
//      ((float*)resultBuf)[i] =  (float)strtof(xmlGetText(xmlBuf->ChildNodes[i]),NULL);
      ((float*)resultBuf)[i] = 0L;
      readableStrToFloatingStr(xmlGetText(xmlBuf->ChildNodes[i]));     
      memcpy(&((float*)resultBuf)[i], xmlGetText(xmlBuf->ChildNodes[i]), sizeof(float));
    } else if (datatype == MPI_DOUBLE){
//      ((double*)resultBuf)[i] =  (double)strtod(xmlGetText(xmlBuf->ChildNodes[i]),NULL);
      ((double*)resultBuf)[i] = 0L;
      readableStrToFloatingStr(xmlGetText(xmlBuf->ChildNodes[i]));     
      memcpy(&((double*)resultBuf)[i], xmlGetText(xmlBuf->ChildNodes[i]), sizeof(double));
    } else if (datatype == MPI_UNSIGNED_SHORT){
      ((unsigned short*)resultBuf)[i] =  (unsigned short)strtoul(xmlGetText(xmlBuf->ChildNodes[i]), NULL, 10);
    } else if (datatype == MPI_UNSIGNED){
      ((unsigned int*)resultBuf)[i] =  (unsigned int)strtoul(xmlGetText(xmlBuf->ChildNodes[i]), NULL, 10);
    } else if (datatype == MPI_UNSIGNED_LONG){
      ((unsigned long*)resultBuf)[i] =  (unsigned long)strtoul(xmlGetText(xmlBuf->ChildNodes[i]), NULL, 10);
    } else if (datatype == MPI_LONG_DOUBLE){
      //((long double*)resultBuf)[i] =  (long double)strtold(xmlGetText(xmlBuf->ChildNodes[i]), NULL);

      ((long double*)resultBuf)[i] = 0L;
      readableStrToFloatingStr(xmlGetText(xmlBuf->ChildNodes[i]));     
      memcpy(&((long double*)resultBuf)[i], xmlGetText(xmlBuf->ChildNodes[i]), sizeof(long double));
    }
  }
  
}


int isFloatingPointType(MPI_Datatype datatype)
{
  if(datatype == MPI_FLOAT || datatype == MPI_DOUBLE || datatype == MPI_LONG_DOUBLE){
    return TRUE;
  }
  else{
    return FALSE;
  }
}

void floatingStrToReadableStr(char* rawFloatingStr,int size)
{
  int i = 0;
  for(i = 0; i < size; i++)
  {
    if(rawFloatingStr[i] == '\0')
      rawFloatingStr[i] = '0';
  }
  
  rawFloatingStr[size + 1] = '\0';
}


void readableStrToFloatingStr(char* readableFloatingStr)
{
  int i = 0;
  while(readableFloatingStr[i] != '\0')
  {
    if(readableFloatingStr[i] == '0')
      readableFloatingStr[i] = '\0';

    i++;
  }
}


void xmlToMPIStatus(XMLNode* xmlStatus, MPI_Status* resultStatus)
{
  resultStatus->MPI_SOURCE = atoi(xmlGetText(xmlGetChildNode(xmlStatus, MPI_SOURCE_STR)));
  resultStatus->MPI_TAG = atoi(xmlGetText(xmlGetChildNode(xmlStatus, MPI_TAG_STR)));
  resultStatus->MPI_ERROR = atoi(xmlGetText(xmlGetChildNode(xmlStatus, MPI_ERROR_STR)));
}

char* mpiTypeToStr(MPI_Datatype datatype)
{ 
  if (datatype == MPI_CHAR)
    return MPI_CHAR_STR;
  else if (datatype == MPI_BYTE) 
    return MPI_BYTE_STR ;
  else if (datatype == MPI_SHORT)
    return MPI_SHORT_STR;
  else if (datatype == MPI_INT) 
    return MPI_INT_STR ;
  else if (datatype == MPI_LONG) 
    return MPI_LONG_STR ;
  else if (datatype == MPI_FLOAT) 
    return MPI_FLOAT_STR;
  else if (datatype == MPI_DOUBLE) 
    return MPI_DOUBLE_STR;
  else if (datatype == MPI_UNSIGNED_CHAR) 
    return MPI_UNSIGNED_CHAR_STR ;
  else if (datatype == MPI_UNSIGNED_SHORT) 
    return MPI_UNSIGNED_SHORT_STR ;
  else if (datatype == MPI_UNSIGNED) 
    return MPI_UNSIGNED_STR;
  else if (datatype == MPI_UNSIGNED_LONG) 
    return MPI_UNSIGNED_LONG_STR ;
  else if (datatype == MPI_LONG_DOUBLE) 
    return MPI_LONG_DOUBLE_STR ;
  else
    return "";
}

MPI_Datatype strToMPIType(char * mpiStr)
{ 
  if (strcmp(mpiStr, MPI_CHAR_STR) == 0)
    return MPI_CHAR;
  else if (strcmp(mpiStr, MPI_BYTE_STR) == 0)
    return MPI_BYTE; 
  else if (strcmp(mpiStr, MPI_SHORT_STR) == 0)
    return MPI_SHORT;
  else if (strcmp(mpiStr, MPI_INT_STR) == 0) 
    return MPI_INT;
  else if (strcmp(mpiStr, MPI_LONG_STR) == 0) 
    return MPI_LONG;
  else if (strcmp(mpiStr, MPI_FLOAT_STR) == 0) 
    return MPI_FLOAT;
  else if (strcmp(mpiStr, MPI_DOUBLE_STR) == 0) 
    return MPI_DOUBLE;
  else if (strcmp(mpiStr, MPI_UNSIGNED_CHAR_STR) == 0) 
    return MPI_UNSIGNED_CHAR;
  else if (strcmp(mpiStr, MPI_UNSIGNED_SHORT_STR) == 0) 
    return MPI_UNSIGNED_SHORT;
  else if (strcmp(mpiStr, MPI_UNSIGNED_STR) == 0) 
    return MPI_UNSIGNED;
  else if (strcmp(mpiStr, MPI_UNSIGNED_LONG_STR) == 0) 
    return MPI_UNSIGNED_LONG;
  else if (strcmp(mpiStr, MPI_LONG_DOUBLE_STR) == 0) 
    return MPI_LONG_DOUBLE;
  else
    return MPI_CHAR;
}

char* mpiErrToStr(int error)
{
  if (error == MPI_SUCCESS)
    return MPI_SUCCESS_STR;
  else if (error == MPI_ERR_BUFFER) 
    return MPI_ERR_BUFFER_STR;
  else if (error == MPI_ERR_COUNT) 
    return MPI_ERR_COUNT_STR;
  else if (error == MPI_ERR_TYPE) 
    return MPI_ERR_TYPE_STR;
  else if (error == MPI_ERR_TAG) 
    return MPI_ERR_TAG_STR;
  else if (error == MPI_ERR_COMM) 
    return MPI_ERR_COMM_STR;
  else if (error == MPI_ERR_RANK) 
    return MPI_ERR_RANK_STR;
  else if (error == MPI_ERR_ROOT) 
    return MPI_ERR_ROOT_STR;
  else if (error == MPI_ERR_GROUP) 
    return MPI_ERR_GROUP_STR;
  else if (error == MPI_ERR_OP) 
    return MPI_ERR_OP_STR;
  else if (error == MPI_ERR_TOPOLOGY) 
    return MPI_ERR_TOPOLOGY_STR;
  else if (error == MPI_ERR_DIMS) 
    return MPI_ERR_DIMS_STR;
  else if (error == MPI_ERR_ARG) 
    return MPI_ERR_ARG_STR;
  else if (error == MPI_ERR_UNKNOWN) 
    return MPI_ERR_UNKNOWN_STR;
  else if (error == MPI_ERR_TRUNCATE) 
    return MPI_ERR_TRUNCATE_STR;
  else if (error == MPI_ERR_OTHER) 
    return MPI_ERR_OTHER_STR;
  else if (error == MPI_ERR_INTERN) 
    return MPI_ERR_INTERN_STR;
  else if (error == MPI_ERR_IN_STATUS) 
    return MPI_ERR_IN_STATUS_STR;
  else if (error == MPI_ERR_PENDING) 
    return MPI_ERR_PENDING_STR;
  else if (error == MPI_ERR_REQUEST) 
    return MPI_ERR_REQUEST_STR;
  else
  {
    char *value;
    int length = 0;
    length = asprintf(&value, "%d", error);

    return value;
  }
}

int strToMPIError(char * mpiStr)
{
  if (strcmp(mpiStr, MPI_SUCCESS_STR) == 0)
    return MPI_SUCCESS;
  else if (strcmp(mpiStr, MPI_ERR_BUFFER_STR) == 0)
    return MPI_ERR_BUFFER;
  else if (strcmp(mpiStr, MPI_ERR_COUNT_STR) == 0)
    return MPI_ERR_COUNT; 
  else if (strcmp(mpiStr, MPI_ERR_TYPE_STR) == 0)
    return MPI_ERR_TYPE;
  else if (strcmp(mpiStr, MPI_ERR_TAG_STR) == 0)
    return MPI_ERR_TAG;
  else if (strcmp(mpiStr, MPI_ERR_COMM_STR) == 0)
    return MPI_ERR_COMM;
  else if (strcmp(mpiStr, MPI_ERR_RANK_STR) == 0)
    return MPI_ERR_RANK;
  else if (strcmp(mpiStr, MPI_ERR_ROOT_STR) == 0)
    return MPI_ERR_ROOT;
  else if (strcmp(mpiStr, MPI_ERR_GROUP_STR) == 0)
    return MPI_ERR_GROUP;
  else if (strcmp(mpiStr, MPI_ERR_OP_STR) == 0)
    return MPI_ERR_OP;
  else if (strcmp(mpiStr, MPI_ERR_TOPOLOGY_STR) == 0)
    return MPI_ERR_TOPOLOGY;
  else if (strcmp(mpiStr, MPI_ERR_DIMS_STR) == 0)
    return MPI_ERR_DIMS;
  else if (strcmp(mpiStr, MPI_ERR_ARG_STR) == 0)
    return MPI_ERR_ARG;
  else if (strcmp(mpiStr, MPI_ERR_UNKNOWN_STR) == 0)
    return MPI_ERR_UNKNOWN;
  else if (strcmp(mpiStr, MPI_ERR_TRUNCATE_STR) == 0)
    return MPI_ERR_TRUNCATE;
  else if (strcmp(mpiStr, MPI_ERR_OTHER_STR) == 0)
    return MPI_ERR_OTHER;
  else if (strcmp(mpiStr, MPI_ERR_INTERN_STR) == 0)
    return MPI_ERR_INTERN;
  else if (strcmp(mpiStr, MPI_ERR_IN_STATUS_STR) == 0)
    return MPI_ERR_IN_STATUS;
  else if (strcmp(mpiStr, MPI_ERR_PENDING_STR) == 0)
    return MPI_ERR_PENDING;
  else if (strcmp(mpiStr, MPI_ERR_REQUEST_STR) == 0)
    return MPI_ERR_REQUEST; 
  else
    return atoi(mpiStr);
}


char* mpiCommToStr(int comm)
{
	if (comm == MPI_COMM_WORLD)
		return MPI_COMM_WORLD_STR;
	else if (comm == MPI_COMM_SELF) 
		return MPI_COMM_SELF_STR;
	else
  {
    char *value;
    int length = 0;
    length = asprintf(&value, "%d", comm);

    return value;
  }
}


int strToMPIComm(char * mpiStr)
{
  if (strcmp(mpiStr, MPI_COMM_WORLD_STR) == 0)
    return MPI_COMM_WORLD;
  else if (strcmp(mpiStr, MPI_COMM_SELF_STR) == 0)
    return MPI_COMM_SELF;
	else
    return atoi(mpiStr);
}
