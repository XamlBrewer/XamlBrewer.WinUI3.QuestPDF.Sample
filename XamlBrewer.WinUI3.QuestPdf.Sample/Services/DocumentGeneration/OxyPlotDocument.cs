using OxyPlot;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Svg.Skia;
using System.Collections.Generic;
using System.IO;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration
{
    internal class OxyPlotDocument : IDocument
    {
        public List<PlotModel> Model { get; }

        public OxyPlotDocument(List<PlotModel> model)
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
                    .Text("XAML Brewer QuestPDF & OxyPlot Sample")
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
                foreach (var plotModel in Model)
                {
                    column.Item()
                        .Height(300)
                        .Canvas((canvas, size) =>
                        {
                            using var stream = new MemoryStream();
                            var exporter = new SvgExporter
                            {
                                Width = 400,
                                Height = 300
                            };

                            exporter.Export(plotModel, stream);
                            stream.Position = 0;
                            var svg = new SKSvg();
                            svg.Load(stream);

                            canvas.DrawPicture(svg.Picture);
                        });
                }
            });
        }
    }
}
