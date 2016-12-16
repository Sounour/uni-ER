using System;
using System.Linq;
using de.sounour.uni.er;

namespace WorldDisplay
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            World = new World(500, 300, true, true);
            World.Lightsources.Add(new Lightsource(World, 20, 20, 200));

            Robot = World.Robots.FirstOrDefault();
            if (Robot == null)
                return;

            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                World.Step();
                double d = r.Next(720);
                if (d <= 90)
                {
                    Robot.Turn(d - 40);
                    Console.WriteLine(@"Turning");
                }
            }
        }

        public static World World { get; set; }
        public static Robot Robot { get; internal set; }
    }
}