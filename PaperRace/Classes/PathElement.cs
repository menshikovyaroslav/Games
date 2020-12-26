using System;
using System.Collections.Generic;
using System.Text;

namespace PaperRace.Classes
{
    public class PathElement
    {
        /// <summary>
        /// Точка начала отрезка по оси X
        /// </summary>
        public int FromX { get; set; }
        /// <summary>
        /// Точка начала отрезка по оси Y
        /// </summary>
        public int FromY { get; set; }
        /// <summary>
        /// Точка конца отрезка по оси X
        /// </summary>
        public int ToX { get; set; }
        /// <summary>
        /// Точка конца отрезка по оси Y
        /// </summary>
        public int ToY { get; set; }
        /// <summary>
        /// Отклонение карты по оси X
        /// </summary>
        public int DeltaX { get; set; }
        /// <summary>
        /// Отклонение карты по оси Y
        /// </summary>
        public int DeltaY { get; set; }
    }
}
