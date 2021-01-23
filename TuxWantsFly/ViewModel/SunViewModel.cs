using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TuxWantsFly.ViewModel
{
    public class SunViewModel
    {
        private Point _centerPoint;

        public Point CenterPoint
        {
            get
            {
                return _centerPoint;
            }
        }

        public SunViewModel()
        {
            _centerPoint = new Point(180, 180);
        }
    }
}
