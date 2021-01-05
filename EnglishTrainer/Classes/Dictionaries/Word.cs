using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Classes.Dictionaries
{
    public class Word
    {
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
                return "".PadRight(Answer.Length, '-');
            }
        }
    }
}
