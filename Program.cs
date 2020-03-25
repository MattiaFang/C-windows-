using System;

namespace Example03
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine();//输入一个数
            var x = Convert.ToInt32(s);//字符串类型转换整数
            for (int i = 0; i < x+1; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
