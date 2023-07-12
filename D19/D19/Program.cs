using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace D19
{
    internal class Program
    {
        static readonly Random R = new Random();
        static void Main(string[] args)
        {
            List<Replacement> rplist = new List<Replacement>();
            string molecule = string.Empty;
            List<string> newmolecules = new List<string>();
            using (StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                string buffer = sr.ReadLine();
                while (!sr.EndOfStream && buffer != string.Empty)
                {
                    string[] tokens = buffer.Split("=>");
                    rplist.Add(new Replacement(tokens[0].Trim(), tokens[1].Trim()));
                    buffer = sr.ReadLine()!;
                }
                molecule = sr.ReadLine()!;
            }
            foreach (Replacement rp in rplist)
            {
                for (int i = 0; i < molecule.Length; i++)
                {
                    char check = molecule[i];
                    if (check == rp.ToReplace[0])
                    {
                        bool ok = false;
                        int j = 1;
                        while (j < rp.ToReplace.Length && i + j < molecule.Length)
                        {
                            if (molecule[i + j] != rp.ToReplace[j])
                            {
                                break;
                            }
                            j++;
                        }
                        if (j == rp.ToReplace.Length)
                        {
                            ok = true;
                        }
                        if (ok)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int l = 0; l < i; l++)
                            {
                                sb.Append(molecule[l]);
                            }
                            for (int l = 0; l < rp.With.Length; l++)
                            {
                                sb.Append(rp.With[l]);
                            }
                            for (int l = i + rp.ToReplace.Length; l < molecule.Length; l++)
                            {
                                sb.Append(molecule[l]);
                            }
                            if (!newmolecules.Contains(sb.ToString()))
                            {
                                newmolecules.Add(sb.ToString());
                            }
                            i = i + rp.ToReplace.Length - 1;
                        }
                    }
                }
            }

            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(newmolecules.Count);

            int gen = 0;
            string target = molecule;
            string s = target;
            while (target != "e")
            {                
                bool found = false;
                foreach (Replacement rp in rplist)
                {                       
                    for (int i = 0; i < s.Length; i++)
                    {
                        char check = s[i];
                        if (check == rp.With[0])
                        {
                            bool ok = false;
                            int j = 1;
                            while (j < rp.With.Length && i + j < s.Length)
                            {
                                if (s[i + j] != rp.With[j])
                                {
                                    break;
                                }
                                j++;
                            }
                            if (j == rp.With.Length)
                            {
                                ok = true;
                            }
                            if (ok)
                            {
                                StringBuilder sb = new StringBuilder();
                                for (int l = 0; l < i; l++)
                                {
                                    sb.Append(s[l]);
                                }
                                for (int l = 0; l < rp.ToReplace.Length; l++)
                                {
                                    sb.Append(rp.ToReplace[l]);
                                }
                                for (int l = i + rp.With.Length; l < s.Length; l++)
                                {
                                    sb.Append(s[l]);
                                }
                                s = sb.ToString();
                                gen++;
                                found = true;
                                break;
                            }
                        }
                    }
                    if (found)
                        break;
                }
                if(target == s) // no more de-evolving can be done so we have to reset the loop
                {
                    target = molecule;
                    s = target;
                    gen = 0;
                    rplist = Shuffle(rplist);
                }
                else
                {
                    target = s; // we update target to new value
                }
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(gen);
        }
        static List<Replacement> Shuffle(List<Replacement> rp)
        {
            List<Replacement> shuffled = new List<Replacement>();
            while(shuffled.Count != rp.Count)
            {
                Replacement toadd = rp[R.Next(rp.Count)];
                if (shuffled.Contains(toadd))
                    continue;
                shuffled.Add(toadd);
            }
            return shuffled;
        }
    }
    public class Replacement
    {
        public string ToReplace { get; private set; }
        public string With { get; private set; }
        public Replacement(string toReplace, string with)
        {
            ToReplace = toReplace;
            With = with;
        }
        public override string ToString()
        {
            return ToReplace + "=>" + With;
        }
    }
}