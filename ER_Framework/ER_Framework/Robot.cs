using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ER_Framework
{
    public class Robot : DrawableObject
    {
        private double speed; 
        public double Speed {
            get { return speed; }
            set {
                speed = value; 
                if (value < 0)
                    speed = 0;
                if (value > GlobalConstraints.MAX_SPEED)
                    speed = GlobalConstraints.MAX_SPEED;
            }
        } 

        public Vector Heading { get; set; }

        public Robot(World w, double x = 0, double y = 0) : base(w, x, y)
        {   
        }

        public void MoveStep()
        {
            float speedFactor = Convert.ToSingle(Speed / Heading.Length); 
            Vector step = Vector.Multiply(Heading,speedFactor);
            this.PositionX += step.X;
            this.PositionY += step.Y;
        }

        public void turn(double d)
        {
            double cosD = Convert.ToSingle(Math.Cos(d));
            double sinD = Convert.ToSingle(Math.Sin(d));
            Matrix transformation = Matrix.Identity;
            transformation.Rotate(d);
            //new Matrix(cosD, sinD, -sinD, -cosD , 0 , 0);
            Heading = Vector.Multiply(Heading, transformation); 
        }
    }
}
