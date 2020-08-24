using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public class AddObjectShapeVectorCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            var objectShapeViewModel = parameter as TrackObjectShapeViewModel;

            if (objectShapeViewModel == null)
            {
                return;
            }

            var edi = new EditableShapeVectorViewModel(objectShapeViewModel.Points)
            {
                From = 0,
                To = 0
            };

            var win = new EditTrackObjectShapeVectorWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = edi
            };

            win.ShowDialog();

            if (win.DialogResult == true)
            {
                var vector = new TrackObjectShapeVectorViewModel
                {
                    From = edi.From,
                    To = edi.To
                };

                objectShapeViewModel.Vectors.Add(vector);
            }
        }
    }
}
