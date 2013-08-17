#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include "Headers/collections.h"
#include "Headers/booleanLogic.h"

#define STARTING_CHAR_LIST_SIZE 1048
#define STARTING_STRING_LIST_SIZE 1

void InitializeCharList(charList* newCharList)
{
  newCharList->ItemCount = 0;
  newCharList->ListSize = STARTING_CHAR_LIST_SIZE;
  newCharList->Items = (char*)malloc(newCharList->ListSize * sizeof(char));
  memset(newCharList->Items, '\0', newCharList->ListSize);
}

void CleanUpCharList(charList* list)
{
  free(list->Items);
  free(list->Items = '\0');
  free(list);
  list = '\0';
}

void SizeCharList(charList* list, int desiredMinimumSize)
{
  int initialListSize = list->ListSize;
 
  //Handle the case where it is too big
  while(list->ListSize < desiredMinimumSize)
  {
    list->ListSize = 2 * list->ListSize;
  }

  //Handle the case where it is too small
  while(list->ListSize/2 >  desiredMinimumSize)
  {
    list->ListSize = list->ListSize /2;      
  }

  //Check if we changed the list size and, if so, reallocate the memory
  if(list->ListSize != initialListSize)
  {
    list->Items = (char*)realloc(list->Items, list->ListSize * sizeof(char));
  }
}

void AddChars(charList* dest, char* newChars, int count)
{
  //first make sure we are big enough to allocate this many bytes
  SizeCharList(dest, dest->ItemCount + count);

  //Append the new chars to the end of the list
  memcpy(dest->Items + dest->ItemCount,newChars, count);

  //update the size of our list
  dest->ItemCount += count;
}

void RemoveChars(char* dest, charList* source, int count)
{
  //Copy the first 'x' characters from the beginning of the list
  if(dest != NULL)
    memcpy(dest, source->Items, count);

  //Move the rest of the buffer to the beginning
  memmove(source->Items, source->Items + count, source->ListSize - count);

  //Free up the buffer if it is less then half full
  SizeCharList(source, source->ItemCount - count);

  //Record the new size of the buffer
  source->ItemCount -= count;
}

void ClearChars(charList* source)
{
  if(source->ListSize != STARTING_CHAR_LIST_SIZE)
  {
    source->ListSize = STARTING_CHAR_LIST_SIZE;
    source->Items = (char*)realloc(source->Items, source->ListSize * sizeof(char));
  }

  memset(source->Items, '\0', source->ListSize);
  source->ItemCount = 0;
}

int ReplaceChars(charList* source, char* value, char* replacement)
{
  charList* dest = (charList*)malloc(sizeof(charList));
  InitializeCharList(dest);
	
	int startChar = 0;
	int sectionLength = 0;
	char* startPtr = strstr(source->Items, value);
	int result = FALSE;
	
	while(startPtr != NULL)
	{
		result = TRUE;
		sectionLength = startPtr -(source->Items + startChar);
		AddChars(dest, source->Items + startChar, sectionLength);
		AddChars(dest, replacement, strlen(replacement));
		startChar = startChar + sectionLength + strlen(value);
		startPtr = strstr(source->Items + startChar, value);
	}	
		
	int remainingLeft = source->Items + source->ItemCount - (source->Items + startChar);
		
	if(remainingLeft > 0)
		AddChars(dest, source->Items + startChar, remainingLeft);
	
	ClearChars(source);
	AddChars(source, dest->Items, dest->ItemCount);
	CleanUpCharList(dest);
	
	return result;
}

