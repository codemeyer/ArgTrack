using System.Windows;
using ArgTrack.TrackStuff;
using ArgTrack.ViewModels;
using TrackConversionWindow = ArgTrack.Windows.Tools.TrackConversionWindow;

namespace ArgTrack.Commands.Tracks
{
    public class ConvertTrackCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var vm = parameter as MainViewModel;

            if (vm == null)
                return;

            var viewModel = new TrackConversionViewModel();

            var window = new TrackConversionWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = viewModel
            };

            var update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                var sourceTrack = Arg2Data.TrackReader.Read(viewModel.SourceTrackPath);
                var templateTrack = new ArgData.TrackReader().Read(viewModel.TemplateTrackPath);
                var outputPath = viewModel.TargetTrackPath;

                TrackConverter.ConvertGP2ToF1GP(sourceTrack, templateTrack, outputPath);

                LoadTrackCommand cmd = new LoadTrackCommand(outputPath);
                cmd.Execute(vm);
            }
        }
    }
}
