using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER_Framework
{
    public abstract class DrawableObject
    {
        private World exteriorWorld; 

        private double positionX;
        public double PositionX
        {
            get { return positionX; }
            set { positionX = value % exteriorWorld.SizeX; }
        }

        private double positionY;
        public double PositionY
        {
            get { return positionY; }
            set { positionY = value % exteriorWorld.SizeY; }
        }

        public DrawableObject(World w, double x =0 , double y = 0) 
        {
            this.exteriorWorld = w;
            this.PositionX = x;
            this.PositionY = y; 
        }

    }
}
