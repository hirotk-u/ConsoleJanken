using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleJanken
{
    internal class JankenProc
    {
        /// <summary>
        ///JankenInfo
        /// </summary>
        private class JankenInfo
        {
            public JankenTe PlayerTe;
            public JankenTe CpuTe;
            public string Judge = "";
            public int WinCntPlayer = 0;
            public int WinCntCpu = 0;

            public JankenInfo(JankenTe playerTe,
                                JankenTe cpuTe,
                                string judge,
                                int winCntPlayer,
                                int winCntCpu)
            {
                PlayerTe = playerTe;
                CpuTe = cpuTe;
                Judge = judge;
                WinCntPlayer = winCntPlayer;
                WinCntCpu = winCntCpu;
            }
        }

        private enum JankenTe
        {
            G = 1,
            C,
            P
        }

        private Dictionary<ConsoleKey, JankenTe> PlayerTeDic;
        private List<JankenTe> CpuTeList;
        private List<JankenInfo> JankenInfoList;

        /// <summary>
        ///コンストラクタ
        /// </summary>
        public JankenProc()
        {
            JankenInit();
        }

        /// <summary>
        ///Janken
        /// </summary>
        public void Janken()
        {
            int jankenCnt = 1;
            int winCntPlayer = 0;
            int winCntCpu = 0;

            //Console.Clear();
            Console.WriteLine(CreateDispMsg(winCntPlayer, winCntCpu));

            while (true)
            {
                Console.Write($"{jankenCnt}回戦");
                var keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:

                        //プレイヤーの手を設定
                        string result = JankenDetail(keyInfo.Key, ref winCntPlayer, ref winCntCpu);

                        Console.WriteLine();

                        //デフォルトメッセージに現在の状況を追加
                        //string defaultMsg = CreateDispMsg(winCntPlayer, winCntCpu);
                        //Console.WriteLine(defaultMsg);
                        Console.WriteLine($"{jankenCnt}回目 結果:{result}");

                        jankenCnt++;

                        break;

                    case ConsoleKey.D9:
                        Console.Clear();
                        Console.WriteLine("ゲーム終了!");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }

                //終了判定
                if (winCntPlayer >= 3 || winCntCpu >= 3)
                {
                    string winUser = (winCntPlayer >= 3 ? "Player win!": "CPU win!");
                    Console.WriteLine(winUser);
                    Console.WriteLine("ゲーム終了!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                }
            }
        }

        /// <summary>
        ///JankenInit
        /// </summary>
        private void JankenInit()
        {
            PlayerTeDic = new Dictionary<ConsoleKey, JankenTe>() {
                {  ConsoleKey.D1, JankenTe.G},
                {  ConsoleKey.D2, JankenTe.C},
                {  ConsoleKey.D3, JankenTe.P}
            };

            CpuTeList = new List<JankenTe>() { JankenTe.G, JankenTe.C, JankenTe.P };

            JankenInfoList = new List<JankenInfo>() {
                new JankenInfo(JankenTe.G, JankenTe.G, "引き分け (Player:グー, CPU:グー)", 0, 0),
                new JankenInfo(JankenTe.G, JankenTe.C, "勝ち (Player:グー, CPU:チョキ)", 1, 0),
                new JankenInfo(JankenTe.G, JankenTe.P, "負け (Player:グー, CPU:パー)", 0, 1),
                new JankenInfo(JankenTe.C, JankenTe.G, "負け (Player:チョキ, CPU:グー)", 0, 1),
                new JankenInfo(JankenTe.C, JankenTe.C, "引き分け (Player:チョキ, CPU:チョキ)", 0, 0),
                new JankenInfo(JankenTe.C, JankenTe.P, "勝ち (Player:チョキ, CPU:パー)", 1, 0),
                new JankenInfo(JankenTe.P, JankenTe.G, "勝ち (Player:パー, CPU:グー)", 1, 0),
                new JankenInfo(JankenTe.P, JankenTe.C, "負け (Player:パー, CPU:チョキ)", 0, 1),
                new JankenInfo(JankenTe.P, JankenTe.P, "引き分け (Player:パー, CPU:パー)", 0, 0)
            };
        }

        /// <summary>
        ///JankenDetail
        /// </summary>
        private string JankenDetail(ConsoleKey key,
                                    ref int winCntPlayer,
                                    ref int winCntCpu)
        {
            //CPUの手
            Random r = new Random();
            int pos = r.Next(3);
            JankenTe cpuTe = CpuTeList[pos];

            JankenInfo resultInfo =
                (from n in JankenInfoList
                 where n.PlayerTe == PlayerTeDic[key]
                 where n.CpuTe == cpuTe
                select n).FirstOrDefault();

            winCntPlayer += resultInfo.WinCntPlayer;
            winCntCpu += resultInfo.WinCntCpu;

            return resultInfo.Judge;
        }

        /// <summary>
        ///画面メッセージ作成
        /// </summary>
        private string CreateDispMsg(int winCntPlayer, int winCntCpu)
        {
            Console.Clear();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************");
            sb.AppendLine("コンピュータとじゃんけん勝負");
            sb.AppendLine("先に3本取ったほうが勝ち");
            sb.AppendLine("****************************");
            sb.AppendLine($"Player勝ち数:{winCntPlayer}, CPU勝ち数:{winCntCpu}");
            sb.AppendLine();
            sb.AppendLine("次の番号を入力してください");
            sb.AppendLine("1:グー");
            sb.AppendLine("2:チョキ");
            sb.AppendLine("3:パー");
            sb.AppendLine("9:ゲーム終了");

            return sb.ToString();
        }
    }
}
