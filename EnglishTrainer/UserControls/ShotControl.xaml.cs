using EnglishTrainer.Classes;
using System.Windows.Controls;

namespace EnglishTrainer.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ShotControl.xaml
    /// </summary>
    public partial class ShotControl : UserControl
    {
        /// <summary>
        /// Ссылка на объект выстрела
        /// </summary>
        public Shot Shot { get; set; }
        public ShotControl(Shot shot)
        {
            InitializeComponent();
            DataContext = this;

            Shot = shot;
        }
    }
}
