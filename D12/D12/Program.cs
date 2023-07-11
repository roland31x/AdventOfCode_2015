
namespace D12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            string json;
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                json = sr.ReadLine()!;
                
            }
            for (int i = 0; i < json.Length; i++)
            {
                int number = 0;
                int sign = 1;
                if (json[i] == '-')
                {
                    sign = -1;
                    i++;
                }
                while (json[i] - '0' >= 0 && json[i] - '0' < 10)
                {
                    number *= 10;
                    number += json[i] - '0';
                    i++;
                }
                sum += number * sign;
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(sum);

            sum = 0;
            for(int i = 0; i < json.Length; i++)
            {
                if (json[i] == '{')
                {
                    bool hasred = HasRed(json, i);
                    if (hasred)
                    {
                        int level = 0;
                        bool go = true;
                        i++;
                        while (go)
                        {
                            if (json[i] == '{')
                                level++;
                            if (json[i] == '}' && level == 0)
                                break;
                            if (json[i] == '}')
                                level--;
                            i++;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    int number = 0;
                    int sign = 1;
                    if (json[i] == '-')
                    {
                        sign = -1;
                        i++;
                    }
                    while (json[i] - '0' >= 0 && json[i] - '0' < 10)
                    {
                        number *= 10;
                        number += json[i] - '0';
                        i++;
                    }
                    sum += number * sign;
                }              
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(sum);

        }

        static bool HasRed(string json, int start)
        {
            for(int i = start + 1; i < json.Length; i++)
            {
                if (json[i] == '[') // skip array property
                {
                    int level = 0;
                    i++;
                    while (true)
                    {
                        if (json[i] == '[')
                            level++;
                        else if (json[i] == ']' && level == 0)
                            break;
                        else if (json[i] == ']')
                            level--;
                        i++;
                    }
                }
                else if (json[i] == '{') // skip object property
                {
                    int level = 0;
                    bool go = true;
                    i++;
                    while (go)
                    {
                        if (json[i] == '{')
                            level++;
                        if (json[i] == '}' && level == 0)
                            break;
                        if (json[i] == '}')
                            level--;
                        i++;
                    }
                }
                else if (json[i] == '}')
                    return false;
                else if (json[i] == 'r' && json[i + 1] == 'e' && json[i + 2] == 'd')
                    return true;
            }
            return false;
        }
    }
}