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

        public void ApplyOverlay()
        {
            // TODO
        }

        private Node<T> GetNode( T item )
        {
            return _nodes.Where( n => _comparer.Equals( n.Item , item ) )
                         .FirstOrDefault();
        }
    }
}
