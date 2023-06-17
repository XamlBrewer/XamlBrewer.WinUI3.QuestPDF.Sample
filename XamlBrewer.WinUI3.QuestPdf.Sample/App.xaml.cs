using Microsoft.UI.Xaml;
using q = QuestPDF;
using QuestPDF.Infrastructure;
using XamlBrewer.WinUI3.Services;

namespace XamlBrewer.WinUI3.QuestPDF.Sample
{
    public partial class App : Application
    {
        private Shell shell;

        public App()
        {
            InitializeComponent();
        }

        internal INavigation Navigation => shell;

        internal UIElement AppRoot => shell.AppRoot;

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            q.Settings.License = LicenseType.Community;

            shell = new Shell();
            shell.Activate();
        }
    }
}
