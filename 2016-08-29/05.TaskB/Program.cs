using System;
using System.Threading;
using System.Threading.Tasks;

namespace _05.TaskB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程启动~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Task<int> task = new Task<int>(n => GetNum((int)n),null);
            task.Start();
            Thread.Sleep(500);
            Console.WriteLine("主线程结束~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Console.Read();
        }


        private static int GetNum(int n)
        {
            Console.WriteLine("子线程启动~~~{0}\n", Task.CurrentId);
            Thread.Sleep(1000);
            Console.WriteLine("子线程结束~~~{0}\n", Task.CurrentId);
            return n * 2;
        }
    }
}
