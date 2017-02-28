using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interactivity;
using System.Windows.Input;
using System.Windows;

namespace Minutes.Behaviors
{
    class DeleteDetailBehavior : Behavior<ImageAwesome>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseDown += DeleteDetailItem;
        }

        // 要素にデタッチされたときの処理。大体イベントハンドラの登録解除をここでやる
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseDown -= DeleteDetailItem;
        }

        private void DeleteDetailItem(object sender, MouseButtonEventArgs e)
        {
            int parentIndex = (this.AssociatedObject.DataContext as DetailItem).m_ParentAgendaIndex;
            var parentAgendas = (Application.Current.MainWindow.DataContext as MainWindowViewModel).m_Agendas;
            parentAgendas[parentIndex].m_DetailItems.RemoveAt((this.AssociatedObject.DataContext as DetailItem).m_ContentIndex);
            //残っているアジェンダのインデックスを更新
            for (int i = 0; i < parentAgendas[parentIndex].m_DetailItems.Count; i++)
            {
                parentAgendas[parentIndex].m_DetailItems[i].m_ContentIndex = i;
            }
        }
    }
}
