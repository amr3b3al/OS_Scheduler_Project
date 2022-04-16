
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
        public static float schedule_prem(List<Process> p, int n)
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\Users\\amrmo\\source\\repos\\OS_Scheduler_Project\\CPU_Scheuler\\gantt_input.txt");
            StringBuilder ganttInput = new StringBuilder();
            float currentTime = p[0].getArrivalTime(); ;
            float nextArrivalTime = p[0].getArrivalTime();
            float TurnAroundTime = 0;
            //point to the start and end of newely arrived processes
            int turn = 0;
            int left = 0;
            int completed = 0; //track the number of processes that finished execution 
            //heap that stores processes according to their burst time (lower on top)
            PriorityQueue<Process, float> priorityQueue = new PriorityQueue<Process, float>();
            Process currentRuning;
            //sort the processes according to their arrival time
            p.Sort((x, y) => x.getArrivalTime().CompareTo(y.getArrivalTime()));
            while (turn < n)
            {
                //exit loop with the last process that has the same arrival time
                while (turn < n && nextArrivalTime == p[turn].getArrivalTime() )
                {
                    turn++;
                }
                if (turn < n)
                    nextArrivalTime = p[turn].getArrivalTime();
                //find the shortest remaining time in the current arrivals
                for (int i = left; i < turn; i++)
                {
                    priorityQueue.Enqueue(p[i], p[i].getBurstTime());
                }
                left = turn;

                while (currentTime != nextArrivalTime)
                {
                    if(priorityQueue.Count == 0)
                    {
                        currentTime = nextArrivalTime;
                        break;
                    } 
                    currentRuning = priorityQueue.Dequeue();
                    ganttInput.Append(currentRuning.getPid() + "_" + currentTime + "_");
                    if ((currentRuning.getBurstTime()) > (nextArrivalTime - currentTime))
                    {
                        currentRuning.setBurstTime(currentRuning.getBurstTime() - (nextArrivalTime - currentTime));
                        priorityQueue.Enqueue(currentRuning, currentRuning.getBurstTime());
                        currentTime = nextArrivalTime;
                    }
                    else
                    {
                        currentRuning.setCompletetionTime(currentRuning.getBurstTime()+currentTime);
                        TurnAroundTime += (currentRuning.getCompletetionTime() - currentRuning.getArrivalTime());
                        completed++;
                        currentTime = currentRuning.getBurstTime() + currentTime;
                    }
                    ganttInput.Append(currentTime + "\n");
                    
                }
            }
            while (completed != n)
            {
                currentRuning = priorityQueue.Dequeue();
                ganttInput.Append(currentRuning.getPid() + "_" + currentTime + "_");
                currentTime += currentRuning.getBurstTime();
                currentRuning.setCompletetionTime(currentTime);
                TurnAroundTime += (currentRuning.getCompletetionTime() - currentRuning.getArrivalTime());
                completed++;
                ganttInput.Append(currentTime + "\n");
            }
            File.WriteAllText(path, ganttInput.ToString());

            return TurnAroundTime;
        }

    }
}
