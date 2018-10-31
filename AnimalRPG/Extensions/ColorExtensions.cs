using AnimalRPG.Common;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Extensions
{
    public static class ColorExtensions
    {
        public static Color Add( this Color c1 , Color c2 )
        {
            return new Color(
                Utility.Clamp( 0 , 255 , c1.R + c2.R , IntegerComparer.New ) ,
                Utility.Clamp( 0 , 255 , c1.G + c2.G , IntegerComparer.New ) ,
                Utility.Clamp( 0 , 255 , c1.B + c2.B , IntegerComparer.New ) ,
                Utility.Clamp( 0 , 255 , c1.A + c2.A , IntegerComparer.New )
                );
        }

        public static Color Minus( this Color c1 , Color c2 )
        {
            return new Color(
                Utility.Clamp( 0 , 255 , c1.R - c2.R , IntegerComparer.New ) ,
                Utility.Clamp( 0 , 255 , c1.G - c2.G , IntegerComparer.New ) ,
                Utility.Clamp( 0 , 255 , c1.B - c2.B , IntegerComparer.New ) ,
                Utility.Clamp( 0 , 255 , c1.A - c2.A , IntegerComparer.New )
                );
        }
    }
}
