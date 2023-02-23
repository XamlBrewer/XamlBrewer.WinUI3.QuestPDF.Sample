using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration;

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
            var switchTheme = false;

            var filePath = "C:\\Temp\\xamlcontrols.pdf";

            if (ActualTheme == ElementTheme.Dark)
            {
                switchTheme = true;
                RequestedTheme = ElementTheme.Light;
            }

            var images = new Dictionary<string, byte[]>
            {
                { "Slider", await CreateBytes(Slider) },
                { "Button", await CreateBytes(Button) },
                { "NumberBox", await CreateBytes(NumberBox) },
                { "RatingControl", await CreateBytes(RatingControl) },
                { "CheckBox", await CreateBytes(CheckBox) },
                { "RadioButton", await CreateBytes(RadioButton) },
                { "RadialGauge", await CreateBytes(RadialGauge) },
                { "OrbitView", await CreateBytes(OrbitView) }
            };

            var document = new XamlControlDocument(images);

            document.GeneratePdf(filePath);

            if (switchTheme)
            {
                RequestedTheme = ElementTheme.Default;
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true
                }
            };

            process.Start();
        }

        private async Task<byte[]> CreateBytes(UIElement control)
        {
            // Get XAML Visual in BGRA8 format
            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(control, (int)control.RenderSize.Width, (int)control.RenderSize.Height);

            // Encode as PNG
            var pixelBuffer = (await rtb.GetPixelsAsync()).ToArray();
            IRandomAccessStream mraStream = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, mraStream);
            encoder.SetPixelData(
                BitmapPixelFormat.Bgra8,
                BitmapAlphaMode.Premultiplied,
                (uint)rtb.PixelWidth,
                (uint)rtb.PixelHeight,
                184,
                184,
                pixelBuffer);
            await encoder.FlushAsync();

            // Transform to byte array
            var bytes = new byte[mraStream.Size];
            await mraStream.ReadAsync(bytes.AsBuffer(), (uint)mraStream.Size, InputStreamOptions.None);
            return bytes;
        }
    }
}
