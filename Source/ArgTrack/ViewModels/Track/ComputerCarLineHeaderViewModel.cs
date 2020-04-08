using System.Windows.Input;
using ArgTrack.Commands;

namespace ArgTrack.ViewModels.Track
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
