using Microcharts;
using Microsoft.UI.Xaml.Controls;
using QuestPDF.Fluent;
using SkiaSharp;
using System.Collections.Generic;
using System.Diagnostics;
using XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Views
{
    public sealed partial class ChartsPage : Page
    {
        public ChartsPage()
        {
            InitializeComponent();
        }

        public Chart BarChart
        {
            get
            {
                var chart = new BarChart
                {
                    Entries = Entries,

                    LabelOrientation = Microcharts.Orientation.Horizontal,
                    ValueLabelOrientation = Microcharts.Orientation.Horizontal,

                    IsAnimated = false
                };

                return chart;
            }
        }

        public Chart LineChart
        {
            get
            {
                var chart = new LineChart
                {
                    Entries = Entries,

                    LabelOrientation = Microcharts.Orientation.Horizontal,
                    ValueLabelOrientation = Microcharts.Orientation.Horizontal,

                    IsAnimated = false
                };

                return chart;
            }
        }

        public Chart RadarChart
        {
            get
            {
                var chart = new RadarChart
                {
                    Entries = Entries,

                    IsAnimated = false
                };

                return chart;
            }
        }

        public Chart RadialGaugeChart
        {
            get
            {
                var chart = new RadialGaugeChart
                {
                    Entries = Entries,

                    IsAnimated = false
                };

                return chart;
            }
        }

        private ChartEntry[] Entries
        {
            get
            {
                return new ChartEntry[]
                  {
                    new ChartEntry(212)
                    {
                        Label = "WinUI",
                                    ValueLabel = "112",
                                    Color = SKColor.Parse("#2c3e50")
                    },
                    new ChartEntry(248)
                    {
                        Label = "Android",
                                    ValueLabel = "648",
                                    Color = SKColor.Parse("#77d065")
                    },
                    new ChartEntry(128)
                    {
                        Label = "iOS",
                                    ValueLabel = "428",
                                    Color = SKColor.Parse("#b455b6")
                    },
                    new ChartEntry(514)
                    {
                        Label = "Forms",
                                    ValueLabel = "214",
                                    Color = SKColor.Parse("#3498db")
                    }
                  };

            }
        }

        private void ChartButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var filePath = "C:\\Temp\\charts.pdf";

            var document = new ChartsDocument(
                new List<Chart> {
                    BarChart,
                    LineChart,
                    RadarChart,
                    RadialGaugeChart
                });

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

