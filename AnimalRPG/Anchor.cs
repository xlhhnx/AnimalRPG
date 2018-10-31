using AnimalRPG.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG
{
    public class Anchor
    {
        public Vector2 Position { get => new Vector2( X , Y ); }
        public float X
        {
            get => _x;
            set
            {
                _x = value;
                _image.DrawPosition = Position;
            }
        }
        public float Y
        {
            get => _y;
            set
            {
                _y = value;
                _image.DrawPosition = Position;
            }
        }

        private float _x;
        private float _y;
        private Image _image;

        public Anchor(float x, float y, Color color)
        {
            _x = x;
            _y = y;

            _image = Primitive.CreateRectangle( new Vector2( 32 ) , new Vector2( 4 ) , new Vector2( 27 ) , color );
            _image.DrawPosition = Position;
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            _image.Draw( spriteBatch );
        }
    }
}
