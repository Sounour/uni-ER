using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using de.sounour.uni.er;
using Color = System.Windows.Media.Color;

namespace WorldDisplay
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            World w = new World(500,300,true,true);
            Random r = new Random();
            Robot robot = w.Robots.FirstOrDefault();
            
            w.Lightsources.Add(new Lightsource(w,20,20,200));
            if (robot == null)
            {
                return; 
            }
            robot.Speed = 3; 

            for (int i = 0; i < 100; i++)
            {
                w.Step();
                double d = r.Next(720);
                if (d <= 60)
                {
                    robot.Turn(d-20);
                    Console.WriteLine("Turning");
                }
            }

            BackgroundCanvas.Children.Add(w.GetBackground(250,150));

            Path p = robot.DrawPath();
            p.Stroke = new SolidColorBrush(Colors.Red);
            PathCanvas.Children.Add(p);
            PathCanvas.UpdateLayout();

        }
    }
}