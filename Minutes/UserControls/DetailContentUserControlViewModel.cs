using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Minutes.UserControls
{
    public class DetailContentUserControlViewModel : BindableBase
    {
        #region Notifiable Property List

        private SolidColorBrush _ToolColorBrush;
        public SolidColorBrush m_ToolColorBrush
        {
            get { return this._ToolColorBrush; }
            set { this.SetProperty(ref this._ToolColorBrush, value); }
        }

        #endregion

        public DetailContentUserControlViewModel()
        {
            _ToolColorBrush = new SolidColorBrush(Color.FromArgb(255,128,115,115));
        }
    }
}
