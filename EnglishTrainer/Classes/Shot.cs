using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EnglishTrainer.Classes
{
    public class Shot
    {
        /// <summary>
        /// Ракета на карте или нет
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Скорость ракеты
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Ссылка на класс корабля по которому идет стрельба
        /// </summary>
        public SpaceShip Ship { get; set; }

        /// <summary>
        /// Угол поворота ракеты. от 1 до 360
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Данный выстрел - это выстрел в цель или мимо
        /// </summary>
        public bool IsSureShot { get; set; }

        /// <summary>
        /// Расстояние от Земли до корабля
        /// </summary>
        public double Distance { get; private set; }

        /// <summary>
        /// Центр карты - точка вылета ракеты
        /// </summary>
        private Point CenterPoint { get; set; }

        /// <summary>
        /// Расчет координат точки на окружности с известным радиусом и углом появления
        /// </summary>
        public Point CurrentPosition
        {
            get
            {
                var x = Distance / 2 * Math.Sin(Angle * Math.PI / 180);
                var y = Distance / 2 * Math.Cos(Angle * Math.PI / 180);

                return new Point(CenterPoint.X + x, CenterPoint.Y - y);
            }
        }

        public Shot(SpaceShip ship, Point point)
        {
            IsEnabled = true;
            Ship = ship;
            CenterPoint = point;
            Speed = 10;

            if (ship != null)
            {
                Angle = ship.Angle;
                IsSureShot = true;
            }
            else
            {
                var random = new Random();
                Angle = random.Next(1, 361);
            }
        }

        /// <summary>
        /// Метод выполнения хода
        /// </summary>
        public void DoStep()
        {
            // Уменьшение дистанции до цели
            Distance += Speed;
        }
    }
}
