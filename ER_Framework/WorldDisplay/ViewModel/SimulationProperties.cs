using System;
using System.Linq;
using System.Windows.Input;
using de.sounour.uni.er;

namespace WorldDisplay.ViewModel
{
    public class SimulationProperties
    {
        public SimulationProperties()
        {
            World = new World(300, 300, true, true);
            World.Lightsources.Add(new Lightsource(World, 100, 100, 200));
            World.Lightsources.Add(new Lightsource(World, 160, 100, 200));
            World.Lightsources.Add(new Lightsource(World, 200, 100, 200));

            Robot = World.Robots.FirstOrDefault();
            if (Robot == null)
                return;

            TakeStepCommand = new CommandExecution<int>(TakeSteps, x => true);
        }

        public World World { get; }
        public Robot Robot { get; }

        public ICommand TakeStepCommand { get; private set; }

        protected void TakeSteps(int numberOfSteps)
        {
            for (int i = 0; i < numberOfSteps; i++)
                World.Step();
        }

        private void TakeRandomSteps(int numberOfSteps)
        {
            Random r = new Random();
            for (int i = 0; i < numberOfSteps; i++)
            {
                World.Step();
                double d = r.Next(720);
                if (d <= 90)
                {
                    Robot.Turn(d - 45);
                    Console.WriteLine(@"Turning");
                }
            }
        }
    }
}