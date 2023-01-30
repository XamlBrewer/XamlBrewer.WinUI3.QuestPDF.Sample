using Microcharts;
using Microsoft.UI.Xaml;
using SkiaSharp.Views.Windows;

namespace XamlBrewer.WinUI.Controls
{
    /// <summary>
    /// A SkiaSharp WinUI Canvas that draws a Microcharts Chart.
    /// </summary>
    public class ChartCanvas : SKXamlCanvas
    {
        public static readonly DependencyProperty ChartProperty =
            DependencyProperty.Register(nameof(Chart), typeof(Chart), typeof(ChartCanvas), new PropertyMetadata(null));

        public Chart Chart
        {
            get { return (Chart)GetValue(ChartProperty); }
            set
            {
                SetValue(ChartProperty, value);
                Invalidate();
            }
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            if (Chart == null)
            {
                e.Surface.Canvas.Clear();
            }
            else
            {
                Chart.DrawContent(e.Surface.Canvas, e.Info.Width, e.Info.Height);
            }

            base.OnPaintSurface(e);
        }
    }
}
