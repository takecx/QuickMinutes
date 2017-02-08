using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minutes.Model;
using MinutesTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minutes.Model.Tests
{
    [TestClass()]
    public class MinutesModelTests
    {
        [TestMethod()]
        public void GenerateContentSeparatorTest()
        {
            MinutesModel model = new MinutesModel();
            string expected = "*********************************************************\n";
            Assert.AreEqual(expected, model.AsDynamic().GenerateContentSeparator());
        }

        [TestMethod()]
        public void GenerateExportDayContentTest()
        {
            MinutesModel model = new MinutesModel();
            model.m_Day = new DateTime(2017,2,1);
            string expected = "[Day]2017/02/01\n";
            Assert.AreEqual(expected, model.AsDynamic().GenerateExportDayContent());
        }

        [TestMethod()]
        public void GenerateExportTitleContentTest()
        {
            MinutesModel model = new MinutesModel();
            model.m_Title = "title";
            string expected = "[Title]title\n";
            Assert.AreEqual(expected, model.AsDynamic().GenerateExportTitleContent());
        }
        [TestMethod()]
        public void GenerateExportTimeContentTest()
        {
            MinutesModel model = new MinutesModel();
            model.m_StartTime = new DateTime(2000,1,2,3,4,5);
            model.m_EndTime = new DateTime(2001, 6, 7, 8, 9, 10);
            string expected = "[Time]03:04:05 ~ 08:09:10\n";
            Assert.AreEqual(expected, model.AsDynamic().GenerateExportTimeContent());
        }
        [TestMethod()]
        public void GenerateExportRoomContentTest()
        {
            MinutesModel model = new MinutesModel();
            model.m_Room = "room1";
            string expected = "[Room]room1\n";
            Assert.AreEqual(expected, model.AsDynamic().GenerateExportRoomContent());
        }
        [TestMethod()]
        public void GenerateExportParticipantsContentTest()
        {
            MinutesModel model = new MinutesModel();
            model.m_Participants = new System.Collections.ObjectModel.ObservableCollection<string> { "man1", "man2", "woman1" };
            string expected = "[Participants]man1,man2,woman1\n";
            Assert.AreEqual(expected, model.AsDynamic().GenerateExportParticipantsContent());
        }
        [TestMethod()]
        public void GenerateExportWritersContentTest()
        {
            MinutesModel model = new MinutesModel();
            model.m_Writers = new System.Collections.ObjectModel.ObservableCollection<string> { "man1", "woman1" };
            string expected = "[Writers]man1,woman1\n";
            Assert.AreEqual(expected, model.AsDynamic().GenerateExportWritersContent());
        }
        [TestMethod()]
        public void GenerateExportDetailContentTest()
        {
            MinutesModel model = new MinutesModel();

            string expected1 = "\t[decided]agenda1->detail1 content\n";
            DetailItem detail1 = new DetailItem();
            detail1.m_ContentIndex = 1;
            detail1.m_ContentIndentLevel = 1;
            detail1.m_Content = "agenda1->detail1 content";
            detail1.m_ContentStateType = ContentStateType.decided;
            Assert.AreEqual(expected1, model.AsDynamic().GenerateExportDetailContent(detail1));

            string expected2 = "\t\t[important]agenda1->detail2 content\n";
            DetailItem detail2 = new DetailItem();
            detail2.m_ContentIndex = 2;
            detail2.m_ContentIndentLevel = 2;
            detail2.m_Content = "agenda1->detail2 content";
            detail2.m_ContentStateType = ContentStateType.important;
            Assert.AreEqual(expected2, model.AsDynamic().GenerateExportDetailContent(detail2));

            string expected3 = "\t\t\t[issue]agenda1->detail3 content\n";
            DetailItem detail3 = new DetailItem();
            detail3.m_ContentIndex = 3;
            detail3.m_ContentIndentLevel = 3;
            detail3.m_Content = "agenda1->detail3 content";
            detail3.m_ContentStateType = ContentStateType.issue;
            Assert.AreEqual(expected3, model.AsDynamic().GenerateExportDetailContent(detail3));

            string expected4 = "\t\t\t・agenda1->detail4 content\n";
            DetailItem detail4 = new DetailItem();
            detail4.m_ContentIndex = 4;
            detail4.m_ContentIndentLevel = 3;
            detail4.m_Content = "agenda1->detail4 content";
            detail4.m_ContentStateType = ContentStateType.none;
            Assert.AreEqual(expected4, model.AsDynamic().GenerateExportDetailContent(detail4));
        }
        [TestMethod()]
        public void GenerateExportAgendaContentTest()
        {
            string expected = "(1) agenda1\n\t[decided]agenda1->detail1 content\n\t\t[important]agenda1->detail2 content\n";
            MinutesModel model = new MinutesModel();
            AgendaItem agenda = new AgendaItem(1);
            agenda.m_AgendaIndex = 1;
            agenda.m_Content = "agenda1";
            agenda.m_DetailItems = new System.Collections.ObjectModel.ObservableCollection<DetailItem>();
            DetailItem detail1 = new DetailItem();
            detail1.m_ContentIndex = 1;
            detail1.m_ContentIndentLevel = 1;
            detail1.m_Content = "agenda1->detail1 content";
            detail1.m_ContentStateType = ContentStateType.decided;
            agenda.m_DetailItems.Add(detail1);
            DetailItem detail2 = new DetailItem();
            detail2.m_ContentIndex = 2;
            detail2.m_ContentIndentLevel = 2;
            detail2.m_Content = "agenda1->detail2 content";
            detail2.m_ContentStateType = ContentStateType.important;
            agenda.m_DetailItems.Add(detail2);

            Assert.AreEqual(expected, model.AsDynamic().GenerateExportAgendaContent(agenda));
        }

        [TestMethod()]
        public void GenerateTextFileContentsTest()
        {
            MinutesModel model = new MinutesModel();
            //条件設定
            model.m_Title = "title";
            model.m_Day = new DateTime(2017, 2, 1);
            model.m_StartTime = new DateTime(2000, 1, 2, 3, 4, 5);
            model.m_EndTime = new DateTime(2001, 6, 7, 8, 9, 10);
            model.m_Room = "room1";
            model.m_Participants = new System.Collections.ObjectModel.ObservableCollection<string> { "participant1", "participant2", "participant3" };
            model.m_Writers = new System.Collections.ObjectModel.ObservableCollection<string> { "writer1","writer2" };
            model.m_Agendas = new System.Collections.ObjectModel.ObservableCollection<AgendaItem>();
            AgendaItem agenda1 = new AgendaItem(1);
            agenda1.m_AgendaIndex = 1;
            agenda1.m_Content = "agenda1 content";
            agenda1.m_DetailItems = new System.Collections.ObjectModel.ObservableCollection<DetailItem>();
            DetailItem detail1 = new DetailItem();
            detail1.m_ContentIndex = 1;
            detail1.m_ContentIndentLevel = 1;
            detail1.m_Content = "agenda1->detail1 content";
            detail1.m_ContentStateType = ContentStateType.decided;
            agenda1.m_DetailItems.Add(detail1);
            DetailItem detail2 = new DetailItem();
            detail2.m_ContentIndex = 2;
            detail2.m_ContentIndentLevel = 2;
            detail2.m_Content = "agenda1->detail2 content";
            detail2.m_ContentStateType = ContentStateType.important;
            agenda1.m_DetailItems.Add(detail2);
            DetailItem detail3 = new DetailItem();
            detail3.m_ContentIndex = 3;
            detail3.m_ContentIndentLevel = 3;
            detail3.m_Content = "agenda1->detail3 content";
            detail3.m_ContentStateType = ContentStateType.issue;
            agenda1.m_DetailItems.Add(detail3);
            DetailItem detail4 = new DetailItem();
            detail4.m_ContentIndex = 4;
            detail4.m_ContentIndentLevel = 3;
            detail4.m_Content = "agenda1->detail4 content";
            detail4.m_ContentStateType = ContentStateType.none;
            agenda1.m_DetailItems.Add(detail4);
            model.m_Agendas.Add(agenda1);

            //結果はこうなるはず
            string expected =   "*********************************************************\n" + 
                                "[Title]title\n" +
                                "[Day]2017/02/01\n" +
                                "[Time]03:04:05 ~ 08:09:10\n" + 
                                "[Room]room1\n" +
                                "[Participants]participant1,participant2,participant3\n" +
                                "[Writers]writer1,writer2\n" +
                                "*********************************************************\n" +
                                "(1) agenda1 content\n" +
                                "\t[decided]agenda1->detail1 content\n" + 
                                "\t\t[important]agenda1->detail2 content\n" + 
                                "\t\t\t[issue]agenda1->detail3 content\n" +
                                "\t\t\t・agenda1->detail4 content\n";
            object result = "";
            PrivateObject pObj = new PrivateObject(model);
            string result2 = pObj.Invoke("GenerateTextFileContents",null) as string;
            Assert.AreEqual(expected, result2);                                         //これが普通の書き方
            Assert.AreEqual(expected, model.AsDynamic().GenerateTextFileContents());    //Chaining Assertionというライブラリでこんなことができる。すげー
            (model.AsDynamic().GenerateTextFileContents() as string).Is(expected);      //Chaining Assertionを使えばこんな風にも書ける。注意点はキャストが必ず必要なことらしい
        }
    }

    [TestClass()]
    public class TestHelperTests
    {
        [TestMethod()]
        public void InvokeMethodTest()
        {
            TestHelperTest helperTest = new TestHelperTest();

            //Case1:足し算　(Public&Instance)Method
            int expected1 = 5;
            object[] parameters1 = { 2, 3 };
            object result1;
            InvokedMethodInfo info1 = new InvokedMethodInfo(helperTest.GetType().AssemblyQualifiedName, "PublicMethod", parameters1, BindingFlags.Public | BindingFlags.Instance);
            TestHelper.InvokeMethod(info1, out result1);
            Assert.AreEqual(expected1, (int)result1);

            //Case2:掛け算　(Private&Instance)Method
            int expected2 = 6;
            object[] parameters2 = { 2, 3 };
            object result2;
            InvokedMethodInfo info2 = new InvokedMethodInfo(helperTest.GetType().AssemblyQualifiedName, "PrivateMethod", parameters2,BindingFlags.NonPublic | BindingFlags.Instance);
            TestHelper.InvokeMethod(info2, out result2);
            Assert.AreEqual(expected2, (int)result2);
            Assert.AreEqual(expected2, helperTest.AsDynamic().PrivateMethod(2,3));
            ((int)helperTest.AsDynamic().PrivateMethod(2,3)).Is(expected2);

            //Case3:足し算　(Public&Class)Method
            int expected3 = 5;
            object[] parameters3 = { 2, 3 };
            object result3;
            InvokedMethodInfo info3 = new InvokedMethodInfo(helperTest.GetType().AssemblyQualifiedName, "PublicStaticMethod", parameters3, BindingFlags.Public | BindingFlags.Static);
            TestHelper.InvokeMethod(info3, out result3);
            Assert.AreEqual(expected3, (int)result3);

            //Case4:掛け算　(Private&Class)Method
            int expected4 = 6;
            object[] parameters4 = { 2, 3 };
            object result4;
            InvokedMethodInfo info4 = new InvokedMethodInfo(helperTest.GetType().AssemblyQualifiedName, "PrivateStaticMethod", parameters4, BindingFlags.NonPublic | BindingFlags.Static);
            TestHelper.InvokeMethod(info4, out result4);
            Assert.AreEqual(expected4, (int)result4);
            //Assert.AreEqual(expected4, helperTest.AsDynamic().PrivateStaticMethod(2,3));  //staticはできないぽい？要調査
            //((int)helperTest.AsDynamic().PrivateStaticMethod(2, 3)).Is(expected4);        //staticはできないぽい？要調査
        }
    }
        }