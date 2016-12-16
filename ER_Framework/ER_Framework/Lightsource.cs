using System.Windows;

namespace de.sounour.uni.er
{
    public class Lightsource : DrawableObject
    {
        public Lightsource(World w, double x = 0, double y = 0, double range = 20, int brightness = 255) : base(w, x, y)
        {
            Range = range;
            Brightnes = brightness;
        }

        public double Range { get; set; }
        public double Brightnes { get; set; }

        public double CalculateBrightness(double x, double y)
        {
            Vector positioVector = new Vector(x, y);
            Vector lightPositionVector = new Vector(PositionX, PositionY);
            double distance = Vector.Subtract(positioVector, lightPositionVector).Length;
            if (distance > Range)
                return 0;
            return Brightnes*(1 - distance/Range);
        }
    }
}