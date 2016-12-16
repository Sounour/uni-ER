using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace de.sounour.uni.er
{


    public class World
    {
        public double Width { get; set; } = 200;
        public double Height { get; set; } = 200;

        public List<Lightsource> Lightsources { get; private set; }
        public List<DrawableObject> Objects { get; private set; } 
        public List<Robot> Robots { get; set; } 
     
        public World(double x  ,double y , bool boundX = false, bool boundY = false)
        {
            GlobalConstraints.BoundX = boundX;
            GlobalConstraints.BoundY = boundY;
            this.Width = x;
            this.Height = y;

            Lightsources = new List<Lightsource>();
            Objects = new List<DrawableObject>();
            Robots = new List<Robot> {new Robot(this)};
        }

        public World()
        {

        }

        public void Step()
        {
            foreach (Robot r in Robots)
            {
                Vector step = r.GetStep();
                Console.WriteLine("Step:{0}, Length: {1}",step,step.Length);
                // TODO Check if step is possible 
                if (true)
                {
                    r.TakeStep(step);
                }
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

        public UIElement GetBackground(int numColumns, int numRows)
        {
            StackPanel completePanel= new StackPanel();
            completePanel.Orientation=Orientation.Horizontal;
            double width = this.Width/numColumns;
            double height = this.Height/numRows;

            for (int i = 0; i < numColumns; i++)
            {
                StackPanel columnPanel = new StackPanel(); 
                for (int j = 0; j < numRows; j++)
                {
                    byte lightlevel = Convert.ToByte(CalculateLightLevel(i * width + width / 2, j*height + height / 2));
                    Color llColor = Color.FromRgb(lightlevel, lightlevel, lightlevel);
                    Rectangle r = new Rectangle
                    {
                        Width = width,
                        Height = height,
                        Stroke = new SolidColorBrush(llColor),
                        Fill = new SolidColorBrush(llColor)
                    };
                    columnPanel.Children.Add(r);

                }
                completePanel.Children.Add(columnPanel);
            }
            return completePanel;
        }

    }
}
