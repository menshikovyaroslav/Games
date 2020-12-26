using PaperRace.Classes;
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

namespace PaperRace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int _currentX = 400;
        int _currentY = 400;

        int _deltaX, _deltaY = 0;

        int _roadWidth = 100;
        int _currentSpeedX, _currentSpeedY = 0;

        List<PathElement> _pathList = new List<PathElement>();
        List<Button> _mapPoints = new List<Button>();
        List<RoadElement> _roadElements = new List<RoadElement>();
        List<Shape> _roadObjects = new List<Shape>();

        public MainWindow()
        {
            InitializeComponent();

            NewGameStart();
        }

        /// <summary>
        /// Нарисовать пройденный путь
        /// </summary>
        private void ShowPaths()
        {
            foreach (var path in _pathList)
            {
                Line line = new Line
                {
                    X1 = path.FromX + (_deltaX - path.DeltaX),
                    Y1 = path.FromY + (_deltaY - path.DeltaY),
                    X2 = path.ToX + (_deltaX - path.DeltaX),
                    Y2 = path.ToY + (_deltaY - path.DeltaY),
                    Stroke = Brushes.Blue,
                    StrokeThickness = 4,
                    Margin = new Thickness(5, 5, 0, 0)
                };
                Panel.SetZIndex(line, 10);
                Map.Children.Add(line);
                _roadObjects.Add(line);
            }
        }

        /// <summary>
        /// Генерация сетки на карте
        /// </summary>
        private void GenerateWeb()
        {
            if (_mapPoints.Count == 0)
            {
                for (int i = 5; i < 780; i += 20)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = 1,
                        Height = 700,
                        Fill = Brushes.Red
                    };

                    Map.Children.Add(rect);
                    Canvas.SetTop(rect, 0);
                    Canvas.SetLeft(rect, i);
                }

                for (int i = 5; i < 760; i += 20)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = 760,
                        Height = 1,
                        Fill = Brushes.Red
                    };

                    Map.Children.Add(rect);
                    Canvas.SetTop(rect, i);
                    Canvas.SetLeft(rect, 0);
                }

                for (int i = 0; i < 780; i += 20)
                {
                    for (int j = 0; j < 780; j += 20)
                    {
                        var button = new Button
                        {
                            Width = 10,
                            Height = 10,
                        };
                        button.Click += DoStep;

                        if (Math.Abs(_currentX + _currentSpeedX * 20 - i) <= 20 && Math.Abs(_currentY + _currentSpeedY * 20 - j) <= 20)
                        {
                            if (_currentX == i && _currentY == j)
                            {
                                button.Background = Brushes.Black;
                                button.IsEnabled = true;
                            }
                            else
                            {
                                button.Background = Brushes.LightGreen;
                                button.IsEnabled = true;
                            }

                        }
                        else
                        {
                            button.Background = Brushes.White;
                            button.IsEnabled = false;
                        }

                     //   if (i == _currentX && j == _currentY)
                     //   {
                     //       button.Background = Brushes.Black;
                        //    button.IsEnabled = false;
                     //   }

                        _mapPoints.Add(button);

                        Panel.SetZIndex(button, 10);
                        Map.Children.Add(button);
                        Canvas.SetTop(button, j);
                        Canvas.SetLeft(button, i);
                    }
                }
            }
            else
            {
                foreach (var button in _mapPoints)
                {
                    var x = Canvas.GetLeft(button);
                    var y = Canvas.GetTop(button);

                    if (Math.Abs(_currentX + _currentSpeedX * 20 - x) <= 20 && Math.Abs(_currentY + _currentSpeedY * 20 - y) <= 20)
                    {
                        button.Background = Brushes.LightGreen;
                        button.IsEnabled = true;
                    }
                    else
                    {
                        button.Background = Brushes.White;
                        button.IsEnabled = false;
                    }

                    if (x == _currentX && y == _currentY)
                    {
                        button.Background = Brushes.Black;
                        button.IsEnabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Текущая линейная скорость
        /// </summary>
        private int CurrentSpeed
        {
            get
            {
                var speedX = Math.Abs(_currentSpeedX);
                var speedY = Math.Abs(_currentSpeedY);

                if (speedX > speedY) return speedX;
                return speedY;
            }
        }

        /// <summary>
        /// Выполнить действие на карте. Обычно - новый ход.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoStep(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var x = Convert.ToInt32(Canvas.GetLeft(button));
            var y = Convert.ToInt32(Canvas.GetTop(button));

            if (x == _currentX && y == _currentY)
            {
                MessageBox.Show("Car options !");
                return;
            }

            _currentSpeedX = Convert.ToInt32((x - _currentX) / 20);
            _currentSpeedY = Convert.ToInt32((y - _currentY) / 20);



            var path = new PathElement
            {
                FromX = _currentX,
                FromY = _currentY,
                ToX = x,
                ToY = y,
                DeltaX = _deltaX,
                DeltaY = _deltaY
            };
            _pathList.Add(path);

            _deltaX -= (x - _currentX);
            _deltaY -= (y - _currentY);

            SpeedTb.Text = CurrentSpeed.ToString();

            
            GenerateWeb();
            ShowRoad();
            ShowPaths();
        }

        /// <summary>
        /// Клик в меню создание новой игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            NewGameStart();
        }

        /// <summary>
        /// Сбросить настройки для начала новой игры
        /// </summary>
        private void ResetGameProcess()
        {
            _currentSpeedX = _currentSpeedY = _deltaX = _deltaY = 0;
            _pathList.Clear();
            _mapPoints.Clear();
            _roadElements.Clear();
            _roadObjects.Clear();

            Map.Children.RemoveRange(0, Map.Children.Count);
        }

        /// <summary>
        /// Начало новой игры
        /// </summary>
        private void NewGameStart()
        {
            ResetGameProcess();
            GenerateWeb();

            double x = _currentX;
            double y = _currentY;

            double angle = _roadElements.Count == 0 ? 0 : _roadElements.Last().Angle;

            for (int i = 0; i < 25; i++)
            {
                var roadElement = GetRoadElement(new Point(x, y), angle);
                _roadElements.Add(roadElement);

                x = roadElement.EndPoint.X;
                y = roadElement.EndPoint.Y;
            }

            ShowRoad();
        }

        /// <summary>
        /// Перерисовать дорогу
        /// </summary>
        private void ShowRoad()
        {
            if (_roadObjects != null)
            {
                foreach (var roadObject in _roadObjects)
                {
                    //   var currShape = (Shape)roadObject;
                    Map.Children.Remove(roadObject);
                }
            }

            _roadObjects = new List<Shape>();

            for (int i = 0; i < _roadElements.Count; i++)
            {
                var currElement = _roadElements[i];

                Line line = new Line
                {
                    X1 = currElement.StartPoint.X + _deltaX,
                    Y1 = currElement.StartPoint.Y + _deltaY,
                    X2 = currElement.EndPoint.X + _deltaX,
                    Y2 = currElement.EndPoint.Y + _deltaY,
                    Stroke = Brushes.Gray,
                    StrokeThickness = _roadWidth,
                    Margin = new Thickness(0, 0, 0, 0)
                };
                Panel.SetZIndex(line, 0);
                Map.Children.Add(line);
                _roadObjects.Add(line);

                double top = currElement.EndPoint.Y - _roadWidth / 2 + _deltaY;
                double left = currElement.EndPoint.X - _roadWidth / 2 + _deltaX;
                var ellipse = new Ellipse
                {
                    Width = _roadWidth,
                    Height = _roadWidth,
                    Fill = Brushes.Gray,
                };
                Panel.SetZIndex(ellipse, 0);
                Map.Children.Add(ellipse);
                Canvas.SetTop(ellipse, top);
                Canvas.SetLeft(ellipse, left);
                _roadObjects.Add(ellipse);

            }
        }

        /// <summary>
        /// Генерация нового элемента дороги
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        private RoadElement GetRoadElement(Point start, double currentAngle)
        {
            var roadElement = new RoadElement();

            var random = new Random();
            var newAngle = _roadElements.Count == 0 ? 0 : random.Next(-90, 90);

            roadElement.Angle = newAngle + currentAngle;
            roadElement.Width = _roadWidth;
            roadElement.Height = random.Next(100, 200);
            roadElement.StartPoint = new Point(start.X, start.Y);

            return roadElement;
        }

    }
}
