using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace PaperRace.Classes
{
    public class RoadElement
    {
        public Rectangle Rectangle { get; set; }
        public int Angle { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint
        {
            get
            {
                var angle = (90 - Angle) * 0.0175;

                var katet1 = Math.Abs(Height * Math.Sin(angle));
                var katet2 = Math.Abs(Height * Math.Cos(angle));

                return new Point(StartPoint.X + katet2, StartPoint.Y - katet1);
            }

        }
    }
}
