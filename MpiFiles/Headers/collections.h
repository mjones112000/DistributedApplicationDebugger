#ifndef _COLLECTIONS_
#define _COLLECTIONS_
#include <semaphore.h>

typedef struct charList_item{
  char* Items;
  int ItemCount;
  int ListSize;
  sem_t ListLock;
} charList;

typedef struct node_item{
  void* Value;
  struct node_item* NextNode;
} node;

typedef struct queue_item{
  node* FirstNode;
  node* LastNode;
  node* IterateNode;
  int Length;
} queue;

void InitializeCharList(charList* newCharList);
void AddChars(charList* dest, char* newChars, int count);
void RemoveChars(char* dest, charList* source, int count);
void CleanUpCharList(charList* list);
void ClearChars(charList* source);
void InitializeQueue(queue* newQueue);
int IsQueueEmpty(queue* initializedQueue);
void Enqueue(queue* source, void* value);
void Dequeue(void** dest, queue* source);
void CleanUpQueue(queue* queueToCleanUp);
void StartIterateQueue(queue* source);
void IterateQueue(void** dest, queue* source);
int Split(char* source, char* delimiter, char*** dest, int startChar, int endChar);
int ReplaceChars(charList* source, char* value, char* replacement);

#endif
