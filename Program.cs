using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJanken
{
    internal class Program
    {
        /// <summary>
        ///Main
        /// </summary>
        static void Main(string[] args)
        {
            Console.WriteLine("***********************");
            Console.WriteLine("*   じゃんけんゲーム  *");
            Console.WriteLine("***********************");
            Console.WriteLine("次の番号を入力してください");
            Console.WriteLine("1:ゲーム開始");
            Console.WriteLine("9:ゲーム終了");

            while(true)
            {
                var keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        JankenProc jProc = new JankenProc();
                        jProc.Janken();
                        break;

                    case ConsoleKey.D9:
                        Console.Clear();
                        Console.WriteLine("ゲーム終了!");
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
