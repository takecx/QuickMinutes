using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Minutes.Model
{
    public class Participant
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public Participant(string inName,int inIndex) { Name = inName;Index = inIndex; }
    }
    public class Writer
    {
        public string Name { get; set; }
        public int Index { get; set; }
        public Writer(string inName,int inIndex) { Name = inName;Index = inIndex; }
    }

    public class MinutesModel : BindableBase
    {
        const string NEWLINE = "\n";

        #region Notifiable Property List
        private string _Title;
        public string m_Title
        {
            get { return this._Title; }
            set { this.SetProperty(ref this._Title, value); }
        }

        private DateTime _Day = DateTime.Today;
        public DateTime m_Day
        {
            get { return this._Day; }
            set { this.SetProperty(ref this._Day, value); }
        }

        private DateTime _StartTime = DateTime.Now;
        public DateTime m_StartTime
        {
            get { return this._StartTime; }
            set { this.SetProperty(ref this._StartTime, value); }
        }
        private DateTime _EndTime;
        public DateTime m_EndTime
        {
            get { return this._EndTime; }
            set { this.SetProperty(ref this._EndTime, value); }
        }

        private string _Room;
        public string m_Room
        {
            get { return this._Room; }
            set { this.SetProperty(ref this._Room, value); }
        }

        private ObservableCollection<Participant> _Participants = new ObservableCollection<Participant>();
        public ObservableCollection<Participant> m_Participants
        {
            get { return this._Participants; }
            set { this.SetProperty(ref this._Participants, value); }
        }

        private ObservableCollection<Writer> _Writers = new ObservableCollection<Writer>();
        public ObservableCollection<Writer> m_Writers
        {
            get { return this._Writers; }
            set { this.SetProperty(ref this._Writers, value); }
        }

        private ObservableCollection<AgendaItem> _Agendas = new ObservableCollection<AgendaItem>();
        public ObservableCollection<AgendaItem> m_Agendas
        {
            get { return this._Agendas; }
            set { this.SetProperty(ref this._Agendas, value); }
        }

        private string _ExportTextFileName;
        public string m_ExportTextFileName
        {
            get { return this._ExportTextFileName; }
            set { this.SetProperty(ref this._ExportTextFileName, value); }
        }
        #endregion

        public MinutesModel()
        {
            #region for Function Test
            //m_Title = "test title";
            //m_Day = System.DateTime.Today;
            //m_StartTime = DateTime.Now;
            ////m_StartTime = new DateTime(2000, 1, 2, 3, 4, 5);
            //m_EndTime = new DateTime(2001, 6, 7, 8, 9, 10);
            //m_Room = "room1";
            //m_Participants = new ObservableCollection<string> { "person1", "person2", "person3" };
            //m_Writers = new ObservableCollection<string> { "person1", "person3" };
            //m_Agendas = new System.Collections.ObjectModel.ObservableCollection<AgendaItem>();
            //AgendaItem agenda1 = new AgendaItem(1);
            //agenda1.m_AgendaIndex = 1;
            //agenda1.m_Content = "agenda1 content";
            //agenda1.m_DetailItems = new System.Collections.ObjectModel.ObservableCollection<DetailItem>();
            //DetailItem detail1 = new DetailItem();
            //detail1.m_ContentIndex = 1;
            //detail1.m_ContentIndentLevel = 1;
            //detail1.m_Content = "agenda1->detail1 content";
            //detail1.m_ContentStateType = ContentStateType.decided;
            //agenda1.m_DetailItems.Add(detail1);
            //DetailItem detail2 = new DetailItem();
            //detail2.m_ContentIndex = 2;
            //detail2.m_ContentIndentLevel = 2;
            //detail2.m_Content = "agenda1->detail2 content";
            //detail2.m_ContentStateType = ContentStateType.important;
            //agenda1.m_DetailItems.Add(detail2);
            //DetailItem detail3 = new DetailItem();
            //detail3.m_ContentIndex = 3;
            //detail3.m_ContentIndentLevel = 3;
            //detail3.m_Content = "agenda1->detail3 content";
            //detail3.m_ContentStateType = ContentStateType.issue;
            //agenda1.m_DetailItems.Add(detail3);
            //DetailItem detail4 = new DetailItem();
            //detail4.m_ContentIndex = 4;
            //detail4.m_ContentIndentLevel = 3;
            //detail4.m_Content = "agenda1->detail4 content";
            //detail4.m_ContentStateType = ContentStateType.none;
            //agenda1.m_DetailItems.Add(detail4);
            //m_Agendas.Add(agenda1);

            m_ExportTextFileName = @"C:\Users\takeshi\Desktop\exported.txt";
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveContents(string inSaveFilePath)
        {
            StreamWriter sWriter = new StreamWriter(inSaveFilePath);
            using (sWriter)
            {
                try
                {
                    sWriter.Write(GenerateTextFileContents());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private string GenerateTextFileContents()
        {
            //Header + MainContents
            return GenerateExportHeaderContents() + GenerateExportMainContents();
        }

        private string GenerateExportHeaderContents()
        {
            string result = GenerateContentSeparator();
            result += GenerateExportTitleContent();
            result += GenerateExportDayContent();
            result += GenerateExportTimeContent();
            result += GenerateExportRoomContent();
            result += GenerateExportParticipantsContent();
            result += GenerateExportWritersContent();
            result += GenerateContentSeparator();
            return result;
        }

        private string GenerateExportDayContent()
        {
            return "[Day]" + m_Day.ToShortDateString() + NEWLINE;
        }

        private string GenerateExportMainContents()
        {
            string result = "";
            foreach (AgendaItem agenda in m_Agendas)
            {
                result += GenerateExportAgendaContent(agenda);
            }
            return result;
        }

        private string GenerateExportAgendaContent(AgendaItem agenda)
        {
            string result = "(" + agenda.m_AgendaIndex.ToString() + ") " + agenda.m_Content + NEWLINE;
            result += GenerateExportDetailContents(agenda);
            return result;
        }

        private string GenerateExportDetailContents(AgendaItem agenda)
        {
            if (agenda.m_DetailItems == null) return "";
            string result = "";
            foreach (DetailItem detail in agenda.m_DetailItems)
            {
                result += GenerateExportDetailContent(detail);
            }
            return result;
        }

        private string GenerateExportDetailContent(DetailItem inDetail)
        {
            string result = "";
            for (int i = 0; i < inDetail.m_ContentIndentLevel; i++) { result += "\t"; }
            if (inDetail.m_ContentStateType == ContentStateType.none)
            {
                result += "・";
            }
            else
            {
                result += "[" + inDetail.m_ContentStateType.ToString() + "]";
            }
            result += inDetail.m_Content + NEWLINE;
            return result;
        }

        private string GenerateContentSeparator()
        {
            return "*********************************************************\n";
        }

        private string GenerateExportWritersContent()
        {
            string result = "[Writers]";
            if (_Writers.Count == 0) return result + NEWLINE;
            foreach (var writer in m_Writers)
            {
                result += writer.Name + ",";
            }
            result = result.Substring(0, result.Length - 1) + NEWLINE;
            return result;
        }

        private string GenerateExportParticipantsContent()
        {
            string result = "[Participants]";
            if (_Participants.Count == 0) return result + NEWLINE;
            foreach (var participant in m_Participants)
            {
                result += participant.Name + ",";
            }
            result = result.Substring(0, result.Length - 1) + NEWLINE;
            return result;
        }

        private string GenerateExportRoomContent()
        {
            return  "[Room]" + m_Room + NEWLINE;
        }

        private string GenerateExportTimeContent()
        {
            return "[Time]" + m_StartTime.ToString("hh:mm:ss") + " ~ " + m_EndTime.ToString("hh:mm:ss") + NEWLINE;
        }

        private string GenerateExportTitleContent()
        {
            return "[Title]" + m_Title + NEWLINE;
        }
    }
}
