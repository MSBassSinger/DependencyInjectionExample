using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyInjectionExample;
using System;
using System.IO;

namespace DependencyInjectionExampleTest
{
    [TestClass]
    public class LoggerUnitTest
    {
        [TestMethod]
        public void LoggerLifeCycleTest()
        {
            String fileDate = DateTime.Now.ToString("yyyyMMdd-HHmmssfff");

            String fileName = $@"{Environment.CurrentDirectory}\Logs\TestLog{fileDate}.txt";

            if (!Directory.Exists($@"{Environment.CurrentDirectory}\Logs"))
            {
                Directory.CreateDirectory($@"{Environment.CurrentDirectory}\Logs");
            }

            Int32 daysToRetainLogs = 14;

            LOG_TYPE debugLogOptions = LOG_TYPE.Error |
                                       LOG_TYPE.Warning |
                                       LOG_TYPE.System |
                                       LOG_TYPE.Test |
                                       LOG_TYPE.ShowModuleClassAndLineNumber |
                                       LOG_TYPE.ShowTimeOnly;

            Boolean retVal = Logger.Instance.SetLogData(fileName, daysToRetainLogs, debugLogOptions);

            Assert.IsTrue(retVal, "Log initialization failed.");

            retVal = Logger.Instance.StartLog();

            Assert.IsTrue(retVal, "Log start failed.");

            retVal = Logger.Instance.WriteDebugLog(LOG_TYPE.Test, "Test write of message");

            Assert.IsTrue(retVal, "Log message write failed.");

            Exception ex = new Exception("Test exception message");

            Exception ex2 = new Exception("Test outer exception", ex);

            String exMsg = "Test detail messsage";

            retVal = Logger.Instance.WriteDebugLog(LOG_TYPE.Test, ex2, exMsg);

            Assert.IsTrue(retVal, "Log exception write failed.");

            retVal = Logger.Instance.StopLog();

            Assert.IsTrue(retVal, "Log stop failed.");

        }

        [TestMethod]
        public void TestSystemInfo()
        {
            Boolean retVal = false;

            String fileDate = DateTime.Now.ToString("yyyyMMdd-HHmmssfff");

            String fileName = $@"{Environment.CurrentDirectory}\Logs\TestLog{fileDate}.txt";

            if (!Directory.Exists($@"{Environment.CurrentDirectory}\Logs"))
            {
                Directory.CreateDirectory($@"{Environment.CurrentDirectory}\Logs");
            }

            Int32 daysToRetainLogs = 14;

            LOG_TYPE debugLogOptions = LOG_TYPE.Error |
                                       LOG_TYPE.Warning |
                                       LOG_TYPE.System |
                                       LOG_TYPE.Test |
                                       LOG_TYPE.ShowModuleClassAndLineNumber |
                                       LOG_TYPE.ShowTimeOnly;

            IContextMgr appContext = new ContextMgr();

            appContext.DaysToRetainLogs = daysToRetainLogs;
            appContext.DebugLogOptions = debugLogOptions;
            appContext.LogFileName = fileName;
            appContext.SharedList.Add("CurrentDirectory", Environment.CurrentDirectory);
            appContext.SharedList.Add("CurrentManagedThreadId", Environment.CurrentManagedThreadId);
            appContext.SharedList.Add("Is64BitOperatingSystem", Environment.Is64BitOperatingSystem);
            appContext.SharedList.Add("Is64BitProcess", Environment.Is64BitProcess);
            appContext.SharedList.Add("MachineName", Environment.MachineName);
            appContext.SharedList.Add("OSVersion", Environment.OSVersion);
            appContext.SharedList.Add("ProcessorCount", Environment.ProcessorCount);
            appContext.SharedList.Add("UserDomainName", Environment.UserDomainName);
            appContext.SharedList.Add("UserName", Environment.UserName);

            SystemInfo si = new SystemInfo(appContext);

            retVal = si.LogTheSystem();

            si = null;

            Assert.IsTrue(retVal, "SystemInfo test failed.");

        }
    }
}
