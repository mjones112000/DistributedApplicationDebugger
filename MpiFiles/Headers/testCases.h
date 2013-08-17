#ifndef XML_TESTCASES_H_INCLUDED
#define XML_TESTCASES_H_INCLUDED
#define TRUE 1
#define FALSE 0

void RunDataTypeTests(int rank, int size, int dest, int src, int isBlocking);
void RunCommTests(int rank, int size, int dest, int src, int isBlocking);

#endif
