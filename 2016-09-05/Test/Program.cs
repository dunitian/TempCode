using LoTLib.Word.Split;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "bootstrap-datetimepicker 进一步跟进~~~开始时间和结束时间的样式显示";
            Console.WriteLine("\n精确模式-带HMM：\n");
            Console.WriteLine(str.GetSplitWordStr());

            Console.WriteLine("\n全模式：\n");
            Console.WriteLine(str.GetSplitWordStr(JiebaTypeEnum.CutAll));

            Console.WriteLine("\n搜索引擎模式：\n");
            Console.WriteLine(str.GetSplitWordStr(JiebaTypeEnum.CutForSearch));

            Console.WriteLine("\n精确模式-不带HMM：\n");
            Console.WriteLine(str.GetSplitWordStr(JiebaTypeEnum.Other));

            Console.ReadKey();
        }
    }
}
