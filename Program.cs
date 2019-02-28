using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace snake_sfml_cs
{
    class Program
    {
        static void Main(string[] args)
        {
            const uint brickSize = 25;
            uint x = 30, y = 20;    // TODO: menu

            World world = new World(x, y, brickSize);
            world.Start();
        }
    }
}
