using EnglishTrainer.Classes;
using System.Windows;

namespace EnglishTrainer.Views
{
    /// <summary>
    /// Логика взаимодействия для EnterNameWindow.xaml
    /// </summary>
    public partial class EnterNameWindow : Window
    {
        /// <summary>
        /// Уровень законченной игры
        /// </summary>
        public Level Level { get; set; }

        /// <summary>
        /// Количество набранных очков
        /// </summary>
        private int _score;

        public EnterNameWindow(Level level, int score)
        {
            InitializeComponent();
            DataContext = this;

            Level = level;
            _score = score;

            GamerName.Focus();
        }

        /// <summary>
        /// Нажатие на кнопку сохранить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var gamerName = GamerName.Text;
            if (string.IsNullOrEmpty(gamerName)) return;

            TopScore.SetTopScore(new TopScoreResult(GamerName.Text, _score, Level));

            Close();
        }

        /// <summary>
        /// Отмена сохранения результата
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
