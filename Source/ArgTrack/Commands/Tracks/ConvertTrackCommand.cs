using System;

namespace ArgTrack.Commands.Tracks
{
    // ReSharper disable once InconsistentNaming
    public class ConvertTrackCommand : CommandBase
    {
        private IFileDialog _fileDialog;

        public ConvertTrackCommand(IFileDialog fileDialog)
        {
            _fileDialog = fileDialog;
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();

            // TODO: open convert-window
            //  there you choose: GP2 track, F1GP "base" track and new file name
            //  then what to convert: Track Sections, Pit Lane Sections only available to start with
            // TODO: retrieve TrackConversionViewModel and see what should be converted
            // TODO: load the new file
        }
    }
}
