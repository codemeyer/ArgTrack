using ArgTrack.ViewModels;

namespace ArgTrack.Commands
{
    public class OpenAboutTabCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var mvm = parameter as MainViewModel;

            if (mvm == null)
                return;

            mvm.SelectedMainTabIndex = 1;
        }
    }
}
