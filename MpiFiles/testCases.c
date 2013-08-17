#define _GNU_SOURCE
#include "Headers/mpi.h"
#include "Headers/testCases.h"

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <math.h>
#include <stdlib.h>
#include <time.h>
#include <unistd.h>

int _isBlocking = TRUE;

void BlockingTest(void* outBuf, void* inBuf, int bufLengths, MPI_Datatype datatype, int src, int dest)
{
  MPI_Status sta;
  MPI_Send(outBuf, bufLengths, datatype, dest, 10, MPI_COMM_WORLD);
  
  MPI_Probe(src, 10, MPI_COMM_WORLD, &sta);
  //MPI_Recv(inBuf, bufLengths, datatype, src, 10, MPI_COMM_WORLD, &sta);	
  MPI_Recv(inBuf, bufLengths, datatype, MPI_ANY_SOURCE, MPI_ANY_TAG, MPI_COMM_WORLD, &sta);	
}

void NonBlockingTest(void* outBuf, void* inBuf, int bufLengths, MPI_Datatype datatype, int src, int dest)
{
  MPI_Request sendRequest, recvRequest;
  
  MPI_Status sta1;
  
  int flag = 0;
	
  MPI_Isend(outBuf, bufLengths, datatype, dest, 10, MPI_COMM_WORLD, &sendRequest);

	MPI_Iprobe(src, 10, MPI_COMM_WORLD, &flag, &sta1);  
	
	//Loop until we detect that the src is ready
//	while(!flag)
//	{
//		usleep(10);
//		MPI_Iprobe(src, 10, MPI_COMM_WORLD, &flag, &sta1);    	
//	}

  MPI_Irecv(inBuf, bufLengths, datatype, src, 10, MPI_COMM_WORLD, &recvRequest); 
  MPI_Wait(&recvRequest, &sta1);
}

void PerformTest(int rank, int size, void* outBuf, void* inBuf, int bufLengths, 
  int bufSizes, MPI_Datatype datatype, int src, int dest, const char* testName)
{

  char* testVerbage = NULL;

  if(_isBlocking == TRUE){
    testVerbage = "Blocking";
    BlockingTest(outBuf, inBuf, bufLengths, datatype, src, dest);
  }
  else{ 
    testVerbage = "NonBlocking";
    NonBlockingTest(outBuf, inBuf, bufLengths, datatype, src, dest);
  }
  
  
  if(rank == 0 || size == 1)
  {  	
    if(memcmp(outBuf, inBuf, bufSizes) == 0){
      printf("%d: %s%s...... PASSED\n", rank, testName, testVerbage);
    } else{
      printf("%d: %s%s...... FAILED\n", rank, testName, testVerbage);
    }
  }
  
}

void TestChar(int rank, int size, int dest, int src)
{
  int bufLengths = 12;  
  int bufSizes = bufLengths*sizeof(char);

  char *outBuf = "Hello World";
  char* inBuf =  (char*)malloc(bufSizes);
  
  PerformTest(rank, size, outBuf, inBuf, bufLengths, bufSizes, MPI_CHAR, src, dest, "TestChar");

  free(inBuf);
}

void TestUnsignedChar(int rank, int size, int dest, int src, int isByteTest)
{
  int bufLengths = 12;  
  int bufSizes = bufLengths*sizeof(unsigned char);
  unsigned char* outBuf = (unsigned char*)malloc(bufSizes);
  unsigned char* inBuf =  (unsigned char*)malloc(bufSizes);

  outBuf[0] = '\x01';
  outBuf[1] = '|';
  outBuf[2] = '\x04';
  outBuf[3] = '$';
  outBuf[4] = '%';
  outBuf[5] = '^';
  outBuf[6] = '&';
  outBuf[7] = '*';
  outBuf[8] = '(';
  outBuf[9] = ')';
  outBuf[10] = '-';
  outBuf[11] = '\0';

  if(isByteTest == TRUE)
    PerformTest(rank, size, outBuf, inBuf, bufLengths, bufSizes, MPI_BYTE, src, dest, "TestByte");
  else
    PerformTest(rank, size, outBuf, inBuf, bufLengths, bufSizes, MPI_UNSIGNED_CHAR, src, dest, "TestUnsignedChar");

  free(outBuf);
  free(inBuf);
}

