using QuestPDF.Drawing;
using QuestPDF.Elements;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeBody);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer header)
        {
            header
                .PaddingBottom(20)
                .Column(column =>
                {
                    column.Item().ShowOnce()
                        .Background(Colors.BlueGrey.Darken4)
                        .PaddingVertical(20)
                        .Image(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Moons", "Banner.png"));
                    column.Item()
                        .Background(Colors.BlueGrey.Darken4)
                        .AlignCenter()
                        .Padding(10)
                        .Text(content =>
                        {
                            content
                                .Span("Fantastic Moons and Where to Find Them")
                                .Style(TextStyle.Default
                                    .FontSize(20)
                                    .FontColor(Colors.White)
                                    .FontFamily(Fonts.ComicSans)
                                    );
                        });
                    column.Item().ShowOnce()
                        .Background(Colors.BlueGrey.Darken4)
                        .AlignCenter()
                        .PaddingTop(0)
                        .PaddingBottom(20)
                        .Text(content =>
                        {
                            content
                                .Span("A QuestPDF Sample by XAML Brewer")
                                .Style(TextStyle.Default
                                    .FontSize(14)
                                    .FontColor(Colors.BlueGrey.Lighten4)
                                    .FontFamily(Fonts.ComicSans)
                                    );
                        });
                });
        }

        private void ComposeBody(IContainer body)
        {
            body.Column(column =>
                {
                    foreach (var moon in Model)
                    {
                        column.Item().Component(new MoonComponent(moon));
                        if (moon != Model.Last())
                        {
                            column.Item().PageBreak();
                        }
                    }
                });
        }

        private void ComposeFooter(IContainer footer)
        {
            footer.Dynamic(new AlternatingFooter());
        }
    }

    internal class MoonComponent : IComponent
    {
        private readonly Moon moon;

        public MoonComponent(Moon model)
        {
            moon = model;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().PaddingBottom(10).Text(content =>
                {
                    content
                        .Span(moon.Name)
                        .Style(TextStyle.Default
                            .FontSize(20)
                            );
                });
                column.Item().PaddingBottom(10).Row(row =>
                {
                    row.RelativeItem().Image(moon.ImagePath);
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .AlignRight()
                                .Text("Planet: ")
                                .SemiBold();
                            row.RelativeItem()
                                .AlignLeft()
                                .Text(moon.Planet);
                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeItem()
                                .AlignRight()
                                .Text("Mass: ")
                                .SemiBold();
                            row.RelativeItem()
                                .AlignLeft()
                                .Text(moon.Mass.ToString());
                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Row(row =>
                            {
                                row.RelativeItem()
                                    .AlignRight()
                                    .Text("Albedo: ")
                                    .SemiBold();
                                row.RelativeItem()
                                    .AlignLeft()
                                    .Text(moon.Albedo.ToString());
                            });
                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeItem().Row(row =>
                            {
                                row.RelativeItem()
                                    .AlignRight()
                                    .Text("Orbital Eccentricity: ")
                                    .SemiBold();
                                row.RelativeItem()
                                    .AlignLeft()
                                    .Text(moon.OrbitalEccentricity.ToString());
                            });
                        });
                    });
                });
                column.Item().Text(moon.Description).FontSize(14);
            });
        }
    }

    internal class AlternatingFooter : IDynamicComponent<int>
    {
        public int State { get; set; }

        public DynamicComponentComposeResult Compose(DynamicContext context)
        {
            var content = context.CreateElement(element =>
            {
                element
                    .Element(x => context.PageNumber % 2 == 0 ? x.AlignLeft() : x.AlignRight())
                    .Text(text =>
                    {
                        text.DefaultTextStyle(x => x.FontSize(16));
                        text.Span("page ");
                        text.CurrentPageNumber();
                        text.Span(" of ");
                        text.TotalPages();
                    });
            });

            return new DynamicComponentComposeResult()
            {
                Content = content,
                HasMoreContent = false
            };
        }
    }
}

