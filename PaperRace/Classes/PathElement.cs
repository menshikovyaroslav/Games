using System;
using System.Collections.Generic;
using System.Text;

namespace PaperRace.Classes
{
    /// <summary>
    /// Фрагмент пройденного пути
    /// </summary>
    public class PathElement
    {
        /// <summary>
        /// Точка начала отрезка по оси X
        /// </summary>
        public double FromX { get; set; }

        /// <summary>
        /// Точка начала отрезка по оси Y
        /// </summary>
        public double FromY { get; set; }

        /// <summary>
        /// Точка конца отрезка по оси X
        /// </summary>
        public double ToX { get; set; }

        /// <summary>
        /// Точка конца отрезка по оси Y
        /// </summary>
        public double ToY { get; set; }

        /// <summary>
        /// Отклонение карты по оси X
        /// </summary>
        public double DeltaX { get; set; }

        /// <summary>
        /// Отклонение карты по оси Y
        /// </summary>
        public double DeltaY { get; set; }

        /// <summary>
        /// Текущий угол наклона пути
        /// </summary>
        public double Angle
        {
            get
            {
                if (ToY == FromY && ToX > FromX) return 90;
                if (ToY == FromY && ToX < FromX) return -90;
                if (ToX == FromX && ToY > FromY) return 180;
                if (ToX == FromX && ToY < FromY) return 0;

                if (ToY <= FromY && ToX >= FromX) return -Math.Atan((ToX - FromX) / (ToY - FromY)) * 180 / Math.PI;
                if (ToY <= FromY && ToX <= FromX) return - Math.Atan((ToX - FromX) / (ToY - FromY)) * 180 / Math.PI;

                if (ToY >= FromY && ToX <= FromX) return Math.Atan((ToY - FromY) / (ToX - FromX)) * 180 / Math.PI - 90;
                if (ToY >= FromY && ToX >= FromX) return Math.Atan((ToY - FromY) / (ToX - FromX)) * 180 / Math.PI + 90;

                return 0;
            }
        }
    }
}
