using System.Text;

namespace D10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "1113122113";
            string res = input.ToLower();
            for(int i = 0; i < 40; i++)
            {
                res = Generate(res);
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(res.Length);
            for (int i = 0; i < 10; i++)
            {
                res = Generate(res);
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(res.Length);
        }
        private static string Generate(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder str = new StringBuilder();
            int say = 1;
            for (int j = 0; j < chars.Length; j++)
            {
                try
                {
                    while (s[j] == s[j + 1])
                    {
                        say++;
                        j++;
                    }
                    str.Append(say);
                    str.Append(s[j]);
                    say = 1;
                }
                catch (Exception)
                {
                    str.Append(say);
                    str.Append(s[j]);
                }
            }
            return str.ToString();
        }
    }
}