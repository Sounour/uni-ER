namespace de.sounour.uni.er
{
    public abstract class DrawableObject
    {
        private readonly World exteriorWorld;

        private double positionX;

        private double positionY;

        protected DrawableObject(World w, double x = 0, double y = 0)
        {
            exteriorWorld = w;
            PositionX = x;
            PositionY = y;
        }

        public double PositionX
        {
            get { return positionX; }
            set { positionX = exteriorWorld.ToX(value); }
        }

        public double PositionY
        {
            get { return positionY; }
            set { positionY = exteriorWorld.ToY(value); }
        }
    }
}