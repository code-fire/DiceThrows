using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace SingletonLogger.Logger
{
    public class Log
    {
        private static Log _logger = null;
        private List<Logger.ILog> _observers;
        public enum Level
        {
            Short,
            Verbose,
            Warning,
            Error
        }

        // no one except this class may instantiate this class.
        private Log()
        {
            _observers = new List<Logger.ILog>();
        }

        public static Log Instance
        {
            get
            {
                // instantiate singleton object if unused
                if (_logger == null)
                {
                    if (_logger == null)
                    {
                        _logger = new Log();
                    }
                }
                return _logger;
            }
        }

        public void RegisterObserver(Logger.ILog observer)
        {
            LogMsg("Adding observer: " + observer.ToString(), Level.Verbose);
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void UnregisterObserver(Logger.ILog observer)
        {
            foreach (Logger.ILog o in _observers)
            {
                if (o == observer)
                {
                    observer.Deactivate();
                    LogMsg("Deactivated observer: " + observer.ToString(), Level.Warning);
                    break;
                }
            }
        }

        public void Terminate()
        {
            foreach (Logger.ILog observer in _observers)
            {
                observer.Deactivate();
                observer.Terminate();
            }
        }

        public void LogMsg(string logMessage, Log.Level level)
        {
            String frmMsg = String.Empty;

            switch (level)
            {
                case Level.Short:
                    frmMsg = string.Format("{0}", logMessage);
                    break;
                default:
                    // Apply some basic formatting like the current timestamp
                    frmMsg = string.Format("{0} - {1}", DateTime.Now.ToString(), logMessage);
                    break;
            }

            foreach (Logger.ILog observer in _observers)
            {
                if (observer.Status)
                {
                    // only log to active observers
                    observer.LogMsg(frmMsg, level);
                }
            }

        }

        public void ViewLog()
        {
            /* only file logger made visible */
            foreach (Logger.ILog item in _observers)
            {
                Logger.FileLogger fl = item as Logger.FileLogger;
                if (fl != null)
                {
                    fl.ViewLog();
                }
            }
        }

    }
}
