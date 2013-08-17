#ifndef _MPIDEBUG_
#define _MPIDEBUG_
int _MPI_Init(char pname[100], int line, int *argc, char ***argv);
int _MPI_Finalize(char pname[100], int line);
int _MPI_Comm_rank(char pname[100], int line, MPI_Comm comm, int *rank);
int _MPI_Comm_size(char pname[100], int line, MPI_Comm comm, int *size);
int _MPI_Send(char pname[100], int line, void *buf, int count, 
	      MPI_Datatype datatype, int dest, int tag, MPI_Comm comm) ;
int _MPI_Recv(char pname[100], int line, void *buf, int count, 
	      MPI_Datatype datatype,  int src, int tag, MPI_Comm comm, 
	      MPI_Status *status);

int _MPI_ISend(char pname[100], int line, void *buf, int count, 
              MPI_Datatype datatype, int dest, int tag, MPI_Comm comm, MPI_Request *request);

int _MPI_IRecv(char pname[100], int line, void *buf, int count, 
	      MPI_Datatype datatype, int src, int tag, MPI_Comm comm, MPI_Request *request);

int _MPI_Wait(char pname[100], int line, MPI_Request *request, MPI_Status *status);

int _MPI_Barrier(char pname[100], int line, MPI_Comm comm);

int _MPI_Probe(char pname[100], int line, int src, int tag, MPI_Comm comm, MPI_Status *status);

int _MPI_Probe(char pname[100], int line, int src, int tag, MPI_Comm comm, MPI_Status *status);

int _MPI_IProbe(char pname[100], int line, int src, int tag, MPI_Comm comm, int *flag, MPI_Status *status);

void* StdOutRedirectThread(void* value);

#endif
