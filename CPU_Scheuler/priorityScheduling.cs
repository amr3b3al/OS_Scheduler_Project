

using System.Text;

namespace CPU_Scheduler
{
    public class priorityScheduling : SchedullingAlgorithm
    {

        private static int getHighestPriority(List<Process> p)
        {
            int max = p[0].getPriority();
            for (int i = 1; i < p.Count; i++)
            {
                if (p[i].getPriority() >= max) max = p[i].getPriority();
            }
            return max;
        }
        public static float schedulePriority_nonPreem(List<Process> p, int n)
        {
            p.Sort((x, y) => x.getArrivalTime().CompareTo(y.getArrivalTime()));
            int x = getHighestPriority(p);
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
                int priority = x;
                int min = 0;
                int i = 0;
                bool check = true;
                for (i = 0; i < n; i++)
                {
                    if (!(p[i].done) && (p[i].getArrivalTime() <= currentTime))
                    {
                        check = false;
                        if (p[i].getPriority() <= priority)
                        {
                            priority = p[i].getPriority();
                            min = i;
                        }
                    }
                }
                if (check)
                {
                    for (int j = 0; j < n; j++)
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
                Console.WriteLine("  " + p[min].getPid() + " : " + p[min].getCompletetionTime());
                counter++;

            }
        }
    }
}
