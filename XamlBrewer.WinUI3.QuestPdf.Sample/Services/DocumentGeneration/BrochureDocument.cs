using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using XamlBrewer.WinUI3.QuestPDF.Sample.Models;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration
{
    internal class BrochureDocument : IDocument
    {
        public List<Moon> Model { get; }

        public BrochureDocument(List<Moon> model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);

                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeFooter(IContainer footer)
        {
            footer
                .DefaultTextStyle(textStyle => textStyle.FontFamily(Fonts.CourierNew))
                .AlignCenter().Text(text =>
                {
                    text.DefaultTextStyle(x => x.FontSize(16));
                    text.Span("page ");
                    text.CurrentPageNumber();
                    text.Span(" of ");
                    text.TotalPages();
                });
        }
    }
}
