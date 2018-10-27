using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Input.Timers
{
    public interface ITimer
    {
        void Start();
        void Update( GameTime gameTime );
        void End();
    }
}
