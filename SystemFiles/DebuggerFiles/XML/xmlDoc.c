#define _GNU_SOURCE
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "xmlDoc.h"


//Copies the string from the value into the value for the field
void assignString(char** field, char* value)
{
  if(value == NULL)
  {
    *field = NULL;
  } 
  else
  {
    *field= (char*)malloc((strlen(value) + 1)*sizeof(char));
    memmove(*field, value, strlen(value));
    (*field)[strlen(value)] = '\0';
  }
}

//Gets the first child of the node passed in as text
char* xmlGetText(XMLNode *node)
{
  return node->ChildNodes[0]->Value;
}

//gets the attribute of a node with the name passed in
Attribute* xmlGetAttribute(XMLNode *node, char* attributeName)
{
  int i = 0;
  Attribute* attribute = NULL;
  for(i = 0; i < node->AttributesCount; i++)
  {
    if(strcmp(node->Attributes[i]->Name, attributeName) == 0)
    {
      attribute = node->Attributes[i];
      break;
    }
  }

  return attribute;
}

//Creates an empty new XML Node and returns it
XMLNode* createEmptyXMLNode()
{
  XMLNode* newNode = (XMLNode *)malloc(sizeof(XMLNode));
  newNode->ChildrenCount = 0;
  newNode->ChildrenArraySize = 1;
  newNode->ChildNodes = (XMLNode **)malloc(sizeof(XMLNode*));
  newNode->AttributesCount = 0;
  newNode->AttributesArraySize = 1;
  newNode->Attributes = (Attribute **)malloc(sizeof(Attribute*));

  //Set pointers to null by default?
  newNode->NodeType = '\0';
  newNode->Name = '\0';
  newNode->Value = '\0';

  return newNode;
}

XMLNode* createStringElementNode(char* elementName, char* nodeValue)
{
  XMLNode *elementNode = xmlCreateNode(ELEMENT_NODE, elementName, NULL);
  xmlAddChildNode(elementNode, xmlCreateNode(TEXT_NODE, TEXT_NAME, nodeValue));

  return elementNode;
}

XMLNode* createIntElementNode(char * elementName, int nodeValue)
{
  XMLNode *elementNode = xmlCreateNode(ELEMENT_NODE, elementName, NULL);

  char *value;
  int length = 0;
  length = asprintf(&value, "%d", nodeValue);

  xmlAddChildNode(elementNode, xmlCreateNode(TEXT_NODE, TEXT_NAME, value));
  free(value);

  return elementNode;
}

//Create an XML Node with the type, name, and value assigned
XMLNode* xmlCreateNode(char* nodeType, char* name, char* value)
{
  XMLNode* newNode = createEmptyXMLNode();
  assignString(&(newNode->NodeType), nodeType);
  assignString(&(newNode->Name), name);
  assignString(&(newNode->Value), value);
  
  return newNode;
}

char* GetText(XMLNode* node)
{
  return node->ChildNodes[0]->Value;
}

//Adds an attribute to the node passed in with the corresponding attribute/value pair
void xmlAddAttribute(XMLNode *xmlNode, char *atrribute, char *value)
{
  if(xmlNode->AttributesCount == xmlNode->AttributesArraySize)
  {
    xmlNode->AttributesArraySize = 2 * xmlNode->AttributesArraySize;
    xmlNode->Attributes = realloc(
      xmlNode->Attributes, xmlNode->AttributesArraySize * sizeof(Attribute*));
  }

  xmlNode->AttributesCount++;
  xmlNode->Attributes[xmlNode->AttributesCount - 1] = (Attribute *)malloc(sizeof(Attribute));
  

  assignString(&(xmlNode->Attributes[xmlNode->AttributesCount - 1]->Name), atrribute);
  
  //TODO Trim new line characters correctly
  int valueLength = (int)strlen(value);
  if(value[valueLength - 1] == '\n')
    value[valueLength - 1] = ' ';

  assignString(&(xmlNode->Attributes[xmlNode->AttributesCount - 1]->Value), value);
}

//Adds a child node to the parent node passed in
XMLNode *xmlAddChildNode(XMLNode *parentNode, XMLNode *childNode)
{
  if(parentNode->ChildrenCount == parentNode->ChildrenArraySize)
  {
    parentNode->ChildrenArraySize = parentNode->ChildrenArraySize * 2;
    parentNode->ChildNodes = 
      realloc(parentNode->ChildNodes, parentNode->ChildrenArraySize * sizeof(XMLNode*));
  }

  parentNode->ChildrenCount++;
  int currentChildIndex = parentNode->ChildrenCount - 1;
 
  parentNode->ChildNodes[currentChildIndex] = childNode;

  return childNode;
}

XMLNode* xmlGetChildNode(XMLNode *parentNode, char* childName)
{

  XMLNode *returnNode = NULL;
  int i = 0;
  for(i = 0; i < parentNode->ChildrenCount; i++)
  {
    if(strcmp(parentNode->ChildNodes[i]->Name, childName) == 0)
    {
      returnNode = parentNode->ChildNodes[i];
      break;
    }
  }
  return returnNode;
}

void freeAttribute(Attribute *attribute)
{
  free(attribute->Name);
  free(attribute->Value);
}

void xmlFree(XMLNode *node)
{
  free(node->Name);
  free(node->NodeType);
  free(node->Value);

  int i = 0;
  for(i=0; i < node->AttributesCount; i++)
  {
    freeAttribute(node->Attributes[i]);
  }  
  
  i = 0;
  for(i=0; i < node->ChildrenCount; i++)
  {
    xmlFree(node->ChildNodes[i]);
  }
  
  free(node->ChildNodes);
  free(node->Attributes);
  
  free(node);
}


