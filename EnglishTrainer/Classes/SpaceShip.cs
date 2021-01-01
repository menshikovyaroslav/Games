using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public SpaceShip()
        {
            var random = new Random();
            Angle = random.Next(1, 361);

            HelpWord = "Тест";
            AnswerWord = "Test";
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
