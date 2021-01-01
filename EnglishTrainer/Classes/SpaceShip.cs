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
        public double HelpWord { get; set; }

        /// <summary>
        /// Слово-ответ
        /// </summary>
        public double AnswerWord { get; set; }
    }
}
