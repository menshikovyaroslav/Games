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
        int _currentY = 660;
        int _roadWidth = 300;
        int _currentSpeedX = 0;
        int _currentSpeedY = 0;

        List<PathElement> _pathList = new List<PathElement>();

        public MainWindow()
        {
            InitializeComponent();

            GenerateWeb();
            Test();


        }

        private void ShowPaths()
        {
            foreach (var path in _pathList)
            {
                Line line = new Line
                {
                    X1 = path.FromX,
                    Y1 = path.FromY,
                    X2 = path.ToX,
                    Y2 = path.ToY,
                    Stroke = Brushes.Blue,
                    StrokeThickness=4,
                    Margin = new Thickness(5,5,0,0)
                };
                Map.Children.Add(line);
            //    Canvas.SetTop(line, 0);
            //    Canvas.SetLeft(line, i);
            }
        }

        private void GenerateWeb()
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

                    if (Math.Abs(_currentX + _currentSpeedX * 20 - i) <= 20 && Math.Abs(_currentY + _currentSpeedY * 20 - j) <= 20)
                    {
                        button.Background = Brushes.LightGreen;
                        button.IsEnabled = true;
                        button.Click += DoStep;
                    }
                    else
                    {
                        button.Background = Brushes.White;
                        button.IsEnabled = false;
                    }

                    if (i == _currentX && j == _currentY)
                    {
                        button.Background = Brushes.Black;
                        button.IsEnabled = false;
                    }

                    Panel.SetZIndex(button, 10);
                    Map.Children.Add(button);
                    Canvas.SetTop(button, j);
                    Canvas.SetLeft(button, i);
                }
            }
        }

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

        private void DoStep(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var x = Convert.ToInt32(Canvas.GetLeft(button));
            var y = Convert.ToInt32(Canvas.GetTop(button));

            _currentSpeedX = Convert.ToInt32((x - _currentX) / 20);
            _currentSpeedY = Convert.ToInt32((y - _currentY) / 20);



            var path = new PathElement
            {
                FromX = _currentX,
                FromY = _currentY,
                ToX = x,
                ToY = y
            };
            _pathList.Add(path);

            _currentX += _currentSpeedX * 20;
            _currentY += _currentSpeedY * 20;

            SpeedTb.Text = CurrentSpeed.ToString();

            ShowPaths();
            GenerateWeb();
        }

        private void GenerateMapMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Test();
        }

        private void Test()
        {
            var random = new Random();

            Rectangle rect = new Rectangle
            {
                Width = _roadWidth,
                Height = random.Next(200, 1000),
                Stroke = Brushes.Gray,
                StrokeThickness = 20
                //   StrokeDashArray = new DoubleCollection() { 20, 0, 20, 0 }
                //   Fill = Brushes.Gray
            };

            Map.Children.Add(rect);
            Canvas.SetTop(rect, _currentY - rect.Height + 45);
            Canvas.SetLeft(rect, _currentX - _roadWidth / 2 + 5);
        }
    }
}
