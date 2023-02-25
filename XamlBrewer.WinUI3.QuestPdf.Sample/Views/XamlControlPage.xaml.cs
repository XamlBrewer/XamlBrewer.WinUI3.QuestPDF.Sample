using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration;
using XamlBrewer.WinUI3.Services;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Views
{
    public sealed partial class XamlControlPage : Page
    {
        public XamlControlPage()
        {
            InitializeComponent();

            CalendarView.MinDate = new DateTime(2023, 12, 01);
            CalendarView.SelectedDates.Add(new DateTime(2023, 12, 25));
        }

        private async void XamlControlButton_Click(object sender, RoutedEventArgs e)
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
                { "Slider", await Slider.AsPng() },
                { "Button", await Button.AsPng() },
                { "NumberBox", await NumberBox.AsPng() },
                { "RatingControl", await RatingControl.AsPng() },
                { "CheckBox", await CheckBox.AsPng() },
                { "RadioButton", await RadioButton.AsPng() },
                { "RadialGauge", await RadialGauge.AsPng() },
                { "OrbitView", await OrbitView.AsPng() },
                { "CalendarView", await CalendarView.AsPng() }
            };

            if (switchTheme)
            {
                RequestedTheme = ElementTheme.Default;
            }

            var document = new XamlControlDocument(images);

            document.GeneratePdf(filePath);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true
                }
            };

            process.Start();
        }
    }
}
