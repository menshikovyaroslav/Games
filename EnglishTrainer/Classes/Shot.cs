using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Classes
{
    public class Shot
    {
        /// <summary>
        /// Угол поворота ракеты. от 1 до 360
        /// </summary>
        public double Angle { get; set; }


        public Shot(SpaceShip ship)
        {
            if (ship != null) Angle = ship.Angle;
            else
            {
                var random = new Random();
                Angle = random.Next(1, 361);
            }
        }
    }
}
