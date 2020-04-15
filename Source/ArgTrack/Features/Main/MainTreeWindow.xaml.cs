using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ArgTrack.Features.TrackDrawing;
using ArgTrack.Features.TrackEditing.Camera;
using ArgTrack.Features.TrackEditing.CarSettings;
using ArgTrack.Features.TrackEditing.CarSetup;
using ArgTrack.Features.TrackEditing.ComputerCarBehavior;
using ArgTrack.Features.TrackEditing.Horizon;
using ArgTrack.Features.TrackEditing.ObjectSettings;
using ArgTrack.Features.TrackEditing.ObjectShapes;
using ArgTrack.Features.TrackEditing.RacingLine;
using ArgTrack.Features.TrackEditing.TrackSection;
using ArgTrack.Features.TrackEditing.TrackSettings;
using ArgTrack.Features.TrackEditing.WorldLocation;

namespace ArgTrack.Features.Main
{
    /// <summary>
    /// Interaction logic for MainTreeWindow.xaml
    /// </summary>
    public partial class MainTreeWindow : Window
    {
        public MainTreeWindow()
        {
            InitializeComponent();

            var defaultMainViewModel = new MainViewModel();

            MainViewModelPasserArounder.MainViewModel = defaultMainViewModel;

            this.DataContext = defaultMainViewModel;
        }

        private void GridGrid_OnLayoutUpdated(object sender, EventArgs e)
        {
            RecalculateDrawingAreaStuffz();
        }

        private void MainTreeWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RecalculateDrawingAreaStuffz();
        }

        private void RecalculateDrawingAreaStuffz()
        {
            var tlvm = MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel;

            if (tlvm == null)
            {
                return;
            }

            // TODO: hmmmm
            tlvm.ViewportWidth = GridGrid.ActualWidth;
            tlvm.ViewportHeight = GridGrid.ActualHeight;
            tlvm.RecalculateViewAndScrollbars();
        }


        private void TrackMiscDataTreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new EditTrackHeaderSettingsCommand();
            cmd.Execute(TrackViewModelPasserArounder.TrackViewModel.TrackHeader);
        }

        private void TrackMiscDataCarSettingsTreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new EditCarSettingsCommand();
            cmd.Execute(TrackViewModelPasserArounder.TrackViewModel.CarSettings);
        }

        private void TrackLayoutTreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new EditTrackLayoutSettingsCommand();
            cmd.Execute(TrackViewModelPasserArounder.TrackViewModel.TrackSettings);
        }

        private void TrackSectionTreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // <.InputBinding><Gesture> was nice but didn't properly register double-clicks...

            var trackSection = GetDataContextFromOriginalSource(e.OriginalSource);

            if (trackSection == null)
            {
                return;
            }

            var cmd = new EditTrackSectionCommand();
            cmd.Execute(trackSection);
        }


        private void ComputerCarsRacingLineHeader_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new EditComputerCarLineHeaderCommand();
            cmd.Execute(MainViewModelPasserArounder.MainViewModel.TrackViewModel);
        }

        private void ComputerCarsRacingLine_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var ccLine = GetDataContextFromOriginalSource(e.OriginalSource);

            if (ccLine == null)
            {
                return;
            }

            var cmd = new EditBestLineCommand();
            cmd.Execute(ccLine);
        }

        private void ComputerCarsSetup_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // TODO: move to command?

            var track = MainViewModelPasserArounder.MainViewModel.TrackViewModel;
            var setup = track.ComputerCarSetup;

            var clone = new SetupViewModel();
            CarSetupMapper.MapCarSetupViewModel(setup, clone);

            var window = new CarSetupWindow
            {
                Owner = System.Windows.Application.Current.MainWindow,
                DataContext = clone
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                CarSetupMapper.MapCarSetupViewModel(clone, setup);
            }
        }

        private void ComputerCarsBehavior_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new EditComputerCarBehaviorCommand();
            cmd.Execute(TrackViewModelPasserArounder.TrackViewModel.ComputerCarBehavior);
        }

        private void ObjectSettingsTreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var objectSetting = GetDataContextFromOriginalSource(e.OriginalSource);

            if (objectSetting == null)
            {
                return;
            }

            var cmd = new EditTrackObjectSettingsCommand();
            cmd.Execute(objectSetting);
        }

        private void ObjectShapesTreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var objectShape = GetDataContextFromOriginalSource(e.OriginalSource);

            if (objectShape == null)
            {
                return;
            }

            var cmd = new EditTrackObjectShapeCommand();
            cmd.Execute(objectShape);
        }

        private object GetDataContextFromOriginalSource(object originalSource)
        {
            if (originalSource is TextBlock textBlock)
            {
                return textBlock.DataContext;
            }

            if (originalSource is Border border)
            {
                return border.DataContext;
            }

            if (originalSource is Image image)
            {
                // ?
                return image.DataContext;
            }

            return null;
        }


        private void TrackHorizonTreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // TODO: move to command?
            var track = MainViewModelPasserArounder.MainViewModel.TrackViewModel;

            var viewModel = new HorizonEditingViewModel
            {
                HorizonBytes = track.HorizonBytes
            };

            var horizWindow = new HorizonWindow
            {
                DataContext = viewModel
            };

            bool? well = horizWindow.ShowDialog();

            if (well.HasValue && well.Value)
            {
                track.HorizonBytes = viewModel.HorizonBytes;
            }
        }

        private void CameraCommandsTreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new OpenCameraCommandListWindowCommand();
            cmd.Execute(MainViewModelPasserArounder.MainViewModel);
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is TrackSectionViewModel selected)
            {
                var pointData = new PointData();

                foreach (var point in selected.SectionPoints)
                {
                    pointData.Points.Add(point);
                }

                pointData.Fill = Brushes.Transparent;
                pointData.Stroke = Brushes.Cyan;
                pointData.StrokeThickness = 0.35;

                MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel.SelectedTrackSection = pointData;
            }
            else
            {
                MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel.SelectedTrackSection = null;
            }
        }

        private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            // ugly as f
            var parent = VisualUpwardSearch(treeViewItem);

            if (parent.DataContext is TrackSectionViewModel dc)
            {
                // TODO: improve like ComputerCarRacingLine below :)
                var index = MainViewModelPasserArounder.MainViewModel.TrackViewModel.TrackSections.IndexOf(dc);
                //MainViewModelPasserArounder.MainViewModel.TrackViewModel.SelectedTrackSectionIndex = index;
                MainViewModelPasserArounder.MainViewModel.TrackViewModel.TrackSections.SelectedIndex = index;  // ?
            }

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private void PitLaneOnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            // ugly as f
            var parent = VisualUpwardSearch(treeViewItem);

            if (parent.DataContext is TrackSectionViewModel dc)
            {
                // TODO: improve like ComputerCarRacingLine below :)
                var index = MainViewModelPasserArounder.MainViewModel.TrackViewModel.PitLaneSections.IndexOf(dc);
                MainViewModelPasserArounder.MainViewModel.TrackViewModel.PitLaneSections.SelectedIndex = index;  // ?
            }

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private void ComputerCarsRacingLine_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            // ugly as f
            var parent = VisualUpwardSearch(treeViewItem);

            if (parent.DataContext is BestLineSegmentViewModel viewModel)
            {
                viewModel.SelectInList();
            }

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }
    }
}
