using EnglishTrainer.Classes;
using EnglishTrainer.UserControls;
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
        private Dictionary<SpaceShip, ShipControl> _shipObjects = new Dictionary<SpaceShip, ShipControl>();

        private int _speed = 1;
        private int _endGameDistance = 80;
        private bool _gameEnded = true;
        private Level _level;
        private int _score;

        public List<TopScoreResult> BeginnerResults
        {
            get
            {
                return TopScore.GetTopScores().Where(s => s.Level == Level.Beginner).ToList();
            }
        }
        public List<TopScoreResult> StandardResults
        {
            get
            {
                return TopScore.GetTopScores().Where(s => s.Level == Level.Standard).ToList();
            }
        }
        public List<TopScoreResult> AdvancedResults
        {
            get
            {
                return TopScore.GetTopScores().Where(s => s.Level == Level.Advanced).ToList();
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Map.Width = Map.Height = MapWidth;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

        private void ClearMap()
        {
            _shipObjects.Clear();
            Map.Children.Clear();
        }

        private void EndGame()
        {
            _gameEnded = true;
            var enterNameWindow = new EnterNameWindow(_level, _score);
            enterNameWindow.ShowDialog();

        }

        private async void GameProcess()
        {
            _gameEnded = false;
            while (!_gameEnded)
            {
                var currShips = _shipObjects.Keys.Where(s => s.IsEnabled).ToList();

                foreach (var ship in currShips)
                {
                    ship.DoStep();

                    if (ship.Distance <= _endGameDistance)
                    {
                        EndGame();
                        break;
                    }
                }

                foreach (var shipPair in _shipObjects)
                {
                    Canvas.SetTop(shipPair.Value, shipPair.Key.CurrentPosition.Y - 50);
                    Canvas.SetLeft(shipPair.Value, shipPair.Key.CurrentPosition.X - 30);
                }

                if (currShips.Count == 0)
                {
                    CreateNewShip();
                }

                await Task.Delay(10);
            }
        }

        private void CreateNewShip()
        {
            var newShip = new SpaceShip(MapCenter, MapWidth, _speed);
            var newShipObject = new ShipControl(newShip);

            Panel.SetZIndex(newShipObject, 11);
            Map.Children.Add(newShipObject);

            Canvas.SetTop(newShipObject, -1000);
            Canvas.SetLeft(newShipObject, -1000);

            _shipObjects[newShip] = newShipObject;
        }

        private void StartNewGame(Level level)
        {
            _level = level;
            WordTb.Focus();
            GameProcess();
        }

        private void WordTb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                var entered = WordTb.Text;
                var goalShips = _shipObjects.Keys.Where(s=>s.IsEnabled && s.Word.Answer.ToLower() == entered.ToLower()).ToList();
                foreach (var ship in goalShips)
                {
                    ship.IsEnabled = false;

                    if (Map.Children.Contains(_shipObjects[ship]))
                    {
                        Map.Children.Remove(_shipObjects[ship]);
                    }
                }

                WordTb.Clear();
            }
        }

        private async void NewGameLevel1(object sender, RoutedEventArgs e)
        {
            _gameEnded = true;
            await Task.Delay(500);

            ClearMap();
            StartNewGame(Level.Beginner);
        }

        private async void NewGameLevel2(object sender, RoutedEventArgs e)
        {
            _gameEnded = true;
            await Task.Delay(500);

            ClearMap();
            StartNewGame(Level.Standard);
        }
    }
}
