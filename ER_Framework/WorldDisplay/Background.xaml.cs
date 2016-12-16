using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using de.sounour.uni.er;

namespace WorldDisplay
{
    /// <summary>
    ///     Interaction logic for Background.xaml
    /// </summary>
    public partial class Background
    {
        public Background()
        {
            InitializeComponent();
            Children.Add(CreateBackgroundFromWorld(
                App.World, 
                Convert.ToInt32(App.World.Width),
                Convert.ToInt32(App.World.Height)));
        }


        public UIElement CreateBackgroundFromWorld(World world, int numColumns, int numRows)
        {
            var completePanel = new StackPanel();
            completePanel.Orientation = Orientation.Horizontal;
            var width = world.Width/numColumns;
            var height = world.Height/numRows;

            for (var i = 0; i < numColumns; i++)
            {
                var columnPanel = new StackPanel();
                for (var j = 0; j < numRows; j++)
                {
                    var lightlevel = Convert.ToByte(world.CalculateLightLevel(i*width + width/2, j*height + height/2));
                    var llColor = Color.FromRgb(lightlevel, lightlevel, lightlevel);
                    var r = new Rectangle
                    {
                        Width = width,
                        Height = height,
                        Stroke = new SolidColorBrush(llColor),
                        Fill = new SolidColorBrush(llColor)
                    };
                    columnPanel.Children.Add(r);
                }
                completePanel.Children.Add(columnPanel);
            }
            return completePanel;
        }
    }
}