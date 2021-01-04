using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTrainer.Classes
{
    /// <summary>
    /// Класс работы с таблицей рекордов
    /// </summary>
    public static class TopScore
    {
        /// <summary>
        /// Запись нового значения
        /// </summary>
        /// <param name="scoreResult"></param>
        public static void SetTopScore(TopScoreResult scoreResult)
        {
            try
            {
                var hkcu = Registry.CurrentUser;
                RegistryKey scoresBranch = hkcu.CreateSubKey("Software\\EnglishTrainer");
                switch (scoreResult.Level)
                {
                    case Level.Beginner:
                        var l1Branch = scoresBranch.CreateSubKey("Beginner");
                        l1Branch.SetValue(scoreResult.Name, scoreResult.ToSaveFormat());
                        l1Branch.Close();
                        break;
                    case Level.Standard:
                        var l2Branch = scoresBranch.CreateSubKey("Standard");
                        l2Branch.SetValue(scoreResult.Name, scoreResult.ToSaveFormat());
                        l2Branch.Close();
                        break;
                    case Level.Advanced:
                        var l3Branch = scoresBranch.CreateSubKey("Advanced");
                        l3Branch.SetValue(scoreResult.Name, scoreResult.ToSaveFormat());
                        l3Branch.Close();
                        break;
                }

                scoresBranch?.Close();
                hkcu?.Close();
            }
            catch (Exception ex)
            {
            }

        }

        /// <summary>
        /// Получение таблицы рекордов
        /// </summary>
        /// <returns></returns>
        public static List<TopScoreResult> GetTopScores()
        {
            List<TopScoreResult> result = new List<TopScoreResult>();

            try
            {
                var hkcu = Registry.CurrentUser;
                RegistryKey scoresBranch = hkcu.CreateSubKey("Software\\EnglishTrainer");

                var l1Branch = scoresBranch.CreateSubKey("Beginner");
                var l2Branch = scoresBranch.CreateSubKey("Standard");
                var l3Branch = scoresBranch.CreateSubKey("Advanced");

                var l1Keys = l1Branch.GetValueNames();
                foreach (var l1Key in l1Keys)
                {
                    var l1Value = ((string[])l1Branch.GetValue(l1Key));
                    var topScoreResult = new TopScoreResult(l1Value);
                    result.Add(topScoreResult);
                }
                l1Branch.Close();

                var l2Keys = l2Branch.GetValueNames();
                foreach (var l2Key in l2Keys)
                {
                    var l2Value = ((string[])l2Branch.GetValue(l2Key));
                    var topScoreResult = new TopScoreResult(l2Value);
                    result.Add(topScoreResult);
                }
                l2Branch.Close();

                var l3Keys = l3Branch.GetValueNames();
                foreach (var l3Key in l3Keys)
                {
                    var l3Value = ((string[])l3Branch.GetValue(l3Key));
                    var topScoreResult = new TopScoreResult(l3Value);
                    result.Add(topScoreResult);
                }
                l3Branch.Close();

                scoresBranch?.Close();
                hkcu?.Close();
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }
}
