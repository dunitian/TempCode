#region Test1
//using System;

//namespace Twitter_Snowflake
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            for (int i = 0; i < 1000; i++)
//            {
//                Console.WriteLine($"开始执行 {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff")}------{Snowflake.Instance().GetId()} \n");
//            }
//            Console.ReadKey();
//        }
//    }
//} 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Twitter_Snowflake
{
    class Program
    {
        private static int N = 2000000;
        private static HashSet<long> set = new HashSet<long>();
        private static Snowflake worker = new Snowflake(1, 1);
        private static int taskCount = 0;

        static void Main(string[] args)
        {
            Task.Run(() => GetID());
            Task.Run(() => GetID());
            Task.Run(() => GetID());

            Task.Run(() => Printf());
            Console.ReadKey();
        }

        private static void Printf()
        {
            while (taskCount != 3)
            {
                Console.WriteLine("...");
                Thread.Sleep(1000);
            }
            Console.WriteLine(set.Count == N * taskCount);
        }

        private static object o = new object();
        private static void GetID()
        {
            for (var i = 0; i < N; i++)
            {
                var id = worker.GetId();

                lock (o)
                {
                    if (set.Contains(id))
                    {
                        Console.WriteLine("发现重复项 : {0}", id);
                    }
                    else
                    {
                        set.Add(id);
                    }
                }

            }
            Console.WriteLine($"任务{++taskCount}完成");
        }
    }
}

