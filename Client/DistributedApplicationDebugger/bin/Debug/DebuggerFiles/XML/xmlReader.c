#include <stdio.h>
#include <stdlib.h>
#include <sys/time.h>
#include <string.h>
#include <time.h>
#include "xmlReader.h"
#include "xmlWriter.h"

//The xml array from file held in memory
char *xmlArray;
//The current index we are reading from the xml array
int chrPtr = 0;

char* myFileName;

//Load the xml from the file passed in into the xml array in memory
void loadXMLFile(char* fileName)
{

  //Open the file and determine its length
  FILE *xmlFile = fopen(fileName, "r");
  fseek(xmlFile, 0, SEEK_END);
  long fileSize = ftell(xmlFile);
  rewind (xmlFile);

  //instantiate and populate the xml array
  xmlArray = (char*)malloc(fileSize *sizeof(char));
  int length = 0;
  length = fread(xmlArray, 1, fileSize, xmlFile);

  //close the file
  fclose(xmlFile);
}

//reads back from the length of the text to the current index
//and returns it as a string
char *readFromXML(int length)
{
  //Create a new string to hold the value int
  char *field = (char*)malloc((length + 1)* sizeof(char));
  //Copy the xml array starting from 'x' characters back until the current char
  strncpy(field, (xmlArray + chrPtr) - length, length);
  //Delimiate the last character so it terminates
  field[length] = '\0';

  //Return the read field
  return field;
}

//moves the current character pointer until it finds a non space, tab or newline character
void clearWhiteSpace()
{
  while((xmlArray[chrPtr] == ' ' || xmlArray[chrPtr] == '\t' || xmlArray[chrPtr] == '\n') && xmlArray[chrPtr] != '\0') 
  {
    chrPtr++;
  }
}


//moves the current character pointer until it finds the character passed in or gets to the end of the array
void moveToChar(char target)
{
  while(xmlArray[chrPtr] != target && xmlArray[chrPtr] != '\0') 
  {
    chrPtr++;
  }
}

//Returns a string of all of the characters until the next space or '>' character
char* getName()
{
    //First move through all the white space
  clearWhiteSpace();

  //Loop until the next space or a closing tag
  int nameLength = 0;
  while(xmlArray[chrPtr] != '>' && xmlArray[chrPtr] != ' ')
  {
    nameLength++;
    chrPtr++;
  }

  //Read the characters from the array
  char* returnValue = readFromXML(nameLength);  

  //Return the new string
  return returnValue;
}

//Gets the key/value pairs of the atributes until it 
//gets to the end of the encapsulating tag.
void getAttributes(XMLNode *xmlNode)
{
  //Clear the white space
  clearWhiteSpace();

  //Loop until we get to the end of the current tag we are in
  while(xmlArray[chrPtr] != '>')
  {
    //The attributes end with an equals sign and can not contain spaces
    int textLength = 0;
    while(xmlArray[chrPtr] != ' ' && xmlArray[chrPtr] != '=')
    {
      chrPtr++;
      textLength++;
    }

    //read the attribute
    char* attribute = readFromXML(textLength);

    //The value starts after a quotation mark after the equals sign
    moveToChar('=');
    moveToChar('"');
    chrPtr++;

    //Loop until we get to the end of the quotation mark
    textLength =0;
    while(xmlArray[chrPtr] != '"')
    {
      chrPtr++;
      textLength++;
    }

    //read the value
    char * value = readFromXML(textLength);

    //Add the attribute/value pair to the attributes of the node
    xmlAddAttribute(xmlNode, attribute, value); 

    //clean up the buffers
    free(attribute);
    free(value);
    
    //Continue moving on
    chrPtr++;
    clearWhiteSpace();
  }
}

//Gets the value of an XML declaration node
char* getDeclerationValue()
{
  //XML decleartion is the string between the question makr signs of an decleration node
  clearWhiteSpace();
  int length = 0;
  int insideQuotation = FALSE;

  //Loop until we finish reading outside of the quotes or we reach the question mark
  while((xmlArray[chrPtr] != ' ' && xmlArray[chrPtr] != '?') || insideQuotation == TRUE)
  {
    length++;
    chrPtr++;
    
    //Everything inside of quotation marks is part of the decleration
    if(xmlArray[chrPtr] == '"')
    {
      if(insideQuotation == TRUE)
      {
        insideQuotation = FALSE;
      }
      else
      {
        insideQuotation  = TRUE;
      }
    }
  }

  //read the decleration
  char *declerationvalue = readFromXML(length);

  //return the decleration that was read
  return declerationvalue;
}

