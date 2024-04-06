using ConsoleApp;

namespace ConsoleApp
{
    public delegate void AlarmHandler(Object sender);
    public delegate void TickHandler(Object sender, TickEventArgs args);
    public class TickEventArgs : EventArgs
    {
        public int TickCount { get; set; }
        
    }
    public class AlarmClock
    {
        public event AlarmHandler OnAlarm;
        public event TickHandler OnTick;
        public void Start(int seconds)
        {
            Console.WriteLine("Alarm set for {0} seconds", seconds);
            for (int i = seconds; i > 0; i--)
            {
                TickEventArgs tickEventArgs = new TickEventArgs { TickCount = i };
                OnTick(this, tickEventArgs);
                Thread.Sleep(1000);
            }
            OnAlarm(this);
        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input a number of seconds: ");
            int seconds = int.Parse(Console.ReadLine());
            AlarmClock alarmClock = new AlarmClock();
            alarmClock.OnTick += (sender, e) => Console.WriteLine("Tick! Count: {0}", e.TickCounts);
            alarmClock.OnAlarm += (sender) => Console.WriteLine("Time's up!!!");
            alarmClock.Start(seconds);
        }
    }
}
