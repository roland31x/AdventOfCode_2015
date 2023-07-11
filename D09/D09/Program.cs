using System.ComponentModel.Design;

namespace D09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    string[] tokens = sr.ReadLine()!.Split(" ");
                    City C1 = City.FindCity(tokens[0]);
                    City C2 = City.FindCity(tokens[2]);
                    int dist = int.Parse(tokens[4]);
                    C1.connections.Add(C2);
                    C1.dist.Add(dist);
                    C2.connections.Add(C1);
                    C2.dist.Add(dist);
                }
            }
            Console.WriteLine("Part 1 solution");
            Console.WriteLine(City.HamiltonianPath(true));
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(City.HamiltonianPath(false));
        }
    }
    public class City
    {
        public static List<City> cities = new List<City>();
        public static City FindCity(string name)
        {
            foreach(City c in cities)
            {
                if (c.Name == name)
                    return c;
            }
            return new City(name);
        }
        public static int HamiltonianPath(bool type) // true = minimum dist , false = maximum dist
        {
            int tor = int.MaxValue;
            if (!type)
                tor = 0;
            foreach(City c in cities)
            {
                int Score = DFS(c, cities.Where(x => x != c).ToList(), type);
                if (type && Score < tor)
                    tor = Score;
                else if (!type && Score > tor)
                    tor = Score;
            }
            return tor;
            
        }
        static int DFS(City current, List<City> left, bool type)
        {
            int tor = int.MaxValue;
            if (!type)
                tor = 0;          
            if(left.Count == 0)
            {
                return 0;
            }
            foreach (City c in left)
            {
                int Score = current.dist[current.connections.IndexOf(c)] + DFS(c, left.Where(x => x != c).ToList(), type);
                if (type && Score < tor)
                    tor = Score;
                else if (!type && Score > tor)
                    tor = Score;
            }
            return tor;
        }
        public List<City> connections = new List<City>();
        public List<int> dist = new List<int>();
        public string Name { get; private set; }
        public City(string name) 
        {
            Name = name;
            cities.Add(this);
        }
    }
}