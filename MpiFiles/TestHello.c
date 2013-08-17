#include <stdio.h>
#include <stdlib.h>
#include "Headers/mpi.h"

int main(int argc, char *argv[]) {
  int noSlaves, size, rank, x=-1;
  MPI_Status status;
  MPI_Request req1,req2;
  char hostname[256];
  //  gethostname(hostname,255);
  
  MPI_Init(&argc, &argv);
  
  MPI_Comm_rank(MPI_COMM_WORLD, &rank);
  MPI_Comm_size(MPI_COMM_WORLD, &size);

  printf("Hello from process with rank = %d on %s\n", rank, "??");
  MPI_Send(&rank, 1, MPI_INT, (rank+1)%size, 1, MPI_COMM_WORLD); 


  MPI_Recv(&x, 1, MPI_INT, (rank+size-1)%size, 1, MPI_COMM_WORLD, &status);


  
  // go do something else here


  printf("That still leaves about 68,000 American troops still in the nation, as was the case in late 2008. And violence continues to rage in parts of Afghanistan, including numerous high-profile green-on-blue attacks of late in which men dressed in Afghan police and military uniforms open fire on other Afghan security officers and coalition forces. If all goes to plan, the withdrawal of U.S. troops will continue as more security responsibilities are handed over to Afghan authorities. NATO leaders this May signed off on Obama's exit strategy that calls for an end to combat operations next year and the withdrawal of the U.S.-led international military force by the end of 2014. After that, a new and different NATO mission will advise, train and assist an expected 350,000-strong Afghanistan force, NATO Secretary General Anders Fogh Rasmussen has said. Obama committed to the surge following years in which the U.S. government poured troops and resources into the war in Iraq, giving the Taliban time to rebuild and start retaking their traditional stronghold in southern Afghanistan. At the time, progress toward having Afghan forces prepared to take over security in their country had seemingly stalled. Earlier this month, the deputy commander of international forces in Afghanistan said NATO-led forces had made progress. Yet Lt. Gen. James L. Terry also noted the continued efforts by insurgents to divide the coalition from our Afghan partners.One example of how the insurgency can strike, seemingly at will and around the country, is last week's brazen assault on a coalition base in southern Afghanistan that killed two U.S. troops and destroyed six coalition fighter jets, as well as a suicide attack in Kabul on Tuesday that killed 12 people. Violence rages as surge troops depart Afghanistan The senior combat leader in Afghanistan, Gen. John Allen, said last month that insurgent violence in southern Afghanistan was down 3 percent over 2011 levels, but admitted it was not statistically significant. However, while violence levels may not have changed much, Allen said the significant change was where the violence had moved. We have pushed hard on the insurgency to push them out of the population centers, much of which was cleared last year, and we've continued to push them into an increasingly smaller series of areas, districts, where we have, in many respects, contained them, said Allen, head of NATO's International Security Assistance Force. Mark Jacobson, a senior fellow at the Washington-based analyst organization German Marshall Fund, positively pointed to signs that the insurgency is not the monolithic structure it was back in 2009, when people said they were at the gates of Kabul.");

  MPI_Finalize();
  return 0;

}
