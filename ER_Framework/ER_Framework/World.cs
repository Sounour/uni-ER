using System.Collections.Generic;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;

namespace de.sounour.uni.er
{


    public class World : FrameworkElement
    {
        public List<Lightsource> Lightsources { get; private set; }
        public List<DrawableObject> Objects { get; private set; } 
     
        public World(double x  ,double y , bool boundX = false, bool boundY = false)
        {
            GlobalConstraints.BoundX = boundX;
            GlobalConstraints.BoundY = boundY;
            this.Width = x;
            this.Height = y;
             
        }

        public World()
        {

        }

        public double ToX(double x)
        {
            double positionX; 
            if (GlobalConstraints.BoundX)
            {
                if (x < 0)
                    positionX = 0;
                else if (x > Width)
                    positionX = Width;
                else
                    positionX = x;
            }
            else
                positionX = x % Width;

            return positionX; 
        }

        public double ToY(double y)
        {
            double positionY;
            if (GlobalConstraints.BoundY)
            {
                if (y < 0)
                    positionY = 0;
                else if (y > Height)
                    positionY = Height;
                else
                    positionY = y;
            }
            else
                positionY = y % Height;

            return positionY;
        }

        public double CalculateLightLevel(double x, double y)
        {
            double result = 0;
            foreach (Lightsource l in Lightsources)
            {
                double t = l.CalculateBrightness(x, y);
                result = result > t ? result : t; 
            }
            return result; 
        }

        

    }
}
