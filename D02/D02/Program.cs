using System.Security;

namespace D02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            int ribbon = 0;
            using (StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    string[] tokens = sr.ReadLine()!.Split('x');
                    Cube C = new Cube(int.Parse(tokens[0]), int.Parse(tokens[1]), int.Parse(tokens[2]));
                    sum += C.SurfaceArea();
                    sum += C.SmallestSide();
                    ribbon += C.CubicVolume();
                    ribbon += C.SmallestPerimeter();
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(sum);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(ribbon);
        }
    }
    public class Cube
    {
        public int L;
        public int W;
        public int H;
        public Cube(int L, int W, int H)
        {
            this.L = L;
            this.W = W;
            this.H = H;
        }
        public int SurfaceArea()
        {
            return 2 * L * H + 2 * W * H + 2 * L * W;
        }
        public int SmallestSide()
        {
            int LH = L * H;
            int WH = W * H;
            int LW = L * W;
            return Math.Min(LH, Math.Min(WH, LW));
        }
        public int CubicVolume()
        {
            return L * W * H;
        }
        public int SmallestPerimeter()
        {
            int P1 = Perimeter(L, W);
            int P2 = Perimeter(W, H);
            int P3 = Perimeter(H, L);
            return Math.Min(P1,Math.Min(P2, P3));
        }
        int Perimeter(int L, int W)
        {
            return 2 * L + 2 * W;
        }
    }
}