using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace CroppingImageLibrary.SampleApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private CroppingWindow _croppingWindow;
        // private BitmapImage bitmapImage;
        private BitmapSource sourceBitmap;

        public MainWindow()
        {
            InitializeComponent();
            Topmost = true;

        }

        private void Button_LoadImage(object sender, RoutedEventArgs e)
        {
            if (_croppingWindow != null)
                return;
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png"
            };
            if (op.ShowDialog() != true) { return; }

            sourceBitmap = new BitmapImage(new Uri(op.FileName));

            _croppingWindow = new CroppingWindow();
            _croppingWindow.Closed += (a, b) => _croppingWindow = null;


            double width = sourceBitmap.PixelWidth;
            double height = sourceBitmap.PixelHeight;
            double maxWidth = Math.Min(SystemParameters.PrimaryScreenWidth - 300, width);
            double maxHeight = Math.Min(SystemParameters.PrimaryScreenHeight - 300, height);

            var aspectRatio = Math.Min(maxWidth / width, maxHeight / height);
            width *= aspectRatio;
            height *= aspectRatio;

            _croppingWindow.Width = width;
            _croppingWindow.Height = height;
            _croppingWindow.SetImage(new BitmapImage(new Uri(op.FileName)), width, height);


            _croppingWindow.Show();
        }

        private void Button_SaveImage(object sender, RoutedEventArgs e)
        {
            var cropArea = _croppingWindow.CropTool.CropService.GetCroppedArea();

            var x = Convert.ToInt32(cropArea.CroppedRectAbsolute.X);
            var y = Convert.ToInt32(cropArea.CroppedRectAbsolute.Y);
            var width = Convert.ToInt32(cropArea.CroppedRectAbsolute.Width);
            var height = Convert.ToInt32(cropArea.CroppedRectAbsolute.Height);

            var cb = new CroppedBitmap(sourceBitmap, new Int32Rect(x, y, width, height));       //select region rect

            //save image to file
            SaveFileDialog dlg = new SaveFileDialog
            {
                FileName = "TestCropping",          // Default file name
                DefaultExt = ".png",                  // Default file extension
                Filter = "Image png (.png)|*.png" // Filter files by extension
            };
            
            // Show save file dialog box
            bool? result = dlg.ShowDialog();
            
            // Process save file dialog box results
            if (result != true)
                return;

            // Save document

            using var fileStream = new FileStream(dlg.FileName, FileMode.Create);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(cb));
            encoder.Save(fileStream);
        }
    }
}