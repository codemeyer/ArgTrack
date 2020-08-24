using System.Windows;
using System.Windows.Input;
using ArgData.Entities;

namespace ArgTrack.Features.TrackEditing.ObjectShapes
{
    public partial class TrackObjectShapeDetailsWindow : Window
    {
        public TrackObjectShapeDetailsWindow()
        {
            InitializeComponent();
        }

        private void OKButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ScaleValues_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            (DataContext as TrackObjectShapeViewModel).EditTrackObjectShapeScaleValueCommand.Execute(DataContext);
        }

        private void PointValues_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // TODO: move to command

            var currentShape = (DataContext as TrackObjectShapeViewModel);

            if (currentShape.SelectedPoint != null)
            {
                var pointIndex = currentShape.Points.IndexOf(currentShape.SelectedPoint);

                var point = currentShape.SelectedPoint;

                if (point.PointType == TrackObjectShapePointTypeEnum.Scale)
                {
                    var edi = new EditableScalePointViewModel(currentShape.ShapeBuddy.ScaleValues, point.X, point.Y, point.Z);

                    var win = new EditTrackObjectShapeScalePointWindow
                    {
                        Owner = System.Windows.Application.Current.MainWindow,
                        DataContext = edi
                    };

                    win.ShowDialog();

                    if (win.DialogResult == true)
                    {
                        point.XScaleValueIndex = edi.XValue.Index;
                        point.XIsNegative = edi.XValue.IsNegative;

                        point.YScaleValueIndex = edi.YValue.Index;
                        point.YIsNegative = edi.YValue.IsNegative;

                        point.Z = edi.ZValue;

                        foreach (var otherRefPoint in currentShape.Points)
                        {
                            if (otherRefPoint.PointType == TrackObjectShapePointTypeEnum.Reference && otherRefPoint.PointIndex == pointIndex)
                            {
                                otherRefPoint.Recalculate();
                                // reference point that references this point!
                            }
                        }

                        currentShape.CalculateSignature();
                    }
                }
                else
                {
                    var edi = new EditableReferencePointViewModel(currentShape.Points, point.PointIndex, point.Z);

                    var win = new EditTrackObjectShapeReferencePointWindow
                    {
                        Owner = System.Windows.Application.Current.MainWindow,
                        DataContext = edi
                    };

                    win.ShowDialog();

                    if (win.DialogResult == true)
                    {
                        point.PointIndex = edi.SelectedPointIndex;
                        point.Z = edi.ZValue;

                        foreach (var otherPoint in currentShape.Points)
                        {
                            otherPoint.Recalculate();
                        }

                        currentShape.CalculateSignature();
                    }
                }
            }
        }

        private void Vectors_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // TODO: as EditCommand

            var currentShape = (DataContext as TrackObjectShapeViewModel);

            if (currentShape.SelectedVector == null)
            {
                return;
            }

            var vector = currentShape.SelectedVector;

            var edi = new EditableShapeVectorViewModel(currentShape.Points)
            {
                From = vector.From,
                To = vector.To
            };

            var win = new EditTrackObjectShapeVectorWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = edi
            };

            win.ShowDialog();

            if (win.DialogResult == true)
            {
                vector.From = edi.From;
                vector.To = edi.To;
            }
        }
    }
}
