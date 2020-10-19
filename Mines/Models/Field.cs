using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mines.Models
{
    public class Field : INotifyPropertyChanged
    {
        private int _xCoor;
        private int _yCoor;
        private int _fieldPixelSize;
        private string _fieldValue;

        private bool _isBomb;
        private bool _isShow;

        public event PropertyChangedEventHandler PropertyChanged;

        public int XCoor
        {
            get { return _xCoor; }
            private set { _xCoor = value; }
        }
        public int YCoor
        {
            get { return _yCoor; }
            private set { _yCoor = value; }
        }
        public int XPixelCoor
        {
            get { return _xCoor * FieldPixelSize; }
            private set { _xCoor = value; }
        }
        public int YPixelCoor
        {
            get { return _yCoor * FieldPixelSize; }
            private set { _yCoor = value; }
        }
        public int FieldPixelSize
        {
            get { return _fieldPixelSize; }
            private set { _fieldPixelSize = value; }
        }
        public bool IsBomb
        {
            get { return _isBomb; }
            private set { _isBomb = value; }
        }
        public bool IsShow
        {
            get { return _isShow; }
            set { _isShow = value; OnPropertyChanged("FieldValue"); }
        }
        public string FieldValue
        {
            get
            {
                if (!IsShow) return "";

                if (IsBomb) return "*";

                return _fieldValue;
            }
            set
            {
                _fieldValue = value;
            }
        }

        public Field(int xCoor, int yCoor, int fieldSize, bool isBomb)
        {
            XCoor = xCoor;
            YCoor = yCoor;
            FieldPixelSize = fieldSize;
            IsBomb = isBomb;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
