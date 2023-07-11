using System.Runtime.CompilerServices;

namespace D08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int literal = 0;
            int memory = 0;
            int encoded = 0;
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string tocheck = sr.ReadLine()!;
                    literal += tocheck.LiteralCount();
                    memory += tocheck.MemoryCount();
                    encoded += tocheck.EncodedCount();
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(literal - memory);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(encoded - literal);
        }
    }
    public static class StringCounter
    {
        public static int LiteralCount(this string s)
        {
            return s.Length;
        }
        public static int MemoryCount(this string s)
        {
            int count = 0;
            for(int i = 1; i < s.Length - 1; i++)
            {
                if (s[i] == '\\')
                {
                    if (s[i + 1] == 'x')
                    {
                        count++;
                        i += 3;
                    }
                    else if (s[i + 1] == '\"')
                    {
                        count++;
                        i++;
                    }
                    else if (s[i+1] == '\\')
                    {
                        count++;
                        i++;
                    }
                }
                else
                    count++;
                
            }

            return count;
        }
        public static int EncodedCount(this string s)
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\\')
                {
                    count += 2;
                }
                else if (s[i] == '\"')
                {
                    count += 2;
                }
                else
                    count++;

            }
            return count + 2; // add two more ' " ' characters to encoded string
        }
    }
}