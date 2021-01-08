using System;
using System.Diagnostics;
using System.Windows;

namespace EnglishTrainer.Classes
{
    /// <summary>
    /// Класс объекта космического корабля
    /// </summary>
    public class SpaceShip
    {
        /// <summary>
        /// Скорость корабля
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Корабль на карте или нет
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Угол появления космического корабля относительно Земли. от 1 до 360
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Угол поворота корабля. Отличается от Angle на 180 градусов
        /// </summary>
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

        /// <summary>
        /// Центр карты - цель корабля
        /// </summary>
        private Point CenterPoint { get; set; }

        /// <summary>
        /// Расстояние от корабля до Земли
        /// </summary>
        public double Distance { get; private set; }

        /// <summary>
        /// Начальное расстояние от корабля до Земли
        /// </summary>
        public double InitialDistance { get; private set; }

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

        /// <summary>
        /// Конструктор класса космического корабля
        /// </summary>
        /// <param name="point">Точка-цель</param>
        /// <param name="distance">Расстояние от корабля до цели</param>
        /// <param name="speed">Скорость корабля</param>
        public SpaceShip(Point point, double distance, double speed, Level level)
        {
            CenterPoint = point;
            Distance = distance;
            InitialDistance = distance;

            Speed = speed;

            var random = new Random();
            Angle = random.Next(1, 361);

            Word = Vocabulary.GetWord(level);

            ShipType = ShipType.Interceptor;
            IsEnabled = true;
        }

        /// <summary>
        /// Метод выполнения хода
        /// </summary>
        public void DoStep()
        {
            // Уменьшение дистанции до цели
            Distance -= Speed;

            // Указываем процент дистанции до цели для расчета подсказки
            Word.DistancePercent = (int)(Distance / InitialDistance * 100);
            

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
        DeathStar = 4,

        /// <summary>
        /// Анимация взрыва
        /// </summary>
        Explosion = 5,
    }
}
