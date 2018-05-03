using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ArgData;
using ArgTrack.Commands.Tracks;

namespace ArgTrack.ViewModels.Track
{
    public class TrackViewModel : BaseViewModel
    {
        public TrackViewModel()
        {
            _trackSections = new ObservableCollection<TrackSectionViewModel>();
            _pitLaneSections = new ObservableCollection<TrackSectionViewModel>();
            _bestLineSegments = new ObservableCollection<BestLineSegmentViewModel>();
            _trackObjectSettings = new ObservableCollection<TrackObjectSettingsViewModel>();
            _trackObjectShapes = new ObservableCollection<TrackObjectShapeViewModel>();

            TrackHeader = new TrackHeaderViewModel();
            ComputerCarSetup = new SetupViewModel("TrackSetup");
            //TrackRawData = new TrackRawDataViewModel();

            TrackSections.CollectionChanged += TrackSections_CollectionChanged;
        }

        private void TrackSections_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            int totalFeet = 0;

            foreach (var section in TrackSections)
            {
                totalFeet += section.Length * 16;
            }

            double meters = Math.Round(totalFeet * 0.3048, 2);

            var cult = new CultureInfo("en-US");
            var meterz = meters.ToString("F", cult);

            //LengthDescription = $"~{meters:F} meters ({feet} feet)";
            TotalLengthText = $"~{meterz} meters ({totalFeet} feet)";
        }

        private ObservableCollection<TrackSectionViewModel> _trackSections;

        public ObservableCollection<TrackSectionViewModel> TrackSections
        {
            get { return _trackSections; }
            set { SetProperty(ref _trackSections, value, () => TrackSections); }
        }


        private ObservableCollection<TrackSectionViewModel> _pitLaneSections;

        public ObservableCollection<TrackSectionViewModel> PitLaneSections
        {
            get { return _pitLaneSections; }
            set { SetProperty(ref _pitLaneSections, value, () => PitLaneSections); }
        }


        private ObservableCollection<BestLineSegmentViewModel> _bestLineSegments;

        public ObservableCollection<BestLineSegmentViewModel> BestLineSegments
        {
            get { return _bestLineSegments; }
            set { SetProperty(ref _bestLineSegments, value, () => BestLineSegments); }
        }


        private ObservableCollection<TrackObjectSettingsViewModel> _trackObjectSettings;

        public ObservableCollection<TrackObjectSettingsViewModel> TrackObjectSettings
        {
            get { return _trackObjectSettings; }
            set { SetProperty(ref _trackObjectSettings, value, () => TrackObjectSettings); }
        }


        private ObservableCollection<TrackObjectShapeViewModel> _trackObjectShapes;

        public ObservableCollection<TrackObjectShapeViewModel> TrackObjectShapes
        {
            get { return _trackObjectShapes; }
            set { SetProperty(ref _trackObjectShapes, value, () => TrackObjectShapes); }
        }


        public TrackHeaderViewModel TrackHeader { get; }


        public SetupViewModel ComputerCarSetup { get; set; }


        // see TrackHeader instead
        //public TrackRawDataViewModel TrackRawData { get; set; }


        private byte _lapCount;

        public byte LapCount
        {
            get { return _lapCount; }
            set { SetProperty(ref _lapCount, value, () => LapCount); }
        }


        private BitmapImage _horizonImage;

        public BitmapImage HorizonImage
        {
            get { return _horizonImage; }
            set { SetProperty(ref _horizonImage, value, () => HorizonImage); }
        }

        private byte[] _horizonBytes;

        public byte[] HorizonBytes
        {
            get { return _horizonBytes; }
            set
            {
                if (SetProperty(ref _horizonBytes, value, () => HorizonBytes))
                {
                    UpdateHorizonImage();
                }
            }
        }


        private void UpdateHorizonImage()
        {
            // TODO: somewhere else ffs
            byte[] horizonBytes = HorizonBytes;

            int pos = 0;

            var bitmap = new Bitmap(512, 8);
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 511; x++)
                {
                    var color = Palette.GetColor(horizonBytes[pos]);

                    bitmap.SetPixel(x, y, color);
                    pos++;
                }
            }


            pos = 0;
            var altBitmap = new Bitmap(4096, 64);
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 511; x++)
                {
                    var color = Palette.GetColor(horizonBytes[pos]);


                    for (int py = 0; py <= 7; py++)
                    {
                        for (int px = 0; px <= 7; px++)
                        {
                            int dox = x * 8 + px;
                            int doy = y * 8 + py;

                            altBitmap.SetPixel(dox, doy, color);
                        }
                    }
                    pos++;
                }
            }

            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream memory = new MemoryStream())
            {
                altBitmap.Save(memory, ImageFormat.Bmp);
                //bitmap.Save(memory, ImageFormat.Bmp);
                //bm.Save(memory, ImageFormat.Bmp);
                //newImage.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            HorizonImage = bitmapImage;
            //viewModel.HorizonBytes = horizonBytes;
        }



        private int _selectedTrackSectionIndex;

        public int SelectedTrackSectionIndex
        {
            get { return _selectedTrackSectionIndex; }
            set { SetProperty(ref _selectedTrackSectionIndex, value, () => SelectedTrackSectionIndex); }
        }

        private int _selectedPitLaneSectionIndex;

        public int SelectedPitLaneSectionIndex
        {
            get { return _selectedPitLaneSectionIndex; }
            set { SetProperty(ref _selectedPitLaneSectionIndex, value, () => SelectedPitLaneSectionIndex); }
        }

        private int _selectedBestLineSectionIndex;

        public int SelectedBestLineSectionIndex
        {
            get { return _selectedBestLineSectionIndex; }
            set { SetProperty(ref _selectedBestLineSectionIndex, value, () => SelectedBestLineSectionIndex); }
        }




        private string _totalLengthText;

        public string TotalLengthText
        {
            get { return _totalLengthText; }
            set { SetProperty(ref _totalLengthText, value, () => TotalLengthText); }
        }



        public ICommand EditTrackSectionCommand { get; } = new EditTrackSectionCommand();
        public ICommand EditBestLineCommand { get; } = new EditBestLineCommand();
        public ICommand EditTrackObjectSettingsCommand { get; } = new EditTrackObjectSettingsCommand();
        public ICommand EditTrackObjectShapeCommand { get; } = new EditTrackObjectShapeCommand();
    }
}
