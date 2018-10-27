using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Maps
{
    public class Terrain
    {
        public string Name { get; private set; }
        public float MoveCost { get; private set; }
        public float DefenseRating { get; set; }
    }
}
