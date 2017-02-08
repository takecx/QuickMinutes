using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minutes
{
    public enum ContentStateType
    {
        none,       //無し（デフォルト）
        decided,    //決定済み
        issue,      //課題
        important,  //重要
    }

    public class DetailItem : AbstractMinutesContent
    {
        #region Notifiable Property List
        private ContentStateType _ContentStateType;
        public ContentStateType m_ContentStateType
        {
            get { return this._ContentStateType; }
            set { this.SetProperty(ref this._ContentStateType, value); }
        }

        private int _ContentIndex;
        public int m_ContentIndex
        {
            get { return this._ContentIndex; }
            set { this.SetProperty(ref this._ContentIndex, value); }
        }

        private int _ContentIndentLevel;
        public int m_ContentIndentLevel
        {
            get { return this._ContentIndentLevel; }
            set { this.SetProperty(ref this._ContentIndentLevel, value); }
        }
        #endregion
        public DetailItem()
        {
            m_ContentStateType = ContentStateType.none; //Set DefaultValue
        }
        public DetailItem(int inIndex)
        {
            m_ContentIndex = inIndex;
        }
    }
}
