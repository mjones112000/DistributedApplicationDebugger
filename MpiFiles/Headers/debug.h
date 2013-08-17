
#ifndef _DEBUG_
#define _DEBUG_
#define MPI_Init(A,B)             _MPI_Init(__FILE__,__LINE__,A,B)
#define MPI_Finalize()            _MPI_Finalize(__FILE__,__LINE__)
#define MPI_Comm_rank(A,B)        _MPI_Comm_rank(__FILE__,__LINE__,A,B)
#define MPI_Comm_size(A,B)        _MPI_Comm_size(__FILE__,__LINE__,A,B)
#define MPI_Send(A,B,C,D,E,F)     _MPI_Send(__FILE__,__LINE__,A,B,C,D,E,F)
#define MPI_Recv(A,B,C,D,E,F,G)   _MPI_Recv(__FILE__,__LINE__,A,B,C,D,E,F,G)
#define MPI_Isend(A,B,C,D,E,F,G)  _MPI_ISend(__FILE__,__LINE__,A,B,C,D,E,F,G)
#define MPI_Irecv(A,B,C,D,E,F,G)  _MPI_IRecv(__FILE__,__LINE__,A,B,C,D,E,F,G)
#define MPI_Wait(A,B)             _MPI_Wait(__FILE__,__LINE__,A,B)
#define MPI_Barrier(A)            _MPI_Barrier(__FILE__,__LINE__,A)
#define MPI_Probe(A,B,C,D)        _MPI_Probe(__FILE__,__LINE__,A,B,C,D)
#define MPI_Iprobe(A,B,C,D,E)     _MPI_IProbe(__FILE__,__LINE__,A,B,C,D,E)
#endif
