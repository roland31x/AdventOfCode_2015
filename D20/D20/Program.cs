namespace D20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = 33100000;

            int[] fq = new int[input / 10];
            int[] fq2 = new int[input / 10];
            for(int i = 1; i < fq.Length; i++)
            {
                for (int j = 1; i * j < fq2.Length && j <= 50; j++)
                {
                    fq2[i * j] += i * 11;
                }
                for (int j = 1; i * j < fq.Length; j++)
                {
                    fq[i * j] += i * 10;
                }
            }
            bool p1 = false;
            bool p2 = false;
            for (int i = 1; i < fq.Length; i++)
            {
                if (fq[i] >= input && !p1)
                {
                    Console.WriteLine("Part 1 solution:");
                    Console.WriteLine(i);
                    p1 = true;
                }
                if (fq2[i] >= input && !p2)
                {
                    Console.WriteLine("Part 2 solution:");
                    Console.WriteLine(i);
                    p2 = true;
                }
                if (p1 && p2)
                    break;
            }
        }
    }
}