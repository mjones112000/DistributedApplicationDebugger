#ifndef GDB_ATTACH_H_INCLUDED
#define GDB_ATTACH_H_INCLUDED

typedef struct gdbAttachInfo_item{
  sem_t quitingSemaphore;
  int streamOutgoing;
  int streamIncoming;
  int gdbSocketOut;
} gdbAttachInfo;

void* gdbProcessListener(void* val);
void* gdbProcessWriter(void* val);
void AttachGDB(char* applicationLocation, int mainSocket, char* callbackAddress, int callbackPort, int nodeId);
void InitializeAttachInfo(gdbAttachInfo* newGdbAttachInfo);

#endif
