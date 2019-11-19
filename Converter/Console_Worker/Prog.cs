using System;
using System.Collections.Generic;
using System.Linq;
using static Input;
using System.Text;
using System.Threading.Tasks;

namespace Console_Worker
{
    class Prog
    {
        public void Main()
        {
            List<Work> worker = new List<Work>();
            //開始時に作成用データを受け取る
            //終了文字(path == "-1")まで読む
            while (true)
            {
                string path = NextString();
                //filestream
                if (path == "-1") break;
                int pages = NextInt();
                bool write = (NextString() == "1");
                bool add = (NextString() == "1");
                Work work = new Work(path, pages, write, add);
                worker.Add(work);
            }

            //work
            foreach (var e in worker)
            {
                e.Write();
            }

            //integ


            Console.WriteLine("Complated");
            return;
        }

        //all integrall func
    }
    class Work
    {
        private string fullpath;
        private int pages;
        private bool write;
        private bool addition;

        public Work(string path, int p, bool w, bool a)
        {
            fullpath = path;
            pages = p;
            write = w;
            addition = a;
        }

        //write func
        public async void Write()
        {
            if (!Confirm())
            {
                //失敗
                ErrorLog("erroe : 隠しファイル");
            }

            //書き込む

        }

        private bool Confirm()
        {
            //隠しファイル判定
            if (Func.HiddenJudge(fullpath)) return false;
            Func.FirstRun(fullpath);
            return true;
        }


        private void ErrorLog(string s)
        {
            Console.WriteLine(s);
        }
    }

}

public class Input
{
    public const long MOD = 1000000007;
    public const int INF = 1000000007;
    private static Queue<string> q = new Queue<string>();
    private static void Confirm() { if (q.Count == 0) foreach (var s in Console.ReadLine().Split(' ')) q.Enqueue(s); }
    public static int NextInt() { Confirm(); return int.Parse(q.Dequeue()); }
    public static long NextLong() { Confirm(); return long.Parse(q.Dequeue()); }
    public static string NextString() { Confirm(); return q.Dequeue(); }
    public static double NextDouble() { Confirm(); return double.Parse(q.Dequeue()); }
    public static int[] LineInt() { return Console.ReadLine().Split(' ').Select(int.Parse).ToArray(); }
    public static long[] LineLong() { return Console.ReadLine().Split(' ').Select(long.Parse).ToArray(); }
    public static string[] LineString() { return Console.ReadLine().Split(' ').ToArray(); }
    public static double[] LineDouble() { return Console.ReadLine().Split(' ').Select(double.Parse).ToArray(); }
}