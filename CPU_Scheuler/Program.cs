

namespace CPU_Scheduler
{
    public class Program
    {
        public static void Main(String[] args)
        {
           Process p1=new Process(1,0,7);
            Process p2 = new Process(2, 3, 2);
            Process p3 = new Process(3, 3, 5);
            Process p4 = new Process(4, 5, 3);
            Process p5 = new Process(5, 4, 1);

            List<Process> processes = new List<Process>();      
            processes.Add(p1);
            processes.Add(p2);
            processes.Add(p3);
            processes.Add(p4);
            processes.Add(p5);
            Console.WriteLine("pid : completetion time");
            Console.WriteLine(SJF.schedule(processes, 5));
            
          
        }
    }
}
