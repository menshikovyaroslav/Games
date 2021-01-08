using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Classes
{
    public class Word : INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null) PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private int _distancePercent;

        /// <summary>
        /// Слово-ответ
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Фраза-вопрос
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Подсказка игроку
        /// </summary>
        public string Help
        {
            get
            {
                // Количество символов для открытия
                var curr = Answer.Length - Convert.ToInt32(Answer.Length * DistancePercent / 100);

                // Часть открытой строки
                var currShowString = Answer.Substring(0, curr);

                // Возврат часть открытой строки + оставшиеся скрытые символы
                return currShowString.PadRight(Answer.Length, '-');
            }
        }

        /// <summary>
        /// Дистанция корабля до цели в процентах 
        /// </summary>
        public int DistancePercent
        {
            get
            {
                return _distancePercent;
            }
            set
            {
                _distancePercent = value;
                OnPropertyChanged("Help");
            }
        }
    }
}
