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
    // AssociatedObjectの型は、TypeConstraintAttributeで指定
    // 互換性のない型で使おうとすると、実行時のxamlのパース中にInalidOperationExceptionが発生した
    // Targetの型は、TargetedTriggerActionの型パラメーターで指定
    // 互換性のない型をTargetに指定すると、Invokeが呼ばれる時にInvalidOperationExceptionが発生した
    [TypeConstraint(typeof(TextBox))]
    public class TextBoxCalenderAction : TargetedTriggerAction<Grid>
    {

        // Actionが実行されたときの処理
        protected override void Invoke(object o)
        {
            // TargetNameが未指定の場合は、TargetプロパティはAssociatedObjectを返す
            // TargetNameに存在しない要素名を渡すと、Targetプロパティはnullを返した(ドキュメントと異なる)
            if (this.Target == null ||
                this.Target == this.AssociatedObject)
            {
                return;
            }

            // AssociatedObjectはDependencyObject型なので、必要ならキャスト
            var source = (TextBox)this.AssociatedObject;
            // Targetは型パラメーターで指定した型Tなので、キャストは不要
            foreach(var control in this.Target.Children)
            {
                if(control.GetType() == typeof(Calendar))
                {
                    (control as Calendar).Visibility = System.Windows.Visibility.Visible;
                    (control as Calendar).Focus();
                }
            }
        }

    }


    [TypeConstraint(typeof(Calendar))]
    //[TypeConstraint(typeof(TextBox))]
    public class TextBoxCalenderBanishAction : TargetedTriggerAction<Grid>
    {

        // Actionが実行されたときの処理
        protected override void Invoke(object o)
        {
            // TargetNameが未指定の場合は、TargetプロパティはAssociatedObjectを返す
            // TargetNameに存在しない要素名を渡すと、Targetプロパティはnullを返した(ドキュメントと異なる)
            if (this.Target == null ||
                this.Target == this.AssociatedObject)
            {
                return;
            }

            // AssociatedObjectはDependencyObject型なので、必要ならキャスト
            var source = (Calendar)this.AssociatedObject;
            //LINQを使って書き換えたい
            TextBox dateTextBox = new TextBox();
            //TextBox dayTextBox = from TextBox t in this.Target.Children.OfType<UIElement>()
            //                     select t;
            foreach(var control in this.Target.Children)
            {
                if((control as FrameworkElement).Name == "DateTextBox")
                {
                    dateTextBox = control as TextBox;
                }
            }
            // Targetは型パラメーターで指定した型Tなので、キャストは不要
            foreach(var control in this.Target.Children)
            {
                if((control as FrameworkElement).Name == "calender")
                {
                    dateTextBox.Text = (control as Calendar).SelectedDate?.ToShortDateString();
                    (control as Calendar).Visibility = Visibility.Hidden;
                }
            }
        }

    }

}
