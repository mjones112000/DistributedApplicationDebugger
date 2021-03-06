#ifndef _COMMUNICATION_
#define _COMMUNICATION_

int CreateIncomingConnection(int port);
int CreateOutgoingConnection(char* path, int port);
void SetSocketOptions(int* socketPtr);
void InitializeSockAddr(struct sockaddr_in *theAddress, in_addr_t path, int port);
int isValidIpAddress(char *ipAddress);
void GetPrimaryIp(char* buffer, size_t buflen);
int hostname_to_ip(char *hostname , char *ip);
#endif