//Splits the source on the delimiter passed in and places it in the destination
int Split(char* source, char* delimiter, char*** dest, int startChar, int endChar)
{	
	//determine length of the string to split
	int strLen = endChar - startChar;
	//Trim the source down first
	char* splitStr = (char*)malloc(strLen * sizeof(char));
	splitStr = memcpy(splitStr ,source + startChar, strLen);
	
	//Initialize a list for the result
	int itemCount = 0;
  *dest = (char**)malloc(sizeof(char*));
	int sectionStartChar = 0;
	int sectionLength = 0;

	//Get location of the first delimiter
	char* startPtr = strstr(splitStr, delimiter);

	//Loop through until we don't find the delimiter anymore
	while(startPtr != NULL)
	{
		//We found one add one to the length of the list
		itemCount++;
	  *dest = (char**)realloc(*dest, itemCount * sizeof(char*));
	  
	  //Get the length of this string and allocate a string to hold the value
		sectionLength = startPtr -(splitStr + sectionStartChar);
				
		char* value = NULL;
		if(sectionLength > 0)
		{
			value = (char*)malloc((sectionLength + 1) * sizeof(char));
			//Copy the section to the result string and add it to the list
			value = memcpy(value ,splitStr + sectionStartChar, sectionLength);
		}
		else
		{
			//This must be running delimiters, make a null item
			value = (char*)malloc(sizeof(char));
			value[0] = '\0';
		}
		
		value[sectionLength] = '\0';
		
		(*dest)[itemCount - 1] = value;

		//Move the section start forward past the delimiter and look for the next instance		
		sectionStartChar = sectionStartChar + sectionLength + strlen(delimiter);
		startPtr = strstr(splitStr + sectionStartChar, delimiter);

	}	

	//Get any remaining characters in the string after the last delimiter
	int remainingLeft = splitStr + strLen - (splitStr + sectionStartChar);
	
	//Check if there were an remaining characters
	if(remainingLeft > 0)
	{
		//Add another space to the list
		itemCount++;
		*dest = (char**)realloc(*dest, itemCount * sizeof(char*));
		
		//Allocate a new string for the remaining characters
		char* value = (char*)malloc((remainingLeft + 1)  * sizeof(char));
		
		if(remainingLeft == 1)
			value[0] = '\0';
		else	
			//Add the last part to the list
			value = memcpy(value ,splitStr + sectionStartChar, remainingLeft);
			
		value[remainingLeft] = '\0';		
		(*dest)[itemCount - 1] = value;
	}

	//clean up
  free(splitStr);

	return itemCount;
}


void ReadChars(char* fileName, charList* resultList)
{
	FILE* fp = fopen(fileName, "r");
	int bufferLength = 255;
	char inBuff[bufferLength];
	  
	int length = 0;
	
	while(!feof(fp))
	{
		memset(inBuff, '\0', bufferLength);
		length = fread(inBuff, sizeof(char), bufferLength - 1, fp);
		AddChars(resultList, inBuff, length);
	}
	
	fclose(fp);

}

void CleanUpNode(node* nodeToCleanUp)
{
  nodeToCleanUp->NextNode = NULL;
  free(nodeToCleanUp);
}


void InitializeQueue(queue* newQueue)
{
  newQueue->FirstNode = NULL;
  newQueue->LastNode = NULL;
  newQueue->IterateNode = NULL;
  newQueue->Length = 0;
}

void CleanUpQueue(queue* queueToCleanUp)
{
  while(queueToCleanUp->FirstNode != NULL)
  {
    node* nodeToCleanUp = queueToCleanUp->FirstNode;
    queueToCleanUp->FirstNode = nodeToCleanUp->NextNode;

    CleanUpNode(nodeToCleanUp);
  }

  queueToCleanUp->FirstNode = NULL;
  queueToCleanUp->LastNode = NULL;
  queueToCleanUp->IterateNode = NULL;
  free(queueToCleanUp);
}

int IsQueueEmpty(queue* initializedQueue)
{
  if(initializedQueue->FirstNode == NULL)
    return TRUE;
  else
    return FALSE;
}

void Enqueue(queue* source, void* value)
{
  //Make a new node
  node* newNode = (node*)malloc(sizeof(node));
  newNode->Value = value;
  newNode->NextNode = NULL;
	
  //Point the queue's last node to the new one
  if(source->LastNode != NULL)
    source->LastNode->NextNode = newNode;

  //Update the last node to the new one
  source->LastNode = newNode;
  //Check if this was the first node added
  if(source->FirstNode == NULL)
    source->FirstNode = newNode;

  source->Length++;
  //No iterating while adding or removing from the queue
  source->IterateNode = NULL;

}

void Dequeue(void** dest, queue* source)
{
  //Get the first node
  node* nodeToDequeue = source->FirstNode;

  //Get the value from the node
  if(dest == NULL)
  {
    //dellocate the space of the dequed item, its not getting returned.
    free(nodeToDequeue->Value);
  }
  else
  {
    *dest = nodeToDequeue->Value;
  }
  
  //Point the queue at the next node
  source->FirstNode = nodeToDequeue->NextNode;

  if(source->FirstNode == NULL)
    source->LastNode = NULL;

  //Clean up the dequeued node
  CleanUpNode(nodeToDequeue);

  source->Length--;
  
  //No iterating while adding or removing from the queue
  source->IterateNode = NULL;
}

void StartIterateQueue(queue* source)
{
  source->IterateNode = source->FirstNode;
}

void IterateQueue(void** dest, queue* source)
{
	if(source->IterateNode != NULL)
	{
		*dest = source->IterateNode->Value;
		source->IterateNode = source->IterateNode->NextNode;
	}
	else
	{
		*dest = NULL;
	}	
}

void CleanupStringArray(char*** source, int itemCount)
{
	int i = 0;
	for(i = 0; i < itemCount; i++)
	{
		(*source)[i] = NULL;
		free((*source)[i]);
	}

	*source = NULL;
	free(*source);
}
