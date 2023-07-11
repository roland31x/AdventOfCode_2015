using System.ComponentModel;

namespace D15
{
    internal class Program
    {
        static List<int[]> ingredients = new List<int[]>();
        static void Main(string[] args)
        {
            
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    string[] tokens = sr.ReadLine()!.Replace(",","").Split(' ');
                    ingredients.Add(new int[] { int.Parse(tokens[2]), int.Parse(tokens[4]), int.Parse(tokens[6]), int.Parse(tokens[8]), int.Parse(tokens[10]) });
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(BestCombo(ingredients,teaspoons: 100, checkCalories: false));
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(BestCombo(ingredients,teaspoons: 100, checkCalories: true));
        }
        static long BestCombo(List<int[]> ingredients, int teaspoons, bool checkCalories)
        {
            long score = 0;
            int[] output = new int[ingredients.Count];
            int[] data = new int[teaspoons + 1];
            int[] sel = new int[data.Length];
            for(int i = 0; i < data.Length; i++)
            {
                data[i] = i;
            }
            Back(0, output.Length, data, output, sel, ref score, checkCalories);
            return score;
        }
        static void CheckOutput(int[] output, ref long score, bool checkCalories)
        {
            int[] traits = new int[5];
            for(int i = 0; i < output.Length; i++)
            {
                for(int j = 0; j < ingredients[i].Length; j++)
                {
                    traits[j] += ingredients[i][j] * output[i];
                }
            }
            long check = 1;
            if(checkCalories)
                if (traits[4] != 500)
                    return;
            for (int i = 0; i < traits.Length - 1; i++)
            {
                if (traits[i] <= 0)
                    return;
                check *= traits[i];           
            }
                        
            if (check > score)
                score = check;
            
        }
        static void Back(int k, int n, int[] data, int[] output, int[] sel, ref long score, bool checkCalories)
        {
            if(k >= n)
            {
                int val = 0;
                for(int i = 0; i < n; i++)
                {
                    val += output[i];
                }
                if(val == 100)
                {
                    CheckOutput(output, ref score, checkCalories);
                }
            }
            else
            {
                for(int i = 0; i < data.Length; i++)
                {
                    if (sel[i] == 0)
                    {
                        sel[i] = 1;
                        output[k] = data[i];
                        Back(k + 1, n, data, output, sel, ref score, checkCalories);
                        sel[i] = 0;
                    }                   
                }
            }
        }
    }
}