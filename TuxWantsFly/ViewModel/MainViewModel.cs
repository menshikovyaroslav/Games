using GalaSoft.MvvmLight;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TuxWantsFly.Commands;

namespace TuxWantsFly.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Commands

        public ICommand StartGameCommand { get; set; }

        public async void StartGameCommand_Execute()
        {
            IsDayStarted = true;

            while (!IsTuxInWater)
            {
                TuxPointX += 0;
                TuxPointY += 2;

                await Task.Delay(10);
            }

            IsDayStarted = false;

            CommandManager.InvalidateRequerySuggested();
        }

        public bool StartGameCommand_CanExecute()
        {
            return !IsDayStarted;
        }

        #endregion

        private double _tuxPointX;
        private double _tuxPointY;

        private bool _isDayStarted;

        private int _day = 1;

        public string StartText
        {
            get
            {
                return $"Start Day {_day}";
            }
        }

        public double TuxPointX
        {
            get { return _tuxPointX; }
            set { _tuxPointX = value; OnPropertyChanged(); }
        }

        public double TuxPointY
        {
            get { return _tuxPointY; }
            set { _tuxPointY = value; OnPropertyChanged(); }
        }

        public bool IsTuxInWater
        {
            get
            {
                if (TuxPointY >= 600)
                {
                //    MessageBox.Show("ds");
                    return true;
                }    

                return false;
            }
        }

        public bool IsDayStarted
        {
            get
            {
                return _isDayStarted;
            }
            set
            {
                _isDayStarted = value;
                if (!value) _day++;
                OnPropertyChanged();
                OnPropertyChanged("StartText");
            }
        }


        public MainViewModel()
        {
            StartGameCommand = new Command(StartGameCommand_Execute, StartGameCommand_CanExecute);

            TuxPointX = 450;
            TuxPointY = 382;
        }
    }
}