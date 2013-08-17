#ifndef XML_Utils_H_INCLUDED
#define XML_Utils_H_INCLUDED
#include <mpi.h>
#include "../XML/xml.h"

#define MPI_CHAR_STR "MPI_CHAR"
#define MPI_BYTE_STR "MPI_BYTE"
#define MPI_SHORT_STR "MPI_SHORT"
#define MPI_INT_STR "MPI_INT"
#define MPI_LONG_STR "MPI_LONG"
#define MPI_FLOAT_STR "MPI_FLOAT"
#define MPI_DOUBLE_STR "MPI_DOUBLE"
#define MPI_UNSIGNED_CHAR_STR "MPI_UNSIGNED_CHAR"
#define MPI_UNSIGNED_SHORT_STR "MPI_UNSIGNED_SHORT"
#define MPI_UNSIGNED_STR "MPI_UNSIGNED"
#define MPI_UNSIGNED_LONG_STR "MPI_UNSIGNED_LONG"
#define MPI_LONG_DOUBLE_STR "MPI_LONG_DOUBLE"

#define MPI_SOURCE_STR "MPI_SOURCE"
#define MPI_TAG_STR "MPI_TAG"
#define MPI_ERROR_STR "MPI_ERROR"

#define MPI_SUCCESS_STR "MPI_SUCCESS" 
#define MPI_ERR_BUFFER_STR "MPI_ERR_BUFFER" 
#define MPI_ERR_COUNT_STR "MPI_ERR_COUNT" 
#define MPI_ERR_TYPE_STR "MPI_ERR_TYPE" 
#define MPI_ERR_TAG_STR "MPI_ERR_TAG" 
#define MPI_ERR_COMM_STR "MPI_ERR_COMM" 
#define MPI_ERR_RANK_STR "MPI_ERR_RANK" 
#define MPI_ERR_ROOT_STR "MPI_ERR_ROOT" 
#define MPI_ERR_GROUP_STR "MPI_ERR_GROUP" 
#define MPI_ERR_OP_STR "MPI_ERR_OP" 
#define MPI_ERR_TOPOLOGY_STR "MPI_ERR_TOPOLOGY" 
#define MPI_ERR_DIMS_STR "MPI_ERR_DIMS" 
#define MPI_ERR_ARG_STR "MPI_ERR_ARG" 
#define MPI_ERR_UNKNOWN_STR "MPI_ERR_UNKOWN" 
#define MPI_ERR_TRUNCATE_STR "MPI_ERR_TRUNCATE" 
#define MPI_ERR_OTHER_STR "MPI_ERR_OTHER" 
#define MPI_ERR_INTERN_STR "MPI_ERR_INTERN" 
#define MPI_ERR_IN_STATUS_STR "MPI_ERR_IN_STATUS" 
#define MPI_ERR_PENDING_STR "MPI_ERR_PENDING" 
#define MPI_ERR_REQUEST_STR "MPI_ERR_REQUEST"

#define MPI_INIT_STR "MPI_INIT"
#define MPI_RANK_STR "MPI_RANK"
#define MPI_SIZE_STR "MPI_SIZE"
#define MPI_RECV_STR "MPI_RECV"
#define MPI_SEND_STR "MPI_SEND"
#define MPI_IRECV_STR "MPI_IRECV"
#define MPI_ISEND_STR "MPI_ISEND"
#define MPI_WAIT_STR "MPI_WAIT"
#define MPI_BARRIER_STR "MPI_BARRIER"
#define MPI_PROBE_STR "MPI_PROBE"
#define MPI_IPROBE_STR "MPI_IPROBE"
#define MPI_FINALIZE_STR "MPI_FINALIZE"

#define MPI_COMM_WORLD_STR "MPI_COMM_WORLD"
#define MPI_COMM_SELF_STR "MPI_COMM_SELF"

#define MPI_COMMAND_ELEMENT "MPI COMMAND NAME"
#define PARAMETERS_ELEMENT "parameters"
#define BUF_ELEMENT "buf"
#define BUF_BYTES_ELEMENT "bufbytes"
#define BYTE_CODE_ELEMENT "bytecode"
#define COUNT_ELEMENT "count"
#define DATA_TYPE_ELEMENT "datatype"
#define DEST_ELEMENT "dest"
#define SRC_ELEMENT "src"
#define COMM_ELEMENT "comm"
#define STATUS_ELEMENT "status"
#define TAG_ELEMENT "tag"
#define FLAG_ELEMENT "flag"

#define VALUE_ELEMENT "value"
#define RETURN_VALUE_ELEMENT "returnvalue"
#define REQUEST_ELEMENT "request"

#define RANK_ATTRIBUTE "rank"
#define COMMAND_ID_ATTRIBUTE "commandId"
#define DATETIME_ATTRIBUTE "dateTime"
#define BUFFER_DELIMITER 0x02


typedef struct AsyncBuf_item{
  int id;
  int src;
  int dest;
  MPI_Datatype datatype;
  int count;
  MPI_Request* request;
  void* buf;
} AsyncBuf;

void xmlToMPIBuf(XMLNode* xmlBuf, void* resultBuf, MPI_Datatype datatype, int count);
void xmlToMPIStatus(XMLNode* xmlStatus, MPI_Status* resultStatus);

char* mpiTypeToStr(MPI_Datatype datatype);
MPI_Datatype strToMPIType(char * mpiStr);

char* mpiErrToStr(int error);
int strToMPIError(char * mpiStr);

int strToMPIComm(char * mpiStr);
char* mpiCommToStr(int comm);

int isFloatingPointType(MPI_Datatype datatype);

void floatingStrToReadableStr(char* rawFloatingStr,int size);
void readableStrToFloatingStr(char* readableFloatingStr);

#endif
