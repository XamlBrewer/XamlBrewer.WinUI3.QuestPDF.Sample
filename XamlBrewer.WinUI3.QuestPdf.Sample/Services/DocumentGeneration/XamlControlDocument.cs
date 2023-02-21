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
                column.Item()
                    .Height(150)
                    .Image(Model["RadialGauge"], ImageScaling.FitHeight);

                column.Item()
                    .Height(450)
                    .Image(Model["OrbitView"], ImageScaling.FitHeight);
            });
        }
    }
}
