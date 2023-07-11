namespace D05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int niceP1 = 0;
            int niceP2 = 0;
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream) 
                {
                    string tocheck = sr.ReadLine();
                    if (tocheck.IsNiceP1())
                        niceP1++;
                    if (tocheck.IsNiceP2())
                        niceP2++;
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(niceP1);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(niceP2);
        }
    }
    public static class NiceStringChecker
    {
        static List<char> Vowels = new List<char>() { 'a', 'e', 'i', 'o', 'u' };
        static List<string> Forbidden = new List<string>() { "ab", "cd", "pq", "xy"};
        public static bool IsNiceP1(this string s) 
        {
            int vowelcount = 0;
            bool twoletters = false;
            bool forbiddenstrings = false;

            

            for(int i = 0; i < s.Length; i++)
            {
                if (Vowels.Contains(s[i]))
                    vowelcount++;
                if (i != s.Length - 1)
                {
                    string sub = s[i].ToString() + s[i + 1].ToString();
                    if (Forbidden.Contains(sub))
                        forbiddenstrings = true;
                    if (s[i] == s[i + 1])
                        twoletters = true;
                }
            }

            if(vowelcount >= 3 && twoletters && !forbiddenstrings)
                return true;
            return false;
        }
        public static bool IsNiceP2(this string s)
        {
            bool jumpvowel = false;
            bool repeatingdouble = false;

            for (int i = 0; i < s.Length; i++)
            {
                if (i < s.Length - 2)
                {
                    string sub = s[i].ToString() + s[i + 1].ToString();
                    for (int j = i + 2; j < s.Length - 1; j++)
                    {
                        string sub2 = s[j].ToString() + s[j + 1].ToString();
                        if (sub == sub2)
                        {
                            repeatingdouble = true;
                            break;
                        }

                    }

                    if (s[i] == s[i + 2])
                        jumpvowel = true;
                }
                else
                    break;
            }

            if (jumpvowel && repeatingdouble)
                return true;
            return false;
        }
    }
}