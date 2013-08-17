#ifndef XML_DOC_H_INCLUDED
#define XML_DOC_H_INCLUDED

typedef struct Attribute_item{
  char* Name;
  char* Value;
} Attribute;

typedef struct XMLNode_item{
  char* Name;
  char* NodeType;
  char* Value;
  int ChildrenCount;
  int ChildrenArraySize;
  int AttributesCount;
  int AttributesArraySize;
  struct XMLNode_item **ChildNodes;
  struct Attribute_item **Attributes;
} XMLNode;

#define TRUE 1
#define FALSE 0

#define DOCUMENT_NODE "Document"
#define DECLERATION_NODE "XmlDecleration"
#define ELEMENT_NODE "Element"
#define TEXT_NODE "Text"
#define TEXT_NAME "#text"
#define DOCUMENT_NAME "Main Document"

XMLNode* xmlCreateNode(char* nodeType, char* name, char* value);
XMLNode* createStringElementNode(char* elementName, char* nodeValue);
XMLNode* createIntElementNode(char * elementName, int nodeValue);
XMLNode* xmlAddChildNode(XMLNode *parentNode, XMLNode *childNode);
XMLNode* xmlGetChildNode(XMLNode *parentNode, char* childName);
Attribute* xmlGetAttribute(XMLNode *node, char* attributeName);

char* xmlGetText(XMLNode* node);
void xmlAddAttribute(XMLNode *xmlNode, char *atrribute, char *value);
void xmlFree(XMLNode *node);

#endif
