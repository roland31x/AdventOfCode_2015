namespace D25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int row = 2981; // your input
            int column = 3075; // your input
            long start = 20151125L;
            int currentrow = 1;
            int currentcolumn = 1;
            int level = 1;
            long past = start;
            long current = GetNext(start);
            while (currentrow != row || currentcolumn != column)
            {
                current = GetNext(past);
                past = current;
                currentrow--;
                currentcolumn++;
                if (currentcolumn > level)
                {
                    level++;
                    currentrow = level;
                    currentcolumn = 1;                   
                }
                
            }
            Console.WriteLine("Solution: " + current);

        }
        static long GetNext(long past)
        {
            checked 
            {
                return (past * 252533L) % 33554393L; 
            }
        }
    }
}