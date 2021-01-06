using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Classes.Dictionaries
{
    public static class Vocabulary
    {
        /// <summary>
        /// Словарь Начинающего уровня
        /// </summary>
        private static List<Word> _beginnerDictionary = new List<Word>();

        /// <summary>
        /// Словарь Опытного уровня
        /// </summary>
        private static List<Word> _standardDictionary = new List<Word>();

        /// <summary>
        /// Словарь Продвинутого уровня
        /// </summary>
        private static List<Word> _advancedDictionary = new List<Word>();
        static Vocabulary()
        {
            #region Загрузка словарей

            var beginnerLines = File.ReadAllLines("beginner.csv", Encoding.GetEncoding("windows-1251"));
            foreach (var line in beginnerLines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                var splitted = line.Split(';');
                if (splitted.Length != 2) continue;

                _beginnerDictionary.Add(new Word() {Answer = splitted[0], Question = splitted[1] });
            }

            var standardLines = File.ReadAllLines("standard.csv", Encoding.GetEncoding("windows-1251"));
            foreach (var line in standardLines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                var splitted = line.Split(';');
                if (splitted.Length != 2) continue;

                _standardDictionary.Add(new Word() { Answer = splitted[0], Question = splitted[1] });
            }

            var advancedLines = File.ReadAllLines("advanced.csv", Encoding.GetEncoding("windows-1251"));
            foreach (var line in advancedLines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                var splitted = line.Split(';');
                if (splitted.Length != 2) continue;

                _advancedDictionary.Add(new Word() { Answer = splitted[0], Question = splitted[1] });
            }

            #endregion
        }

        /// <summary>
        /// Получить слово из словаря Начинающего уровня
        /// </summary>
        /// <returns></returns>
        public static Word GetWord(Level level)
        {
            var random = new Random();

            switch (level)
            {
                case Level.Beginner:
                    var i1 = random.Next(1, _beginnerDictionary.Count + 1);
                    return _beginnerDictionary[i1 - 1];
                case Level.Standard:
                    var i2 = random.Next(1, _standardDictionary.Count + 1);
                    return _standardDictionary[i2 - 1];
                case Level.Advanced:
                    var i3 = random.Next(1, _advancedDictionary.Count + 1);
                    return _advancedDictionary[i3 - 1];
                default:
                    var iDefault = random.Next(1, _beginnerDictionary.Count + 1);
                    return _beginnerDictionary[iDefault - 1];
            }
        }
    }
}
