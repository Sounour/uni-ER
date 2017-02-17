using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace de.sounour.uni.er
{
    public class Robot : DrawableObject
    {
        private double speed = 2;

        public Robot(World w, double x = 0, double y = 0) : base(w, x, y)
        {
            StartinPoint = new Point(x, y);
            TakenPath = new List<Point>();
        }

        public Point StartinPoint { get; }

        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                if (value < 0)
                    speed = 0;
                if (value > GlobalConstraints.MaxSpeed)
                    speed = GlobalConstraints.MaxSpeed;
            }
        }

        public Vector Heading { get; set; } = new Vector(1, 0);
        public List<Point> TakenPath { get; }

        public Vector GetStep()
        {
            float speedFactor = Convert.ToSingle(Speed/Heading.Length);
            return Vector.Multiply(Heading, speedFactor);
        }

        public bool TakeStep(Vector v)
        {
            PositionX += v.X;
            PositionY += v.Y;
            TakenPath.Add(new Point(PositionX, PositionY));
            return true;
        }


        public void Turn(double d)
        {
            Matrix transformation = Matrix.Identity;
            transformation.Rotate(d);
            Heading = Vector.Multiply(Heading, transformation);
        }

        /// <inheritdoc />
        public override Visual Draw()
        {
            return new Rectangle() {Width = 10, Height = 10, Fill = Brushes.Red, Visibility = Visibility.Visible};
        }
    }
}