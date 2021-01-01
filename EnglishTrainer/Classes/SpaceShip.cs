﻿using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Угол появления космического корабля относительно Земли. от 1 до 360
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Слово-подсказка
        /// </summary>
        public string HelpWord { get; set; }

        /// <summary>
        /// Слово-ответ
        /// </summary>
        public string AnswerWord { get; set; }

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

        public SpaceShip(Point point, double distance)
        {
            CenterPoint = point;
            Distance = distance;

            var random = new Random();
            Angle = random.Next(180, 360);

            HelpWord = "Тест";
            AnswerWord = "Test";

            ShipType = ShipType.Interceptor;
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
