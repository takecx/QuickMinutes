using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minutes
{
    public class AgendaItem : AbstractMinutesContent
    {
        #region Notifiable Property List
        private ObservableCollection<DetailItem> _DetailItems = new ObservableCollection<DetailItem>();
        public ObservableCollection<DetailItem> m_DetailItems
        {
            get { return this._DetailItems; }
            set { this.SetProperty(ref this._DetailItems, value); }
        }

        private int _AgendaIndex;
        public int m_AgendaIndex
        {
            get { return this._AgendaIndex; }
            set { this.SetProperty(ref this._AgendaIndex, value); }
        }
        #endregion

        public AgendaItem(int inAgendaIndex)
        {
            m_AgendaIndex = inAgendaIndex;
        }
        internal void AddNewDetailItem()
        {
            //Modelへの追加
            _DetailItems.Add(new DetailItem(m_DetailItems.Count,m_AgendaIndex));
        }
    }
}