//Get everything until the next node begins
char* getTextValue()
{
  //clearWhiteSpace();
  int textLength = 0;
  
  //Loop until the next tag starts
  while(xmlArray[chrPtr] != '<')
  {
    textLength++;
    chrPtr++;
  }
  
  //Read the text
  char *textValue = readFromXML(textLength);

  //Return the text that was read in
  return textValue;
}

//Creates a new decleration node based on the decleration it reads
XMLNode *readXMLDeclerationNode()
{
  char* name = getName();
  char* value = getDeclerationValue();
  XMLNode* newNode = xmlCreateNode(DECLERATION_NODE, name, value);

  free(name);
  free(value);
  return newNode;
}

//Creates a new element node based on the name it reads
XMLNode *readXMLElementNode()
{
  char* name = getName();
  XMLNode* newNode = xmlCreateNode(ELEMENT_NODE, name, NULL);
  free(name);

  return newNode;
}

//Creates a new text node based on the text that it reads
XMLNode *readXMLTextNode()
{
  char* textValue = getTextValue();
  XMLNode* newNode = xmlCreateNode(TEXT_NODE, TEXT_NAME, textValue);      
  free(textValue);

  return newNode;
}

//Passes through the xml array and adds children to currently passed in node
void parseNode(XMLNode *node)
{
  //Save off the previous chr ptr in case white spaces should be part of the text value
  int prevChrPtr = chrPtr;

  clearWhiteSpace();

  //Loop until the end of the file
  while(xmlArray[chrPtr] != '\0'){ 
    //Check if war are at the beginning of a tag
    if(xmlArray[chrPtr] == '<' && xmlArray[chrPtr + 1] != '/'){
      //Check if we are reading an xml decleration node
      chrPtr++;
      if(xmlArray[chrPtr] == '?'){
        //This muse be an xml decleration monde.  Move passed the 
        //question mark just read in and get the decleration part.
        chrPtr++;
        xmlAddChildNode(node, readXMLDeclerationNode());

        //Move to the end tab
        moveToChar('>');

        //Move past the end tab and fall off
        chrPtr++;
        clearWhiteSpace();
      }
      else{
        //This must be a regular element node.  Read the element node
        XMLNode* childNode = xmlAddChildNode(node, readXMLElementNode());

        //Get the attributes for the element
        getAttributes(childNode);
        //Move to the closing tab
        moveToChar('>');
        chrPtr++;

        //Recursivley call up and see if the current node has any children
        parseNode(childNode);
        
        //Now that we have all of its children, move past our closing tag.
        clearWhiteSpace();
                
        if(xmlArray[chrPtr] == '<'){
          moveToChar('>');

          if(xmlArray[chrPtr] == '>'){
            chrPtr++;
          }
        }
        
        //Now are are passed our closing tag.  See if we were are not at our
        //parents closing tag and, if so, fall off.
        clearWhiteSpace();

        if(xmlArray[chrPtr] == '<' && xmlArray[chrPtr + 1] == '/')
          break;
        

        //We were not the last sub element of our parent.  Loop again and add the next one as a child.
      }
    }
    else{
      //move back to the beginning of this section
      chrPtr = prevChrPtr;
      //printf("About to make text node, next 3 char are '%c%c%c'", xmlArray[chrPtr], xmlArray[chrPtr + 1], xmlArray[chrPtr + 2]);
      //We are not at the beginning of a tag, so we must be in the text
      //the node.  Read its text and fall off.
      xmlAddChildNode(node, readXMLTextNode());
      break;
    }
  }
}

//Reads in the file passed in and returns it as an XML node
XMLNode *xmlRead(char* file)
{
	chrPtr = 0;
  loadXMLFile(file);
  XMLNode *docNode = xmlCreateNode(DOCUMENT_NODE, DOCUMENT_NAME, NULL); 
  
  parseNode(docNode);
  
  free(xmlArray);
 
  return docNode;
}


