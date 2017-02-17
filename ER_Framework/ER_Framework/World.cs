using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace de.sounour.uni.er
{
    public class World
    {
        public World(int x, int y, bool boundX = false, bool boundY = false)
        {
            GlobalConstraints.BoundX = boundX;
            GlobalConstraints.BoundY = boundY;
            Width = x;
            Height = y;

            Lightsources = new List<Lightsource>();
            Objects = new List<DrawableObject>();
            Robots = new List<Robot> {new Robot(this)};
        }

        public World()
        {
        }

        public int Width { get; set; } = 200;
        public int Height { get; set; } = 200;
        public int Speed { get; set; } = 5;

        public List<Lightsource> Lightsources { get; }
        public List<DrawableObject> Objects { get; private set; }
        public List<Robot> Robots { get; set; }

        public void Step()
        {
            foreach (Robot r in Robots)
            {
                Vector step = r.GetStep();
                Console.WriteLine("Step:{0}, Length: {1}", step, step.Length);
                // TODO Check if step is possible 
                if (true)
                    r.TakeStep(step);
            }
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
            {
                if (x < 0)
                    positionX = x + Width;
                else if (x > Width)
                    positionX = x - Width;
                else
                    positionX = x;
            }

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
            {
                if (y < 0)
                    positionY = y + Height;
                else if (y > Height)
                    positionY = y - Height;
                else
                    positionY = y;
            }

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