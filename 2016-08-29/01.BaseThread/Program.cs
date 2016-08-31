using System;
using System.Threading;

namespace _01.BaseThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程在此，贼人可知吾？{0}\n", Thread.CurrentThread.ManagedThreadId);

            Thread thread = new Thread(SonThread);
            thread.Start();

            Thread.Sleep(500);//模拟主线程操作
            //thread.Join(); //等待子线程执行完再执行（可以变相理解为 await）

            Console.WriteLine("主线程：本王要去睡觉了，不跟你BB~{0}\n", Thread.CurrentThread.ManagedThreadId);
            Console.ReadKey();
        }

        private static void SonThread()
        {
            Console.WriteLine("子线程：大王叫我来寻山，尔等速速报上名来！{0}\n", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(1000);//模拟子线程操作
            Console.WriteLine("子线程：算了，我编个理由忽悠大王得了~{0}\n", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
