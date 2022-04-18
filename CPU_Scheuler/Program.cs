

namespace CPU_Scheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Process p1 =  new Process(1,0,8);
            Process p2 = new Process(2, 1, 4);
            Process p3 = new Process(3, 2, 9);
            Process p4 = new Process(4, 3, 5);

            p1.setPriority(3);
            p2.setPriority(0);
            p3.setPriority(1);
            p4.setPriority(1);

            List<Process> processes = new List<Process>
            {
                p1,p2,p3,p4

            };



            Console.WriteLine("pid : completetion time");
            Console.WriteLine(SJF.schedule_prem(processes, 4 , true));

            
          
        }
    }
}
