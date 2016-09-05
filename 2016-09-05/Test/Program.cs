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

            Console.WriteLine("\n====================================\n");
            string content = "【微信公众号：我为NET狂】我心向道问鼎天，红尘黄土人间游！~本号用于收集网络资源，若牵扯到版权请联系本人~ QQ:1054186320 http://pan.baidu.com/share/home?uk=1814855244&view=follow";
            Console.WriteLine(content);
            Console.WriteLine("\n提取文章关键词字符串：\n");
            Console.WriteLine(content.GetArticleKeywordStr());
            Console.ReadKey();
        }
    }
}
