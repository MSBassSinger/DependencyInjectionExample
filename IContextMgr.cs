using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExample
{
    public interface IContextMgr
    {
        /// <summary>
        /// Fully qualified log file name
        /// </summary>
        String LogFileName { get; set; }

        /// <summary>
        /// Debug flags to recognize when writing to the debug log
        /// </summary>
        LOG_TYPE DebugLogOptions { get; set; }

        /// <summary>
        /// How long log files are retained, in days.
        /// </summary>
        Int32 DaysToRetainLogs { get; set; }

        /// <summary>
        /// A keyed collection of various values and objects that can be 
        /// dynamcially assigned and retain the strong data type of the value.
        /// </summary>
        Dictionary<String, dynamic> SharedList { get; set; }


    }
}
