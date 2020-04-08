using System;
using System.Windows;
using Arg2Data;
using ArgTrack.AppStuff;
using ArgTrack.ViewModels;

namespace ArgTrack.Windows.Tools
{
    /// <summary>
    /// Interaction logic for TrackConversionWindow.xaml
    /// </summary>
    public partial class TrackConversionWindow : Window
    {
        public TrackConversionWindow()
        {
            InitializeComponent();
        }

        private void BrowseSourceTrack_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as TrackConversionViewModel;

            var path = new FileDialog().SelectAnyFileToOpen();

            if (!string.IsNullOrEmpty(path))
            {
                vm.SourceTrackPath = path;
            }
        }

        private void BrowseTemplateTrack_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as TrackConversionViewModel;

            var path = new FileDialog().SelectAnyFileToOpen();

            if (!string.IsNullOrEmpty(path))
            {
                vm.TemplateTrackPath = path;
            }
        }

        private void BrowseTargetTrack_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as TrackConversionViewModel;

            var path = new FileDialog().SelectAnyFileToSave();

            if (!string.IsNullOrEmpty(path))
            {
                vm.TargetTrackPath = path;
            }
        }

        private TrackConversionViewModel ViewModel => DataContext as TrackConversionViewModel;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // TODO: only enabled if all three textboxes are filled?

            if (string.IsNullOrEmpty(ViewModel.SourceTrackPath))
                MessageBox.Show("Please select a GP2 source track", "ArgTrack", MessageBoxButton.OK,
                    MessageBoxImage.Information);

            if (string.IsNullOrEmpty(ViewModel.SourceTrackPath))
                MessageBox.Show("Please select an F1GP template track", "ArgTrack", MessageBoxButton.OK,
                    MessageBoxImage.Information);

            try
            {
                new TrackReader().Read(ViewModel.SourceTrackPath);
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem reading the GP2 source track.", "ArgTrack", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                new ArgData.TrackReader().Read(ViewModel.TemplateTrackPath);
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem reading the F1GP template track.", "ArgTrack", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.DialogResult = true;
        }
    }
}
