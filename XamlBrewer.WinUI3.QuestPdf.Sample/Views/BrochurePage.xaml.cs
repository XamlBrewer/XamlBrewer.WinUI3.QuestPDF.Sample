using Microsoft.UI.Xaml.Controls;
using QuestPDF.Fluent;
using System.Collections.Generic;
using System.Diagnostics;
using XamlBrewer.WinUI3.QuestPDF.Sample.Models;
using XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Views
{
    public sealed partial class BrochurePage : Page
    {
        public BrochurePage()
        {
            InitializeComponent();
        }

        public List<Moon> Moons
        {
            get
            {
                return Moon.Moons;
            }
        }

        private void BrochureButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var filePath = "C:\\Temp\\brochure.pdf";

            var document = new BrochureDocument(Moons);

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
