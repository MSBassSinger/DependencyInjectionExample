using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExample
{
    public class ContextMgr : IContextMgr
    {

        private String m_LogFileName = "";
        private LOG_TYPE m_DebugLogOptions = LOG_TYPE.Error | LOG_TYPE.Warning;
        private Dictionary<String, dynamic> m_SharedList = null;
        private Int32 m_DaysToRetainLogs = 14;

        public ContextMgr()
        {
            m_SharedList = new Dictionary<String, dynamic>();
        }

        public String LogFileName
        {
            get
            {
                return m_LogFileName;
            }
            set
            {
                m_LogFileName = value ?? "";
            }
        }

        public LOG_TYPE DebugLogOptions
        {
            get
            {
                return m_DebugLogOptions;
            }
            set
            {
                m_DebugLogOptions = value;
            }
        }

        public Int32 DaysToRetainLogs
        {
            get
            {
                return m_DaysToRetainLogs;
            }
            set
            {
                m_DaysToRetainLogs = value;
            }
        }

        public Dictionary<String, dynamic> SharedList
        {
            get
            {
                return m_SharedList;
            }
            set
            {
                if (value == null)
                {
                    m_SharedList = new Dictionary<String, dynamic>();
                }
                else
                {
                    m_SharedList = value;
                }
            }
        }



    }
}
