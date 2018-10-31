using AnimalRPG.Graphics;
using AnimalRPG.Input.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using AnimalRPG.Input;

namespace AnimalRPG.Display.Controls
{
    public class Button : IControl
    {
        public enum ButtonState
        {
            Unfocused,
            Focused,
            Clicked
        }
        public event Action OnPress = delegate
        { };

        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                foreach ( var i in _images.Values )
                {
                    i.DrawPosition = Position;
                }
                _boundingBox = new Rectangle( Position.ToPoint() , Dimensions.ToPoint() );
            }
        }
        public Vector2 Dimensions
        {
            get => _dimensions;
            set
            {
                _dimensions = value;
                foreach ( var i in _images.Values )
                {
                    i.DrawDimensions = Dimensions;
                }
                _boundingBox = new Rectangle( Position.ToPoint() , Dimensions.ToPoint() );
            }
        }
        public Rectangle BoundingBox { get => _boundingBox; }
        
        private ButtonState _state;
        private Vector2 _position;
        private Vector2 _dimensions;
        private Image ActiveImage { get; set; }
        private Rectangle _boundingBox;
        private Dictionary<ButtonState , Image> _images;

        public Button( Image unFocusedImage , Image focusedImage , Image clickedImage )
        {
            _images = new Dictionary<ButtonState , Image>()
            {
                { ButtonState.Unfocused , unFocusedImage },
                { ButtonState.Focused , focusedImage },
                { ButtonState.Clicked , clickedImage }
            };

            MouseController.OnMouseMove += HandleMouseMove;
            MouseController.OnButtonPress += HandleMouseButtonPress;
            MouseController.OnButtonRelease += HandleMouseButtonRelease;
            KeyboardController.OnKeyPress += HandleKeyPress;
        }

        private void HandleMouseMove( int index , Vector2 position )
        {
            var pos = Camera.ConvertToWorldCoordinates( position );
            if ( BoundingBox.Contains( pos.ToPoint() ) )
            {
                _state = ButtonState.Focused;
            }
            else if(_state != ButtonState.Clicked)
            {
                _state = ButtonState.Unfocused;
            }
        }

        private void HandleMouseButtonPress( int index , MouseButtons button , Vector2 position )
        {
            var pos = Camera.ConvertToWorldCoordinates( position );
            if ( BoundingBox.Contains( pos.ToPoint() ) && button == MouseButtons.Left )
            {
                _state = ButtonState.Clicked;
            }
        }

        private void HandleMouseButtonRelease( int index , MouseButtons button , Vector2 position )
        {
            var pos = Camera.ConvertToWorldCoordinates( position );
            if ( BoundingBox.Contains( pos.ToPoint() ) && button == MouseButtons.Left )
            {
                OnPress();
                HandleMouseMove( index , position );
            }
        }

        private void HandleKeyPress( int index, Keys key )
        {
            if ( _state == ButtonState.Focused )
            {
                _state = ButtonState.Clicked;
            }
        }

        public void Draw( SpriteBatch spriteBatch )
        {
            if ( _images.TryGetValue( _state , out var image ) )
            {
                image.Draw( spriteBatch );
            }
        }
    }
}
