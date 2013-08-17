#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string.h>
#include <sys/time.h>
#include "Headers/mpi.h"

#define MASTER (size - 1)

void makeMatrix(int, int**, int*, double*);
void divide(int, int, int *, int *);

int main(int argc, char *argv[])
{
	int size, rank;
	int i, j, n;
	int **a, *b, *div, *procs;
	double *x;
	n = atoi(argv[1]);

	MPI_Init(&argc, &argv);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Status status;

    a = (int **)malloc(sizeof(int *) * n);
	b = (int *)malloc(sizeof(int) * n);
	x = (double *)malloc(sizeof(double) * n);
	for ( i=0; i<n; i++ ) a[i] = (int *)malloc(sizeof(int) * (i + 1));
	
	makeMatrix(n, a, b, x);
	div = (int *)malloc(sizeof(int) * n);
	procs = (int *)malloc(sizeof(int) * MASTER);
	divide(n, size, div, procs);

	if ( rank == MASTER )
	{
		int p = 0;
		for ( i=0; i<MASTER; i++ ) 
		{
			MPI_Recv(&x[p], procs[i], MPI_DOUBLE, i, 0, MPI_COMM_WORLD, &status);
			p = p + procs[i];
		}
		
		printf("b: [ ");
		for ( i=0; i<n; i++ ) printf("%d ", b[i]);
		printf("]\n");
		printf("x: [ ");
		for ( i=0; i<n; i++ ) printf("%lf ", x[i]);
		printf("]\n");
		
	} else {
		if ( rank )
		{
			int p;
			MPI_Status status1;
			for ( i=0, p=0; i<rank; i++ ) p = p + procs[i];
			for ( i=0; i<procs[rank]; i++, p++ )
			{
				int sum = 0;
				for ( j=0; j<p; j++ )
				{
					if ( i == 0 ) 
					{
						//MPI_Probe(rank-1, j, MPI_COMM_WORLD, &status1);
						MPI_Recv(&x[j], 1, MPI_DOUBLE, rank-1, MPI_ANY_TAG, MPI_COMM_WORLD, &status);
						//while ( status.MPI_TAG != status1.MPI_TAG ) MPI_Recv(&x[j], 1, MPI_DOUBLE, rank-1, MPI_ANY_TAG, MPI_COMM_WORLD, &status);
					}
					if ( (rank != (MASTER - 1)) && (procs[rank+1] > 0) ) MPI_Send(&x[j], 1, MPI_DOUBLE, rank+1, j, MPI_COMM_WORLD);

					sum = sum + (a[p][j] * x[j]);
				}
				x[p] = (double)(b[p] - sum) / a[p][p];
				if ( (rank != (MASTER - 1)) && (procs[rank+1] > 0) ) MPI_Send(&x[p], 1, MPI_DOUBLE, rank+1, p, MPI_COMM_WORLD);
			}
			for ( i=0, p=0; i<rank; i++ ) p = p + procs[i];
			MPI_Send(&x[p], procs[rank], MPI_DOUBLE, MASTER, 0, MPI_COMM_WORLD);
		} else {
			int p = 0;
			for ( i=0; i<rank; i++ ) p = p + procs[i];
			x[0] = (double)b[0]/a[0][0];
			MPI_Send(&x[0], 1, MPI_DOUBLE, rank+1, 0, MPI_COMM_WORLD);
			
			for ( i=1; i<procs[rank]; i++ )
			{
				int sum = 0;
				for ( j=0; j<(i + 1); j++ ) sum = sum + (a[i][j] * x[j]);
				x[i] = (b[i] - sum) / a[i][i];
				MPI_Send(&x[i], 1, MPI_DOUBLE, rank+1, i, MPI_COMM_WORLD);
			}
			MPI_Send(&x[rank], procs[rank], MPI_DOUBLE, MASTER, 0, MPI_COMM_WORLD);
		}
	}
	
	MPI_Finalize();
    
	return 0;
}

void makeMatrix(int n, int **a, int *b, double *x) 
{
	int i, j;
	for ( i=0; i<n; i++ ) for ( j=0; j<i+1; j++ ) a[i][j] = 1;
	for ( i=0; i<n ; i++ ) b[i] = (int)rand() % 10;
	for ( i=0; i<n ; i++ ) x[i] = 0.;
} 

void divide(int n, int size, int *div, int *procs)
{
	int nums = (n * (n + 1)) / (2 * MASTER);
	int sum = 0, i, j;
	for ( i=0; i<MASTER; i++ ) procs[i] = 0;
	for ( i=0, j=0; i<MASTER && j<n; j++ )
	{
		if ( ((sum + (j + 1)) >= nums) && (i != (MASTER-1)) ) 
		{
			div[j] = i;
			procs[i] = procs[i] + 1;
			i = i + 1;
			sum = j + 1;
		} else {
			sum = sum + j + 1;
			div[j] = i;
			procs[i] = procs[i] + 1;
		}
	}
}
