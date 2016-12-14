using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Framework
{


    public class World
    {
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

        public List<Lightsource> Lightsources { get; private set; }
        public List<DrawableObject> Objects { get; private set; } 
     
        public World(int x, int y)
        {
            this.SizeX = x;
            this.SizeY = y; 
        }    

    }
}
