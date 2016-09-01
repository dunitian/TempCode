using System;
using System.Threading;
using System.Threading.Tasks;

namespace _07.TaskDContinueWith
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程开始：{0}\n", Thread.CurrentThread.ManagedThreadId);
            int num = 2000;
            Console.WriteLine("计算之前：{0}\n", num);
            var task = new Task<int>(n => GetNum(Convert.ToInt32(n)), num);
            task.Start();
            //任务执行完毕后执行
            task.ContinueWith(t => Console.WriteLine("计算之后：{0}。线程ID：{1}\n", t.Result, Task.CurrentId), TaskContinuationOptions.OnlyOnRanToCompletion);
            //发生异常的时候执行
            task.ContinueWith(t => Console.WriteLine("异常信息：{0}。线程ID：{1}\n", t.Exception, Task.CurrentId), TaskContinuationOptions.OnlyOnFaulted);
            //任务取消的时候执行
            task.ContinueWith(t => Console.WriteLine("任务已经取消，线程ID：{0}\n", Task.CurrentId),TaskContinuationOptions.OnlyOnCanceled);

            task.Wait();

            Console.WriteLine("主线程结束：{0}\n", Thread.CurrentThread.ManagedThreadId);
            Console.Read();
        }

        private static int GetNum(int v)
        {
            Console.WriteLine("子线程在此：{0}\n", Task.CurrentId);
            Thread.Sleep(1000);
            Console.WriteLine("子线程走也：{0}\n", Task.CurrentId);
            return v * 2;
        }
    }
}
