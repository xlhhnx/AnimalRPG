using AnimalRPG.Display;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Graphics
{
    public class Image
    {
        public Vector2 DrawPosition
        {
            get => _drawPosition;
            set
            {
                _drawPosition = value;
                CalculateDrawRectangle();
            }
        }
        public Vector2 DrawDimensions
        {
            get => _drawDimensions;
            set
            {
                _drawDimensions = value;
                CalculateDrawRectangle();
            }
        }
        public Rectangle DrawRectangle { get => _drawRectangle; }
        public Color Tint
        {
            get => _tint;
            set => _tint = value;
        }
        public bool Enabled
        {
            get => _enabled;
            set => _enabled = value;
        }

        protected Texture2D _texture;
        protected Vector2 _sourcePosition;
        protected Vector2 _sourceDimensions;
        protected Rectangle _sourceRectangle;
        protected Vector2 _drawPosition;
        protected Vector2 _drawDimensions;
        protected Rectangle _drawRectangle;
        protected Color _tint;
        protected bool _enabled;

        public Image( Texture2D texture , Vector2 sourcePosition , Vector2 sourceDimensions , Color tint )
        {
            _texture = texture;
            _sourcePosition = sourcePosition;
            _sourceDimensions = sourceDimensions;
            _tint = tint;

            _enabled = true;
            _drawPosition = Vector2.Zero;
            _drawDimensions = _sourceDimensions;

            CalculateSourceRectangle();
            CalculateDrawRectangle();
        }

        public void CalculateSourceRectangle()
        {
            _sourceRectangle = new Rectangle( _sourcePosition.ToPoint() , _sourceDimensions.ToPoint() );
        }

        public void CalculateDrawRectangle()
        {
            var drawPosition = Camera.ConvertToScreenCoordinates( _drawPosition );
            _drawRectangle = new Rectangle( drawPosition.ToPoint() , _drawDimensions.ToPoint() );
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            if ( Enabled && Camera.IsVisible( DrawRectangle ) )
            {
                spriteBatch.Draw( _texture , DrawRectangle , _sourceRectangle , _tint );
            }
        }
    }
}
