using System;
using System.Threading;
using System.Threading.Tasks;

namespace _08.TaskEAttachedToParent
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程开始：{0}\n", Thread.CurrentThread.ManagedThreadId);
            Task<int[]> tasks = new Task<int[]>(() =>
            {
                var results = new int[3];

                new Task(() => results[0] = GetNum(1), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[1] = GetNum(2), TaskCreationOptions.AttachedToParent).Start();
                new Task(() => results[2] = GetNum(3), TaskCreationOptions.AttachedToParent).Start();

                return results;

            });
            tasks.Start();

            var task = tasks.ContinueWith(t => Array.ForEach(tasks.Result, n => Console.WriteLine("线程ID：{0},计算结果：{1}", Task.CurrentId, n)));

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
