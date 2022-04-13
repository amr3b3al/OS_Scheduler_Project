

namespace CPU_Scheduler
{
    public class Process
    {
       private int pid;
       public bool done;
       private float arrivalTime;
       private float burstTime;
       private float waitingTime;
        private float completetionTime;
        public Process() { }
        public Process(int pid,float arrival,float burst)
        {
        this.pid = pid; 
            this.arrivalTime = arrival;
            this.burstTime = burst;

        }
        public int getPid() { return pid; }
        public float getArrivalTime() { return arrivalTime; }
        public float getBurstTime() { return burstTime; }
        public float getCompletetionTime() { return completetionTime; }
        public float getWaitingTime() { return waitingTime; } 
        public void setCompletetionTime(float time) {this.completetionTime = time; }
        public void setPid(int pid) { this.pid = pid; }
        public void setArrivalTime(float arrivalTime) { this.arrivalTime = arrivalTime; }   
        public void setBurstTime(float burstTime) { this.burstTime = burstTime; }
        public void setWaitingTime(float waitingTime) { this.waitingTime = waitingTime; }
        
    }
}
