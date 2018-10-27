using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Maps.Pathfinding
{
    public class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Parent { get; set; }

        public Node( T item , Node<T> parent )
        {
            Item = item;
            Parent = parent;
        }
    }
}
