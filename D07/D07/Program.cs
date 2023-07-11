namespace D07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Wire a = null;
            Wire b = null;
            using (StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string Line = sr.ReadLine()!;
                    string[] tokens = Line.Split("->");
                    Wire toadd = new Wire(tokens[1].Trim(), tokens[0].Trim());
                    if (toadd.ID == "a")
                        a = toadd;
                    else if (toadd.ID == "b")
                        b = toadd;
                }
            }
            ushort value = a!.GetValue();
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(value);
            foreach (Wire w in Wire.Wires)
            {
                w.SetSignal(null);
            }
            b!.SetSignal(value);            
            value = a!.GetValue();
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(value);
        }
    }
    public class Wire
    {
        public static List<Wire> Wires = new List<Wire>();
        public static Wire FindWire(string ID)
        {
            if(ushort.TryParse(ID,out ushort res))
            {
                return new Wire(res);
            }
            foreach(Wire wire in Wires)
            {
                if(wire.ID == ID)
                {
                    return wire;
                }
            }
            return null;
        }
        public string ID { get; private set; }
        ushort? signal = null;
        public string operation;
        Wire(ushort signal)
        {
            this.signal = signal;
            operation = string.Empty;
            ID = "temp";
        }
        public Wire(string ID, string op) 
        {
            this.ID = ID;
            operation = op;
            Wires.Add(this);
        }
        public void SetSignal(ushort? signal)
        {
            this.signal = signal;
        }
        public ushort GetValue()
        {
            if (signal == null)
            {
                return SolveOp(operation);
            }
            else
                return (ushort)signal;
        }
        public ushort SolveOp(string op)
        {
            if(ushort.TryParse(operation, out ushort value))
            {
                this.signal = value;
                return (ushort)signal;
            }
            else
            {
                string[] ops = operation.Split(' ');
                if(ops.Length == 1)
                {
                    this.signal = FindWire(ops[0]).GetValue();
                    return (ushort)signal;
                }
                else if(ops.Length == 2)
                {
                    this.signal = Not(FindWire(ops[1]).GetValue());
                    return (ushort)signal;
                }
                else
                {
                    string operand = ops[1];
                    if(operand == "AND")
                        signal = (ushort)(FindWire(ops[0]).GetValue() & FindWire(ops[2]).GetValue());
                    else if(operand == "OR")
                        signal = (ushort)(FindWire(ops[0]).GetValue() | FindWire(ops[2]).GetValue());
                    else if(operand == "RSHIFT")
                        signal = (ushort)(FindWire(ops[0]).GetValue() >> FindWire(ops[2]).GetValue());
                    else
                        signal = (ushort)(FindWire(ops[0]).GetValue() << FindWire(ops[2]).GetValue());

                    return (ushort)signal;
                }
                
            }
        }
        ushort Not(ushort a)
        {
            return (ushort)(ushort.MaxValue - a);
        }
        public override string ToString()
        {
            return ID;
        }
    }
}