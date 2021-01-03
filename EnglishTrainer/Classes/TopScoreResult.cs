using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Classes
{
    public class TopScoreResult
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public Level Level { get; set; }

        public TopScoreResult(string name, int score, Level level)
        {
            Name = name;
            Score = score;
            Level = level;
        }

        public string[] ToSaveFormat()
        {
            var result = new string[3];
            result[0] = Name;
            result[1] = Score.ToString();
            result[2] = Level.ToString();

            return result;
        }

        public TopScoreResult(string[] topScoreResult)
        {
            if (topScoreResult != null && topScoreResult.Length == 3)
            {
                Name = topScoreResult[0];
                var score = 0;
                Int32.TryParse(topScoreResult[1], out score);
                Score = score;
                var level = 0;
                Int32.TryParse(topScoreResult[2], out level);
                Level = (Level)level;
            }
        }
    }

    public enum Level
    {
        Beginner = 1,
        Standard = 2,
        Advanced = 3
    }
}