void TestShort(int rank, int size, int dest, int src)
{
  int bufLengths = 10;
  int bufSizes = bufLengths*sizeof(short);
  short* outBuf = (short*)malloc(bufSizes);
  short* inBuf =  (short*)malloc(bufSizes);

  outBuf[0] = 0;
  outBuf[1] = -pow(2,15);
  outBuf[2] = pow(2,15)-1;
  outBuf[3] = 356;
  outBuf[4] = 76;
  outBuf[5] = 68;
  outBuf[6] = 67;
  outBuf[7] = 17;
  outBuf[8] = 586;
  outBuf[9] = 25;
      
  PerformTest(rank, size, outBuf, inBuf, bufLengths, bufSizes, MPI_SHORT, src, dest, "TestShort");

  free(outBuf);
  free(inBuf);
}

void TestInt(int rank, int size, int dest, int src)
{
  int bufLengths = 10;
  int bufSizes = bufLengths*sizeof(int);
  int* outBuf = (int*)malloc(bufSizes);
  int* inBuf =  (int*)malloc(bufSizes);

  outBuf[0] = 0;
  outBuf[1] = -pow(2,31);
  outBuf[2] = pow(2,31)-1;
  outBuf[3] = 356456;
  outBuf[4] = 765;
  outBuf[5] = 68378376;
  outBuf[6] = 67787;
  outBuf[7] = 17636;
  outBuf[8] = 585356;
  outBuf[9] = 253636;
  
  PerformTest(rank, size, outBuf, inBuf, bufLengths, bufSizes, MPI_INT, src, dest, "TestInt");

  free(outBuf);
  free(inBuf);
}

void TestLong(int rank, int size, int dest, int src)
{
  int bufLengths = 10;
  int bufSizes = bufLengths*sizeof(long);  
  long* outBuf = (long*)malloc(bufSizes);
  long* inBuf =  (long*)malloc(bufSizes);

  outBuf[0] = 0;
  outBuf[1] = (long)-pow(2,63);
  outBuf[2] = (long)(pow(2,63)-1);
  outBuf[3] = 356456;
  outBuf[4] = 765;
  outBuf[5] = 68378376;
  outBuf[6] = 67787;
  outBuf[7] = 17636;
  outBuf[8] = 585356;
  outBuf[9] = 253636;

  PerformTest(rank, size, outBuf, inBuf, bufLengths, bufSizes, MPI_LONG, src, dest, "TestLong");

  free(outBuf);
  free(inBuf);
}

void TestFloat(int rank, int size, int dest, int src)
{
  int bufLengths = 10;
  int bufSizes = bufLengths*sizeof(float); 
  float* outBuf = (float*)malloc(bufSizes);
  float* inBuf =  (float*)malloc(bufSizes);

  outBuf[0] = 0.0f;
  outBuf[1] = -45.1234f;
  outBuf[2] = 245.3333f;
  outBuf[3] = 356.134143f;
  outBuf[4] = 76.55443f;
  outBuf[5] = 68.3422134f;
  outBuf[6] = 67.6f;
  outBuf[7] = 17.0f;
  outBuf[8] = 586.0f;
  outBuf[9] = 25.0f;

  char* testVerbage = NULL;

  if(_isBlocking == TRUE){
    testVerbage = "Blocking";
    BlockingTest(outBuf, inBuf, bufLengths, MPI_FLOAT, src, dest);
  }
  else{ 
    testVerbage = "NonBlocking";
    NonBlockingTest(outBuf, inBuf, bufLengths, MPI_FLOAT, src, dest);
  }


  if(rank == 0 || size == 1)
  {
    int i = 0;
    int allEqual = TRUE;
    for(i = 0; i < bufLengths; i++)
    {
      if(outBuf[i] != inBuf[i])
      {
        printf("TestFloat%s element %d: %f does not equal %f\n", testVerbage, i, outBuf[i], inBuf[i]);
        allEqual = FALSE;
        break;
      }
    }

    if(allEqual == TRUE)
      printf("%d: %s%s...... PASSED\n", rank, "TestFloat", testVerbage);
    else
      printf("%d: %s%s...... FAILED\n", rank, "TestFloat", testVerbage);

  }

  free(outBuf);
  free(inBuf);
}

