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
    }
}
