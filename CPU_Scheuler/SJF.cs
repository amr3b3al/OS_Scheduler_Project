
using System.Text;
namespace CPU_Scheduler
{
    public class SJF : SchedullingAlgorithm
    {
        private static float computeTotalBurst(List<Process>p)
        {
            float totalBurst = 0;
            for(int i = 0; i <p.Count;i++)
            {
                totalBurst=totalBurst+p[i].getBurstTime();
            }
            return totalBurst;  
        }
        public static float schedule(List<Process> p, int n)
        {
            //sorting processes according to arrival time
            p.Sort((x, y) => x.getArrivalTime().CompareTo(y.getArrivalTime()));
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\amrmo\\source\\repos\\OS_Scheduler_Project\\CPU_Scheuler\\gantt_input.txt");
            StringBuilder ganttInput = new StringBuilder();
            int counter = 0;
            float currentTime = 0;
            float totalWaitingTime = 0;

            while (true)
            {

                if (counter == n)
                {
                    File.WriteAllText(path, ganttInput.ToString());
                    return totalWaitingTime / n;
                }
                float minB = SJF.computeTotalBurst(p);
                int min = 0;
                int i = 0;
                bool check = true;
                for (i = 0; i < n; i++)
                {
                    if (!(p[i].done) && (p[i].getArrivalTime() <= currentTime))
                    {
                        check = false;
                        if (p[i].getBurstTime() < minB)
                        {
                            minB = p[i].getBurstTime();
                            min = i;
                        }
                    }
                }
                if (check)
                {
                    for(int j = 0; j < n; j++)
                    {
                        if (!p[j].done)
                        {
                            currentTime = p[j].getArrivalTime();
                            break;
                        }
                    }
                    continue;
                }
                ganttInput.Append(p[min].getPid() + "_" + currentTime + "_");
                currentTime = currentTime + p[min].getBurstTime();
                ganttInput.Append(currentTime + "\n");
                p[min].done = true;
                p[min].setCompletetionTime(currentTime);
                p[min].setWaitingTime(currentTime - (p[min].getArrivalTime() + p[min].getBurstTime()));
                totalWaitingTime = totalWaitingTime + p[min].getWaitingTime();
                //Console.WriteLine("  " + p[min].getPid() + " : " + p[min].getCompletetionTime());
                counter++;
            }
        }


        //p is list of process waiting to be served
        //n is the size of that list
        //type 1 for sjf , type 0 for piriority 
        public static float schedule_prem(List<Process> p, int n , bool type)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gantt_input.txt");
            StringBuilder ganttInput = new();
            float TurnAroundTime = 0;
            //point to the start and end of newely arrived processes
            int turn = 0;
            int left = 0;
            int completed = 0; //track the number of processes that finished execution 
            //heap that stores processes according to their burst time (lower on top)
            List<Process> priorityQueue = new();
            Process currentRuning;
            //sort the processes according to their arrival time
            p.Sort((x, y) => x.getArrivalTime().CompareTo(y.getArrivalTime()));
            float currentTime = p[0].getArrivalTime(); ;
            float nextArrivalTime = p[0].getArrivalTime();
            while (turn < n)
            {
                //exit loop with the index of last process that has the same arrival time
                while (turn < n && nextArrivalTime == p[turn].getArrivalTime() )
                {
                    turn++;
                }
                //if we are currently at the last process the turn will be out of range hence the condition
                if (turn < n)
                    nextArrivalTime = p[turn].getArrivalTime();
                for (int i = left; i < turn; i++)
                {
                    Add(priorityQueue, p[i], type);
         
                }
                left = turn;

                while (currentTime != nextArrivalTime)
                {
                    //handle the idle period cases between two sets of processes in which one set finishes before another set arrives
                    //if at any moment the heap becomes empty the current time is updated to become the arrival time of the next set of processes
                    if(priorityQueue.Count == 0)
                    {
                        currentTime = nextArrivalTime;
                        break;
                    }

                    //currentRuning = priorityQueue.Dequeue();
                    currentRuning = priorityQueue[0];
                    priorityQueue.RemoveAt(0);

                    //record the strating time of current process in a text file for drawing gantt chart
                    ganttInput.Append(currentRuning.getPid() + "_" + currentTime + "_");
                    if ((currentRuning.getBurstTime()) > (nextArrivalTime - currentTime))
                    {
                        currentRuning.setBurstTime(currentRuning.getBurstTime() - (nextArrivalTime - currentTime));
                        Add(priorityQueue, currentRuning, type);
                        currentTime = nextArrivalTime;
                    }
                    else
                    {
                        currentRuning.setCompletetionTime(currentRuning.getBurstTime()+currentTime);
                        TurnAroundTime += (currentRuning.getCompletetionTime() - currentRuning.getArrivalTime());
                        completed++;
                        currentTime = currentRuning.getBurstTime() + currentTime;
                    }
                    //record the finishing time of current process
                    ganttInput.Append(currentTime + "\n");
                    
                }
            }
            //run the rest of processes after they finish
            while (completed != n)
            {
                currentRuning = priorityQueue[0];
                priorityQueue.RemoveAt(0);
                ganttInput.Append(currentRuning.getPid() + "_" + currentTime + "_");
                currentTime += currentRuning.getBurstTime();
                currentRuning.setCompletetionTime(currentTime);
                TurnAroundTime += (currentRuning.getCompletetionTime() - currentRuning.getArrivalTime());
                completed++;

                ganttInput.Append(currentTime + "\n");
            }

            //the path is in another directory
            File.WriteAllText(path, ganttInput.ToString());

            //divide the returned value by n to get avg
            //subtract the sum of bursts of processes in the current list to get the waiting time
            return TurnAroundTime;
        }




        private static void Add(List<Process> processes, Process p,bool type) { 
            processes.Add(p);
            if (type)
            {
                processes.Sort((x, y) => x.getBurstTime().CompareTo(y.getBurstTime()));

            }
            else {
                processes.Sort((x, y) => x.getPriority().CompareTo(y.getPriority()));

            }
        }
    }
}
