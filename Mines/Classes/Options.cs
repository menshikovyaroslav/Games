using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mines.Classes
{
    public static class Options
    {
        public static int MapWidth { get; set; }
        public static int MapHeight { get; set; }

        static Options()
        {
            MapWidth = 10;
            MapHeight = 10;
        }
    }
}
