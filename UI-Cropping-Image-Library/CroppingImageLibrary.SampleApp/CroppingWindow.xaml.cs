using System.Windows;
using System.Windows.Media.Imaging;

namespace CroppingImageLibrary.SampleApp
{
    /// <summary>
    ///     Interaction logic for CroppingWindow.xaml
    /// </summary>
    public partial class CroppingWindow : Window
    {
        public CroppingWindow()
        {
            InitializeComponent();
        }

        public void SetImage(BitmapSource bitmapImage, double width, double height)
        {
            CropTool.SetImage(bitmapImage);
            CropTool.SetSize(width, height);
        }
    }
}