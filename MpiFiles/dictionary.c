#include <stdlib.h>
#include <stdio.h>

#include "Headers/dictionary.h"

struct KeyValuePair_item{
  void *Key;
  void *Value;
  struct KeyValuePair_item *NextNode;
  struct KeyValuePair_item *PrevNode;
};

typedef struct KeyValuePair_item KeyValuePair;

KeyValuePair *headerNode = NULL;
KeyValuePair *lastNode = NULL;

void* DictionaryAdd(void* key, void* value)
{
  KeyValuePair *node = (KeyValuePair*)malloc(sizeof(KeyValuePair));
  node->Key = key;
  node->Value = value;
  node->NextNode = NULL;
  node->PrevNode = NULL;

  if(headerNode == NULL)
  {
    headerNode = node;
    lastNode = node;
  }else{
    
    if(lastNode == NULL)
    {
      printf("LastNode is NULL\n");
    }
    lastNode->NextNode = node;
    node->PrevNode = lastNode;
    
    lastNode = node;
  }

  return key;
}


void* DictionaryRemove(void* key)
{
  void* value = NULL;

  if(headerNode != NULL)
  {
    KeyValuePair *currentNode = headerNode;
  
    while(1)
    {
      if(currentNode->Key == key)
      {
        value = currentNode->Value;
        
        if(currentNode->PrevNode != NULL)
        {
          currentNode->PrevNode->NextNode = currentNode->NextNode;
        }
        
        if(currentNode->NextNode != NULL)
        {
          currentNode->NextNode->PrevNode = currentNode->PrevNode;
        }

        if(currentNode == headerNode)
        {
          headerNode= currentNode->NextNode;
        }
        
        if(currentNode == lastNode)
        {
          lastNode = currentNode->PrevNode;
        }


        currentNode->NextNode = NULL;
        currentNode->PrevNode = NULL;

        free(currentNode);
        break;
      }

      if(currentNode->NextNode == NULL)
      {
        break;
      }else{
        currentNode = currentNode->NextNode;
      }
    }
  }


  return value;
}

void* DictionaryRemoveByInt(int key)
{
  void* value = NULL;

  if(headerNode != NULL)
  {
    KeyValuePair *currentNode = headerNode;
  
    while(1)
    {
      if(((int*)currentNode->Key)[0] == key)
      {
        value = currentNode->Value;
        
        if(currentNode->PrevNode != NULL)
        {
          currentNode->PrevNode->NextNode = currentNode->NextNode;
        }

        if(currentNode->NextNode != NULL)
        {
          currentNode->NextNode->PrevNode = currentNode->PrevNode;
        }

        if(currentNode == headerNode)
        {
          headerNode= currentNode->NextNode;
        }
        
        if(currentNode == lastNode)
        {
          lastNode = currentNode->PrevNode;
        }

        currentNode->NextNode = NULL;
        currentNode->PrevNode = NULL;

        //We can free the key here because it was malloced to save as a literal integer
        free(currentNode->Key);
        free(currentNode);
        break;
      }

      if(currentNode->NextNode == NULL)
      {
        break;
      }else{
        currentNode = currentNode->NextNode;
      }
    }
  }


  return value;
}

void DictionaryTest()
{
  char* buf4 = "Hello";
  char* buf3 = "Michael";
  char* buf2 = "Quinn";
  char* buf1 = "Jones";

  int* int1 = (int*)DictionaryAdd((void*)1, buf1);
  int* int2 = (int*)DictionaryAdd((void*)2, buf2);
  int* int3 = (int*)DictionaryAdd((void*)3, buf3);
  int* int4 = (int*)DictionaryAdd((void*)4, buf4);

  printf("%s\n", (char*)DictionaryRemove(int4));
  printf("%s\n", (char*)DictionaryRemove(int3));
  printf("%s\n", (char*)DictionaryRemove(int2));
  printf("%s\n", (char*)DictionaryRemove(int1));
}

