#include <stdio.h>
#include <stdlib.h>
#include <sys/time.h>
#include <string.h>
#include <time.h>
#include "xmlWriter.h"

//Recursively xmlPrints the values of the node passed in with 
//a margin based on the level passed in
void xmlPrintXMLHelper(XMLNode *node, int level, FILE *outputFile)
{
  int i = 0;
  char margin[100] = "\0";
  while(i < level * 2)
  {
    margin[i] = ' ';
    i++;
  }

  margin[level * 2] = '\0';
 

  if(strcmp(node->NodeType, DOCUMENT_NODE) == 0)
  {
    int j =0;
    while(j < node->ChildrenCount)
    {
      xmlPrintXMLHelper(node->ChildNodes[j], 0, outputFile);
      j++;
    }
  }
  else if(strcmp(node->NodeType,DECLERATION_NODE) == 0)
  {
    fprintf(outputFile, "%s<?%s %s ?>\n",margin, node->Name, node->Value);
  }
  else if(strcmp(node->NodeType,TEXT_NODE) == 0)
  {
    fprintf(outputFile, "%s", node->Value);
  }
  else if(strcmp(node->NodeType,ELEMENT_NODE) == 0)
  {
    fprintf(outputFile, "%s<%s", margin, node->Name);
    i = 0;
    while(i < node->AttributesCount)
    {
      fprintf(outputFile, " %s=\"%s\"", node->Attributes[i]->Name, node->Attributes[i]->Value);
      i++;
    }
    fprintf(outputFile, ">");

    if(node->ChildrenCount > 0 && strcmp(node->ChildNodes[0]->NodeType,ELEMENT_NODE) == 0)
    {
      fprintf(outputFile, "\n");
    }
    else
    {
      margin[0] = '\0';
    }

    i=0;
    while(i < node->ChildrenCount)
    {
      xmlPrintXMLHelper(node->ChildNodes[i], level+1, outputFile);
      i++;
    }

    fprintf(outputFile, "%s</%s>\n",margin, node->Name);
  }
}

//Recursivley xmlPrints ths XML node's values to the screen
void xmlPrint(XMLNode *node)
{
  xmlPrintXMLHelper(node, 0, stdout);
}

//Recursivley xmlPrints ths XML node's values to file
void xmlWrite(XMLNode *node, FILE *outputFile)
{
  xmlPrintXMLHelper(node, 0, outputFile);
  fprintf(outputFile, "\n");
}


