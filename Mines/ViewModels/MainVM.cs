using Mines.Commands;
using Mines.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mines.ViewModels
{
    internal class MainVM : ViewModelBase
    {
        private int _fieldSize;
        private MapModel _mapModel;
        public int FieldSize
        {
            get { return _fieldSize; }
            private set
            {
                _fieldSize = value;
            //    OnPropertyChanged("");
            }
        }

        public ICommand FieldClickCommand { get; set; }

        public void FieldClickCommand_Execute(Field field)
        {
            
        }

        public bool FieldClickCommand_CanExecute(Field field)
        {
            return true;
        }


        public MapModel MapModel
        {
            get { return _mapModel; }
            private set
            {
                _mapModel = value;
                OnPropertyChanged("MapModel");
            }
        }


        public MainVM()
        {
            FieldClickCommand = new RelayCommand<Field>(FieldClickCommand_Execute, FieldClickCommand_CanExecute);

            FieldSize = 10;
            MapModel = new MapModel(FieldSize, FieldSize, 20, 20);
        }

    }
}
