using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MinutesTests
{
    public class TestHelper
    {
        public static bool InvokeMethod(InvokedMethodInfo inInvokedMethodInfo, out object outResult)
        {
            try
            {
                Type t = Type.GetType(inInvokedMethodInfo.m_ClassName);
                object o = Activator.CreateInstance(t);
                MethodInfo method = t.GetMethod(inInvokedMethodInfo.m_MethodName, inInvokedMethodInfo.m_BindingFlags);
                outResult = method.Invoke(o, inInvokedMethodInfo.m_Parameters);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                outResult = null;
                return false;
            }
        }
    }

    public class InvokedMethodInfo
    {
        public string m_ClassName { get; set; }
        public string m_MethodName { get; set; }
        public object[] m_Parameters { get; set; }
        public BindingFlags m_BindingFlags { get; set; }

        public InvokedMethodInfo(string inClassName,string inMethodName,object[] inParameters,BindingFlags inBindingFlags)
        {
            m_ClassName = inClassName;
            m_MethodName = inMethodName;
            m_Parameters = inParameters;
            m_BindingFlags = inBindingFlags;
        }
    }

    class TestHelperTest
    {
        public int PublicMethod(int a, int b)
        {
            return a + b;
        }
        private int PrivateMethod(int a,int b)
        {
            return a * b;
        }
        public static int PublicStaticMethod(int a, int b)
        {
            return a + b;
        }
        private static int PrivateStaticMethod(int a, int b)
        {
            return a * b;
        }

    }
}
