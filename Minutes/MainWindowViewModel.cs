using Minutes.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Minutes
{
    public class MainWindowViewModel : BindableBase
    {
        //Model
        //ViewModel -> Modelの通知はviewからの通知を受けたViewModelのプロパティのsetter内でModelのインスタンスに伝える
        private MinutesModel m_MinutesModel = new MinutesModel();

        #region 通知可能プロパティ一覧
        private string _Title;
        public string m_Title
        {
            get { return this._Title; }
            set {
                this.SetProperty(ref this._Title, value);
                m_MinutesModel.m_Title = value;
            }
        }

        private string _Day;

        public string m_Day
        {
            get {
                this._Day = m_MinutesModel.m_Day.ToShortDateString();
                return this._Day;
            }
            set {
                this.SetProperty(ref this._Day, value);
                DateTime dayTemp;
                DateTime.TryParse(value,out dayTemp);
                m_MinutesModel.m_Day = dayTemp;
            }
        }

        private string _StartTime;
        private const string TIME_DISPLAY_MODE = "HH:mm";
        public string m_StartTime
        {
            get {
                this._StartTime = m_MinutesModel.m_StartTime.ToString(TIME_DISPLAY_MODE);
                return this._StartTime;
            }
            set {
                this.SetProperty(ref this._StartTime, value);
                DateTime startTimeTemp;
                DateTime.TryParse(value,out startTimeTemp);
                m_MinutesModel.m_StartTime = startTimeTemp;
            }
        }
        private string _EndTime;
        public string m_EndTime
        {
            get {
                this._EndTime = m_MinutesModel.m_EndTime.ToString(TIME_DISPLAY_MODE);
                return this._EndTime;
            }
            set {
                this.SetProperty(ref this._EndTime, value);
                DateTime endTimeTemp;
                DateTime.TryParse(value,out endTimeTemp);
                m_MinutesModel.m_EndTime = endTimeTemp;
            }
        }

        private string _Room;
        public string m_Room
        {
            get { return this._Room; }
            set {
                this.SetProperty(ref this._Room, value);
                m_MinutesModel.m_Room = value;
            }
        }

        private ObservableCollection<string> _Participants;
        public ObservableCollection<string> m_Participants
        {
            get { return this._Participants; }
            set { this.SetProperty(ref this._Participants, value); }
        }

        private ObservableCollection<string> _Writer;
        public ObservableCollection<string> m_Writer
        {
            get { return this._Writer; }
            set { this.SetProperty(ref this._Writer, value); }
        }

        private ObservableCollection<string> _Agendas;
        public ObservableCollection<string> m_Agendas
        {
            get { return this._Agendas; }
            set { this.SetProperty(ref this._Agendas, value); }
        }
        #endregion

        #region コマンド一覧
        public DelegateCommand SaveMinutesCommand { get; private set; }
        #endregion

        public List<string> m_SelectableTimeList{ get;set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindowViewModel()
        {
            this.SaveMinutesCommand = new DelegateCommand(SaveMinutes, CanSaveMinutes);
            _Participants = new ObservableCollection<string>();
            _Writer = new ObservableCollection<string>();
            m_Agendas = new ObservableCollection<string>();
            m_SelectableTimeList = new List<string> {
            "07:00","07:15","07:30","07:45","08:00","08:15","08:30","08:45",
            "09:00","09:15","09:30","09:45","10:00","10:15","10:30","10:45",
            "11:00","11:15","11:30","11:45","12:00","12:15","12:30","12:45",
            "13:00","13:15","13:30","13:45","14:00","14:15","14:30","14:45",
            "15:00","15:15","15:30","15:45","16:00","16:15","16:30","16:45",
            "16:00","16:15","16:30","16:45","17:00","17:15","17:30","17:45",
            "18:00","18:15","18:30","18:45","19:00","19:15","19:30","19:45",
            "20:00","20:15","20:30","20:45","21:00","21:15","21:30","21:45",
            };
        }

        private void SaveMinutes()
        {
            m_MinutesModel.SaveContents();
        }

        private bool CanSaveMinutes()
        {
            //とりあえず常に実行可能にしとく
            return true;
        }
        internal void AddNewAgendaItemModel()
        {
            m_MinutesModel.m_Agendas.Insert(m_MinutesModel.m_Agendas.Count, new AgendaItem(m_MinutesModel.m_Agendas.Count));
        }
        internal AgendaItem GetLastAgendaItem()
        {
            return m_MinutesModel.m_Agendas[m_MinutesModel.m_Agendas.Count - 1];
        }

    }
}
