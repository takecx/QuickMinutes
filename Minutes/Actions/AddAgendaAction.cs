﻿using FontAwesome.WPF;
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
    class AddAgendaAction : TargetedTriggerAction<ListView>
    {
        protected override void Invoke(object parameter)
        {
            (this.Target.DataContext as MainWindowViewModel).AddNewAgendaItem();
        }
    }

}
