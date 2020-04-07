using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ArgData;
using ArgTrack.AppStuff;
using ArgTrack.Commands.Tracks;
using ArgTrack.TrackStuff;

namespace ArgTrack.ViewModels.Track
{
    public class TrackViewModel : BaseViewModel
    {
        public TrackViewModel()
        {
            _trackSections = new TrackSectionViewModelCollection(this);
            _pitLaneSections = new TrackSectionViewModelCollection(this);
            _bestLineSegments = new BestLineSegmentViewModelCollection(this);
            _trackObjectSettings = new ObservableCollection<TrackObjectSettingsViewModel>();
            _trackObjectShapes = new ObservableCollection<TrackObjectShapeViewModel>();

            TrackSettings = new TrackSettingsViewModel();

            ComputerCarBehavior = new ComputerCarBehaviorViewModel();

            CarSettings = new CarSettingsViewModel();

            TrackCameraCommands = new TrackCameraCommandViewModelCollection(this);

            TrackHeader = new TrackHeaderViewModel();
            ComputerCarSetup = new SetupViewModel("TrackSetup");

            TrackSections.CollectionChanged += TrackSections_CollectionChanged;
        }

        private void TrackSections_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            TotalLengthText = UnitConvert.TrackLengthToMetersDescription(TrackSections.Sum(t => t.Length));
        }

        private TrackSectionViewModelCollection _trackSections;

        public TrackSectionViewModelCollection TrackSections
        {
            get { return _trackSections; }
            set { SetProperty(ref _trackSections, value, () => TrackSections); }
        }

        public void RecalculateLayout()
        {
            var layoutVM = TrackLayoutViewModel.FromTrackViewModel(this);

            MainViewModelPasserArounder.MainViewModel.TrackLayoutViewModel = layoutVM;
        }


        private TrackSectionViewModelCollection _pitLaneSections;

        public TrackSectionViewModelCollection PitLaneSections
        {
            get { return _pitLaneSections; }
            set { SetProperty(ref _pitLaneSections, value, () => PitLaneSections); }
        }


        private BestLineSegmentViewModelCollection _bestLineSegments;

        public BestLineSegmentViewModelCollection BestLineSegments
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


        private TrackCameraCommandViewModelCollection _trackCameraCommands;

        public TrackCameraCommandViewModelCollection TrackCameraCommands
        {
            get { return _trackCameraCommands; }
            set { SetProperty(ref _trackCameraCommands, value, () => TrackCameraCommands); }
        }


        public TrackSettingsViewModel TrackSettings { get; }


        public TrackHeaderViewModel TrackHeader { get; }


        public SetupViewModel ComputerCarSetup { get; set; }


        public ComputerCarBehaviorViewModel ComputerCarBehavior { get; set; }


        public CarSettingsViewModel CarSettings { get; set; }



        private short _computerCarLineDisplacement;

        // place elsewhere in the future?
        public short ComputerCarLineDisplacement
        {
            get { return _computerCarLineDisplacement; }
            set { SetProperty(ref _computerCarLineDisplacement, value, () => ComputerCarLineDisplacement); }
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
            var palette = Palette.CreateDrivingPalette();

            // TODO: somewhere else ffs
            byte[] horizonBytes = HorizonBytes;

            int pos = 0;

            var bitmap = new Bitmap(512, 8);
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 511; x++)
                {
                    var color = palette.GetColor(horizonBytes[pos]);

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
                    var color = palette.GetColor(horizonBytes[pos]);


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
                memory.Position = 0;

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            HorizonImage = bitmapImage;
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

        public ICommand UpdateHorizonFromFileCommand { get; } = new LoadHorizonFromFileCommand(new FileDialog());
        public ICommand SaveHorizonToFileCommand { get; } = new SaveHorizonToFileCommand(new FileDialog());
    }
}
