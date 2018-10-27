using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Characters.Stats
{
    public class ResourceStat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double BaseMaxValue { get; set; }
        public double Modifier { get; set; }
        public double Bonus { get; set; }
        public double MaxValue { get { return (BaseMaxValue * Modifier) + Bonus; } }
        public double CurrentValue { get; set; }
        public double CurrentBonus { get; set; }
        public double CurrentTotal
        {
            get { return CurrentValue + CurrentBonus; }
            set
            {
                var v = value;
                if ( v > CurrentBonus )
                {
                    v -= CurrentBonus;
                    CurrentBonus = 0;
                    CurrentValue -= v;
                }
                else
                {
                    CurrentBonus -= v;
                }
            }
        }

        public ResourceStat( int id , string name , double baseMaxValue )
        {
            Id = id;
            Name = name;
            BaseMaxValue = baseMaxValue;
        }
    }
}
