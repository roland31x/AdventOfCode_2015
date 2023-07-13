namespace D23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine()!);
                }
            }
            Machine myMachine = new Machine(lines,a: 0);
            myMachine.Execute();
            Console.WriteLine("Part 1 Solution:");
            Console.WriteLine(myMachine.GetB());
            myMachine = new Machine(lines, a: 1);
            myMachine.Execute();
            Console.WriteLine("Part 2 Solution:");
            Console.WriteLine(myMachine.GetB());
        }
    }
    public class Machine
    {
        List<string> lines;
        int a = 0;
        int b = 0;
        int driver = 0;
        public Machine(List<string> lines, int a) 
        {
            this.lines = lines;
            this.a = a;
        }
        public int GetA()
        {
            return a;
        }
        public int GetB()
        {
            return b;
        }
        public void Execute()
        {           
            while(driver < lines.Count)
            {
                ExecCommand(lines[driver]);
                driver++;
            }
        }
        void ExecCommand(string command)
        {
            string[] tokens = command.Split(' ');
            switch (tokens[0])
            {
                case "inc":
                    Inc(tokens[1]);
                    break;
                case "tpl":
                    Triple(tokens[1]);
                    break;
                case "hlf":
                    Half(tokens[1]);
                    break;
                case "jio":
                    Jio(tokens[1].Replace(",",""), int.Parse(tokens[2]));
                    break;
                case "jie":
                    Jie(tokens[1].Replace(",", ""), int.Parse(tokens[2]));
                    break;
                case "jmp":
                    Jmp(int.Parse(tokens[1]));
                    break;
            }
        }
        void Inc(string r)
        {
            if (r == "a")
                a++;
            else
                b++;
        }
        void Triple(string r)
        {
            if (r == "a")
                a *= 3;
            else
                b *= 3;
        }
        void Jio(string r, int offset)
        {
            if(r == "a")
            {
                if (a == 1)
                {
                    driver += offset - 1;
                }
            }
            else
            {
                if(b == 1)
                {
                    driver += offset - 1;
                }
            }    
                
        }
        void Jie(string r, int offset)
        {
            if (r == "a")
            {
                if (a % 2 == 0)
                {
                    driver += offset - 1;
                }
            }
            else
            {
                if (b % 2 == 0)
                {
                    driver += offset - 1;
                }
            }

        }
        void Jmp(int offset)
        {
            driver += offset - 1;
        }
        void Half(string r)
        {
            if (r == "a")
                a /= 2;
            else
                b /= 2;
        }
    }
}