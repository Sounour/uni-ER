using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using de.sounour.uni.er;
using WorldDisplay.ViewModel;

namespace WorldDisplay
{
    /// <summary>
    ///     Interaction logic for FloorWithLight.xaml
    /// </summary>
    public partial class FloorWithLight : Canvas
    {
        private int numberOfXTiles = 100;
        private int numberOfYTiles = 100;

        private World world; 

        public FloorWithLight() : base()
        {
            InitializeComponent();
            GenerateWorldCommand = new CommandExecution<World>(GenerateWorld, x => true);
        }

        public ICommand GenerateWorldCommand { get; private set; }

        public int NumberOfXTiles
        {
            get { return numberOfXTiles; }
            set { numberOfXTiles = value < 0 ? 0: value > world.Width ? world.Width :  value; }
        }

        public int NumberOfYTiles
        {
            get { return numberOfYTiles; }
            set { numberOfYTiles = value < 0 ? 0 : value > world.Height ? world.Height : value; }
        }

        private void GenerateWorld(object obj) 
        {
            world = obj as World ?? world;
            UIElement generatedElement = CreateBackgroundFromWorld();
            this.Children.Add(generatedElement);
            this.UpdateLayout();
        }


        public UIElement CreateBackgroundFromWorld()
        {
            if (world == null)
            {
                return new Label {Content = "No World created."};    
            }

            StackPanel completePanel = new StackPanel();
            completePanel.Orientation = Orientation.Horizontal;
            int width = world.Width/NumberOfXTiles;
            int height = world.Height/NumberOfYTiles;

            for (int i = 0; i < NumberOfXTiles; i++)
            {
                StackPanel columnPanel = new StackPanel();
                for (int j = 0; j < NumberOfYTiles; j++)
                {
                    byte lightlevel = Convert.ToByte(world.CalculateLightLevel(i*width + width/2, j*height + height/2));
                    Color llColor = Color.FromRgb(lightlevel, lightlevel, lightlevel);
                    Rectangle r = new Rectangle
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