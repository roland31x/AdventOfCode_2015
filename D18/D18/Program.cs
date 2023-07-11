using System.Globalization;

namespace D18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] mat = new int[100, 100];
            int[,] mat2 = new int[100, 100];

            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                int i = 0;
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine()!;
                    for(int j = 0; j < line.Length; j++)
                    {
                        if (line[j] == '.')
                        {
                            mat[i, j] = 0;
                            mat2[i, j] = 0;
                        }
                        else
                        {
                            mat[i, j] = 1;
                            mat2[i, j] = 1;
                        }
                    }
                    i++;
                }
            }

            for(int i = 0; i < 100; i++)
            {
                Cycle(mat, cornerLights: false);
                Cycle(mat2, cornerLights: true);
            }

            int score = 0;
            int score2 = 0;
            for (int i = 0; i < mat.GetLength(0); i++)
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j] == 1)
                        score++;
                    if (mat2[i, j] == 1)
                        score2++;
                }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(score);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(score2);
        }
        static void Cycle(int[,] mat, bool cornerLights)
        {
            List<int[]> nextalive = new List<int[]>();
            List<int[]> nextdead = new List<int[]>();
            if (cornerLights)
            {
                mat[0, mat.GetLength(1) - 1] = 1;
                mat[mat.GetLength(0) - 1, mat.GetLength(1) - 1] = 1;
                mat[mat.GetLength(0) - 1, 0] = 1;
                mat[0, 0] = 1;
            }
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j = 0; j <  mat.GetLength(1); j++)
                {
                    int neighboralive = GetAliveNeighbors(i, j, mat);
                    if (mat[i, j] == 1)
                        if (neighboralive == 2 || neighboralive == 3)
                            nextalive.Add(new int[] { i, j });
                        else
                            nextdead.Add(new int[] { i, j });
                    else
                        if (neighboralive == 3)
                        nextalive.Add(new int[] { i, j });
                }
            }
            foreach (int[] nexta in nextalive)
            {
                mat[nexta[0], nexta[1]] = 1;
            }
            foreach (int[] nextd in nextdead)
            {
                mat[nextd[0], nextd[1]] = 0;
            }
            if (cornerLights)
            {
                mat[0, mat.GetLength(1) - 1] = 1;
                mat[mat.GetLength(0) - 1, mat.GetLength(1) - 1] = 1;
                mat[mat.GetLength(0) - 1, 0] = 1;
                mat[0, 0] = 1;
            }
        }
        static int GetAliveNeighbors(int i, int j, int[,] mat)
        {
            int count = 0;
            for(int k = i - 1; k <= i + 1; k++)
            {
                for(int l = j - 1; l <= j + 1; l++)
                {
                    if (k == i && l == j)
                        continue;
                    try
                    {
                        if (mat[k, l] == 1)
                            count++;
                    }
                    catch(IndexOutOfRangeException)
                    {
                        continue;
                    }
                }
            }
            return count;
        }
        static void Show(int[,] mat)
        {
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    if (mat[i, j] == 1)
                        Console.Write('#');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }
        }
    }
}