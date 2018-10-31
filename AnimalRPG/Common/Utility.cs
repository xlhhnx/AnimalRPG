using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Common
{
    public static class Utility
    {
        public static T Clamp<T>( T min , T max , T value, IComparer<T> comparer)
        {
            if ( comparer.Compare(value, min) < 0 )
                return min;
            else if ( comparer.Compare(value, max) > 0 )
                return max;
            else
                return value;
        }
    }
}
