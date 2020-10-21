using Mines.Classes;
using Mines.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mines.ViewModels
{
    internal class OptionsViewModel : INotifyPropertyChanged
    {
        public ICommand WidthClickCommand { get; set; }
        public ICommand HeigthClickCommand { get; set; }

        public void WidthClickCommand_Execute(int width)
        {
            Options.MapWidth = width;
        }

        public bool WidthClickCommand_CanExecute(int width)
        {
            return Options.MapWidth != width;
        }

        public void HeigthClickCommand_Execute(int heigth)
        {
            Options.MapHeight = heigth;
        }

        public bool HeigthClickCommand_CanExecute(int heigth)
        {
            return Options.MapHeight != heigth;
        }

        public int MapWidth
        {
            get { return Options.MapWidth; }
            set { Options.MapWidth = value; }
        }

        public int MapHeigth
        {
            get { return Options.MapHeight; }
            set { Options.MapHeight = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public OptionsViewModel()
        {
            WidthClickCommand = new RelayCommand<int>(WidthClickCommand_Execute, WidthClickCommand_CanExecute);
            HeigthClickCommand = new RelayCommand<int>(HeigthClickCommand_Execute, HeigthClickCommand_CanExecute);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
