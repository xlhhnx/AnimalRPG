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

        public static Image CreateRectangle( Vector2 dimensions , Color color ) => CreateRectangle( dimensions.X , dimensions.Y , color );
        public static Image CreateRectangle(float width, float height, Color color)
        {
            CheckInitialization();

            var pixel = GetPixel();
            return new Image( pixel , Vector2.Zero , new Vector2( 1 , 1 ) , color )
            {
                DrawDimensions = new Vector2( width , height )
            };
        }

        public static Image CreateRectangle( Vector2 dimensions , Vector2 startPosition , Vector2 endPosition , Color color ) => CreateRectangle( dimensions.X , dimensions.Y , startPosition.X , startPosition.Y , endPosition.X , endPosition.Y , color );
        public static Image CreateRectangle(float width, float height, float xPos, float yPos, float xEnd, float yEnd, Color color)
        {            
            CheckInitialization();

            var colors = new Color[ (int)width * (int)height ];
            for ( var y = 0; y < height; y++ )
            {
                for ( var x = 0; x < width; x++ )
                {
                    if ( x > xPos
                        && y > yPos
                        && x <= xEnd
                        && y <= yEnd
                        )
                    {
                        colors[ x + y * (int)width ] = color;
                    }
                    else
                    {
                        colors[ x + y * (int)width ] = new Color();
                    }
                }
            }

            var texture = CreateTexture(width, height, colors);
            return new Image( texture , Vector2.Zero , new Vector2( texture.Width , texture.Height ) , Color.White );
        }

        private static Texture2D CreateTexture( float width , float height , Color[] colors )
        {
            var texture = new Texture2D( _graphicsDevice , (int)width , (int)height , false , SurfaceFormat.Color );
            texture.SetData( colors );
            return texture;
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
