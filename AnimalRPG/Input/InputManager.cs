using AnimalRPG.Input.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Input
{
    public class InputManager
    {
        public Dictionary<int , IController> Controllers { get; set; }

        public InputManager()
        {
            Controllers = new Dictionary<int , IController>();
        }

        public void Update()
        {
            foreach ( var c in Controllers.Values )
            {
                c.Update();
            }
        }
    }
}
