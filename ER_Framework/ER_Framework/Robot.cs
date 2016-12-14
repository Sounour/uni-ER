using System;
using System.Windows;
using System.Windows.Media;

namespace de.sounour.uni.er
{
    public class Robot : DrawableObject
    {
        private double speed = 2; 
        public double Speed {
            get { return speed; }
            set {
                speed = value; 
                if (value < 0)
                    speed = 0;
                if (value > GlobalConstraints.MaxSpeed)
                    speed = GlobalConstraints.MaxSpeed;
            }
        } 

        public Vector Heading { get; set; } = new Vector(1,0);

        public Robot(World w, double x = 0, double y = 0) : base(w, x, y)
        {   
        }

        public void MoveStep()
        {
            float speedFactor = Convert.ToSingle(Speed / Heading.Length); 
            Vector step = Vector.Multiply(Heading,speedFactor);
            PositionX += step.X;
            PositionY += step.Y;
        }

        public void Turn(double d)
        {
            Matrix transformation = Matrix.Identity;
            transformation.Rotate(d);
            Heading = Vector.Multiply(Heading, transformation); 
        }
    }
}
