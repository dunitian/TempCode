using System;
using System.Threading;
using System.Threading.Tasks;

namespace _05.TaskB
{
    /// <summary>
    /// 在用Result的时候，内部会调用Wait（这个时候Wait其实就可以省略了）
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("主线程启动~~~{0}\n", Thread.CurrentThread.ManagedThreadId);
            int num = 1000;
            Console.WriteLine("计算前：{0}\n", num);
            Task<int> task = new Task<int>(n => GetNum(Convert.Toint(n)), num);
            task.Start();
            //task.Wait();//在一个线程调用Wait方法时，系统会检查线程要等待的Task是否已经开始执行，如果任务正在执行，那么这个Wait方法会使线程阻塞，知道Task运行结束为止
            Console.WriteLine("计算后：{0}\n", task.Result);//在用Result的时候，内部会调用Wait
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
