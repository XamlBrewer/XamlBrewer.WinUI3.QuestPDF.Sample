using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using XamlBrewer.WinUI3.QuestPDF.Sample.Services.DocumentGeneration;

namespace XamlBrewer.WinUI3.QuestPDF.Sample.Views
{
    public sealed partial class OxyPlotPage : Page
    {
        private PlotModel areaSeriesModel;
        private PlotModel functionSeriesModel;
        private PlotModel lineSeriesModel;
        private PlotModel pieSeriesModel;

        public PlotModel AreaSeriesModel => areaSeriesModel;

        public PlotModel FunctionSeriesModel => functionSeriesModel;

        public PlotModel LineSeriesModel => lineSeriesModel;

        public PlotModel PieSeriesModel => pieSeriesModel;

        public OxyPlotPage()
        {
            InitializeComponent();

            InitializeAreaSeriesModel();
            InitializeFunctionSeriesModel();
            InitializeLineSeriesModel();
            InitializePieSeriesModel();
        }

        private static DataPointSeries CreateNormalDistributionSeries(double x0, double x1, double mean, double variance, int n = 1001)
        {
            var ls = new LineSeries
            {
                Title = string.Format("μ={0}, σ²={1}", mean, variance)
            };

            for (int i = 0; i < n; i++)
            {
                double x = x0 + ((x1 - x0) * i / (n - 1));
                double f = 1.0 / Math.Sqrt(2 * Math.PI * variance) * Math.Exp(-(x - mean) * (x - mean) / 2 / variance);
                ls.Points.Add(new DataPoint(x, f));
            }

            return ls;
        }

        private void InitializeAreaSeriesModel()
        {
            areaSeriesModel = new PlotModel(); // { Title = "AreaSeries with different stroke colors" };
            areaSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;
            var areaSeries1 = new AreaSeries();
            areaSeries1.Points.Add(new DataPoint(0, 50));
            areaSeries1.Points.Add(new DataPoint(10, 40));
            areaSeries1.Points.Add(new DataPoint(20, 60));
            areaSeries1.Points2.Add(new DataPoint(0, 60));
            areaSeries1.Points2.Add(new DataPoint(5, 80));
            areaSeries1.Points2.Add(new DataPoint(20, 70));
            areaSeries1.Color = OxyColors.Red;
            areaSeries1.Color2 = OxyColors.Blue;
            areaSeriesModel.Series.Add(areaSeries1);
        }

        private void InitializeFunctionSeriesModel()
        {
            functionSeriesModel = new PlotModel(); // { Title = "Nana nana nana nana nana nana nana nana" };

            functionSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

            Func<double, double> batFn1 = (x) => 2 * Math.Sqrt(-Math.Abs(Math.Abs(x) - 1) * Math.Abs(3 - Math.Abs(x)) / ((Math.Abs(x) - 1) * (3 - Math.Abs(x)))) * (1 + Math.Abs(Math.Abs(x) - 3) / (Math.Abs(x) - 3)) * Math.Sqrt(1 - Math.Pow((x / 7), 2)) + (5 + 0.97 * (Math.Abs(x - 0.5) + Math.Abs(x + 0.5)) - 3 * (Math.Abs(x - 0.75) + Math.Abs(x + 0.75))) * (1 + Math.Abs(1 - Math.Abs(x)) / (1 - Math.Abs(x)));
            Func<double, double> batFn2 = (x) => -3 * Math.Sqrt(1 - Math.Pow((x / 7), 2)) * Math.Sqrt(Math.Abs(Math.Abs(x) - 4) / (Math.Abs(x) - 4));
            Func<double, double> batFn3 = (x) => Math.Abs(x / 2) - 0.0913722 * (Math.Pow(x, 2)) - 3 + Math.Sqrt(1 - Math.Pow((Math.Abs(Math.Abs(x) - 2) - 1), 2));
            Func<double, double> batFn4 = (x) => (2.71052 + (1.5 - .5 * Math.Abs(x)) - 1.35526 * Math.Sqrt(4 - Math.Pow((Math.Abs(x) - 1), 2))) * Math.Sqrt(Math.Abs(Math.Abs(x) - 1) / (Math.Abs(x) - 1)) + 0.9;

            functionSeriesModel.Series.Add(new FunctionSeries(batFn1, -8, 8, 0.0001));
            functionSeriesModel.Series.Add(new FunctionSeries(batFn2, -8, 8, 0.0001));
            functionSeriesModel.Series.Add(new FunctionSeries(batFn3, -8, 8, 0.0001));
            functionSeriesModel.Series.Add(new FunctionSeries(batFn4, -8, 8, 0.0001));

            functionSeriesModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MaximumPadding = 0.1, MinimumPadding = 0.1 });
            functionSeriesModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MaximumPadding = 0.1, MinimumPadding = 0.1 });
        }

        private void InitializeLineSeriesModel()
        {

            // http://en.wikipedia.org/wiki/Normal_distribution

            lineSeriesModel = new PlotModel
            {
                //Title = "Normal distribution",
                //Subtitle = "Probability density function"
            };

            lineSeriesModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -0.05,
                Maximum = 1.05,
                MajorStep = 0.2,
                MinorStep = 0.05,
                TickStyle = TickStyle.Inside
            });
            lineSeriesModel.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = -5.25,
                Maximum = 5.25,
                MajorStep = 1,
                MinorStep = 0.25,
                TickStyle = TickStyle.Inside
            });
            lineSeriesModel.Series.Add(CreateNormalDistributionSeries(-5, 5, 0, 0.2));
            lineSeriesModel.Series.Add(CreateNormalDistributionSeries(-5, 5, 0, 1));
            lineSeriesModel.Series.Add(CreateNormalDistributionSeries(-5, 5, 0, 5));
            lineSeriesModel.Series.Add(CreateNormalDistributionSeries(-5, 5, -2, 0.5));

            lineSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;
        }

        private void InitializePieSeriesModel()
        {
            pieSeriesModel = new PlotModel(); // { Title = "Pie Sample1" };

            pieSeriesModel.PlotAreaBorderColor = OxyColors.Transparent;

            dynamic seriesP1 = new PieSeries { StrokeThickness = 2.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0 };

            seriesP1.Slices.Add(new PieSlice("Africa", 1030) { IsExploded = false, Fill = OxyColors.PaleVioletRed });
            seriesP1.Slices.Add(new PieSlice("Americas", 929) { IsExploded = true });
            seriesP1.Slices.Add(new PieSlice("Asia", 4157) { IsExploded = true });
            seriesP1.Slices.Add(new PieSlice("Europe", 739) { IsExploded = true });
            seriesP1.Slices.Add(new PieSlice("Oceania", 35) { IsExploded = true });

            pieSeriesModel.Series.Add(seriesP1);
        }

        private void ChartButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = "C:\\Temp\\oxyplot.pdf";

            var document = new OxyPlotDocument(
                new List<PlotModel> {
                    LineSeriesModel,
                    AreaSeriesModel,
                    PieSeriesModel,
                    FunctionSeriesModel
                });

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
