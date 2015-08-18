using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LoggerLib
{
    public sealed class Logger
    {
        EventLog log;

        private static readonly Logger execute = new Logger();
        private Logger()
        {
            log = new EventLog("Application");
            log.Source = "Totál Szervíz";
            
        }

        public static Logger Execute
        {
            get
            {
                return execute;
            }
        }

        public void WriteLog(string message, EventLogEntryType type)
        {
            log.WriteEntry(message, type);
        }

        public void WriteExceptionToLog(Exception ex)
        {
            WriteLog(ex.Message, EventLogEntryType.Error);
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                WriteLog(ex.Message, EventLogEntryType.Error);
            }
        }
    }
}
