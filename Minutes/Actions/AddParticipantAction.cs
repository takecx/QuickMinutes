using FontAwesome.WPF;
using Minutes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Minutes.Actions
{
    [TypeConstraint(typeof(ImageAwesome))]
    class AddParticipantAction : TargetedTriggerAction<ListBox>
    {
        protected override void Invoke(object parameter)
        {
            (this.Target.DataContext as MainWindowViewModel).AddNewParticipant((this.Target.DataContext as MainWindowViewModel).m_Participants.Count);
            //(this.Target.DataContext as MainWindowViewModel).AddNewParticipant((this.Target as ListBox).Items.Count);
        }
    }
}
