namespace D22
{
    public class Boss // YOUR INPUT GOES HERE
    {
        public int HP = 51; // <--
        public int Damage = 9; // <--

        public Boss GetState()
        {
            Boss tor = new Boss();
            tor.HP = HP;
            return tor;
        }
    }
    internal class Program
    {
        static List<Spell> Spells = new List<Spell>() { new Spell(53, "Missile"), new Spell(73, "Drain"), new Spell(113, "Shield"), new Spell(173, "Poison"), new Spell(229, "Recharge"), };
        static void Main(string[] args)
        {           
            Player current = new Player();
            Boss boss = new Boss();
            int leastmana = 10000;
            Simulate(current,boss, ref leastmana, 0, playerturn: true, part2damage: false);
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(leastmana);
            leastmana = 10000;
            Simulate(current, boss, ref leastmana, 0, playerturn: true, part2damage: true);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(leastmana);
        }

        static void Simulate(Player current, Boss boss, ref int leastmana, int spentmana, bool playerturn, bool part2damage)
        {
            if (spentmana > leastmana)
                return;

            if (part2damage)
            {
                if (playerturn)
                    current.HP -= 1;
                if (current.HP <= 0)
                    return;
            }             


            foreach (Effect e in current.active)
            {
                current.Tick(e, boss);
            }
            current.active = current.active.Where(x => x.Time > 0).ToList();

            if (boss.HP <= 0)
            {
                if (spentmana < leastmana)
                    leastmana = spentmana;
                return;
            }

            if (playerturn)
            {                
                for (int i = 0; i < Spells.Count; i++)
                {
                    Player currentstate = current.GetState();
                    Boss bstate = boss.GetState();
                    Spell tocast = Spells[i];
                    if (currentstate.Cast(tocast, bstate))
                    {
                        if (bstate.HP <= 0)
                        {
                            if (spentmana + tocast.Cost< leastmana)
                                leastmana = spentmana + tocast.Cost;
                            return;
                        }                          
                        Simulate(currentstate, bstate, ref leastmana, spentmana + tocast.Cost, !playerturn, part2damage);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            else
            {
                int totake = boss.Damage - current.Armor;
                if (totake <= 0)
                    totake = 1;
                current.HP -= totake;
                if (current.HP <= 0)
                    return;
                Simulate(current, boss, ref leastmana, spentmana, !playerturn, part2damage);
            }
            
        }
    }
    public class Player
    {
        public int HP = 50;
        public int Armor = 0;
        public int Mana = 500;
        public List<Effect> active = new List<Effect>();

        public bool Cast(Spell spell, Boss target)
        {
            if (spell.Cost > Mana)
                return false;
            switch (spell.Name)
            {
                case "Missile":
                    target.HP -= 4;
                    break;
                case "Drain":
                    target.HP -= 2;
                    HP += 2;
                    break;
                case "Shield":
                    if (!EffectIsActive("Shield"))
                    {
                        Armor += 7;
                        active.Add(new Effect("Shield", 6));
                    }
                    else return false;
                    break;
                case "Poison":
                    if (!EffectIsActive("Poison"))
                    {
                        active.Add(new Effect("Poison", 6));
                    }
                    else return false;
                    break;
                case "Recharge":
                    if (!EffectIsActive("Recharge"))
                    {
                        active.Add(new Effect("Recharge", 5));
                    }
                    else return false;
                    break;
            }
            Mana -= spell.Cost;
            return true;            
        }
        public bool EffectIsActive(string name)
        {
            foreach(Effect e in active)
            {
                if (e.Name == name)
                    return true;
            }
            return false;
        }
        public void Tick(Effect effect, Boss target)
        {
            if (effect.Time == 0)
            {
                return;
            }
            switch (effect.Name)
            {
                case "Shield":
                    break;
                case "Poison":
                    target.HP -= 3;
                    break;
                case "Recharge":
                    Mana += 101;
                    break;
            }
            effect.Time--;
            if (effect.Time == 0)
            {
                if (effect.Name == "Shield")
                    Armor -= 7;
            }
        }
        public Player GetState()
        {
            Player tor = new Player();
            tor.HP = HP;
            tor.Armor = Armor;
            tor.Mana = Mana;
            foreach(Effect effect in active)
            {
                tor.active.Add(new Effect(effect.Name, effect.Time));
            }
            return tor;
        }
        
    }
    public class Spell
    {
        public int Cost;
        public string Name;
        public Spell(int cost, string name)
        {
            Cost = cost;
            Name = name;
        }
    }
    public class Effect
    {
        public string Name;
        public int Time;
        public Effect(string name, int time)
        {
            Name = name;
            Time = time;
        }
    }   
}