using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Input;
using TuxWantsFly.Commands;

namespace TuxWantsFly.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Commands

        public ICommand StartGameCommand { get; set; }

        public void StartGameCommand_Execute()
        {
            
        }

        public bool StartGameCommand_CanExecute()
        {
            return true;
        }

        #endregion

        public MainViewModel()
        {
            StartGameCommand = new Command(StartGameCommand_Execute, StartGameCommand_CanExecute);

            
        }
    }
}