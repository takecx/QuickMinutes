using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minutes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minutes.Tests
{
    [TestClass()]
    public class MainWindowViewModelTests
    {
        [TestMethod()]
        public void GetLastAgendaItemTest()
        {
            var viewModwl = new MainWindowViewModel();
            int expected1 = 0;
            viewModwl.AsDynamic().AddNewAgendaItemModel();
            Assert.AreEqual(expected1, viewModwl.AsDynamic().GetLastAgendaItem().m_AgendaIndex);

            int expected2 = 1;
            viewModwl.AsDynamic().AddNewAgendaItemModel();
            Assert.AreEqual(expected2, viewModwl.AsDynamic().GetLastAgendaItem().m_AgendaIndex);
        }
    }
}