#ifndef MPI_XML_H_INCLUDED
#define MPI_XML_H_INCLUDED
#include <mpi.h>
#include "../XML/xmlDoc.h"

//Standard Init, Rank and Size commands
XMLNode* xmlMPIInit(int rank, int returnValue, int commandId);
XMLNode* xmlMPIRank(int rank, MPI_Comm comm, int rankValue, int returnValue, int commandId);
XMLNode* xmlMPISize(int rank, MPI_Comm comm, int sizeValue, int returnValue, int commandId);
XMLNode* xmlMPIFinalize(int rank, int returnValue, int commandId);

//Blocking Send
XMLNode* xmlMPISend(int rank, void *buf, int count, 
         MPI_Datatype datatype, int dest, int tag, MPI_Comm comm, int returnValue, int commandId);


//Blocking Recv
XMLNode* xmlMPIRecv(int rank,void *buf, int count, 
	      MPI_Datatype datatype, int src, int tag, MPI_Comm comm, 
	      MPI_Status *status, int returnValue, int commandId);


//Nonblocking Send
XMLNode* xmlMPIISend(int rank, void *buf, int count, 
  MPI_Datatype datatype, int dest, int tag, MPI_Comm comm, MPI_Request *request, int returnValue, int commandId);

//Nonblocking Recv
XMLNode* xmlMPIIRecv(int rank, void *buf,  int count, 
	      MPI_Datatype datatype, int src, int tag, MPI_Comm comm, MPI_Request *request, int returnValue, int commandId);

//Wait Command
XMLNode* xmlMPIWait(int rank, MPI_Request *request, MPI_Status *status, int returnValue, int commandId);

//Barrier Command
XMLNode* xmlMPIBarrier(int rank,  MPI_Comm comm, int returnValue, int commandId);

//Probe Command
XMLNode* xmlMPIProbe(int rank,  int src, int tag, MPI_Comm comm, MPI_Status *status, int returnValue, int commandId);

//Nonblocking Probe Command
XMLNode* xmlMPIIProbe(int rank,  int src, int tag, MPI_Comm comm, int *flag, MPI_Status *status, int returnValue, int commandId);

#endif
