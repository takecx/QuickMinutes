using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interactivity;
using System.Windows.Input;
using Minutes.Model;
using System.Windows;

namespace Minutes.Behaviors
{
    class DeleteParticipantAction : Behavior<ImageAwesome>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseDown += DeleteParticipant;
        }

        // 要素にデタッチされたときの処理。大体イベントハンドラの登録解除をここでやる
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseDown -= DeleteParticipant;
        }

        private void DeleteParticipant(object sender, MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow.DataContext as MainWindowViewModel).DeleteParticipantModel((this.AssociatedObject.DataContext as Participant).Index);
        }
    }
}
