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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tarakan.Classes;

namespace Tarakan
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count = 0;
        int maxCount = 10;

        int tarakanMaxCount = 1;

        bool isDown = false;

        List<Cockroach> tarakans = new List<Cockroach>();
        Dictionary<Image, Cockroach> images = new Dictionary<Image, Cockroach>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    if (isDown)
                    {
                        foreach (var image in images)
                        {
                            Map.Dispatcher.BeginInvoke((Action)(() =>
                            {
                                var x = Canvas.GetLeft(image.Key);
                                var y = Canvas.GetTop(image.Key);

                                var currentPoint = System.Windows.Forms.Control.MousePosition;
                                var cursorX = currentPoint.X;
                                var cursorY = currentPoint.Y;

                                if (x < cursorX && cursorX < x + 40 && y < cursorY && cursorY < y + 40)
                                {
                                    System.Windows.MessageBox.Show("ok");

                                    tarakans.Remove(image.Value);
                                    images.Remove(image.Key);
                                    Map.Children.Remove(image.Key);
                                }
                            }));





                        }
                    }

                    Thread.Sleep(10);
                }

            });
            t.IsBackground = true;
            t.Start();


            while (count < maxCount)
            {
                if (tarakans.Count() == 0)
                {
                    var tarakan = Factory.GetBeast();
                    tarakans.Add(tarakan);

                    var beast = new Image()
                    {
                        Width = 40,
                        Height = 40,
                        Source = new BitmapImage(new Uri("/Images/tarakan.png", UriKind.Relative))
                    };
                    System.Windows.Controls.Panel.SetZIndex(beast, 10);
                    Map.Children.Add(beast);

                    Canvas.SetTop(beast, tarakan.Y);
                    Canvas.SetLeft(beast, tarakan.X);

                    images[beast] = tarakan;
                }


                foreach (var beast in tarakans)
                {
                    beast.Move();

                    if (!beast.IsOnTheMap())
                    {
                        var image = images.Single(i => i.Value == beast).Key;
                        tarakans.Remove(beast);
                        images.Remove(image);
                        Map.Children.Remove(image);
                        break;
                    }
                }

                foreach (Image mapChild in Map.Children)
                {
                    if (images.ContainsKey(mapChild))
                    {
                        var beast = images[mapChild];

                        Canvas.SetTop(mapChild, beast.Y);
                        Canvas.SetLeft(mapChild, beast.X);
                    }


                    //    Map.Children.Remove(item);
                }


                await Task.Delay(10);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) isDown = true;
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isDown)
            {
                this.Cursor = System.Windows.Input.Cursors.Cross;
                this.Title = "pressed";

                var currentPoint = System.Windows.Forms.Control.MousePosition;
                var x = currentPoint.X;
                var y = currentPoint.Y;

                var random = new Random();
                x += random.Next(-1, 1);
                y += random.Next(-1, 1);

                int w = Screen.PrimaryScreen.WorkingArea.Width;
                int h = Screen.PrimaryScreen.WorkingArea.Height;

                var xNew = (65535 * x) / w;
                var yNew = (65535 * y) / h;

                System.Windows.Forms.Cursor.Position = new System.Drawing.Point(x, y);
            }
            else
            {
                this.Cursor = System.Windows.Input.Cursors.Arrow;
                this.Title = "not";
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released) isDown = false;
        }
    }
}
