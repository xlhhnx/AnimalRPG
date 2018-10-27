using AnimalRPG.Display;
using AnimalRPG.Graphics;
using AnimalRPG.Input.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Systems.Maps
{
    public class Tile
    {
        public Point MapIndex { get => new Point( X , Y ); }
        public Vector2 Location { get => new Vector2( X * 32 , Y * 32 ); }
        public Terrain Terrain { get; set; }
        public int X
        {
            get => _x;
            set
            {
                _x = value;
                Terrain.Image.DrawPosition = (MapIndex * new Point(32)).ToVector2();
            }
        }
        public int Y
        {
            get => _y;
            set
            {
                _y = value;
                Terrain.Image.DrawPosition = (MapIndex * new Point(32)).ToVector2();
            }
        }
        public float MoveCost { get; set; }
        public bool Occupied { get; set; }

        private int _x;
        private int _y;

        public Tile( int x , int y )
        {
            Terrain = new Terrain()
            {
                Name = "Test" ,
                MoveCost = 1 ,
                Image = Primitive.CreateRectangle( 32 , 32 , Color.White )
            };

            MouseController.MouseMove += HandleMouseMove;

            X = x;
            Y = y;
            MoveCost = 1;
        }

        ~Tile()
        {
            MouseController.MouseMove -= HandleMouseMove;
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            Terrain.Draw( spriteBatch );
        }

        public void HandleMouseMove( int inputIndex , Vector2 mousePosition )
        {
            var mouseInsideCurrent = IsMouseInside( mousePosition );
            if ( IsMouseInside( mousePosition ) )
                Terrain.Image.Tint = Color.Blue;
            else
                Terrain.Image.Tint = Color.White;
        }

        private bool IsMouseInside( Vector2 mousePosition )
        {
            var worldMousePosition = Camera.ConvertToWorldCoordinates( mousePosition );
            if ( 
                worldMousePosition.X > Location.X
                && worldMousePosition.Y > Location.Y
                && worldMousePosition.X < Location.X + 32
                && worldMousePosition.Y < Location.Y + 32 
                )
                return true;
            else
                return false;
        }
    }
}
