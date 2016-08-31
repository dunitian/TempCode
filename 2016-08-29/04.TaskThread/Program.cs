using System;
using System.Threading;
using System.Threading.Tasks;

namespace _04.TaskThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程启动~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Task task = new Task(RunMe,"我是一个数据A");
            task.Start();
            Thread.Sleep(500);
            Console.WriteLine("主线程结束~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Console.Read();
        }

        private static void RunMe(object state)
        {
            Console.WriteLine(state);
            Console.WriteLine();
            Console.WriteLine("子线程启动~~~{0}\n", Task.CurrentId);
            Thread.Sleep(1000);
            Console.WriteLine("子线程结束~~~{0}\n", Task.CurrentId);
        }
    }
}
