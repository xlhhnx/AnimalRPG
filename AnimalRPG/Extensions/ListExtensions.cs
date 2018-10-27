using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Extensions
{
    public static class ListExtensions
    {
        public static List<T> SubRange<T>( this List<T> list , int startIndex , int endIndex )
        {
            var length = endIndex - startIndex;
            return list.SubList( startIndex , length );
        }

        public static List<T> SubList<T>( this List<T> list , int startIndex , int length )
        {
            T[] result = new T[ length ];
            Array.Copy( list.ToArray() , startIndex , result , 0 , length );
            return result.ToList();
        }

        public static int LastIndex<T>( this List<T> list)
        {
            return list.Count - 1;
        }
    }
}
