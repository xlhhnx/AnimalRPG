using AnimalRPG.Extensions;
using AnimalRPG.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Maps.Pathfinding
{
    public class Path<T>
    {
        public List<T> Nodes { get; set; }
        public int CurrentIndex { get; set; }

        private IEqualityComparer<T> _comparer;

        public Path(IEqualityComparer<T> comparer)
        {
            _comparer = comparer;

            Nodes = new List<T>();
        }

        public void Advance( bool prune = false )
        {
            CurrentIndex++;

            if ( prune )
            {
                Prune();
            }
        }

        public void Prune()
        {
            Nodes = Nodes.SubRange( CurrentIndex , Nodes.LastIndex() );
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            if ( typeof( T ) == typeof( Tile ) )
            {
                var image = Primitive.CreateRectangle( new Vector2( 32 ) , new Vector2( 8 ) , new Vector2( 23 ) , Color.Magenta );
                foreach ( var t in Nodes )
                {
                    var tile = t as Tile;
                    image.DrawPosition = tile.Location;
                    image.Draw( spriteBatch );
                }
            }
        }

        public void Join( Path<T> path )
        {
            var otherNodes = path.Nodes;
            if ( _comparer.Equals( otherNodes[ 0 ] , Nodes[ Nodes.LastIndex() ] ) )
                otherNodes = otherNodes.SubRange( 1 , otherNodes.LastIndex() );

            Nodes.AddRange( otherNodes );
        }
    }
}
