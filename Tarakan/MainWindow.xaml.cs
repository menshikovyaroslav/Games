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

        List<Cockroach> tarakans = new List<Cockroach>();
        Dictionary<Image, Cockroach> images = new Dictionary<Image, Cockroach>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                    Panel.SetZIndex(beast, 10);
                    Map.Children.Add(beast);

                    Canvas.SetTop(beast, tarakan.Y);
                    Canvas.SetLeft(beast, tarakan.X);

                    images[beast] = tarakan;
                }


                foreach (var beast in tarakans)
                {
                    beast.Move();

                    
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
    }
}
