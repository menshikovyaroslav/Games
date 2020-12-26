using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace PaperRace.Classes
{
    /// <summary>
    /// Фрагмент дороги
    /// </summary>
    public class RoadElement
    {
        /// <summary>
        /// Угол наклона
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        /// Ширина фрагмента
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Длина фрагмента
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Начальная точка фрагмента
        /// </summary>
        public Point StartPoint { get; set; }

        /// <summary>
        /// Конечная точка фрагмента
        /// </summary>
        public Point EndPoint
        {
            get
            {
                if (Angle < -180) Angle += 360;
                if (Angle > 180) Angle -= 360;

                var angle = Angle * Math.PI / 180;
                var katetA = Math.Abs(Height * Math.Sin(angle));
                var katetB = Math.Abs(Height * Math.Cos(angle));

                // TODO : сделай если углы > 90 и < - 90

                double deltaX, deltaY = 0;



                if (Angle >= 0 && Angle <= 90)
                {
                    deltaX = katetA;
                    deltaY = -katetB;
                }
                else if (Angle < 0 && Angle >= -90)
                {
                    deltaX = -katetA;
                    deltaY = -katetB;
                }
                else if (Angle < -90 && Angle >= -180)
                {
                    deltaX = -katetA;
                    deltaY = katetB;
                }
                else
                {
                    deltaX = katetA;
                    deltaY = katetB;
                }

                return new Point(StartPoint.X + deltaX, StartPoint.Y + deltaY);

            }
        }
    }
}
