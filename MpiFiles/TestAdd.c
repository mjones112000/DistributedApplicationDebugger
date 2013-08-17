#define _GNU_SOURCE

#include "Headers/mpi.h"

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#define TAG 0

int main(int argc, char *argv[]) {
  int rank, size;

  MPI_Init(&argc, &argv);
  MPI_Comm_rank(MPI_COMM_WORLD, &rank);
  MPI_Comm_size(MPI_COMM_WORLD, &size);

  int total = 0;
  int myId = rank;
  int i = 0;
  int numbersToAdd  = atoi(argv[1]);
  int j = 0;

  if(myId == 0)
  {
    printf("Hello I am the master process, numbers to add %d\n", numbersToAdd);
    int* receiveBuffer = (int*)malloc(sizeof(int));
    MPI_Status stat;
    
    for(i=1;i<size;i++)
    {
      for(j = 1; j <= numbersToAdd; j++)
      {
        MPI_Recv(receiveBuffer, 1, MPI_INT, i, TAG, MPI_COMM_WORLD, &stat);
      }
    }
        
    free(receiveBuffer);
  }
  else
  {
    printf("%d: Hello I am the process%d\n", myId, myId );


    int* sendBuffer = (int*)malloc(sizeof(int));

    for(i = 1; i <= numbersToAdd; i++)
    {
      total = total + i;
      printf("%d: My Total is %d\n", myId, total);
      sendBuffer[0] = total;
      MPI_Send(sendBuffer, 1, MPI_INT, 0, TAG, MPI_COMM_WORLD);
    }
		
    free(sendBuffer);
  }

  printf("%d: I'm finalizing\n", myId );
  MPI_Finalize();
  return 0;
}

