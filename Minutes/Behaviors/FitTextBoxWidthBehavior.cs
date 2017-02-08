using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Minutes.Behaviors
{
    class FitTextBoxWidthBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.TextChanged += FitTextBoxWidth;
        }

        private void FitTextBoxWidth(object sender, TextChangedEventArgs e)
        {
            this.AssociatedObject.Width = MeasureStringActualSize(this.AssociatedObject.Text, this.AssociatedObject).Width;
        }

        // 要素にデタッチされたときの処理。大体イベントハンドラの登録解除をここでやる
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.TextChanged -= FitTextBoxWidth;
        }

        private Size MeasureStringActualSize(string inCandidate,TextBox inTextBox)
        {
            var formattedText = new FormattedText(
                inCandidate,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(inTextBox.FontFamily, inTextBox.FontStyle, inTextBox.FontWeight, inTextBox.FontStretch),
                inTextBox.FontSize,
                Brushes.Black);

            return new Size(formattedText.Width+10, formattedText.Height);
        }
    }
}
