using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ArgData;
using ArgTrack.Features.Application.Shared;
using ArgTrack.Features.Persistence;
using ArgTrack.Infrastructure;

namespace ArgTrack.Features.TrackEditing.Horizon
{
    public class HorizonEditingViewModel : BaseViewModel
    {
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


        public ICommand UpdateHorizonFromFileCommand { get; } = new LoadHorizonFromFileCommand(new FileDialog());
        public ICommand SaveHorizonToFileCommand { get; } = new SaveHorizonToFileCommand(new FileDialog());
        public ICommand HelpDocsCommand { get; } = new HelpDocsCommand("screens/track-horizon-window/");
    }
}
