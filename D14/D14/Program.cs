namespace D14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Reindeer> deers = new List<Reindeer>();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] tokens = sr.ReadLine()!.Split(' ');
                    deers.Add(new Reindeer(tokens[0], int.Parse(tokens[3]), int.Parse(tokens[6]), int.Parse(tokens[13])));
                }             
            }

            
            for(int i = 0; i < 2503; i++)
            {
                int maxscore = 0;
                foreach (Reindeer r in deers)
                {
                    r.Fly();
                    if(r.Dist > maxscore)
                        maxscore = r.Dist;
                }
                foreach (Reindeer r in deers)
                {
                    if (r.Dist == maxscore)
                        r.Score++;
                }
            }

            int score = 0;
            int maxdist = 0;
            foreach (Reindeer r in deers)
            {
                if(r.Dist > maxdist)
                    maxdist = r.Dist;
                if(r.Score > score)
                    score = r.Score;
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(maxdist);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(score);


        }
    }
    public class Reindeer
    {
        int StopWatch = 0;
        public int Dist { get; private set; }
        public int Score = 0;
        public int Speed { get; private set; }
        public int FlyTime { get; private set; }
        public int BreakTime { get; private set; }
        public string Name { get; private set; }
        public Reindeer(string name, int speed, int flyTime, int breakTime)
        {
            Name = name;
            Speed = speed;
            BreakTime = breakTime;
            FlyTime = flyTime;
        }
        public void Fly()
        {
            if (StopWatch / FlyTime < 1)
                Dist += Speed;
            else if (StopWatch / (FlyTime + BreakTime - 1) > 0)
            {
                StopWatch = 0;
                return;
            }              
            StopWatch++;
        }
    }
}