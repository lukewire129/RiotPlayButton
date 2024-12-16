﻿using Delta.WPF;
using System.Drawing;
using System.Windows.Input;
using System.Windows;
using Base = System.Windows.Controls;

namespace RiotPlayButton.Components
{
    public class RiotPlayButtonComponent : Component
    {
        private System.Windows.Media.Brush ArrowFillOver() => LinearGradient ("0.5,0", "0.5,1")
                                                               .AddGradientStop ("#FF1D3B4A", 0)
                                                               .AddGradientStop ("#FF082734", 1);
        private System.Windows.Media.Brush ArrowFill() => new System.Windows.Media.SolidColorBrush (ColorHelper.ToSWMColor (ColorTranslator.FromHtml ("#1E2328")));

        private System.Windows.Media.Brush ArrowStrokeOver() => LinearGradient ("0.5,0", "0.5,1")
                                                                .AddGradientStop ("#FFAFF5FF", 0)
                                                                .AddGradientStop ("#FF46E6FF", 0.5)
                                                                .AddGradientStop ("#FF00ADD4", 1);
        private System.Windows.Media.Brush ArrowStroke() => LinearGradient ("0.5,0", "0.5,1")
                                                                .AddGradientStop ("#CC3FE7FF", 0)
                                                                .AddGradientStop ("#CC006D7D", 0.5)
                                                                .AddGradientStop ("#CC0493A7", 1);

        private string pathString => "M 0,0 L 103,0 L 118,14 L 103,28 L 0,28 C 10,14 0,0 0,0 Z";

        public override IVisual Render()
        {
            var (count, setCount) = UseState (0);
            var (isHover, setIsHover) = UseState (false);
            var (isToggle, setIsToggle) = UseState (true);
            return Grid (
                        Border ()
                            .Background ("#00070E")
                            .Brush ("#34291E")
                            .Margin (left: 10),

                        Img ($"Resources/logo.png")
                            .Start ()
                            .Height (38)
                            .BitmapScalingMode (System.Windows.Media.BitmapScalingMode.Fant),

                        Border ()
                            .Background ("#1E2328")
                            .Brush ("#09343D")
                            .Thickness (2)
                            .Margin (50, 4, 4, 4),

                        isToggle ? ToggleOnPath (isHover) : ToggleOffPath (),

                        Grid (
                            Text ("Play")
                                .FontSize (15)
                                .FontWeight (FontWeights.Bold)
                                .Center ()
                                .FontColor ("#FFFFFF")
                                .Margin (30, isToggle ? 0 : 100)
                                .Transitions ("Margin", 500, Easing.CubicInOut),
                            Text ("Stop")
                                .FontSize (15)
                                .FontWeight (FontWeights.Bold)
                                .Center ()
                                .FontColor ("#3C3C41")
                                .Margin (30, bottom: isToggle ? 100 : 0)
                                .Transitions ("Margin", 500, Easing.CubicInOut)
                        )
                   )
                   .OnHover ((s, e) =>
                   {
                       setIsHover (e.RoutedEvent == UIElement.MouseEnterEvent);
                   })
                   .OnClick ((s, e) =>
                   {
                       setIsToggle (!isToggle);
                   })
                   .Size (165, 38)
                   .Cursor (isHover ? Cursors.Hand : Cursors.Arrow)
                   .Background (Color.Transparent);
        }

        private IVisual ToggleOnPath(bool isHover) => Path (pathString)
                                        .Fill (isHover ? ArrowFillOver () : ArrowFill ())
                                        .Brush (isHover ? ArrowStrokeOver () : ArrowStroke ())
                                        .Thickness (2)
                                        .Margin (40, 5, 4, -5)
                                        .DropShadowEffect (BlurRadius: 5, Depth: 2);

        private IVisual ToggleOffPath() => Path (pathString)
                                        .Fill ("#1E2328")
                                        .Brush ("#5C5B57")
                                        .Thickness (2)
                                        .Margin (40, 5, 4, -5)
                                        .DropShadowEffect (BlurRadius: 5, Depth: 2);
    }
}