void TestDouble(int rank, int size, int dest, int src)
{

  int bufLengths = 10;
  int bufSizes = bufLengths*sizeof(double);   
  double* outBuf = (double*)malloc(bufSizes);
  double* inBuf =  (double*)malloc(bufSizes);

  outBuf[0] = 0;
  outBuf[1] = -45.1234;
  outBuf[2] = 245.3333;
  outBuf[3] = 356.134143;
  outBuf[4] = 76.55443;
  outBuf[5] = 68.342213;
  outBuf[6] = 67.6;
  outBuf[7] = 17;
  outBuf[8] = 586;
  outBuf[9] = 25;

  char* testVerbage = NULL;

  if(_isBlocking == TRUE){
    testVerbage = "Blocking";
    BlockingTest(outBuf, inBuf, bufLengths, MPI_DOUBLE, src, dest);
  }
  else{ 
    testVerbage = "NonBlocking";
    NonBlockingTest(outBuf, inBuf, bufLengths, MPI_DOUBLE, src, dest);
  }

  if(rank == 0 || size == 1)
  {
    int i = 0;
    int allEqual = TRUE;
    for(i = 0; i < bufLengths; i++)
    {
      if(outBuf[i] != inBuf[i])
      {
        allEqual = FALSE;
        break;
      }
    }

    if(allEqual == TRUE)
      printf("%d: %s%s...... PASSED\n", rank, "TestDouble", testVerbage);
    else
      printf("%d: %s%s...... FAILED\n", rank, "TestDouble", testVerbage);

  }

  free(outBuf);
  free(inBuf);

}

void TestUnsignedShort(int rank, int size, int dest, int src)
{
  int bufLengths = 10;
  int bufSizes = bufLengths*sizeof(unsigned short);
  unsigned short* outBuf = (unsigned short*)malloc(bufSizes);
  unsigned short* inBuf =  (unsigned short*)malloc(bufSizes);

  outBuf[0] = 0;
  outBuf[1] = pow(2,16)-1;
  outBuf[2] = 245;
  outBuf[3] = 356;
  outBuf[4] = 76;
  outBuf[5] = 65535;
  outBuf[6] = 9541;
  outBuf[7] = 17;
  outBuf[8] = 586;
  outBuf[9] = 25;

  PerformTest(rank, size, outBuf, inBuf, bufLengths, bufSizes, MPI_UNSIGNED_SHORT, src, dest, "TestUnsignedShort");

  free(outBuf);
  free(inBuf);
}

void TestUnsignedInt(int rank, int size, int dest, int src)
{
  int bufLengths = 10;
  int bufSizes = bufLengths*sizeof(unsigned int);
  unsigned int* outBuf = (unsigned int*)malloc(bufSizes);
  unsigned int* inBuf =  (unsigned int*)malloc(bufSizes);

  outBuf[0] = 0;
  outBuf[1] = pow(2,32)-1;
  outBuf[2] = 35;
  outBuf[3] = 1514;
  outBuf[4] = 5549;
  outBuf[5] = 4294967295;
  outBuf[6] = 999494;
  outBuf[7] = 884984;
  outBuf[8] = 921;
  outBuf[9] = 10;

  PerformTest(rank, size, outBuf, inBuf, bufLengths, bufSizes, MPI_UNSIGNED, src, dest, "TestUnsignedInt");

  free(outBuf);
  free(inBuf);
}

