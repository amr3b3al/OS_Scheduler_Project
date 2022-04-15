

namespace CPU_Scheduler
{
    public class SJF : SchedullingAlgorithm
    {
        public static float schedule(List<Process> p,int n)
        {
            //sorting processes according to arrival time
            p.Sort((x, y) => x.getArrivalTime().CompareTo(y.getArrivalTime()));
       
            int counter = 0;
            float currentTime = 0;
            float totalWaitingTime = 0;
            
            while (true)
            {
                
                if (counter == n) return totalWaitingTime/n;
                float minB = 1000;
                int min=0;
                int i=0;
                bool check = true;
                for (i=0; i < n; i++)
                {
                    
                    
                    if(!(p[i].done)&&(p[i].getArrivalTime()<=currentTime))
                    {
                        check = false;
                        if(p[i].getBurstTime() <minB)
                        {
                            minB= p[i].getBurstTime();  
                            min = i;
                        }
                        
                    }
                   
                }
                if (check) {
                    currentTime = currentTime + 1;
                    continue;
                }
                currentTime = currentTime + p[min].getBurstTime();
                p[min].done = true;
                p[min].setCompletetionTime(currentTime);
                p[min].setWaitingTime(currentTime-(p[min].getArrivalTime()+p[min].getBurstTime()));
                totalWaitingTime = totalWaitingTime + p[min].getWaitingTime();
                Console.WriteLine("  "+p[min].getPid()+" : "+p[min].getCompletetionTime());
                counter++;
            }
           public static void schedule_prem(List<Process> p, int n)
        {
            float currentTime = p[0].getArrivalTime(); ;
            float nextArrivalTime = p[0].getArrivalTime();
            //point to the end of the arrived processes
            int turn = 0;
            int left = 0;
            //number of completed processes
            int completed = 0;
            //heap
            PriorityQueue<Process, float> priorityQueue = new PriorityQueue<Process, float>();
            Process currentRuning;
            //sort the processes according to their arrival time
            p.Sort((x, y) => x.getArrivalTime().CompareTo(y.getArrivalTime()));
            while (true)
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
                    if ((currentRuning.getBurstTime()) > (nextArrivalTime - currentTime))
                    {
                        currentRuning.setBurstTime(currentRuning.getBurstTime() - (nextArrivalTime - currentTime));
                        priorityQueue.Enqueue(currentRuning, currentRuning.getBurstTime());
                        currentTime = nextArrivalTime;
                    }
                    else
                    {
                        currentRuning.setCompletetionTime(currentRuning.getBurstTime()+currentTime);
                        completed++;
                        currentTime = currentRuning.getBurstTime() + currentTime;
                    }
                    
                }
                if (turn >= n)
                {
                    break;
                }
            }
            while (completed != n)
            {
                currentRuning = priorityQueue.Dequeue();
                currentTime += currentRuning.getBurstTime();
                currentRuning.setCompletetionTime(currentTime);
                completed++;
            }
        }



        }
    }
}
