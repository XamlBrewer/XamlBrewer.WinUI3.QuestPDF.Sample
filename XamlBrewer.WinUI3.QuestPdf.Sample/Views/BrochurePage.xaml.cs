using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using XamlBrewer.WinUI3.QuestPDF.Sample.Models;

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
            // coming soon ...
        }
    }
}
