namespace D06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] mat = new int[1000, 1000];
            int[,] mat2 = new int[1000, 1000];

            List<string> commands = new List<string>();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    commands.Add(sr.ReadLine()!);

                }
            }
            foreach(string command in commands)
            {
                string[] tokens = command.Split(' ');
                int idx = 1;
                if (command.Contains("turn"))
                    idx++;
                int startI = int.Parse(tokens[idx].Split(',')[0]);
                int startJ = int.Parse(tokens[idx].Split(',')[1]);
                int endI = int.Parse(tokens[idx + 2].Split(',')[0]);
                int endJ = int.Parse(tokens[idx + 2].Split(',')[1]);
                if (command.Contains("off"))
                {
                    TurnOFF(mat, startI, startJ, endI, endJ);
                    Light(mat2, startI, startJ, endI, endJ, -1);
                }                   
                else if (command.Contains("on"))
                {
                    TurnOn(mat, startI, startJ, endI, endJ);
                    Light(mat2, startI, startJ, endI, endJ, 1);
                }
                else
                {
                    Toggle(mat, startI, startJ, endI, endJ);
                    Light(mat2, startI, startJ, endI, endJ, 2);
                }                  
            }
            int score = 0;
            int score2 = 0;
            for(int i = 0; i < 1000; i++)
            {
                for(int j = 0; j < 1000; j++)
                {
                    score += mat[i, j];
                    score2 += mat2[i, j];
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(score);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(score2);
        }
        static void Light(int[,] mat, int startI, int startJ, int endI, int endJ, int amount)
        {
            for (int i = startI; i <= endI; i++)
            {
                for (int j = startJ; j <= endJ; j++)
                {
                    mat[i, j] += amount;
                    if (mat[i, j] < 0)
                        mat[i, j] = 0;
                }
            }
        }
        static void Toggle(int[,] mat, int startI, int startJ, int endI,int endJ)
        {
            for(int i = startI; i <= endI; i++)
            {
                for(int j = startJ; j <= endJ; j++)
                {
                    mat[i, j] = mat[i, j] ^ 1;
                }
            }
        }
        static void TurnOn(int[,] mat, int startI, int startJ, int endI, int endJ)
        {
            for (int i = startI; i <= endI; i++)
            {
                for (int j = startJ; j <= endJ; j++)
                {
                    mat[i, j] = 1;
                }
            }
        }
        static void TurnOFF(int[,] mat, int startI, int startJ, int endI, int endJ)
        {
            for (int i = startI; i <= endI; i++)
            {
                for (int j = startJ; j <= endJ; j++)
                {
                    mat[i, j] = 0;
                }
            }
        }
    }
}