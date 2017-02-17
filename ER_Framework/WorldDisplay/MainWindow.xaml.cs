using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using de.sounour.uni.er;
using WorldDisplay.ViewModel;

namespace WorldDisplay
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            SimulationProperties properties = (SimulationProperties) DataContext; 

            RobotPath  rbPath = new RobotPath((properties).Robot);
            // Get the path and draw it. 
            PathCanvas.Children.Add(rbPath);
            PathCanvas.UpdateLayout();
        }


    }

    public class RobotPath : UIElement
    {
        public Robot Robot { get; private set; }

        public RobotPath(Robot robot)
        {
            Robot = robot;
            Path p = DrawPath(robot);
            p.Stroke = new SolidColorBrush(Colors.Red);
        }

        public Path DrawPath(Robot robot)
        {
            Path p = new Path();
            PathFigure f = new PathFigure(robot.StartinPoint, new List<PathSegment>(), false);
            foreach (Vector vector in robot.TakenPath)
            {
                LineSegment segment = new LineSegment(new Point(vector.X, vector.Y), true);
                f.Segments.Add(segment);
            }
            p.Data = new PathGeometry(new List<PathFigure> { f });
            return p;
        }
    }
}