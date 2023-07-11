namespace D16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> AuntSue = new Dictionary<string, int>();
            AuntSue.Add("children", 3);
            AuntSue.Add("cats", 7);
            AuntSue.Add("samoyeds", 2);
            AuntSue.Add("pomeranians", 3);
            AuntSue.Add("akitas", 0);
            AuntSue.Add("vizslas", 0);
            AuntSue.Add("goldfish", 5);
            AuntSue.Add("trees", 3);
            AuntSue.Add("cars", 2);
            AuntSue.Add("perfumes", 1);

            int auntnr = 1;
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine()!;
                    string[] tokens = line.Split(' ');
                    int okscore = 0;
                    if (AuntSue[tokens[2].Replace(":", "")] == int.Parse(tokens[3].Replace(",", "")))
                        okscore++;
                    if (AuntSue[tokens[4].Replace(":", "")] == int.Parse(tokens[5].Replace(",", "")))
                        okscore++;
                    if (AuntSue[tokens[6].Replace(":", "")] == int.Parse(tokens[7].Replace(",", "")))
                        okscore++;
                    if (okscore == 3)
                    {
                        Console.WriteLine("Part 1 solution:");
                        Console.WriteLine(auntnr);
                    }    
                        
                    auntnr++;
                }
            }
            auntnr = 1;
            using (StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine()!;
                    string[] tokens = line.Split(' ');
                    int okscore = 0;
                    List<string> stuff = new List<string>() { tokens[2].Replace(":", ""), tokens[4].Replace(":", ""), tokens[6].Replace(":", "") };
                    List<int> number = new List<int>() { int.Parse(tokens[3].Replace(",", "")), int.Parse(tokens[5].Replace(",", "")), int.Parse(tokens[7].Replace(",", "")) };
                    foreach(string item in stuff)
                    {
                        if(item == "cats" || item == "trees")
                        {
                            if (AuntSue[item] < number[stuff.IndexOf(item)])
                                okscore++;
                        }
                        else if(item == "pomeranians" || item == "goldfish")
                        {
                            if (AuntSue[item] > number[stuff.IndexOf(item)])
                                okscore++;
                        }
                        else
                        {
                            if (AuntSue[item] == number[stuff.IndexOf(item)])
                                okscore++;
                        }
                    }
                    if (okscore == 3)
                    {
                        Console.WriteLine("Part 2 solution:");
                        Console.WriteLine(auntnr);
                    }

                    auntnr++;
                }
            }
        }
    }
}