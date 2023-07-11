using System.Text;

namespace D11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "hepxcrrq";

            MyPassword pw = new MyPassword(input);
            pw.FindNext();
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(pw.ToString());
            pw.FindNext();
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(pw.ToString());
        }
    }
    public class MyPassword
    {
        List<int> forbidden = new List<int>() { 'i' - 'a', 'o' - 'a', 'l' - 'a' };
        static int maxval = 'z' - 'a';
        List<int> ints = new List<int>();
        public MyPassword(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                ints.Add(input[i] - 'a');
            }
        }
        public void FindNext()
        {
            bool found = false;
            while (!found)
            {
                int i = ints.Count - 1;
                ints[i]++;
                while(ints[i] > maxval)
                {
                    ints[i] = 0;
                    ints[i - 1]++;
                    i--;
                }
                found = OK();
            }
        }
        bool OK()
        {
            foreach(int f in forbidden)
            {
                if (ints.Contains(f))
                    return false;
            }
            bool threeconsecutive = false;
            for(int i = 0; i < ints.Count - 3; i++)
            {
                if (ints[i] == ints[i + 1] - 1 && ints[i] == ints[i + 2] - 2)
                {
                    threeconsecutive = true;
                    break;
                }
                    
            }

            bool twopairs = false;
            for(int i = 0; i < ints.Count - 1; i++)
            {
                if (ints[i] == ints[i + 1])
                {
                    for(int j = i + 2; j < ints.Count - 1; j++)
                    {
                        if (ints[j] == ints[j+1])
                        {
                            twopairs = true;
                            break;
                        }
                    }
                }
            }

            if (twopairs && threeconsecutive)
                return true;

            return false;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ints.Count; i++)
            {
                sb.Append(Convert.ToChar(ints[i] + 'a'));
            }
            return sb.ToString();
        }
    }
}