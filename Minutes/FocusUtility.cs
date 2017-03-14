using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Minutes
{
    public static class FocusUtility
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, false);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused", typeof(bool), typeof(FocusUtility),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if ((bool)e.NewValue && uie.Dispatcher != null)
            {
                uie.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() => uie.Focus())); // invoke behaves nicer, if e.g. you have some additional handler attached to 'GotFocus' of UIE. 
                uie.SetValue(IsFocusedProperty, false); // reset bound value if possible, to allow setting again
            }
        }
    }
}
