using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectionExample
{
    /// <summary>
    /// Implementation for ILogger used in production.
    /// The class is sealed to prevent derived instances, which would defeat the purpose of a singleton.
    /// </summary>
    public sealed class Logger : ILogger
    {
        /// <summary>
        /// Member variables
        /// </summary>
        private String m_LogFileName = "";
        private Int32 m_DaysToRetainLogs = 0;
        private LOG_TYPE m_DebugLogOptions = LOG_TYPE.Error & LOG_TYPE.Warning;
        private String m_EmailServer = "";
        private String m_EmailLogonName = "";
        private String m_EmailPassword = "";
        private Int32 m_SMTPPort = 25;
        private List<String> m_SendToAddresses = null;
        private String m_FromAddress = "";
        private String m_ReplyToAddress = "";
        private Boolean m_EmailEnabled = false;
        private Boolean m_blnDisposeHasBeenCalled = false;
        private static readonly Lazy<ILogger> m_objLogger = new Lazy<ILogger>(() => new Logger());


        /// <summary>
        /// Parameterless constructor required for a singleton
        /// </summary>
        private Logger()
        {
            m_SendToAddresses = new List<String>();
        }

        /// <summary>
        /// Once the ILogger instance is configured, this is used to start logging.
        /// </summary>
        /// <returns></returns>
        public Boolean StartLog()
        {
            Boolean retVal = false;

            try
            {


                retVal = true;
            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("User", Environment.UserName);
                throw;
            }

            return retVal;
        }

        /// <summary>
        /// When the ILogger instance is running, this is used to stop logging.
        /// </summary>
        /// <returns></returns>
        public Boolean StopLog()
        {
            Boolean retVal = false;

            try
            {


                retVal = true;
            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("User", Environment.UserName);
                throw;
            }

            return retVal;

        }

        /// <summary>
        /// The property that consumer code uses to access the singleton instance.
        /// </summary>
        public static ILogger Instance
        {
            get
            {
                return m_objLogger.Value;
            }
        }

        /// <summary>
        /// Fully qualified file name for the log file.
        /// </summary>
        public String LogFileName
        {
            get
            {
                return m_LogFileName;
            }
        }

        /// <summary>
        /// How many days that the ILogger instance retains previous log files.
        /// </summary>
        public Int32 DaysToRetainLogs
        {
            get
            {
                return m_DaysToRetainLogs;
            }
        }

        /// <summary>
        /// The debug flags that are active during the lifetime of the ILogger instance
        /// </summary>
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

        /// <summary>
        /// The IP address or DNS name of the outgoing mail server
        /// </summary>
        public String EmailServer
        {
            get
            {
                return m_EmailServer;
            }
        }

        /// <summary>
        /// The logon name expected by the SMTP email server.
        /// </summary>
        public String EmailLogonName
        {
            get
            {
                return m_EmailLogonName;
            }
        }

        /// <summary>
        /// The logon password expected by the SMTP email server.
        /// </summary>
        public String EmailPassword
        {
            get
            {
                return m_EmailPassword;
            }
        }

        /// <summary>
        /// The port that the SMTP email server listens on.
        /// </summary>
        public Int32 SMTPPort
        {
            get
            {
                return m_SMTPPort;
            }
        }

        /// <summary>
        /// A list of email addresses that the ILogger instance sends emails to if 
        /// email is enabled.
        /// </summary>
        public List<String> SendToAddresses
        {
            get
            {
                return m_SendToAddresses;
            }
            set
            {
                if (value == null)
                {
                    m_SendToAddresses = new List<String>();
                }
                else
                {
                    m_SendToAddresses = value;
                }
            }
        }

        /// <summary>
        /// The email address to use with sending emails to indicate who the email is from.
        /// </summary>
        public String FromAddress
        {
            get
            {
                return m_FromAddress;
            }
            set
            {
                if (value == null)
                {
                    m_FromAddress = "";
                }
                else
                {
                    m_FromAddress = value;
                }
            }
        }

        /// <summary>
        /// The email address used to tell the recipient what address to reply to.
        /// </summary>
        public String ReplyToAddress
        {
            get
            {
                return m_ReplyToAddress;
            }
            set
            {
                if (value == null)
                {
                    m_ReplyToAddress = "";
                }
                else
                {
                    m_FromAddress = value;
                }
            }
        }

        /// <summary>
        /// True if sending email is enabled globally, false to turn it off globally.
        /// </summary>
        public Boolean EmailEnabled
        {
            get
            {
                return m_EmailEnabled;
            }
            set
            {
                m_EmailEnabled = value;
            }
        }

        /// <summary>
        /// Method used to set the email sending configuration.
        /// </summary>
        /// <param name="emailServer"></param>
        /// <param name="emailLogonName"></param>
        /// <param name="emailPassword"></param>
        /// <param name="smtpPort"></param>
        /// <param name="sendToAddresses"></param>
        /// <param name="fromAddress"></param>
        /// <param name="replyToAddress"></param>
        /// <param name="emailEnabled"></param>
        /// <returns></returns>
        public Boolean SetEmailData(String emailServer,
                                    String emailLogonName,
                                    String emailPassword,
                                    Int32 smtpPort,
                                    List<string> sendToAddresses,
                                    String fromAddress,
                                    String replyToAddress, 
                                    Boolean emailEnabled)
        {

            Boolean retVal = false;

            try
            {
                m_EmailServer = emailServer ?? "";
                m_EmailLogonName = emailLogonName ?? "";
                m_EmailPassword = emailPassword ?? "";
                m_SMTPPort = smtpPort;
                this.SendToAddresses = sendToAddresses;
                m_FromAddress = fromAddress;
                m_ReplyToAddress = replyToAddress;
                m_EmailEnabled = emailEnabled;


                retVal = true;
            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("m_EmailServer", m_EmailServer);
                throw;
            }

            return retVal;


        }

        /// <summary>
        /// Method used to configure the ILogger instance.
        /// </summary>
        /// <param name="logFileName"></param>
        /// <param name="daysToRetainLogs"></param>
        /// <param name="debugLogOptions"></param>
        /// <returns></returns>
        public Boolean SetLogData(string logFileName, int daysToRetainLogs, LOG_TYPE debugLogOptions)
        {

            Boolean retVal = false;

            try
            {


            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("m_LogFileName", m_LogFileName);
                throw;
            }

            return retVal;

        }

        /// <summary>
        /// Method used to write exception information to the log.
        /// </summary>
        /// <param name="debugLogOptions"></param>
        /// <param name="ex"></param>
        /// <param name="detailMessage">Optional addition message beside what is in the Exception</param>
        /// <returns></returns>
        public Boolean WriteDebugLog(LOG_TYPE debugLogOptions, Exception ex, String detailMessage)
        {
            Boolean retVal = false;

            try
            {


            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("debugLogOptions", debugLogOptions.ToString());
                throw;
            }

            return retVal;

        }

        /// <summary>
        /// Method used to write a message to the log.
        /// </summary>
        /// <param name="debugLogOptions"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Boolean WriteDebugLog(LOG_TYPE debugLogOptions, String message)
        {
            Boolean retVal = false;

            try
            {


            }
            catch (Exception exUnhandled)
            {
                exUnhandled.Data.Add("debugLogOptions", debugLogOptions.ToString());
                throw;
            }

            return retVal;

        }

        /// <summary>
        /// Returns true if the Object is being disposed, false if not.
        /// 
        /// </summary>
        /// <returns></returns>
        public Boolean IsDisposing
        {
            get
            {
                return m_blnDisposeHasBeenCalled;
            }
        }

        #region IDisposable Implementation

        /// <summary>
        /// Implement the IDisposable.Dispose() method
        /// Developers are supposed to call this method when done with this Object.
        /// There is no guarantee when or if the GC will call it, so 
        /// the developer is responsible to.  GC does NOT clean up unmanaged 
        /// resources, such as COM objects, so we have to clean those up, too.
        /// 
        /// </summary>
        public void Dispose()
        {
            try
            {
                // Check if Dispose has already been called 
                // Only allow the consumer to call it once with effect.
                if (!m_blnDisposeHasBeenCalled)
                {
                    // Call the overridden Dispose method that contains common cleanup code
                    // Pass true to indicate that it is called from Dispose
                    Dispose(true);

                    // Prevent subsequent finalization of this Object. This is not needed 
                    // because managed and unmanaged resources have been explicitly released
                    GC.SuppressFinalize(this);
                }
            }

            catch (Exception exUnhandled)
            {

                throw;

            } 
        } 

        /// <summary>
        /// Explicit Finalize method.  The GC calls Finalize, if it is called.
        /// There are times when the GC will fail to call Finalize, which is why it is up to 
        /// the developer to call Dispose() from the consumer Object.
        /// </summary>
        ~Logger()
        {
            // Call Dispose indicating that this is not coming from the public
            // dispose method.
            Dispose(false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected void Dispose(Boolean disposing)
        {

            try
            {

                // Here we dispose and clean up the unmanaged objects and managed Object we created in code
                // that are not in the IContainer child Object of this object.
                // Unmanaged objects do not have a Dispose() method, so we just set them to null
                // to release the reference.  For managed objects, we call their respective Dispose()
                // methods and then release the reference.
                // DEVELOPER NOTE:
                //if (m_obj != null)
                //    {
                //    m_obj = null;
                //    }


                // Set the flag that Dispose has been called and executed.
                m_blnDisposeHasBeenCalled = true;

            }

            catch (Exception exUnhandled)
            {

                throw;


            }  
        }

        #endregion IDisposable Implementation


    }
}
