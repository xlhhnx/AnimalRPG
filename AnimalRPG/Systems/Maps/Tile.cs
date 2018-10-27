using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Maps
{
    public class Tile
    {
        public Point Location { get => new Point( X , Y ); }
        public int X { get; set; }
        public int Y { get; set; }
        public float MoveCost { get; set; }
        public bool Occupied { get; set; }

        public Tile( int x , int y )
        {
            X = x;
            Y = y;
            MoveCost = 1;
        }
    }
}
