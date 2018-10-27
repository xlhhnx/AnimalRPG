using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Display
{
    public static class Camera
    {
        public static Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                CalculateVisibleRectangle();
            }
        }
        public static Vector2 Dimensions
        {
            get => _dimensions;
            set
            {
                _dimensions = value;
                CalculateVisibleRectangle();
            }
        }
        public static Rectangle VisibleRectangle { get => _visibleRectangle; }

        private static Rectangle _visibleRectangle;
        private static Vector2 _position;
        private static Vector2 _dimensions;

        public static void Initialize( Vector2 position , Vector2 dimensions )
        {
            _position = position;
            _dimensions = dimensions;

            CalculateVisibleRectangle();
        }

        public static bool IsVisible( Rectangle drawRectangle )
        {
            return VisibleRectangle.Intersects( drawRectangle );
        }

        public static Vector2 ConvertToScreenCoordinates( Vector2 worldCoordinates )
        {
            return worldCoordinates - Position;
        }

        public static Vector2 ConvertToWorldCoordinates( Vector2 screenCoordinates )
        {
            return screenCoordinates + Position;
        }

        private static void CalculateVisibleRectangle()
        {
            _visibleRectangle = new Rectangle( Position.ToPoint() , Dimensions.ToPoint() );
        }
    }
}
