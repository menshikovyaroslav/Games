using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mines.Models
{
    public class MapModel : INotifyPropertyChanged
    {
        private int _fieldPixelSize;
        private int _mapWidth;
        private int _mapHeight;
        private int _bombCount;
        private List<Field> _fields;

        private int[,] _bombInfo;
        public int FieldPixelSize
        {
            get { return 20; }
        }
        public int MapEntireWidth
        {
            get
            {
                var width = MapWidth * FieldPixelSize + 40;
                if (width < 200) return 200;
                else return width;
            }
        }

        public int MapEntireHeight
        {
            get
            {
                var height = MapHeight * FieldPixelSize + 80;
                if (height < 200) return 200;
                else return height;
            }
        }
        public int MapWidth
        {
            get { return _mapWidth; }
            private set{ _mapWidth = value; }
        }
        public int MapHeight
        {
            get { return _mapHeight; }
            private set { _mapHeight = value; }
        }
        public int BombCount
        {
            get { return _bombCount; }
            private set { _bombCount = value; }
        }
        public List<Field> Fields
        {
            get { return _fields; }
            set { _fields = value; }
        }

        public int[,] BombInfo
        {
            get { return _bombInfo; }
        }

        public void CalculateBombs()
        {
            foreach (var field in Fields)
            {
                if (field.IsBomb)
                {
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (field.YCoor + j < 0 || field.YCoor + j > MapHeight - 1 || field.XCoor + i < 0 || field.XCoor + i > MapWidth - 1) continue;
                            _bombInfo[field.XCoor + i, field.YCoor + j]++;
                        }
                    }
                }
            }

            foreach (var field in Fields)
            {
                field.FieldValue = _bombInfo[field.XCoor, field.YCoor].ToString();
            }
        }

        public void AddBomb(Field field)
        {
            Fields.Add(field);
        }

        public MapModel()
        {

        }

        public void Init(int mapWidth, int mapHeight, int bombCount)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            BombCount = bombCount;
            Fields = new List<Field>();
            _bombInfo = new int[mapWidth, mapHeight];
        }

        

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
