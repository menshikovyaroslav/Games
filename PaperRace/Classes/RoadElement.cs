using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace PaperRace.Classes
{
    public class RoadElement
    {
        //   public Rectangle Rectangle { get; set; }
        public int CurrentAngle { get; set; }
        public double Angle { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint
        {
            get
            {
                var angle = (CurrentAngle + Angle) * 180 / Math.PI;
                var katetA = Math.Abs(Height * Math.Sin(angle));
                var katetB = Math.Abs(Height * Math.Cos(angle));

                if (angle >= 0)
                {
                    return new Point(StartPoint.X + katetA, StartPoint.Y - katetB);
                }
                else
                {
                    return new Point(StartPoint.X - katetA, StartPoint.Y - katetB);
                }
            }
        }
    }
}
