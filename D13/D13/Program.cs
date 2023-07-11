using System.Text;

namespace D13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] tokens = sr.ReadLine()!.Split(' ');
                    int sign = 1;
                    if (tokens[2] == "lose")
                        sign = -1;
                    Friend.FindFriend(tokens[0]).AddAffection(Friend.FindFriend(tokens[10].Replace(".","")), int.Parse(tokens[3]) * sign);
                }              
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(Friend.FindBestCombo());
            Friend.AddMe();
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(Friend.FindBestCombo());
        }
    }
    public class Friend
    {
        static List<Friend> friends = new List<Friend>();
        public static Friend FindFriend(string name)
        {
            foreach (Friend friend in friends)
            {
                if(friend.Name == name) 
                    return friend;
            }
            return new Friend(name);
        }
        public static int FindBestCombo()
        {
            int score = 0;
            Friend[] frens = new Friend[friends.Count];
            for(int i = 0; i < frens.Length; i++)
            {
                frens[i] = friends[i];
            }
            Friend[] output = new Friend[frens.Length];
            int[] sel = new int[frens.Length];
            Back(0, frens.Length, frens, output, sel, ref score);
            return score;
        }
        static void Back(int k, int n, Friend[] frens, Friend[] output,int[] sel, ref int score)
        {
            if(k >= n)
            {
                int checkscore = 0;
                for (int i = 0; i < n; i++)
                {
                    checkscore += output[i].AffectionateScore[output[(i + 1) % n]];
                    checkscore += output[i].AffectionateScore[output[(n + (i - 1)) % n]];
                }
                if(checkscore > score)
                {
                    score = checkscore;
                }
            }
            else
            {
                for(int i = 0; i < n; i++)
                {
                    if (sel[i] == 0)
                    {
                        sel[i] = 1;
                        output[k] = frens[i];
                        Back(k + 1, n, frens, output, sel, ref score);
                        sel[i] = 0;
                    }                 
                }
            }
        }
        public static void AddMe()
        {
            new Friend("Myself");
            foreach(Friend F in friends)
            {
                if (F.Name == "Myself")
                {
                    continue;
                }
                else
                {
                    FindFriend("Myself").AddAffection(F, 0);
                    F.AddAffection(FindFriend("Myself"), 0);
                }
                    
            }
        }
        public string Name { get; private set; }
        Dictionary<Friend, int> AffectionateScore = new Dictionary<Friend, int>();
        public Friend(string name)
        {
            Name = name;
            friends.Add(this);
        }
        public void AddAffection(Friend f, int score)
        {
            AffectionateScore.Add(f, score);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}