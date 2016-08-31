using System;
using System.Threading;

namespace _02.BackThread
{
    /// <summary>
    /// 前台的好处是，你可以保证你的后台线程能执行完毕，后台线程的好处是，你不用管它的执行
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程开始执行~~~{0}\n", Thread.CurrentThread.ManagedThreadId);

            Thread thread = new Thread(SonThread);
            thread.IsBackground = true;//默认前台线程
            thread.Start();

            Console.WriteLine("主线程执行完毕~~ ~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }

        private static void SonThread()
        {
            Console.WriteLine("子线程开始执行~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(5000);//模拟子线程操作
            Console.WriteLine("子线程执行完毕~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
