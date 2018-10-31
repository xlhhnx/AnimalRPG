using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Input.Controllers
{
    public class MouseController : IController
    {
        public int Id { get; set; }
        
        private List<MouseButtons> _allButtons;
        private MouseState _currentState;
        private MouseState _previousState;

        public MouseController(int id)
        {
            Id = id;
            _previousState = new MouseState();
            _allButtons = Enum.GetValues( typeof( MouseButtons ) )
                .Cast<MouseButtons>()
                .ToList();
        }

        public void Update()
        {
            _currentState = Mouse.GetState();

            var currentPosition = _currentState.Position.ToVector2();
            var previousPosition = _previousState.Position.ToVector2();

            foreach ( var b in _allButtons )
            {
                switch ( b )
                {
                    case (MouseButtons.Left):
                        CheckButton( _previousState.LeftButton , _currentState.LeftButton , MouseButtons.Left , currentPosition );
                        break;
                    case (MouseButtons.Middle):
                        CheckButton( _previousState.MiddleButton , _currentState.MiddleButton , MouseButtons.Middle , currentPosition );
                        break;
                    case (MouseButtons.Right):
                        CheckButton( _previousState.RightButton , _currentState.RightButton , MouseButtons.Right , currentPosition );
                        break;
                    case (MouseButtons.X1):
                        CheckButton( _previousState.XButton1 , _currentState.XButton1 , MouseButtons.X1 , currentPosition );
                        break;
                    case (MouseButtons.X2):
                        CheckButton( _previousState.XButton2 , _currentState.XButton2 , MouseButtons.X2 , currentPosition );
                        break;
                }
            }

            if ( _currentState.ScrollWheelValue != _previousState.ScrollWheelValue )
                OnMouseScroll( Id , (_currentState.ScrollWheelValue - _previousState.ScrollWheelValue) / 120 , currentPosition );

            if ( currentPosition != previousPosition )
                OnMouseMove( Id , currentPosition );

            _previousState = _currentState;
        }

        public void CheckButton( ButtonState previousState , ButtonState currentState , MouseButtons button , Vector2 position )
        {
            if ( currentState == ButtonState.Pressed && previousState == ButtonState.Released )
                OnButtonPress( Id , button , position );
            else if ( currentState == ButtonState.Pressed )
                OnButtonDown( Id , button , position );
            else if ( previousState == ButtonState.Pressed )
                OnButtonRelease( Id , button , position );
            else
                OnButtonUp( Id , button , position );
        }

        // Static Events
        public static event Action<int , MouseButtons , Vector2> OnButtonPress = delegate
          { };
        public static event Action<int , MouseButtons , Vector2> OnButtonDown = delegate
          { };
        public static event Action<int , MouseButtons , Vector2> OnButtonRelease = delegate
          { };
        public static event Action<int , MouseButtons , Vector2> OnButtonUp = delegate
          { };
        public static event Action<int , Vector2> OnMouseMove = delegate
        { };
        public static event Action<int , int , Vector2> OnMouseScroll = delegate
        { };
    }
}
