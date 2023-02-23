using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration
{
    internal class XamlControlDocument : IDocument
    {
        public Dictionary<string, byte[]> Model { get; }

        public XamlControlDocument(Dictionary<string, byte[]> model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);

                page.Header()
                    .PaddingBottom(20)
                    .Text("XAML Brewer QuestPDF XAML Controls Sample")
                    .FontSize(16);

                page.Content().Element(ComposeBody);

                page.Footer().Text(text =>
                {
                    text.Span("page ");
                    text.CurrentPageNumber();
                });
            });
        }

        private void ComposeBody(IContainer body)
        {
            body.Column(column =>
            {
                column.Spacing(10);

                column.Item().Text("Slider:");
                column.Item()
                    .Height(50)
                    .Image(Model["Slider"], ImageScaling.FitArea);

                column.Item().Text("Radial Gauge:");
                column.Item()
                    .Height(150)
                    .Image(Model["RadialGauge"], ImageScaling.FitArea);

                column.Item().Text("NumberBox:");
                column.Item()
                    .Height(30)
                    .Image(Model["NumberBox"], ImageScaling.FitArea);

                column.Item().Text("CheckBox:");
                column.Item()
                    .Height(40)
                    .Image(Model["CheckBox"], ImageScaling.FitArea);

                column.Item().Text("RatingControl:");
                column.Item()
                    .Height(40)
                    .Image(Model["RatingControl"], ImageScaling.FitArea);

                column.Item().Text("RadioButton:");
                column.Item()
                    .Height(30)
                    .Image(Model["RadioButton"], ImageScaling.FitArea);

                column.Item().Text("Button:");
                column.Item()
                    .Height(60)
                    .Image(Model["Button"], ImageScaling.FitArea);

                column.Item().PageBreak();

                column.Item().Text("OrbitView:");
                column.Item()
                    .Height(600)
                    .Image(Model["OrbitView"], ImageScaling.FitArea);
            });
        }
    }
}
