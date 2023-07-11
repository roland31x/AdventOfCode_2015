namespace D01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                string line = sr.ReadLine()!;
                int sum = 0;
                int firstbase = -1;
                for(int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '(')
                    {
                        sum++;
                    }
                    else
                    {
                        sum--;
                    }
                    if(sum < 0 && firstbase == -1)
                    {
                        firstbase = i;
                    }
                }
                Console.WriteLine("Part 1 solution:");
                Console.WriteLine(sum);
                Console.WriteLine("Part 2 solution:");
                Console.WriteLine(firstbase + 1); // indexing from 1 
            }
        }
    }
}