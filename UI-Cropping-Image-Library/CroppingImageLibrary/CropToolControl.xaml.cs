using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CroppingImageLibrary.Services;

namespace CroppingImageLibrary
{
    /// <summary>
    /// Interaction logic for CropToolControl.xaml
    /// </summary>
    public partial class CropToolControl : UserControl
    {
        public CropService CropService { get; private set; }

        public CropToolControl()
        {
            InitializeComponent();
        }

        private void RootGrid_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CropService.Adorner.RaiseEvent(e);
        }

        private void RootGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CropService.Adorner.RaiseEvent(e);
        }

        private void RootGrid_Loaded(object sender, RoutedEventArgs e)
        {
            CropService = new CropService(this);
        }

        public void SetImage(BitmapSource bitmapImage)
        {
            SourceImage.Source = bitmapImage;
        }

        public void SetSize(double width, double height)
        {
            RootGrid.Width = width;
            RootGrid.Height = height;            
        }

        public void SetStretch(Stretch stretch)
        {
            SourceImage.Stretch = stretch;
        }

        public void SetBackground(Brush brush)
        {
            BackgroundColor.Background = brush;
        }

    }
}
