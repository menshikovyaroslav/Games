using EnglishTrainer.Classes;
using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EnglishTrainer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ShipControl.xaml
    /// </summary>
    public partial class ShipControl : UserControl
    {
        /// <summary>
        /// Объект класса корабля
        /// </summary>
        public SpaceShip Ship { get; set; }

        /// <summary>
        /// Конструктор визуальной части космического корабля
        /// </summary>
        /// <param name="ship"></param>
        public ShipControl(SpaceShip ship)
        {
            InitializeComponent();
            DataContext = this;

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
