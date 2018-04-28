using System;
using System.Collections.Generic;
using System.Text;

namespace SingletonLogger.Logger
{
    public interface ILog
    {
        // Method for string logging
        void LogMsg(String logMessage, Log.Level level);
        // Method to close all log streams or sockets 
        void Terminate();
        // Method to deactivate logger
        void Deactivate();
        // Property to check status
        bool Status
        {
            get;
        }
    }
}
