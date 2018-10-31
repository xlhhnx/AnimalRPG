using AnimalRPG.Display.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AnimalRPG.Display
{
    public class Scene
    {
        private List<IControl> _controls;

        public Scene()
        {
            _controls = new List<IControl>();
        }

        public void Update( GameTime gameTime )
        {

        }

        public void Draw( SpriteBatch spriteBatch )
        {

        }
    }
}
