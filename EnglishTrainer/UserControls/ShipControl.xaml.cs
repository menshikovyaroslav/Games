using EnglishTrainer.Classes;
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

namespace EnglishTrainer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ShipControl.xaml
    /// </summary>
    public partial class ShipControl : UserControl
    {
        private SpaceShip Ship;
        public ShipControl(SpaceShip ship)
        {
            InitializeComponent();

            Ship = ship;

            switch (Ship.ShipType)
            {
                case ShipType.Interceptor:
                    ShipImage.Source = new BitmapImage(new Uri("/Images/interceptor.png", UriKind.Relative));
                    break;
                case ShipType.LightCruiser:
                    break;
                case ShipType.HeavyCruiser:
                    break;
                case ShipType.DeathStar:
                    break;
                default:
                    break;
            }
        }
    }
}
