using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minutes
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ListBoxItem item = new ListBoxItem();
            item.Background = new SolidColorBrush(Colors.AliceBlue);
            Messenger.Instance.GetEvent<PubSubEvent<bool>>().Subscribe(
            d =>
            {
                if (d == true)
                {
                        //Invokeを使ってUIスレッドから実行する必要がある
                        saveIcon.Dispatcher.BeginInvoke(
                        new Action(() => { (saveIcon.Resources["HilightSaveIconAnimation"] as Storyboard).Begin(); })
                        );
                }
            }
            );
        }


        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            if (thumb == null) return;

            //親コントロールを探す
            UIElement parent = (UIElement)thumb.Parent;
            if (parent == null) return;

            double x = Canvas.GetLeft(parent);
            if (double.IsNaN(x)) x = 0;
            double y = Canvas.GetTop(parent);
            if (double.IsNaN(y)) y = 0;

            //ドラッグ量に応じてThumbコントロールを移動する
            Canvas.SetLeft(parent, x + e.HorizontalChange);
            Canvas.SetTop(parent, y + e.VerticalChange);
        }
    }
}
