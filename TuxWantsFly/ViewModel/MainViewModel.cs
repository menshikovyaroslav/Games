using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Input;
using TuxWantsFly.Commands;

namespace TuxWantsFly.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Commands

        public ICommand StartGameCommand { get; set; }

        public void StartGameCommand_Execute()
        {
            MessageBox.Show("ok");
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