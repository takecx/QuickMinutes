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
        private bool _IsInstantSaveExecuting;
        public bool m_IsInstantSaveExecuting
        {
            get { return this._IsInstantSaveExecuting; }
            set { this.SetProperty(ref this._IsInstantSaveExecuting, value); }
        }
        #endregion

        private string m_InstantSaveMinutesName = "";
        private const string K_TITLE = "%Title%";
        private const string K_DAY = "%Day%";
        private const string K_STARTTIME = "%StartTime%";
        private const string K_ENDTIME = "%EndTime%";
        private const string K_ROOM = "%Room%";

        public MinutesModel()
        {
            //Create InstantSave Directory if not exist
            Directory.CreateDirectory(Properties.Settings.Default.InstantSaveDir);
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

        public bool InstantSaveContents()
        {
            try{
                _IsInstantSaveExecuting = true;
                //Delete minutes if filename change
                string previousMinutesName = m_InstantSaveMinutesName;
                m_InstantSaveMinutesName = Properties.Settings.Default.InstantSaveDir + ConvertToRealMinutesName(Properties.Settings.Default.InstantSaveMinutesName) + ".txt";
                if (previousMinutesName != m_InstantSaveMinutesName && previousMinutesName != "")
                {
                    File.Delete(previousMinutesName);
                    return false;
                }
                //Save new minutes
                SaveContents(m_InstantSaveMinutesName);
                return m_IsInstantSaveExecuting;
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, e.GetType().ToString() + " occured.");
                return false;
            }
            finally
            {
            }
        }

        private string ConvertToRealMinutesName(string instantSaveMinutesName)
        {
            string result = instantSaveMinutesName.Replace(K_TITLE, m_Title);
            result = result.Replace(K_DAY, m_Day.ToString("yyyyMMdd"));
            result = result.Replace(K_STARTTIME, m_StartTime.ToString("HH:mm"));
            result = result.Replace(K_ENDTIME, m_EndTime.ToString("HH:mm"));
            result = result.Replace(K_ROOM, m_Room);
            return result;
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
            return "[Time]" + m_StartTime.ToString("HH:mm") + " ~ " + m_EndTime.ToString("HH:mm") + NEWLINE;
        }

        private string GenerateExportTitleContent()
        {
            return "[Title]" + m_Title + NEWLINE;
        }
    }
}
