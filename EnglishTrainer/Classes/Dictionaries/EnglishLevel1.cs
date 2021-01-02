using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Classes.Dictionaries
{
    public static class EnglishLevel1
    {
        private static List<Word> _words = new List<Word>();
        static EnglishLevel1()
        {
            _words.Add(new Word() { Answer = "do", Help = "делать" });
            _words.Add(new Word() { Answer = "in", Help = "в" });
            _words.Add(new Word() { Answer = "jump", Help = "прыгать" });
            _words.Add(new Word() { Answer = "run", Help = "бегать" });
            _words.Add(new Word() { Answer = "fly", Help = "летать" });
            _words.Add(new Word() { Answer = "see", Help = "видеть" });
            _words.Add(new Word() { Answer = "put", Help = "ложить" });
            _words.Add(new Word() { Answer = "trust", Help = "верить" });
            _words.Add(new Word() { Answer = "work", Help = "работа" });
            _words.Add(new Word() { Answer = "mother", Help = "мама" });
            _words.Add(new Word() { Answer = "father", Help = "папа" });
        }

        public static Word GetWord()
        {
            var random = new Random();
            var i = random.Next(1, _words.Count);
            return _words[i];
        }

    }
}
