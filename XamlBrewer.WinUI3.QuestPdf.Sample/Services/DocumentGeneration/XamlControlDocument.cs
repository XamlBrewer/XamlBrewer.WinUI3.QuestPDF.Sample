using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration
{
    internal class XamlControlDocument : IDocument
    {
        public List<byte[]> Model { get; }

        public XamlControlDocument(List<byte[]> model)
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
                foreach (var xamlControl in Model)
                {
                    column.Item()
                        .Height(100)
                        .Image(xamlControl, ImageScaling.FitHeight);
                }
            });
        }
    }
}
