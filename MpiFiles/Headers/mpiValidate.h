#ifndef XML_VALIDATE_H_INCLUDED
#define XML_VALIDATE_H_INCLUDED
#include <mpi.h>
#include "../XML/xmlDoc.h"
#include "mpiUtils.h"
#include "collections.h"

void validateIntField(char* fieldName, XMLNode* expectedValues, int actualValue, 
			charList* resultBuffer);

void validateDatatypeField(XMLNode* expectedParams, MPI_Datatype actualValue, 
		charList* resultBuffer);

void validateStatusField(XMLNode* expectedParams, 
	MPI_Status *actualStatus,charList* resultBuffer);

void validateBufField(XMLNode *expectedParams, MPI_Datatype expectedDatatype, 
	int expectedCount, void *actualBuf, charList* result, 
	char* sohReplace, char* partitionReplace, char* eotReplace);

int EncodeMpiBuff(void* values, MPI_Datatype datatype, int count, 
		 char* sohReplace, char* partitionReplace, char* eotReplace, charList* result);

#endif
