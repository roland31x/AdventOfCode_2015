namespace D21
{
    internal class Program
    {
        static List<Item> weapons = new List<Item>(); // max 1 weapons
        static List<Item> armor = new List<Item>(); // max 1 armor (optional)
        static List<Item> rings = new List<Item>(); // max 2 rings (optional)
        static void Main(string[] args)
        {
            
            using(StreamReader sr = new StreamReader(@"..\..\..\Shop.txt"))
            {
                string buffer = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    if (buffer.Contains("Weapons:"))
                    {
                        buffer = sr.ReadLine();
                        while(buffer != string.Empty && buffer != null)
                        {
                            string[] tokens = buffer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            weapons.Add(new Item(new int[] { int.Parse(tokens[tokens.Length - 3]), int.Parse(tokens[tokens.Length - 2]), int.Parse(tokens[tokens.Length - 1]) }));
                            buffer = sr.ReadLine();
                        }
                    }
                    else if (buffer.Contains("Armor:"))
                    {
                        buffer = sr.ReadLine();
                        while (buffer != string.Empty && buffer != null)
                        {
                            string[] tokens = buffer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            armor.Add(new Item(new int[] { int.Parse(tokens[tokens.Length - 3]), int.Parse(tokens[tokens.Length - 2]), int.Parse(tokens[tokens.Length - 1]) }));
                            buffer = sr.ReadLine();
                        }
                    }
                    else if(buffer.Contains("Rings:"))
                    {
                        buffer = sr.ReadLine();
                        while (buffer != string.Empty && buffer != null)
                        {
                            string[] tokens = buffer.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            rings.Add(new Item(new int[] { int.Parse(tokens[tokens.Length - 3]), int.Parse(tokens[tokens.Length - 2]), int.Parse(tokens[tokens.Length - 1]) }));
                            buffer = sr.ReadLine();
                        }
                    }
                    buffer = sr.ReadLine();
                }
                
            }

            CalcLowestCost();
        }

        static void CalcLowestCost()
        {
            int score = -1;
            int maxscore = 1000;
            List<int[]> alreadychecked = new List<int[]>();
            GetScore(0, -1, -1, -1, ref score, ref maxscore, alreadychecked);

            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(score);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(maxscore);

        }
        static void GetScore(int weapidx, int armoridx, int ringidx1, int ringidx2, ref int score, ref int maxscore, List<int[]> ok) 
        {
            Player current = new Player();
            if (weapidx >= weapons.Count || armoridx >= armor.Count || ringidx1 >= rings.Count || ringidx2 >= rings.Count)
                return;
            if (ringidx1 >= 0 && ringidx2 >= 0 && ringidx2 == ringidx1)
                return;

            int[] curr = new int[] { weapidx,armoridx,ringidx1, ringidx2 };
            bool dupe = false;
            foreach (int[] done in ok)
            {
                for(int i = 0; i < 4; i++)
                {
                    if (done[i] == curr[i])
                    {
                        if (i == 3)
                            dupe = true;
                    }
                    else
                    {
                        break;
                    }                        
                }
                if (dupe)
                    break;

            }
            if (dupe)
                return;
            else
                ok.Add(curr);

            current.items.Add(weapons[weapidx]);
            if (armoridx >= 0)
                current.items.Add(armor[armoridx]);
            if (ringidx1 >= 0)
                current.items.Add(rings[ringidx1]);
            if (ringidx2 >= 0)
                current.items.Add(rings[ringidx2]);

            Simulate(current, new Boss(), ref score, ref maxscore);
          
            GetScore(weapidx + 1, armoridx, ringidx1, ringidx2, ref score, ref maxscore, ok);
            GetScore(weapidx, armoridx + 1, ringidx1, ringidx2, ref score, ref maxscore, ok);
            GetScore(weapidx, armoridx, ringidx1 + 1, ringidx2, ref score, ref maxscore, ok);
            GetScore(weapidx, armoridx, ringidx1, ringidx2 + 1, ref score, ref maxscore, ok);
        }
        static void Simulate(Player p, Boss b, ref int score, ref int maxscore)
        {
            bool playerturn = true;
            int playerdamage = 0;
            int playerarmor = 0;
            int totalcost = 0;
            foreach (Item item in p.items)
            {
                playerdamage += item.stats[1];
                playerarmor += item.stats[2];
                totalcost += item.stats[0];
            }

            while(p.HP > 0 && b.HP > 0)
            {
                if(playerturn)
                {
                    playerturn = false;
                    int totake = playerdamage - b.Armor;
                    if (totake <= 0)
                        totake = 1;
                    b.HP -= totake;
                }
                else
                {
                    playerturn = true;
                    int totake = b.Dmg - playerarmor;
                    if (totake <= 0)
                        totake = 1;
                    p.HP -= totake;
                }
            }
            if(p.HP > 0)
            {
                if (score == -1)
                    score = totalcost;
                if(totalcost < score)
                    score = totalcost;
            }
            else
            {
                if (maxscore == 1000)
                    maxscore = totalcost;
                if (totalcost > maxscore)
                    maxscore = totalcost;
            }
        }
    }
    public class Item
    {
        public int[] stats = new int[3]; // COST / DMG / ARMOR;
        public Item(int[] st)
        {
            stats = st;
        }
    }
    public class Player
    {
        public int HP = 100;
        public List<Item> items = new List<Item>();
    }
    public class Boss // YOUR INPUT HERE
    {
        public int HP = 109;
        public int Dmg = 8;
        public int Armor = 2;
    }
}