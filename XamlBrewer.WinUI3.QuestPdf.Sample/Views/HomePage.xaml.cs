using Microcharts;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using QuestPDF.ExampleInvoice;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using SkiaSharp;
using System.Diagnostics;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Views
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        // Source: https://www.questpdf.com/quick-start
        private void HelloWorldButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = "C:\\Temp\\hello.pdf";

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text("Hello PDF!")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);

                            x.Item().Text(Placeholders.LoremIpsum());
                            x.Item().Image(Placeholders.Image(200, 100));
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            });

            // document.ShowInPreviewer();
            document.GeneratePdf(filePath);

            // Process.Start("explorer.exe", filePath);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo(filePath)
                {
                    UseShellExecute = true
                }
            };

            process.Start();
        }

        // Source: https://www.questpdf.com/getting-started
        private void InvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = "C:\\Temp\\invoice.pdf";

            var model = InvoiceDocumentDataSource.GetInvoiceDetails();
            var document = new InvoiceDocument(model);

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

        // Source: https://github.com/QuestPDF/QuestPDF/blob/main/Source/QuestPDF.Examples/ChartExamples.cs
        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            var entries = new[]
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

            var filePath = "C:\\Temp\\chart.pdf";

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Content().Element(container =>
                    {
                        container
                            .Background(Colors.White)
                            .Padding(25)
                            .Column(column =>
                            {
                                column
                                    .Item()
                                    .PaddingBottom(10)
                                    .Text("Chart example")
                                    .FontSize(20)
                                    .SemiBold()
                                    .FontColor(Colors.Blue.Medium);

                                column
                                    .Item()
                                    .Border(1)
                                    .ExtendHorizontal()
                                    .Height(300)
                                    .Canvas((canvas, size) =>
                                    {
                                        var chart = new LineChart
                                        {
                                            Entries = entries,

                                            LabelOrientation = Microcharts.Orientation.Horizontal,
                                            ValueLabelOrientation = Microcharts.Orientation.Horizontal,

                                            IsAnimated = false,
                                        };

                                        chart.DrawContent(canvas, (int)size.Width, (int)size.Height);
                                    });
                            });
                    });
                });
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

