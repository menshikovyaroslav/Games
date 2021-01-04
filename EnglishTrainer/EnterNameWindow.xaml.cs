using EnglishTrainer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnglishTrainer
{
    /// <summary>
    /// Логика взаимодействия для EnterNameWindow.xaml
    /// </summary>
    public partial class EnterNameWindow : Window
    {
        public Level Level { get; set; }
        private int _score;

        public EnterNameWindow(Level level, int score)
        {
            InitializeComponent();
            DataContext = this;

            Level = level;
            _score = score;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            TopScore.SetTopScore(new TopScoreResult(GamerName.Text, _score, Level));

            Close();
        }
    }
}
