
#ifndef XML_WRITER_H_INCLUDED
#define XML_WRITER_H_INCLUDED
#include "xmlDoc.h"
void xmlPrint(XMLNode *node);
void xmlWrite(XMLNode *node, FILE *outputFile);
#endif
