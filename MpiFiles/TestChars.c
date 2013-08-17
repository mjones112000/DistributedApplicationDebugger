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
  
  printf("No encoding Needeed\n");
  printf("Part1|Part2\n");
  printf("%c%c%c Did it work???\n", '\x01', '|', '\x04');
  printf("%c%c%c How 'bout this?\n", '\x04', '|', '\x01');
  
  printf("%c%c%c What about with, the dreaded comma?\n", '\x01', '|', '\x04');

  MPI_Finalize();
  return 0;
}

