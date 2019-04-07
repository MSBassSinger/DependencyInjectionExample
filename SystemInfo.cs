using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExample
{
    /// <summary>
    /// This class uses dependency injection in two ways.
    /// First, the Logger is a singleton that uses dependency injection via method injection.
    /// Second, IContextMgr is an object this calls can use without dependency on the implementation class.  It is constructor injection.
    /// Setter injection was not shown.
    /// The overall purpose is for this class to have no direct dependencies (loosely coupled) on implemented classes, but on interfaces. 
    /// 
    /// </summary>
    public class SystemInfo
    {
        private IContextMgr m_AppContext = null;

        public SystemInfo(IContextMgr appContext)
        {
            // The actual instance is not tied to this project's ContextMgr class.
            m_AppContext = appContext;

            // Injected by singleton reference, but can be the production logger or test logger
            Logger.Instance.SetLogData(m_AppContext.LogFileName, m_AppContext.DaysToRetainLogs, m_AppContext.DebugLogOptions);
            Logger.Instance.StartLog();
        }

        /// <summary>
        /// Normally, the Logger is configured in a Program.cs or similar startup code.
        /// It is configured and ended in this class for clarity.
        /// </summary>
        public Boolean LogTheSystem()
        {
            Boolean retVal = false;

            try
            {
                if ((Logger.Instance.DebugLogOptions & LOG_TYPE.System) == LOG_TYPE.System)
                {
                    foreach (KeyValuePair<String, dynamic> item in this.m_AppContext.SharedList)
                    {
                        Logger.Instance.WriteDebugLog(LOG_TYPE.System, $"{item.Key} = {item.Value.ToString()}");
                    }
                }

                retVal = true;
            }
            catch (Exception exUnhandled)
            {
                if ((Logger.Instance.DebugLogOptions & LOG_TYPE.Error) == LOG_TYPE.Error)
                {
                    Logger.Instance.WriteDebugLog(LOG_TYPE.Error, exUnhandled, "Unexpected error while logging the system attributes.");
                }
            }

            return retVal;
        }

        ~SystemInfo()
        {
            Logger.Instance.StopLog();
        }
    }

}
