using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mines.Models
{
    public class Field
    {
        private int _xCoor;
        private int _yCoor;
        private int _fieldSize;
        private string _fieldValue;

        private bool _isBomb;

        public int XCoor
        {
            get { return _xCoor * FieldSize; }
            private set { _xCoor = value; }
        }
        public int YCoor
        {
            get { return _yCoor * FieldSize; }
            private set { _yCoor = value; }
        }
        public int FieldSize
        {
            get { return _fieldSize; }
            private set { _fieldSize = value; }
        }
        public bool IsBomb
        {
            get { return _isBomb; }
            private set { _isBomb = value; }
        }
        public string FieldValue
        {
            get
            {
                if (IsBomb) return "*";
                return "";
            }
        }

        public Field(int xCoor, int yCoor, int fieldSize, bool isBomb)
        {
            XCoor = xCoor;
            YCoor = yCoor;
            FieldSize = fieldSize;
            IsBomb = isBomb;
        }
    }
}
