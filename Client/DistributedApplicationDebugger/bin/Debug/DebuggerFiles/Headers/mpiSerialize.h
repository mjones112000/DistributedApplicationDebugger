#ifndef MPI_SERIALIZE_H_INCLUDED
#define MPI_SERIALIZE_H_INCLUDED
#include <mpi.h>
#include "collections.h"

charList* preSerializeMPIInit(int nodeId, int lineNum, int *commandId);

charList* preSerializeMPIRank(int nodeId, int lineNum, MPI_Comm comm, int *commandId);

charList* preSerializeMPISize(int nodeId, int lineNum, MPI_Comm comm, int *commandId);

charList* preSerializeMPISend(int nodeId, int lineNum, int count,
	MPI_Datatype datatype, int dest, int tag, MPI_Comm comm, int *commandId);

charList* preSerializeMPIISend(int nodeId, int lineNum, int count, 
			 MPI_Datatype datatype, int dest, int tag, MPI_Comm comm, MPI_Request *request, int *commandId);

charList* preSerializeMPIRecv(int nodeId, int lineNum, int count, MPI_Datatype datatype, int src, int tag, MPI_Comm comm, int *commandId);
              
charList* preSerializeMPIIRecv(int nodeId, int lineNum, int count, 
			MPI_Datatype datatype, int src, int tag, MPI_Comm comm, MPI_Request *request, int *commandId);
			
charList* preSerializeMPIWait(int nodeId, int lineNum, MPI_Request *request, int *commandId);

charList* preSerializeMPIBarrier(int nodeId, int lineNum, MPI_Comm comm, int *commandId);

charList* preSerializeMPIProbe(int nodeId, int lineNum,  int src, int tag, MPI_Comm comm, int *commandId);

charList* preSerializeMPIIProbe(int nodeId, int lineNum, int src, int tag, MPI_Comm comm, int *commandId);

charList* preSerializeMPIFinalize(int nodeId, int lineNum, int *commandId);
			              
charList* postSerializeMPIInit(int nodeId, int commandId, int returnValue);

charList* postSerializeMPIRank(int nodeId, int commandId, int rank, int returnValue);

charList* postSerializeMPISize(int nodeId, int commandId, int size, int returnValue);

charList* postSerializeMPISend(int nodeId, int commandId, int returnValue);

charList* postSerializeMPIISend(int nodeId, int commandId, int returnValue);

charList* postSerializeMPIRecv(int nodeId, int commandId, MPI_Status *status, int returnValue);

charList* postSerializeMPIIRecv(int nodeId, int commandId, int returnValue);

charList* postSerializeMPIWait(int nodeId, int commandId, MPI_Status *status, int returnValue);

charList* postSerializeMPIBarrier(int nodeId, int commandId, int returnValue);

charList* postSerializeMPIProbe(int nodeId, int commandId, MPI_Status *status, int returnValue);

charList* postSerializeMPIIProbe(int nodeId, int commandId, int *flag, MPI_Status *status, int returnValue);

charList* postSerializeMPIFinalize(int nodeId, int commandId, int returnValue);


void ParseInputBuffer(charList* buffer, queue* result, int trimControlChars);

//Serialize a console message
charList* serializeConsole(char* message, int messageLength, int sourceId);


#endif
