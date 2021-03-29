using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarakan.Classes
{
    public class Cockroach
    {
        public int Speed { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Angle { get; set; }

        public Cockroach(int speed, int x, int y, int angle)
        {
            Speed = speed;
            X = x;
            Y = y;
            Angle = angle;
        }

        public void Move()
        {
            var dY = (int)(Speed * Math.Sin(Angle));
            var dX = (int)(Speed * Math.Cos(Angle));

            Y += dY;
            X += dX;
        }

        public bool IsOnTheMap()
        {
            if (X < 0 || X > 800 || Y < 0 || Y > 600) return false;
            return true;
        }
    }
}
