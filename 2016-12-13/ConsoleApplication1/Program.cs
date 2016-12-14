using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = "0";var b = a ?? "g";
            Console.WriteLine(b);
            Console.ReadKey();
        }
    }
}
