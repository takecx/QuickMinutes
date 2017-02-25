using FontAwesome.WPF;
using Minutes.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace Minutes.Actions
{
    [TypeConstraint(typeof(ImageAwesome))]
    class AddDetailAction : TargetedTriggerAction<StackPanel>
    {
        protected override void Invoke(object parameter)
        {
            (this.Target.DataContext as AgendaItem).AddNewDetailItem();
            //(Application.Current.MainWindow.DataContext as MainWindowViewModel).AddNewDetailItem(newDetailItem, (this.Target.DataContext as AgendaItem).m_AgendaIndex);
        }
    }
}
