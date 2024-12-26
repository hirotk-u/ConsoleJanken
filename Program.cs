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
            Console.WriteLine("********************");
            Console.WriteLine("* じゃんけんゲーム *");
            Console.WriteLine("********************");
            Console.WriteLine("何本勝負にしますか？ [1～5]");

            while(true)
            {
                var keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:

                        int winCnt = (int)char.GetNumericValue(keyInfo.KeyChar);
                        JankenProc jProc = new JankenProc();
                        jProc.Janken(winCnt);
                        break;

                    default:
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
