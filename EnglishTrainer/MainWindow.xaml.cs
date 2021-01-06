using EnglishTrainer.Classes;
using EnglishTrainer.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class MainWindow : INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        /// <summary>
        /// Словарь сопоставления класса корабля и визуальным представлением корабля
        /// </summary>
        private Dictionary<SpaceShip, ShipControl> _shipObjects = new Dictionary<SpaceShip, ShipControl>();

        /// <summary>
        /// Текущая скорость передвижения кораблей
        /// </summary>
        private int _speed = 1;

        /// <summary>
        /// Расстояние от Земли при котором Земле конец
        /// </summary>
        private int _endGameDistance = 80;

        /// <summary>
        /// Окончена ли игра
        /// </summary>
        private bool _gameEnded = true;

        /// <summary>
        /// Уровень текущей игры
        /// </summary>
        private Level _level;

        /// <summary>
        /// Количество набранных очков, private
        /// </summary>
        private int _score;

        /// <summary>
        /// Количество набранных очков, public
        /// </summary>
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Таблица рекордов для уровня Beginner
        /// </summary>
        public List<TopScoreResult> BeginnerResults
        {
            get
            {
                return TopScore.GetTopScores().Where(s => s.Level == Level.Beginner).OrderByDescending(s => s.Score).Take(10).ToList();
            }
        }

        /// <summary>
        /// Таблица рекордов для уровня Standard
        /// </summary>
        public List<TopScoreResult> StandardResults
        {
            get
            {
                return TopScore.GetTopScores().Where(s => s.Level == Level.Standard).OrderByDescending(s => s.Score).Take(10).ToList();
            }
        }

        /// <summary>
        /// Таблица рекордов для уровня Advanced
        /// </summary>
        public List<TopScoreResult> AdvancedResults
        {
            get
            {
                return TopScore.GetTopScores().Where(s => s.Level == Level.Advanced).OrderByDescending(s => s.Score).Take(10).ToList();
            }
        }

        public MainWindow()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            InitializeComponent();

            DataContext = this;

            Map.Width = Map.Height = MapWidth;
        }

        /// <summary>
        /// Метод после загрузки визуальной части окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        /// <summary>
        /// Точка центра карты
        /// </summary>
        private Point MapCenter
        {
            get
            {
                var centerX = Map.ActualWidth / 2;
                var centerY = Map.ActualHeight / 2;
                return new Point(centerX, centerY);
            }
        }

        /// <summary>
        /// Ширина карты
        /// </summary>
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

        /// <summary>
        /// Метод очистки карты - подготовка к новой игре
        /// </summary>
        private void ClearMap()
        {
            _shipObjects.Clear();
            Map.Children.Clear();
        }

        /// <summary>
        /// Метод при наступлении конца текущей игры
        /// </summary>
        private void EndGame()
        {
            _gameEnded = true;
            var enterNameWindow = new EnterNameWindow(_level, Score);
            enterNameWindow.ShowDialog();

        }

        /// <summary>
        /// Функционал игрового процесса
        /// </summary>
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

        /// <summary>
        /// Создание нового корабля
        /// </summary>
        private void CreateNewShip()
        {
            var newShip = new SpaceShip(MapCenter, MapWidth, _speed, _level);
            var newShipObject = new ShipControl(newShip);

            Panel.SetZIndex(newShipObject, 11);
            Map.Children.Add(newShipObject);

            Canvas.SetTop(newShipObject, -1000);
            Canvas.SetLeft(newShipObject, -1000);

            _shipObjects[newShip] = newShipObject;
        }

        /// <summary>
        /// Начало новой игры
        /// </summary>
        /// <param name="level">Уровень игры</param>
        private void StartNewGame(Level level)
        {
            Score = 0;
            _level = level;
            WordTb.Focus();
            GameProcess();
        }

        /// <summary>
        /// Метод обработки ввода слов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        Score++;
                    }
                }

                WordTb.Clear();
            }
        }

        /// <summary>
        /// Старт новой игры уровня Beginner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void NewGame_Click(object sender, RoutedEventArgs e)
        {
            var level = (Level)(sender as Button).Tag;

            _gameEnded = true;
            await Task.Delay(500);

            ClearMap();
            StartNewGame(level);
        }
    }
}
