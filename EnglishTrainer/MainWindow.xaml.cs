using EnglishTrainer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private List<SpaceShip> _spaceShips = new List<SpaceShip>();
        private Dictionary<SpaceShip, Image> _shipObjects = new Dictionary<SpaceShip, Image>();

        private double _gameTime = 0;

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

        private async void GameProcess()
        {
            while (true)
            {
                var currShips = _spaceShips.Where(s => s.IsEnabled).ToList();

                foreach (var ship in currShips)
                {
                    ship.DoStep();
                }

                foreach (var shipPair in _shipObjects)
                {
                    TranslateTransform translate = shipPair.Value.RenderTransform as TranslateTransform;
                    if (translate == null)
                    {
                        translate = new TranslateTransform(shipPair.Key.CurrentPosition.X, shipPair.Key.CurrentPosition.Y);
                        shipPair.Value.RenderTransform = translate;
                    }
                    translate.X = shipPair.Key.CurrentPosition.X;
                    translate.Y = shipPair.Key.CurrentPosition.Y;
                }

                await Task.Delay(2000);
            }
        }

        private void StartNewGame()
        {
            GameProcess();

            var newShip = new SpaceShip(MapCenter, MapWidth, 10);

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

            _spaceShips.Add(newShip);
            _shipObjects[newShip] = newShipObject;

            //      var x = newShipObject.
        }
    }
}
