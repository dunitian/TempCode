using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Snowflake.Net
{
    class Program
    {
        /// <summary>
        /// Task完成总数
        /// </summary>
        private static int taskCount = 0;
        private static List<long> list = new List<long>();

        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                Task.Run(() => GetUid());
                Task.Run(() => GetUid());
            }

            Console.WriteLine("稍等10s");
            Task.Run(() => Printf());
            Console.ReadKey();
        }

        private static void GetUid()
        {
            for (int i = 0; i < 50000; i++)
            {
                var id = Snowflake.Init().GetId();
                //Console.WriteLine($"开始执行 {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff")}------{id} \n");
                list.Add(id);
            }
            taskCount++;
        }

        private static void Printf()
        {
            while (taskCount != 6)
            {
                Thread.Sleep(1000);
                Console.WriteLine("------");
            }
            Console.WriteLine(list.Count);
            Console.WriteLine(list.Distinct().Count());
        }
    }
}
