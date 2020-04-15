using ArgTrack.Features.Main;
using ArgTrack.Features.Persistence;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackConversion
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
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = viewModel
            };

            var update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                var sourceTrackReader = new Arg2Data.TrackReader();
                var sourceTrack = sourceTrackReader.Read(viewModel.SourceTrackPath);
                var templateTrack = new ArgData.TrackReader().Read(viewModel.TemplateTrackPath);
                var outputPath = viewModel.TargetTrackPath;

                TrackConverter.ConvertGP2ToF1GP(sourceTrack, templateTrack, outputPath);

                LoadTrackCommand cmd = new LoadTrackCommand(outputPath);
                cmd.Execute(vm);
            }
        }
    }
}
