using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace D04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pw = "";
            MD5 md5 = MD5.Create();
            using (StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                pw = sr.ReadLine()!;
            }
            int iP1 = 0;
            bool foundP1 = false;
            string res = "";
            while (!foundP1)
            {
                iP1++;
                res = pw + iP1.ToString();
                byte[] arr = ASCIIEncoding.ASCII.GetBytes(res);
                md5.ComputeHash(arr);
                res = Convert.ToHexString(md5.Hash);
                for(int k = 0; k < 5; k++)
                {
                    if (res[k] - '0' != 0)
                    {
                        break;
                    }
                    if(k == 4)
                    {
                        foundP1 = true;
                    }
                }
            }
            int iP2 = 0;
            bool foundP2 = false;
            while (!foundP2)
            {
                iP2++;
                res = pw + iP2.ToString();
                byte[] arr = ASCIIEncoding.ASCII.GetBytes(res);
                md5.ComputeHash(arr);
                res = Convert.ToHexString(md5.Hash);
                for (int k = 0; k < 6; k++)
                {
                    if (res[k] - '0' != 0)
                    {
                        break;
                    }
                    if (k == 5)
                    {
                        foundP2 = true;
                    }
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(iP1);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(iP2);
        }
    }
}