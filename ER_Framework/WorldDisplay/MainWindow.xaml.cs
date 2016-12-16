using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using de.sounour.uni.er;

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
            Path p = DrawPath(App.Robot);
            p.Stroke = new SolidColorBrush(Colors.Red);
            PathCanvas.Children.Add(p);
            PathCanvas.UpdateLayout();
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
            p.Data = new PathGeometry(new List<PathFigure> {f});
            return p;
        }
    }
}