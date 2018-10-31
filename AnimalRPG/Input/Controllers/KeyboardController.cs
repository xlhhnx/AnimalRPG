using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Input.Controllers
{
    public class KeyboardController : IController
    {
        public int Id { get; set; }
        
        private List<Keys> _allKeys;
        private KeyboardState _currentState;
        private KeyboardState _previousState;

        public KeyboardController(int id)
        {
            Id = id;
            _previousState = new KeyboardState();
            _allKeys = Enum.GetValues( typeof( Keys ) )
                .Cast<Keys>()
                .ToList();
        }

        public void Update()
        {
            _currentState = Keyboard.GetState();

            foreach ( var k in _allKeys )
            {
                if ( _currentState.IsKeyDown( k ) && _previousState.IsKeyUp( k ) )
                    OnKeyPress( Id , k );
                else if ( _currentState.IsKeyDown( k ) )
                    OnKeyDown( Id , k );
                else if ( _previousState.IsKeyDown( k ) )
                    OnKeyRelease( Id , k );
                else
                    OnKeyUp( Id , k );
            }

            _previousState = _currentState;
        }
        
        // Static Events
        public static event Action<int , Keys> OnKeyPress = delegate
         { };
        public static event Action<int , Keys> OnKeyDown = delegate
         { };
        public static event Action<int , Keys> OnKeyRelease = delegate
         { };
        public static event Action<int , Keys> OnKeyUp = delegate
         { };
    }
}
