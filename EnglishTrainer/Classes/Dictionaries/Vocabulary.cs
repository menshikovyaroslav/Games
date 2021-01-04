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
                var splitted = line.Split(';');
                _beginnerDictionary.Add(new Word() {Answer = splitted[0], Help = splitted[1] });
            }

            var standardLines = File.ReadAllLines("standard.csv", Encoding.GetEncoding("windows-1251"));
            foreach (var line in standardLines)
            {
                var splitted = line.Split(';');
                _standardDictionary.Add(new Word() { Answer = splitted[0], Help = splitted[1] });
            }

            var advancedLines = File.ReadAllLines("advanced.csv", Encoding.GetEncoding("windows-1251"));
            foreach (var line in advancedLines)
            {
                var splitted = line.Split(';');
                _advancedDictionary.Add(new Word() { Answer = splitted[0], Help = splitted[1] });
            }

            #endregion
        }

        /// <summary>
        /// Получить слово из словаря Начинающего уровня
        /// </summary>
        /// <returns></returns>
        public static Word GetBeginnerWord()
        {
            var random = new Random();
            var i = random.Next(1, _beginnerDictionary.Count + 1);
            return _beginnerDictionary[i - 1];
        }

        /// <summary>
        /// Получить слово из словаря Опытного уровня
        /// </summary>
        /// <returns></returns>
        public static Word GetStandardWord()
        {
            var random = new Random();
            var i = random.Next(1, _standardDictionary.Count + 1);
            return _standardDictionary[i - 1];
        }

        /// <summary>
        /// Получить слово из словаря Продвинутого уровня
        /// </summary>
        /// <returns></returns>
        public static Word GetAdvancedWord()
        {
            var random = new Random();
            var i = random.Next(1, _advancedDictionary.Count + 1);
            return _advancedDictionary[i - 1];
        }
    }
}
