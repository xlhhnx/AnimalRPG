using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace AnimalRPG.Input.Timers
{
    public class DragTimer : ITimer
    {
        public bool IsDrag { get { return _started ? _elapsedTime.TotalMilliseconds > _timeout : false; } }

        private TimeSpan _elapsedTime;
        private int _timeout;
        private bool _started;

        public DragTimer( int timeout )
        {
            _timeout = timeout;

            _elapsedTime = new TimeSpan();
        }

        public void End()
        {
            _started = false;
            _elapsedTime = new TimeSpan();
        }

        public void Start()
        {
            _started = true;
        }

        public void Update( GameTime gameTime )
        {
            if ( _started )
            {
                _elapsedTime += gameTime.ElapsedGameTime;
            }
        }
    }
}
