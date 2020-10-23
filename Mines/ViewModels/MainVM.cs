using Mines.Classes;
using Mines.Commands;
using Mines.Models;
using Mines.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mines.ViewModels
{
    internal class MainVM : INotifyPropertyChanged
    {
        private int _fieldSize;
        private MapModel _mapModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public int FieldSize
        {
            get { return _fieldSize; }
            private set
            {
                _fieldSize = value;
            }
        }

        public ICommand FieldClickCommand { get; set; }
        public ICommand StartGameCommand { get; set; }
        public ICommand OptionsCommand { get; set; }

        public void FieldClickCommand_Execute(Field field)
        {
            field.IsShow = true;

            if (field.FieldValue == "0")
            {
                var nearlyFields = from t in MapModel.Fields where !t.IsShow && Math.Abs(t.XCoor - field.XCoor) <= 1 && Math.Abs(t.YCoor - field.YCoor) <= 1 select t;
                foreach (var nearlyField in nearlyFields)
                {
                    FieldClickCommand_Execute(nearlyField);
                }
            }
        }

        public bool FieldClickCommand_CanExecute(Field field)
        {
            return true;
        }

        public void StartGameCommand_Execute()
        {
            StartGame();
        }

        public bool StartGameCommand_CanExecute()
        {
            return true;
        }

        public void OptionsCommand_Execute()
        {
            var optionsWindow = new OptionsWindow();
            optionsWindow.ShowDialog();
        }

        public bool OptionsCommand_CanExecute()
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
            StartGameCommand = new DelegateCommand(StartGameCommand_Execute, StartGameCommand_CanExecute);
            OptionsCommand = new DelegateCommand(OptionsCommand_Execute, OptionsCommand_CanExecute);

            FieldSize = 20;

            MapModel = new MapModel();
        }

        private void StartGame()
        {
            MapModel.Init(Options.MapWidth, Options.MapHeight, Options.Bombs);

         //   var mapSize = MapWidth * MapHeight;
            var bombsRemain = MapModel.BombCount;
            var fieldsRemain = Options.MapWidth * Options.MapHeight;

            var rnd = new Random();

            for (int i = 0; i < Options.MapWidth; i++)
            {
                for (int j = 0; j < Options.MapHeight; j++)
                {
                    double chance = (double)bombsRemain / fieldsRemain;
                    var isBomb = bombsRemain == fieldsRemain ? true : rnd.Next(0, 100) <= chance * 100;

                    var field = new Field(i, j, FieldSize, isBomb);
                    MapModel.AddBomb(field);

                    if (isBomb) bombsRemain--;

                    fieldsRemain--;
                }
            }

            MapModel.CalculateBombs();

            OnPropertyChanged("MapModel");
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //   public void Click(Field clickedfield)
        //    {
        //    var countAround = 0;
        //   foreach (var field in Fields)
        //   {
        //       if (field.)
        //   }
        //  }

    }
}
