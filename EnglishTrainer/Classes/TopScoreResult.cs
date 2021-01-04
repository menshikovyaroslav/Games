using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Classes
{
    /// <summary>
    /// Элемент таблицы рекордов
    /// </summary>
    public class TopScoreResult
    {
        /// <summary>
        /// Имя игрока
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество набранных очков
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Уровень игры
        /// </summary>
        public Level Level { get; set; }

        public TopScoreResult(string name, int score, Level level)
        {
            Name = name;
            Score = score;
            Level = level;
        }

        /// <summary>
        /// Преобразование объекта элемента таблицы рекордов в формат удобный для записи
        /// </summary>
        /// <returns>Массив значений объекта элемента таблицы рекордов</returns>
        public string[] ToSaveFormat()
        {
            var result = new string[3];
            result[0] = Name;
            result[1] = Score.ToString();
            result[2] = ((int)Level).ToString();

            return result;
        }

        /// <summary>
        /// Преобразование сохраненного элемента таблицы рекордов в объект TopScoreResult
        /// </summary>
        /// <param name="topScoreResult"></param>
        public TopScoreResult(string[] topScoreResult)
        {
            if (topScoreResult != null && topScoreResult.Length == 3)
            {
                Name = topScoreResult[0];
                var score = 0;
                Int32.TryParse(topScoreResult[1], out score);
                Score = score;
                Level level;
                Enum.TryParse(topScoreResult[2], out level);
                Level = level;
            }
        }
    }

    /// <summary>
    /// Перечисление уровней игры
    /// </summary>
    public enum Level
    {
        Beginner = 1,
        Standard = 2,
        Advanced = 3
    }
}
