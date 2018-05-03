using System.Windows;
using ArgTrack.ViewModels;

namespace ArgTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var defaultMainViewModel = new MainViewModel();

            // HACK: world class hack!
            MainViewModelPasserArounder.MainViewModel = defaultMainViewModel;

            this.DataContext = defaultMainViewModel;
        }
    }
}
