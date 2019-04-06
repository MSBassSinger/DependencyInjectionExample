using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExample
{
    public class SystemInfo
    {
        

        public SystemInfo()
        {
            // Injected by singleton reference, but can be the production logger or test logger
            Logger.Instance.SetLogData()

        }


    }
}
