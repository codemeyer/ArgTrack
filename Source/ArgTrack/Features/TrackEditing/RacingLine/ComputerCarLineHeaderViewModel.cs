using System.Windows.Input;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.RacingLine
{
    public class ComputerCarLineHeaderViewModel : BaseViewModel
    {
        private short _computerCarLineDisplacement;

        public short Displacement
        {
            get { return _computerCarLineDisplacement; }
            set { SetProperty(ref _computerCarLineDisplacement, value, () => Displacement); }
        }


        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/racing-line-header-window/");
    }
}
