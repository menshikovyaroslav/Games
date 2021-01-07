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

        private Dictionary<Shot, ShotControl> _shotObjects = new Dictionary<Shot, ShotControl>();

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

                // Движение кораблей
                foreach (var ship in currShips)
                {
                    ship.DoStep();

                    if (ship.Distance <= _endGameDistance)
                    {
                        EndGame();
                        break;
                    }

                    var shipObject = _shipObjects[ship];

                    Canvas.SetTop(shipObject, ship.CurrentPosition.Y - shipObject.ActualHeight / 2);
                    Canvas.SetLeft(shipObject, ship.CurrentPosition.X - shipObject.ActualWidth / 2);
                }

                // Движение ракет
                var currShots = _shotObjects.Keys.Where(s => s.IsEnabled).ToList();
                foreach (var shot in currShots)
                {
                    shot.DoStep();

                    var shotObject = _shotObjects[shot];

                    Canvas.SetTop(shotObject, shot.CurrentPosition.Y - shotObject.ActualHeight / 2);
                    Canvas.SetLeft(shotObject, shot.CurrentPosition.X - shotObject.ActualWidth / 2);

                    // Если ракета улетела за пределы карты, то ее нужно убрать из логики
                    if (shot.Distance > MapWidth)
                    {
                        shot.IsEnabled = false;
                        if (Map.Children.Contains(_shotObjects[shot]))
                        {
                            Map.Children.Remove(_shotObjects[shot]);
                        }
                    }
                }

                // Обработка логики ракет летящих в цель
                var currSureShots = _shotObjects.Keys.Where(s => s.IsEnabled && s.IsSureShot).ToList();
                foreach (var shot in currSureShots)
                {
                    if (shot.Distance >= shot.Ship.Distance)
                    {
                        shot.IsEnabled = false;
                        shot.Ship.IsEnabled = false;

                        if (Map.Children.Contains(_shipObjects[shot.Ship]))
                        {
                            Map.Children.Remove(_shipObjects[shot.Ship]);
                            Score++;
                        }

                        if (Map.Children.Contains(_shotObjects[shot]))
                        {
                            Map.Children.Remove(_shotObjects[shot]);
                        }
                    }
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

        private void Shoot(SpaceShip ship)
        {
            var shot = new Shot(ship, MapCenter);


            var newShotObject = new ShotControl(shot);

            Panel.SetZIndex(newShotObject, 20);
            Map.Children.Add(newShotObject);

            Canvas.SetTop(newShotObject, -1000);
            Canvas.SetLeft(newShotObject, -1000);

            _shotObjects[shot] = newShotObject;

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
                var goalShips = _shipObjects.Keys.Where(s => s.IsEnabled && s.Word.Answer.ToLower() == entered.ToLower()).ToList();

                Shoot(goalShips.FirstOrDefault());

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
