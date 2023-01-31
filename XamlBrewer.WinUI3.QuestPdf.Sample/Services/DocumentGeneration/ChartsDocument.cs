using Microcharts;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration
{
    internal class ChartsDocument : IDocument
    {
        public List<Chart> Model { get; }

        public ChartsDocument(List<Chart> model)
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
                    .Text("XAML Brewer QuestPDF & Microcharts Sample")
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
                foreach (var chart in Model)
                {
                    column.Item()
                        .Height(300)
                        .Canvas((canvas, size) =>
                        {
                            chart.DrawContent(canvas, (int)size.Width, (int)size.Height);
                        });
                }
            });
        }
    }
}
