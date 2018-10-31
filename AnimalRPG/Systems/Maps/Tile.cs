using AnimalRPG.Display;
using AnimalRPG.Extensions;
using AnimalRPG.Graphics;
using AnimalRPG.Input.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AnimalRPG.Systems.Maps
{
    public class Tile
    {
        public static Random rand = new Random();

        public Point MapIndex { get => new Point( X , Y ); }
        public Vector2 Location { get => new Vector2( X * 32 , Y * 32 ); }
        public Terrain Terrain { get; set; }
        public Image Overlay { get; set; }
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
            MoveCost = rand.Next( 1 , 10 );
            var colorVector = new Vector3( 255 ) / MoveCost;
            var color = new Color( (int)colorVector.X , (int)colorVector.Y , (int)colorVector.Z , 255 );
            var image = Primitive.CreateRectangle( 32 , 32 , color );

            Terrain = new Terrain()
            {
                Name = "Test" ,
                MoveCost = MoveCost ,
                Image = image
            };
            Overlay = Primitive.CreateRectangle( 32 , 32 , new Color() );

            X = x;
            Y = y;
            Overlay.DrawPosition = new Vector2( X * 32 , Y * 32 );
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            Terrain.Draw( spriteBatch );
            Overlay.Draw( spriteBatch );
        }
    }
}
