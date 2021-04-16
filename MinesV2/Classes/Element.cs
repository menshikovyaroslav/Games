using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MinesV2.Classes
{
    public class Element
    {
        static Random _random = new Random();

        public int X { get; set; }
        public int Y { get; set; }
        public bool IsBomb { get; set; }
        public bool IsShow { get; set; }

        public Element()
        {
            var entireElements = Options.ElementCountX * Options.ElementCountY;
            var chanse = (double)Options.Bombs / entireElements * 100;

            var randomChanse = _random.Next(1, 101);

            if (chanse > randomChanse)
            {
                IsBomb = true;
            }
        }
    }
}
