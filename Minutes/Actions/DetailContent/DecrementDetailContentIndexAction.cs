using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Minutes.Actions.DetailContent
{
    [TypeConstraint(typeof(ImageAwesome))]
    class DecrementDetailContentIndexAction : TargetedTriggerAction<Grid>
    {
        protected override void Invoke(object parameter)
        {
            if ((this.Target.DataContext as DetailItem).m_ContentIndentLevel > 0)
            {
                (this.Target.DataContext as DetailItem).m_ContentIndentLevel--;
            }
            this.Target.Margin = new System.Windows.Thickness(30 * (this.Target.DataContext as DetailItem).m_ContentIndentLevel, 0, 0, 0);
        }
    }
}
