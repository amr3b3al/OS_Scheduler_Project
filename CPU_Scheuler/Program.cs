

namespace CPU_Scheduler
{
    public class Program
    {
        public static void Main(String[] args)
        {
           Process p1=  new Process(1,0,8);
            Process p2 = new Process(2, 0.3f, 4);
            Process p3 = new Process(3, 13.5f, 9);
            Process p4 = new Process(4, 23.3f, 5);
           

            List<Process> processes = new List<Process>();     
            processes.Add(p2);
            processes.Add(p1);
            
            processes.Add(p3);
            processes.Add(p4);

           
            Console.WriteLine("pid : completetion time");
            SJF.schedule_prem(processes, 4);
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("  "+processes[i].getPid() +" : "+ processes[i].getCompletetionTime());
            }
            
            
          
        }
    }
}
