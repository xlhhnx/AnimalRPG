using AnimalRPG.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace AnimalRPG.Systems.Maps.Pathfinding
{
    public class SearchRegion<T>
    {
        private List<Node<T>> _nodes;
        private IEqualityComparer<T> _comparer;

        public SearchRegion(IEqualityComparer<T> comparer)
        {
            _comparer = comparer;

            _nodes = new List<Node<T>>();
        }

        public void Add( T item , T parent )
        {
            var parentNode = GetNode( parent );
            if ( ReferenceEquals( parent , null ) || ReferenceEquals( parentNode , null ) )
                _nodes.Add( new Node<T>( item , null ) );
            else if ( !ReferenceEquals( item , null ) )
                _nodes.Add( new Node<T>( item , parentNode ) );
        }

        public Path<T> GetPath( T item )
        {
            var items = new Stack<T>();

            var node = GetNode( item );
            if ( !ReferenceEquals( node , null ) )
            {
                items.Push( node.Item );
                while ( !ReferenceEquals( node.Parent , null ) )
                {
                    node = node.Parent;
                    items.Push( node.Item );
                }
            }
            else
                return null;

            var path = new Path<T>( _comparer );
            while ( items.Count > 0 )
            {
                path.Nodes.Add( items.Pop() );
            }
            return path;
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            if ( typeof( T ) == typeof( Tile ) )
            {
                var image = Primitive.CreateRectangle( new Vector2( 32 ) , new Vector2( 4 ) , new Vector2( 27 ) , new Color( 255 , 255 , 0 , 150 ) );
                foreach ( var t in _nodes )
                {
                    var tile = t.Item as Tile;
                    image.DrawPosition = tile.Location;
                    image.Draw( spriteBatch );
                }
            }
        }

        public bool ContainsItem( T item )
        {
            return _nodes.Where( n => _comparer.Equals( n.Item , item ) )
                         .Any();
        }

        private Node<T> GetNode( T item )
        {
            return _nodes.Where( n => _comparer.Equals( n.Item , item ) )
                         .FirstOrDefault();
        }
    }
}
