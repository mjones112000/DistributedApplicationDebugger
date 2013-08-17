#include <stdio.h>
#include <stdlib.h>
#include "Headers/booleanLogic.h"

int main(int argc, char *argv[]) 
{
  int _mainSocketOut = FALSE;
  if(argc < 5)
  {
    printf("Exiting. Not all parameters supplied, expected callback address, callback port, application path, and gdb port.\n");
    exit(0);
  }

  char* callbackAddress = argv[1];
  int callbackPort = atoi(argv[2]);
  char* applicationPath = argv[3];
  int gdbPort = atoi(argv[4]);

  char* localAddress = (char*)malloc(50*sizeof(char));      
  GetPrimaryIp(localAddress, 50) ;

  _mainSocketOut = CreateOutgoingConnection(callbackAddress, callbackPort);

  AttachGDB(applicationPath, _mainSocketOut, localAddress, gdbPort);

  printf("my id is %d\n", getpid());
  printf("1\n");
  printf("2\n");
  printf("3\n");
  printf("4\n");
  printf("5\n");
  printf("6\n");
  printf("7\n");
  printf("8\n");
  printf("9\n");
  printf("10\n");
  printf("11\n");
  printf("12\n");

  shutdown(_mainSocketOut, 2);

  return 0;
}

