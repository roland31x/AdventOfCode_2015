using System.ComponentModel;

namespace D24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<long> weights = new List<long>();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    weights.Add(long.Parse(sr.ReadLine()));
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(Calc(weights, 3));
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(Calc(weights, 4));
        }
        static long Calc(List<long> weights, int howmanygroups)
        {
            long best = -1;
            long sums = 0;
            for(int i = 0; i < weights.Count; i++)
                sums += weights[i];
            sums /= howmanygroups;
            for(int i = 1; i < weights.Count - 1; i++)
            {
                long[] output = new long[i];
                Back(0, output.Length, 0, weights, output, ref best, sums);
                if (best != -1)
                    return best;
            }
            return best;
        }
        static void Back(int k, int n, int start, List<long> data, long[] output, ref long best, long sumtosearch)
        {           
            if(k >= n)
            {                
                long sum = 0;
                for (int i = 0; i < n; i++)
                    sum += output[i];
                if(sum == sumtosearch)
                {
                    sum = 1;
                    for (int i = 0; i < n; i++)
                    {
                        checked
                        {
                            sum *= output[i];
                        }
                    }
                    if(best == -1)
                    {
                        best = sum;
                    }    
                    if (sum < best)
                        best = sum;
                }
            }
            else
            {
                for(int i = start; i < data.Count; i++)
                {
                    output[k] = data[i];
                    Back(k + 1, output.Length, i + 1, data, output, ref best, sumtosearch);
                }
            }
        }
    }
}