﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Common
{
    public class IntegerComparer : IComparer<int>
    {
        public static IntegerComparer New { get { return new IntegerComparer(); } }

        public int Compare( int x , int y )
        {
            if ( x < y )
                return -1;
            else if ( x > y )
                return 1;
            else
                return 0;
        }
    }
}
