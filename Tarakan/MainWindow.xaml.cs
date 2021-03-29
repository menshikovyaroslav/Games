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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            while (count < maxCount)
            {
                if (tarakans.Count() == 0)
                {
                    var tarakan = Factory.GetBeast();
                    tarakans.Add(tarakan);
                }





                Thread.Sleep(10);
            }
        }
    }
}
