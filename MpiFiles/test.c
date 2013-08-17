
#define _GNU_SOURCE

#include "Headers/mpi.h"
#include "Headers/testCases.h"

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

int main(int argc, char *argv[]) {
  int rank, size;

  MPI_Init(&argc, &argv);

  MPI_Comm_rank(MPI_COMM_WORLD, &rank);
  MPI_Comm_size(MPI_COMM_WORLD, &size);

  int dest, src;
  dest = (rank+1)%size;
  src  = (rank-1+size)%size;

  if(rank == 0 || size == 1)
    printf("\n************BLOCKING TESTS**************\n");


  RunDataTypeTests(rank, size, dest, src, TRUE);


  if(rank == 0 || size == 1)
    printf("\n\n\n************NONBLOCKING TESTS**************\n");


  RunDataTypeTests(rank, size, dest, src, FALSE);


  if(rank == 0 || size == 1)
    printf("\n");

  MPI_Finalize();
  return 0;
}


