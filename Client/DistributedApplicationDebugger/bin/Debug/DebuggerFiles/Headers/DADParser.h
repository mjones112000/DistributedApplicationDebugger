#ifndef DAD_PARSER_H_INCLUDED
#define DAD_PARSER_H_INCLUDED
#include "collections.h"

#define SOH '\x01'
#define EOT '\x04'
#define PARTITION_CHR '|'
#define PARTITION_STR "|"
#define CONSOLE_HEADER_STR "CONSOLE"
#define GDB_HEADER_STR "GDB"
#define PRE_HEADER_STR "PRE"
#define POST_HEADER_STR "POST"
#define NODE_ID_HEADER_STR "NODE ID"

void ParseInputBuffer(charList* buffer, queue* result, int trimControlChars);
void SendIdData(int writeSocket, int nodeId, int processId);

#endif
