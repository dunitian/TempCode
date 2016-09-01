using System;
using System.Threading;
using System.Threading.Tasks;

namespace _06.TaskException
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程启动~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            CancellationTokenSource cts = new CancellationTokenSource(10);//设置10毫秒后取消，看看抛不抛异常
            Task<int> task = new Task<int>(n => GetNum(Convert.Toint(n)), 1000, cts.Token);
            task.Start();
            try
            {
                Console.WriteLine("计算结果:{0}\n", task.Result);//如果任务已经取消了，Result会抛出AggregateException
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
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
