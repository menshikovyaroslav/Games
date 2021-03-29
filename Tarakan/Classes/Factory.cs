using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarakan.Classes
{
    public static class Factory
    {
        public static Cockroach GetBeast()
        {
            var random = new Random();
            var speed = random.Next(1, 10);
            int x = 380, y = 280;
            int angle = random.Next(0, 360);

            var tarakan = new Cockroach(speed, x, y, angle);

            return tarakan;
        }
    }
}
