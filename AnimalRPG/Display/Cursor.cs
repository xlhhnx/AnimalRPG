using AnimalRPG.Graphics;
using AnimalRPG.Input.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalRPG.Display
{
    public static class Cursor
    {
        public static Image Ghost { get; set; }
        public static Image Icon
        {
            get => _icon;
            set
            {
                if ( !ReferenceEquals( value , null ) )
                {
                    _icon = value;
                    MouseCursor.FromTexture2D( _icon.Texture , (int)_icon.SourcePosition.X , (int)_icon.SourcePosition.Y );
                }
            }
        }

        private static Image _icon;
        private static Vector2 _ghostAreaDimensions;

        public static void Initialize( Vector2 ghostAreaDimensions)
        {
            MouseController.MouseMove += HandleMouseMove;
            SetGhostArea( ghostAreaDimensions );
        }

        public static void SetGhostArea( Vector2 dimensions )
        {
            _ghostAreaDimensions = dimensions;
        }

        public static void HandleMouseMove( int index , Vector2 position )
        {
            if ( position.X > 0 
                && position.Y > 0  
                && position.X < _ghostAreaDimensions.X 
                && position.Y < _ghostAreaDimensions.Y)
            {
                var reduced = (position / 32).ToPoint();
                Ghost.DrawPosition = reduced.ToVector2() * 32;
                Ghost.Enabled = true;
            }
            else
            {
                Ghost.Enabled = false;
            }
        }

        public static void Draw( SpriteBatch spriteBatch )
        {
            if(!ReferenceEquals(Ghost, null))
                Ghost.Draw( spriteBatch );
        }
    }
}
