using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Display.Controls
{
    public interface IControl
    {
        void Draw( SpriteBatch spriteBatch );
    }
}
