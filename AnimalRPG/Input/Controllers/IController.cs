using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRPG.Input.Controllers
{
    public interface IController
    {
        int Id { get; set; }

        void Update();
    }
}
