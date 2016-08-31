using System;
using System.Threading;

namespace _03.MyThreadPool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程启动~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            ThreadPool.QueueUserWorkItem(RunMe);
            ThreadPool.QueueUserWorkItem(RunMe2,111);
            Thread.Sleep(500);
            Console.WriteLine("主线程结束~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Console.Read();
        }

        private static void RunMe2(object state)
        {
            Console.WriteLine(state);
            Console.WriteLine("子线程2启动~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            Console.WriteLine("子线程2结束~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
        }

        private static void RunMe(object state)
        {
            Console.WriteLine(state);
            Console.WriteLine("子线程启动~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);
            Console.WriteLine("子线程结束~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
