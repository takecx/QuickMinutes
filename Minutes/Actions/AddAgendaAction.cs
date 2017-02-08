using FontAwesome.WPF;
using Minutes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Minutes.Actions
{
    [TypeConstraint(typeof(ImageAwesome))]
    class AddAgendaAction : TargetedTriggerAction<StackPanel>
    {
        protected override void Invoke(object parameter)
        {
            (this.Target.DataContext as MainWindowViewModel).AddNewAgendaItemModel();

            //コントロール追加
            this.Target.Children.Insert(this.Target.Children.Count - 1, 
                new UserControls.MainContentUserControl
                {
                    Margin = new System.Windows.Thickness(10, 10, 10, 0),
                    DataContext = (this.Target.DataContext as MainWindowViewModel).GetLastAgendaItem(),
                    //Style = Application.Current.FindResource("HilightTextBox") as Style,
                });
        }
    }
}
