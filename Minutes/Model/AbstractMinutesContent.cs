using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minutes
{
    public class AbstractMinutesContent : BindableBase
    {
        #region Notifiable Property List
        /// <summary>
        /// main content
        /// </summary>
        private string _Content;
        public string m_Content
        {
            get { return this._Content; }
            set { this.SetProperty(ref this._Content, value); }
        }
        #endregion
    }
}
