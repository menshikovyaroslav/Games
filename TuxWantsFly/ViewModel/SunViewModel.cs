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

        public Point Ray1Point
        {
            get
            {
                return new Point(30, 30);
            }
        }

        public Point Ray2Point
        {
            get
            {
                return new Point(330, 30);
            }
        }

        public Point Ray3Point
        {
            get
            {
                return new Point(330, 330);
            }
        }

        public Point Ray4Point
        {
            get
            {
                return new Point(30, 330);
            }
        }

        public Point Ray5Point
        {
            get
            {
                return new Point(180, 40);
            }
        }

        public Point Ray6Point
        {
            get
            {
                return new Point(180, 320);
            }
        }

        public Point Ray7Point
        {
            get
            {
                return new Point(40, 180);
            }
        }

        public Point Ray8Point
        {
            get
            {
                return new Point(320, 180);
            }
        }

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
