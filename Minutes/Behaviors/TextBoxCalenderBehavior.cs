using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Minutes.Behaviors
{
    class TextBoxCalenderBehavior : Behavior<TextBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.GotFocus += ShowCalender;
        }

        private void ShowCalender(object sender, RoutedEventArgs e)
        {
            Calendar calender = new Calendar();
            calender.Visibility = System.Windows.Visibility.Visible;
        }

        // 要素にデタッチされたときの処理。大体イベントハンドラの登録解除をここでやる
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.GotFocus -= ShowCalender;
        }
    }
}
