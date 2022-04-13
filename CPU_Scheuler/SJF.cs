

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
           



        }
    }
}
