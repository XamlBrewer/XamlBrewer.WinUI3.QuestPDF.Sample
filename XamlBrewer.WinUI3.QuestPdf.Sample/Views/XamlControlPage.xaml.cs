using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Views
{
    public sealed partial class XamlControlPage : Page
    {
        public XamlControlPage()
        {
            InitializeComponent();
        }

        private async void XamlControlButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // Test: transform XAML visual to image.

            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(RadialGauge, (int)RadialGauge.DesiredSize.Width, (int)RadialGauge.DesiredSize.Height);
            TestImage.Source = rtb;

            var pixelBuffer = await rtb.GetPixelsAsync();

            var stream = File.Create("C:\\Temp\\bitmap.png");
            stream.Flush();
            stream.Close();

            using IRandomAccessStream fileStream = await FileRandomAccessStream.OpenAsync("C:\\Temp\\bitmap.png", FileAccessMode.ReadWrite);
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream);

            encoder.SetPixelData(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Premultiplied,
                (uint)rtb.PixelWidth,
                (uint)rtb.PixelHeight,
                96,
                96,
                pixelBuffer.ToArray());

            await encoder.FlushAsync();
        }
    }
}
