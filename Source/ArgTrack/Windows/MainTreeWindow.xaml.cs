using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ArgTrack.Commands.Tracks;
using ArgTrack.ViewModels;
using ArgTrack.ViewModels.Track;
using ArgTrack.Windows.Track;

namespace ArgTrack.Windows
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

        private void TrackLayoutTreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new EditTrackLayoutSettingsCommand();
            cmd.Execute(TrackViewModelPasserArounder.TrackViewModel.TrackHeader);
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
            var track = MainViewModelPasserArounder.MainViewModel.TrackViewModel;
            var setup = track.ComputerCarSetup;

            var clone = new SetupViewModel();
            Mappers.Mapper.MapCarSetupViewModel(setup, clone);

            var window = new CarSetupWindow
            {
                Owner = Application.Current.MainWindow,
                DataContext = clone
                // TODO: also pass in original, to allow reseting all properties
            };

            bool? update = window.ShowDialog();

            if (update.HasValue && update.Value)
            {
                Mappers.Mapper.MapCarSetupViewModel(clone, setup);
            }
        }

        private void ComputerCarsAdditionalData_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new EditGenericByteDataCommand();
            var bytes = MainViewModelPasserArounder.MainViewModel.TrackViewModel.TrackHeader.DataAfterSetup;

            cmd.Execute(bytes);
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
            var track = MainViewModelPasserArounder.MainViewModel.TrackViewModel;
            var originalBytes = track.HorizonBytes;
            // TODO: in command?
            var horizWindow = new HorizonWindow();
            horizWindow.DataContext = track;
            bool? well = horizWindow.ShowDialog();

            if (well.HasValue && well.Value)
            {
            }
            else
            {
                track.HorizonBytes = originalBytes;
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //(DataContext as MainViewModel).SelectedTreeViewItem = e.NewValue;
            // or just bind it?

            if (e.NewValue is TrackSectionViewModel selected)
            {
                var pointData = new PointData();

                // TODO: after adding a new section, we crash here (because there is no Layout?)
                foreach (var point in selected.Layout.SectionPoints)
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

        private void TrackMiscAdditionalData1TreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new EditGenericByteDataCommand();
            var bytes = MainViewModelPasserArounder.MainViewModel.TrackViewModel.TrackHeader.FinalData1;

            cmd.Execute(bytes);
        }

        private void TrackMiscAdditionalData2TreeView_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var cmd = new EditGenericByteDataCommand();
            var bytes = MainViewModelPasserArounder.MainViewModel.TrackViewModel.TrackHeader.FinalData2;

            cmd.Execute(bytes);
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
