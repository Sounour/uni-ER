﻿using System.Windows;

namespace de.sounour.uni.er
{
    public class Lightsource : DrawableObject
    {
        public double Range { get; set; } 
        public double Brightnes { get; set; }

        public Lightsource(World w, double x = 0, double y = 0, double range = 20 , double brightness = 20) : base(w, x, y)
        {
            Range = range;
            Brightnes = brightness; 
        }

        public double CalculateBrightness(double x, double y)
        {
            Vector positioVector = new Vector(x,y);
            Vector lightPositionVector = new Vector(PositionX,PositionY);
            double distance = Vector.Subtract(positioVector, lightPositionVector).Length;
            if (distance > Range)
                return 0;
            else
                return Brightnes*(1 - (distance/Range));
        }

    }
}
