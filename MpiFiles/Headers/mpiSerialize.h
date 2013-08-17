#ifndef MPI_SERIALIZE_H_INCLUDED
#define MPI_SERIALIZE_H_INCLUDED
#include <mpi.h>
#include "../XML/xmlDoc.h"
#include "collections.h"
#include "mpiUtils.h"

charList* preSerializeMPIInit(int nodeId, int lineNum, 
								int *commandId, XMLNode *expectedValues);

charList* preSerializeMPIRank(int nodeId, int lineNum, MPI_Comm comm, int *commandId);

charList* preSerializeMPISize(int nodeId, int lineNum, MPI_Comm comm, int *commandId);

charList* preSerializeMPISend(int nodeId, int lineNum, int count, MPI_Datatype datatype, 
							int dest, int tag, MPI_Comm comm, int *commandId,  XMLNode *expectedValues);

charList* preSerializeMPIISend(int nodeId, int lineNum, int count, 
			 MPI_Datatype datatype, int dest, int tag, MPI_Comm comm, 
			 MPI_Request *request, int *commandId, XMLNode *expectedValues);

charList* preSerializeMPIRecv(int nodeId, int lineNum, int count,
              MPI_Datatype datatype, int src, int tag, MPI_Comm comm, 
              int *commandId, XMLNode *expectedValues);
              
charList* preSerializeMPIIRecv(int nodeId, int lineNum, int count, 
			MPI_Datatype datatype, int src, int tag, MPI_Comm comm, MPI_Request 
			*request, int *commandId, XMLNode *expectedValues);
			
charList* preSerializeMPIWait(int nodeId, int lineNum, MPI_Request *request, int *commandId);

charList* preSerializeMPIBarrier(int nodeId, int lineNum, MPI_Comm comm, int *commandId);

charList* preSerializeMPIProbe(int nodeId, int lineNum,  int src, int tag, 
							MPI_Comm comm, int *commandId, XMLNode *expectedValues);

charList* preSerializeMPIIProbe(int nodeId, int lineNum, int src, int tag, MPI_Comm comm, int *commandId, XMLNode *expectedValues);

charList* preSerializeMPIFinalize(int nodeId, int lineNum, int *commandId);

			           
charList* postSerializeMPIInit(int nodeId, int commandId, 
									int returnValue, XMLNode *expectedValues);

charList* postSerializeMPIRank(int nodeId, int commandId, int rank, int returnValue, 
				XMLNode *expectedValues);

charList* postSerializeMPISize(int nodeId, int commandId, int size, int returnValue, 
				XMLNode *expectedValues);
				

charList* postSerializeMPISend(int nodeId, MPI_Datatype datatype, int count, void *buf,
			int commandId, int returnValue, XMLNode *expectedValues, 
			char* sohReplace, char* partitionReplace, char* eotReplace);
			
charList* postSerializeMPIISend(int nodeId, MPI_Datatype datatype, 
			int count, void *buf, int commandId, int returnValue, XMLNode *expectedValues, 
			char* sohReplace, char* partitionReplace, char* eotReplace);

charList* postSerializeMPIRecv(int nodeId, MPI_Datatype datatype, 
										MPI_Status *status, int count, void *buf,
										int commandId, int returnValue, XMLNode *expectedValues, 
										char* sohReplace, char* partitionReplace, char* eotReplace);

charList* postSerializeMPIIRecv(int nodeId, int commandId, int returnValue, XMLNode *expectedValues);

charList* postSerializeMPIWait(int nodeId, MPI_Status *status, AsyncBuf* asyncBuf,
			int commandId, int returnValue, XMLNode *expectedValues, 
			char* sohReplace, char* partitionReplace, char* eotReplace);

charList* postSerializeMPIBarrier(int nodeId, int commandId, int returnValue, XMLNode *expectedValues);

charList* postSerializeMPIProbe(int nodeId, int commandId, MPI_Status *status, int returnValue, XMLNode *expectedValues);

charList* postSerializeMPIIProbe(int nodeId, int commandId, int *flag, MPI_Status *status, int returnValue);

charList* postSerializeMPIFinalize(int nodeId, int commandId, int returnValue, XMLNode *expectedValues);


void ParseInputBuffer(charList* buffer, queue* result, int trimControlChars);

//Serialize a console message
charList* serializeConsole(char* message, int messageLength, int sourceId, 
									char* sohReplace, char* partitionReplace, char* eotReplace);

#endif
