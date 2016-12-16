using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace de.sounour.uni.er
{
    public class Robot : DrawableObject
    {
        private readonly Point startingVector; 

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
        public List<Point> TakenPath { get;  }

        public Robot(World w, double x = 0, double y = 0) : base(w, x, y)
        {
            startingVector = new Point(x,y);
            TakenPath = new List<Point>();
        }

        public Vector GetStep()
        {
            float speedFactor = Convert.ToSingle(Speed / Heading.Length); 
            return Vector.Multiply(Heading,speedFactor);
        }

        public bool TakeStep(Vector v)
        {
            PositionX += v.X;
            PositionY += v.Y;
            TakenPath.Add(new Point(PositionX,PositionY));
            return true;
        }


        public void Turn(double d)
        {
            Matrix transformation = Matrix.Identity;
            transformation.Rotate(d);
            Heading = Vector.Multiply(Heading, transformation); 
        }

        public Path DrawPath()
        {
            Path p = new Path();

            PathFigure f = new PathFigure(startingVector,  new List<PathSegment>(), false);

            foreach (Vector vector in TakenPath)
            {
                LineSegment segment = new LineSegment(new Point(vector.X, vector.Y), true);
                f.Segments.Add(segment);
            }

            p.Data = new PathGeometry(new List<PathFigure>() {f});
            return p; 
        }
    }
}
