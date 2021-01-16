using EnglishTrainer.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
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
        /// Завершено ли событие анимации взрыва
        /// </summary>
        public bool IsExplosionCompleted { get; set; }

        public void Explosion()
        {
            QuestionTb.Visibility = HelpTb.Visibility = Visibility.Hidden;

            ShipImage.Source = new BitmapImage(new Uri("/Images/explosion.png", UriKind.Relative));

            Storyboard sb = this.FindResource("ExplosionBoard") as Storyboard;
            sb.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 600));
            sb.Completed += ExplosionCompleted;
            sb.Begin();
        }

        /// <summary>
        /// Событие завершения анимации взрыва
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExplosionCompleted(object sender, EventArgs e)
        {
            IsExplosionCompleted = true;
        }

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
                    ShipImage.Source = new BitmapImage(new Uri("/Images/lightcruiser.png", UriKind.Relative));
                    break;
                case ShipType.HeavyCruiser:
                    ShipImage.Source = new BitmapImage(new Uri("/Images/heavycruiser.png", UriKind.Relative));
                    break;
                case ShipType.DeathStar:
                    ShipImage.Source = new BitmapImage(new Uri("/Images/deathstar.png", UriKind.Relative));
                    break;
                default:
                    break;
            }
        }
    }
}
