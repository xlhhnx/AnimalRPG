using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Maps
{
    public class TileEqualityComparer : IEqualityComparer<Tile>
    {
        public bool Equals( Tile t1 , Tile t2 )
        {
            if ( !ReferenceEquals( t1 , null ) && !ReferenceEquals( t2 , null ) )
            {
                return t1.X == t2.X && t1.Y == t2.Y;
            }
            return false;
        }

        public int GetHashCode( Tile obj )
        {
            return obj.GetHashCode();
        }
    }
}
