namespace D03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int[]> HousesVisitedPart1 = new List<int[]>() { new int[] { 0, 0 } };
            List<int[]> HousesVisitedPart2 = new List<int[]>() { new int[] { 0, 0 } };
            Santa Santa = new Santa();
            Santa Robot = new Santa();
            using (StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                string line  = sr.ReadLine()!;
                for(int i = 0; i < line.Length; i++)
                {
                    Santa.Move(line[i]);
                    int[] newhouse = new int[] { Santa.Y, Santa.X };
                    bool ok = true;
                    foreach (int[] house in HousesVisitedPart1)
                    {
                        if (house[0] == newhouse[0] && house[1] == newhouse[1])
                        {
                            ok = false;
                            break;
                        }
                    }
                    if (ok) 
                    { 
                        HousesVisitedPart1.Add(newhouse);
                    }
                }

                Santa = new Santa();
                for (int i = 0; i < line.Length; i++)
                {
                    Santa.Move(line[i]);
                    i++;
                    bool ok = true;
                    if (i < line.Length)
                    {
                        Robot.Move(line[i]);
                        int[] nh1 = new int[] {Robot.Y, Robot.X};
                        foreach (int[] house in HousesVisitedPart2)
                        {
                            if (house[0] == nh1[0] && house[1] == nh1[1])
                            {
                                ok = false;
                                break;
                            }
                        }
                        if (ok)
                        {
                            HousesVisitedPart2.Add(nh1);
                        }
                    }                       
                    int[] newhouse = new int[] { Santa.Y, Santa.X };
                    ok = true;
                    foreach (int[] house in HousesVisitedPart2)
                    {
                        if (house[0] == newhouse[0] && house[1] == newhouse[1])
                        {
                            ok = false;
                            break;
                        }
                    }
                    if (ok)
                    {
                        HousesVisitedPart2.Add(newhouse);
                    }
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(HousesVisitedPart1.Count);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(HousesVisitedPart2.Count);
        }
    }
    public class Santa
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Santa()
        {
            X = 0;
            Y = 0;
        }
        public void Move(char c)
        {
            if(c == 'v')
            {
                Y--;
            }
            if(c == '^')
            {
                Y++;
            }
            if(c == '>')
            {
                X++;
            }
            if(c == '<')
            {
                X--;
            }
        }
    }
}