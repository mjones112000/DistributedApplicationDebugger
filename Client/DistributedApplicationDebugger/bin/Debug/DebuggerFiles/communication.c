#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <netdb.h>           
#include <sys/socket.h>
#include <errno.h>
#include <arpa/inet.h>
#include "Headers/communication.h"
#include "Headers/booleanLogic.h"

void InitializeSockAddr(struct sockaddr_in *theAddress, in_addr_t path, int port);

//Creates an incoming connection on the port passed in
int CreateIncomingConnection(int port)
{
	//Create a socket for the binding process
  int bindingSocket = socket(AF_INET, SOCK_STREAM, 0);
  SetSocketOptions(&bindingSocket);

	//Bind to the port passed in
	struct sockaddr_in incomingAddress;
  InitializeSockAddr(&incomingAddress,INADDR_ANY,port);  
	if(bind(bindingSocket, (struct sockaddr *) &incomingAddress,sizeof(incomingAddress)) < 0)
 		return FALSE;

	//Listen for 1 connection on the incoming port	
  listen(bindingSocket,1);
  
  //Accept the connection
  socklen_t clilen = sizeof(incomingAddress);
  int acceptedSocket = accept(bindingSocket, (struct sockaddr *) &incomingAddress, &clilen);
  SetSocketOptions(&acceptedSocket);
  
  //No need to listen for any more connections
  close(bindingSocket);

	//Check if their were any problems with the incoming connection
  if (acceptedSocket < 0)
    return FALSE;
	
	//Return the connection
  return acceptedSocket;   
}

//Connects to the path passed in on the port passed in.
int CreateOutgoingConnection(char* path, int port)
{	
	//Create a connection to connect to
  int connectedSocket =socket(AF_INET,SOCK_STREAM,0);
	SetSocketOptions(&connectedSocket);
  
  //Initialize the destination address
  struct sockaddr_in outgoingAddress;
  InitializeSockAddr(&outgoingAddress,inet_addr(path),port);
  
  //Establish the connection
  int status = connect(connectedSocket, (struct sockaddr *)&outgoingAddress, sizeof(outgoingAddress));
	
	//Check if there was an error in the connection
  if(status < 0)
  	return FALSE;
  
  //Return the connection	
  return connectedSocket;
}


//Sets the socket to not linger after connection has ended
void SetSocketOptions(int* socketPtr)
{
	int theSocket = *socketPtr;	

  struct linger so_linger;
  
  so_linger.l_onoff = TRUE;
  so_linger.l_linger = 0;
  setsockopt(theSocket, SOL_SOCKET, SO_LINGER, &so_linger, sizeof so_linger);
 }

//Initailizes the address's port patha dn port to the values passed in
void InitializeSockAddr(struct sockaddr_in *theAddress, in_addr_t path, int port)
{
  bzero(theAddress,sizeof(*theAddress));
  theAddress->sin_family = AF_INET;
  theAddress->sin_addr.s_addr=path;
  theAddress->sin_port=htons(port);
}

//Determine if the string passed in is formated as a valid ip address, taken from
//http://stackoverflow.com/questions/791982/determine-if-a-string-is-a-valid-ip-address-in-c
int isValidIpAddress(char *ipAddress)
{
  //inet_pton return if the address does not contain a charaacter string representing a valid network address.
  struct sockaddr_in sa;
  if (inet_pton(AF_INET, ipAddress, &(sa.sin_addr)) == 0)
    return FALSE;
  else
    return TRUE;
}

//Code to get internal ip address, taken from 
//http://stackoverflow.com/questions/212528/linux-c-get-the-ip-address-of-local-computer
void GetPrimaryIp(char* buffer, size_t buflen) 
{
  int sock = socket(AF_INET, SOCK_DGRAM, 0);

  const char* kGoogleDnsIp = "8.8.8.8";
  uint16_t kDnsPort = 53;
  struct sockaddr_in serv;
  memset(&serv, 0, sizeof(serv));
  serv.sin_family = AF_INET;
  serv.sin_addr.s_addr = inet_addr(kGoogleDnsIp);
  serv.sin_port = htons(kDnsPort);

  
  int err = connect(sock, (const struct sockaddr *) &serv, sizeof(serv));

  struct sockaddr_in name;
  socklen_t namelen = sizeof(name);
  err = getsockname(sock, (struct sockaddr*) &name, &namelen);

  inet_ntop(AF_INET, &name.sin_addr, buffer, buflen);

  close(sock);
}

//Gets IP address from a domain name
//Code taken from http://www.binarytides.com/blog/get-ip-address-from-hostname-in-c-using-linux-sockets/ 
int hostname_to_ip(char *hostname , char *ip)
{ 
	struct addrinfo hints, *servinfo, *p;
	struct sockaddr_in *h;
	int rv;

	memset(&hints, 0, sizeof hints);
	hints.ai_family = AF_UNSPEC; // use AF_INET6 to force IPv6
	hints.ai_socktype = SOCK_STREAM;

	if ( (rv = getaddrinfo( hostname , "http" , &hints , &servinfo)) != 0) 
	{
		fprintf(stderr, "getaddrinfo: %s\n", gai_strerror(rv));
		return 1;
	}

	// loop through all the results and connect to the first we can
	for(p = servinfo; p != NULL; p = p->ai_next) 
	{
		h = (struct sockaddr_in *) p->ai_addr;
		strcpy(ip , inet_ntoa( h->sin_addr ) );
	}
	
	freeaddrinfo(servinfo); // all done with this structure
	return 0;
}

//Writes the buffer to the file descriptor and ignores the result
void Write(int fd, const void *buf, size_t count)
{
	if(write (fd, buf, count)< 0)
		return;
}

