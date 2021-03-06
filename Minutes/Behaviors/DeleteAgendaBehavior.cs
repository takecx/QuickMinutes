﻿using FontAwesome.WPF;
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
    class DeleteAgendaBehavior : Behavior<ImageAwesome>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseDown += DeleteAgendaItem;
        }

        // 要素にデタッチされたときの処理。大体イベントハンドラの登録解除をここでやる
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseDown -= DeleteAgendaItem;
        }

        private void DeleteAgendaItem(object sender, MouseButtonEventArgs e)
        {
            var viewModel = (Application.Current.MainWindow.DataContext as MainWindowViewModel);
            //Modelを更新
            viewModel.DeleteAgendaModel((this.AssociatedObject.DataContext as AgendaItem).m_AgendaIndex);
        }
    }
}
