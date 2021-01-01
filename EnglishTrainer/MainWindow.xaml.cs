using EnglishTrainer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EnglishTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Map.Width = Map.Height = MapWidth;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }

        private Point MapCenter
        {
            get
            {
                var centerX = Map.ActualWidth / 2;
                var centerY = Map.ActualHeight / 2;
                return new Point(centerX, centerY);
            }
        }

        private double MapWidth
        {
            get
            {
                var w = System.Windows.SystemParameters.PrimaryScreenWidth;
                var h = System.Windows.SystemParameters.PrimaryScreenHeight - 60;

                if (w > h) return h;
                return w;
            }
        }

        private void StartNewGame()
        {
            var newShip = new SpaceShip(MapCenter, MapWidth);

            var circle = new Ellipse()
            {
                Width = MapWidth,
                Height = MapWidth,
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Canvas.SetTop(circle, 0);
            Canvas.SetLeft(circle, 0);
            Panel.SetZIndex(circle, 1);
            Map.Children.Add(circle);

            var newShipObject = new Image()
            {
                Width = 50,
                Height = 50,
                Source = new BitmapImage(new Uri("/Images/interceptor.png", UriKind.Relative))
            };
            Canvas.SetTop(newShipObject, newShip.CurrentPosition.Y);
            Canvas.SetLeft(newShipObject, newShip.CurrentPosition.X);
            Panel.SetZIndex(newShipObject, 10);
            Map.Children.Add(newShipObject);

            //      var x = newShipObject.
        }
    }
}
