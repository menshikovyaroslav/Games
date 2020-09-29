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
        public List<Field> Fields
        {
            get { return _fields; }
            private set { _fields = value; }
        }

        public MapModel(int mapWidth, int mapHeight, int fieldSize)
        {
            MapWidth = mapWidth;
            MapHeight = mapHeight;
            FieldSize = fieldSize;
            UpdateFields();
        }

        private void UpdateFields()
        {
            _fields = new List<Field>();
            for (int i = 0; i < MapWidth; i++)
            {
                for (int j = 0; j < MapHeight; j++)
                {
                    var field = new Field(i, j, FieldSize);
                    Fields.Add(field);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
