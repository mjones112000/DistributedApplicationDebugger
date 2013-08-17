
#ifndef _CALL_CENTER_
#define _CALL_CENTER_

typedef struct clusterNode_item{
  sem_t clusterNodeLock;
  sem_t messageNotification;
  int nodeId;
  int processId;
  int clientSocket;
  int gdbSocket;
  queue* messages;
} clusterNode;

typedef struct clusterCommunication_item
{
  int clusterSize;
  char* clusterCommandLine;
} clusterCommunication;

void ListenForClient();
void* ListenToCluster(void* value);
void LaunchCluster(clusterCommunication* clusterComm);
void ProcessClientMessages(queue* messages);
void InitializeClusterNode(clusterNode* newClusterNode, int clientSocket, int nodeId, int processId);
void CleanupClusterNode(clusterNode* disposingClusterNode);
void* ReadFromMinion(void* value);
void* ProcessMinionMessages(void* value);
void GetSessionHistory(char* dirLocation, charList* resultList);
void* WriteToClient(void* value);

void GetBufferValues(int commandId, char* fileLocation, char* SOHReplace, 
	char* PartitionReplace, char* EOTReplace, charList* resultList);

char* GetConfigInfo();
void getDateTime(char** resultBuff, int buffLength);

void CreateMPIRequest(char* mpiRequest, int clusterSize, 
	char* exeLocation,char* hostFileLocation, char* parameters, char* gdbNodeList);

void LogSessionInfo(char* sessionName, char* timeBuffer, int clusterSize, 
	char* hostFileLocation, char* exeLocation, char* parameters, 
	char* sessionsFolderLocation, char* sessionFolder, char* sessionInfoFolder, 
	charList* recordSessionData);

void StoreReplacementChars(char* sohChars, char* partitionChars, char* eotChars);

#endif
