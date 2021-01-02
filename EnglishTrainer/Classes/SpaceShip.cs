using EnglishTrainer.Classes.Dictionaries;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EnglishTrainer.Classes
{
    /// <summary>
    /// Класс объекта космического корабля
    /// </summary>
    public class SpaceShip
    {
        public double Speed { get; set; }
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Угол появления космического корабля относительно Земли. от 1 до 360
        /// </summary>
        public double Angle { get; set; }

        public double TransformAngle
        {
            get
            {
                return Angle + 180;
            }
        }

        /// <summary>
        /// Слово из словаря
        /// </summary>
        public Word Word { get; set; }

        /// <summary>
        /// Тип космического корабля
        /// </summary>
        public ShipType ShipType { get; set; }

        private Point CenterPoint { get; set; }
        private double Distance { get; set; }
        public Point CurrentPosition
        {
            get
            {
                var x = Distance / 2 * Math.Sin(Angle * Math.PI / 180);
                var y = Distance / 2 * Math.Cos(Angle * Math.PI / 180);

                return new Point(CenterPoint.X + x, CenterPoint.Y - y);
            }
        }

        public SpaceShip(Point point, double distance, double speed)
        {
            CenterPoint = point;
            Distance = distance;
            Speed = speed;

            var random = new Random();
            Angle = random.Next(1, 361);

            Word = EnglishLevel1.GetWord();


            ShipType = ShipType.Interceptor;
            IsEnabled = true;
        }

        public void DoStep()
        {
            Distance -= Speed;

#if DEBUG
            Debug.WriteLine($"Angle={Angle}, x={CurrentPosition.X}, y={CurrentPosition.Y}");
#endif
        }
    }

    /// <summary>
    /// Тип космического корабля
    /// </summary>
    public enum ShipType
    {
        /// <summary>
        /// Перехватчик
        /// </summary>
        Interceptor = 1,

        /// <summary>
        /// Легкий крейсер
        /// </summary>
        LightCruiser = 2,

        /// <summary>
        /// Тяжелый крейсер
        /// </summary>
        HeavyCruiser = 3,

        /// <summary>
        /// Звезда смерти
        /// </summary>
        DeathStar = 4
    }
}
