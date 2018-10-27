using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Graphics
{
    public static class Primitive
    {
        private static GraphicsDevice _graphicsDevice;
        private static bool _initialized = false;

        private static Texture2D _pixel;

        public static void Initialize(GraphicsDevice device)
        {
            _graphicsDevice = device;
            _initialized = true;
        }

        public static Image CreateRectangle(float width, float height, Color color)
        {
            CheckInitialization();

            var pixel = GetPixel();
            return new Image( pixel , Vector2.Zero , new Vector2( 1 , 1 ) , color )
            {
                DrawDimensions = new Vector2( width , height )
            };
        }

        private static Texture2D GetPixel()
        {
            if ( ReferenceEquals( _pixel , null ) )
            {
                _pixel = new Texture2D( _graphicsDevice , 1 , 1 , false , SurfaceFormat.Color );
                _pixel.SetData( new Color[ 1 ] { Color.White } );
            }
            return _pixel;
        }

        private static void CheckInitialization()
        {
            if ( !_initialized )
                throw new InvalidOperationException("Prmitive must be initialized with a GraphicsDevice before any images can be created.");
        }
    }
}
