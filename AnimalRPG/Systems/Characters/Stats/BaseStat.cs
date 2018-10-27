using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Characters.Stats
{
    public class BaseStat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double BaseValue { get; set; }
        public double Modifier { get; set; }
        public double Bonus { get; set; }
        public double Value { get { return (BaseValue * Modifier) + Bonus; } }

        public BaseStat( int id , string name , double baseValue)
        {
            Id = id;
            Name = name;
            BaseValue = baseValue;
        }
    }
}
