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
            (this.Target.DataContext as AgendaItem).AddDetailItemModel();

            //追加するコントロールの生成
            var detailContent = new DetailContentUserControl() {
                Margin = new Thickness(50, 10, 10, 0),
                DataContext = (this.Target.DataContext as AgendaItem).GetLastDetailItem(),
            };
            this.Target.Children.Insert(this.Target.Children.Count - 1, detailContent);
        }
    }
}
