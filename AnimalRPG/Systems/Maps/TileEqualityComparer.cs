using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Maps
{
    public class TileEqualityComparer : IEqualityComparer<Tile>
    {
        public bool Equals( Tile x , Tile y )
        {
            return x == y;
        }

        public int GetHashCode( Tile obj )
        {
            return obj.GetHashCode();
        }
    }
}
