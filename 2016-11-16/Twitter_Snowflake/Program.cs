using System;

namespace Twitter_Snowflake
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine($"开始执行 {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffffff")}------{Snowflake.Instance().GetId()} \n");
            }
            Console.ReadKey();
        }
    }
}
