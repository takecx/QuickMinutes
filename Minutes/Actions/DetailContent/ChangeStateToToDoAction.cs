using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Minutes.Actions.DetailContent
{
    [TypeConstraint(typeof(StackPanel))]
    class ChangeStateToToDoAction : TargetedTriggerAction<ImageAwesome>
    {
        protected override void Invoke(object parameter)
        {
            if ((this.Target.DataContext as DetailItem).m_ContentStateType == ContentStateType.issue)
            {
                //Change Bullet
                this.Target.Icon = FontAwesomeIcon.Circle;
                this.Target.Width = 7;
                this.Target.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)); //Black

                //Change model
                (this.Target.DataContext as DetailItem).m_ContentStateType = ContentStateType.none;
            }
            else
            {
                //Change Bullet
                this.Target.Icon = FontAwesomeIcon.Edit;
                this.Target.Width = 15;
                this.Target.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));

                //Change model
                (this.Target.DataContext as DetailItem).m_ContentStateType = ContentStateType.issue;
            }
        }
    }
}
