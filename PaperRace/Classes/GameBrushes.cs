using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace PaperRace.Classes
{
    public static class GameBrushes
    {
        static BrushConverter _bc = new BrushConverter();
        public static Brush RoadBrush
        {
            get
            {
                return (Brush)_bc.ConvertFrom("#494949");
            }
        }
    }
}
