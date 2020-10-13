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
        private int _fieldSize;
        private int _mapWidth;
        private int _mapHeight;
        private int _bombCount;
        private List<Field> _fields;
        public int FieldSize
        {
            get { return _fieldSize; }
            private set { _fieldSize = value; }
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
            private set { _fields = value; }
        }

        public MapModel(int mapWidth, int mapHeight, int fieldSize, int bombCount)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            FieldSize = fieldSize;
            BombCount = bombCount;
            UpdateFields();
        }

        private void UpdateFields()
        {
            Fields = new List<Field>();

            var mapSize = MapWidth * MapHeight;
            var bombsRemain = BombCount;
            var fieldsRemain = MapWidth * MapHeight;

            var rnd = new Random();

            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {

                    double chance = (double)bombsRemain / fieldsRemain;

                    var isBomb = bombsRemain == fieldsRemain ? true : rnd.Next(0, 100) <= chance * 100;

                    var field = new Field(i, j, FieldSize, isBomb);
                    Fields.Add(field);

                    if (isBomb) bombsRemain--;

                    fieldsRemain--;
                }
            }

            OnPropertyChanged("Fields");
        }

        public void Click(Field clickedfield)
        {
            var countAround = 0;
            foreach (var field in Fields)
            {
         //       if (field.)
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
