using System.Threading.Tasks.Sources;

namespace D17
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<int> containers = new List<int>();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    containers.Add(int.Parse(sr.ReadLine()!));
                }
            }
            int[] cont = new int[containers.Count];
            for(int i = 0; i < cont.Length; i++)
            {
                cont[i] = containers[i];
            }
            int score = 0;
            int scorep2 = 0;
            int minAmount = -1;
            for(int i = 1; i < cont.Length; i++)
            {
                int[] output = new int[i];
                Back(0, output.Length, 0, cont, output, ref score, ref minAmount, ref scorep2);
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(score);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(scorep2);
        }
        static void Back(int k, int n, int start, int[] data, int[] output, ref int count, ref int minCont, ref int countp2)
        {
            if(k >= n)
            {
                int sc = 0;
                for(int i = 0; i < n; i++)
                {
                    sc += output[i];
                }
                if (sc == 150)
                {
                    if (minCont == -1)
                        minCont = output.Length;
                    if (output.Length == minCont)
                            countp2++;
                    count++;
                }
                    
            }
            else
            {
                for(int i = start; i < data.Length; i++)
                {
                    output[k] = data[i];
                    Back(k + 1, n, i + 1, data, output, ref count, ref minCont, ref countp2);
                }
            }
        }
    }

}