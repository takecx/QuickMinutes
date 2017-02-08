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
        private ObservableCollection<DetailItem> _DetailItems;
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
            m_DetailItems = new ObservableCollection<DetailItem>();
            m_AgendaIndex = inAgendaIndex;
        }
        internal void AddDetailItemModel()
        {
            m_DetailItems.Insert(m_DetailItems.Count, new DetailItem(m_DetailItems.Count));
        }
        internal object GetLastDetailItem()
        {
            return m_DetailItems[m_DetailItems.Count - 1];
        }

    }
}
