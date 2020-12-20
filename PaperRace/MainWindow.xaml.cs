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
        int _currentSpeed = 0;

        public MainWindow()
        {
            InitializeComponent();

            GenerateWeb();
            Test();
        }

        private void GenerateWeb()
        {
            for (int i = 0; i < 800; i += 20)
            {
                Rectangle rect = new Rectangle
                {
                    Width = 1,
                    Height = 800,
                    Fill = Brushes.Red
                };

                Map.Children.Add(rect);
                Canvas.SetTop(rect, 0);
                Canvas.SetLeft(rect, i);
            }

            for (int i = 0; i < 800; i += 20)
            {
                Rectangle rect = new Rectangle
                {
                    Width = 800,
                    Height = 1,
                    Fill = Brushes.Red
                };

                Map.Children.Add(rect);
                Canvas.SetTop(rect, i);
                Canvas.SetLeft(rect, 0);
            }

            for (int i = 0; i < 800; i += 20)
            {
                for (int j = 0; j < 800; j += 20)
                {
                    var button = new Button
                    {
                        Width = 10,
                        Height = 10,
                        
                    };

                    if (Math.Abs(i - _currentX) <= (_currentSpeed + 1) * 20 && Math.Abs(j - _currentY) <= (_currentSpeed + 1) * 20)
                    {
                        button.Background = Brushes.LightGreen;
                        button.IsEnabled = true;
                    }
                    else
                    {
                        button.IsEnabled = false;
                    }

                    if (i == _currentX && j == _currentY)
                    {
                        button.Background = Brushes.Black;
                        button.IsEnabled = false;
                    }

                    Panel.SetZIndex(button, 10);
                    Map.Children.Add(button);
                    Canvas.SetTop(button, j - button.Height / 2);
                    Canvas.SetLeft(button, i - button.Width / 2);
                }
            }
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
            Canvas.SetTop(rect, _currentY - rect.Height + 40);
            Canvas.SetLeft(rect, _currentX - _roadWidth / 2);
        }
    }
}