void TestUnsignedLong(int rank, int size, int dest, int src)
{
  int bufLengths = 10;
  int bufSizes = bufLengths*sizeof(unsigned long);  
  unsigned long* outBuf = (unsigned long*)malloc(bufSizes);
  unsigned long* inBuf =  (unsigned long*)malloc(bufSizes);

  outBuf[0] = 0;
  outBuf[1] = (unsigned long)(pow(2,64)-1);
  outBuf[2] = 2454235;
  outBuf[3] = 356456;
  outBuf[4] = 765;
  outBuf[5] = 94984;
  outBuf[5] = 2147483648;
  outBuf[6] = 45;
  outBuf[7] = 17636;
  outBuf[8] = 585356;
  outBuf[9] = 253636;

  PerformTest(rank, size, outBuf, inBuf, bufLengths, bufSizes, MPI_UNSIGNED_LONG, src, dest, "TestUnsignedLong");

  free(outBuf);
  free(inBuf);
}

void TestLongDouble(int rank, int size, int dest, int src)
{

  int bufLengths = 10;
  int bufSizes = bufLengths*sizeof(long double);  
  long double* outBuf = (long double*)malloc(bufSizes);
  long double* inBuf =  (long double*)malloc(bufSizes);

  outBuf[0] = 0.0L;
  outBuf[1] = -45.1234L;
  outBuf[2] = 245.3333L;
  outBuf[3] = 356.134143L;
  outBuf[4] = 76.55443L;
  outBuf[5] = 68.342213L;
  outBuf[6] = 6555167.6515465L;
  outBuf[7] = 56.0L;
  outBuf[8] = 586.0L;
  outBuf[9] = 25.0L;

  char* testVerbage = NULL;

  if(_isBlocking == TRUE){
    testVerbage = "Blocking";
    BlockingTest(outBuf, inBuf, bufLengths, MPI_LONG_DOUBLE, src, dest);
  }
  else{ 
    testVerbage = "NonBlocking";
    NonBlockingTest(outBuf, inBuf, bufLengths, MPI_LONG_DOUBLE, src, dest);
  }

  if(rank == 0 || size == 1)
  {
    int i = 0;
    int allEqual = TRUE;
    for(i = 0; i < bufLengths; i++)
    {
      if(outBuf[i] != inBuf[i])
      {
        printf("TestLongDouble%s element %d: %Lf does not equal %Lf\n", testVerbage, i, outBuf[i], inBuf[i]);
        allEqual = FALSE;
        break;
      }
    }

    if(allEqual == TRUE)
      printf("%d: %s%s...... PASSED\n", rank, "TestLongDouble", testVerbage);
    else
      printf("%d: %s%s...... FAILED\n", rank, "TestLongDouble", testVerbage);

  }

  free(outBuf);
  free(inBuf);

}

void RunDataTypeTests(int rank, int size, int dest, int src, int isBlocking)
{
  _isBlocking = isBlocking;

  TestChar(rank, size, dest, src);
  MPI_Barrier(MPI_COMM_WORLD);
  TestUnsignedChar(rank, size, dest, src, TRUE);
  TestShort(rank, size, dest, src);
  TestInt(rank, size, dest, src);
  TestLong(rank, size, dest, src);
  TestFloat(rank, size, dest, src);
  TestDouble(rank, size, dest, src);
  TestUnsignedChar(rank, size, dest, src, FALSE);
  TestUnsignedShort(rank, size, dest, src);
  TestUnsignedInt(rank, size, dest, src);
  TestUnsignedLong(rank, size, dest, src);
  TestLongDouble(rank, size, dest,src);
}


