using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SingletonLogger.Logger
{
    public class FileLogger : ILog
    {
        private String _fileName; // full path to file
        private String _folderName;
        private FileStream _logStream;
        private StreamWriter _logFile;
        private bool _activated = true;

        public FileLogger(string folderName, string fileName)
        {
            if (folderName == "")
            {
                string appName = AppDomain.CurrentDomain.FriendlyName.Split('.')[0];
                _folderName = string.Concat(GetAllUsersPublicFolder(), @"\Documents\", appName, @"\Log\");
            }
            else
            {
                if (!Path.IsPathRooted(folderName))
                {
                    _folderName = Path.GetDirectoryName(folderName);

                }
                else
                {
                    _folderName = folderName;
                }
            }
            _fileName = fileName;

            CreateLogFolder();
            try
            {
                _logStream = new FileStream(_folderName + "//" + _fileName, FileMode.Append);
            }
            catch (IOException ex)
            {
                // File may be already open (e.g. program already running)  --> open a new file with current PID as suffix
                _fileName = String.Format("log-{0}_{1}.txt", DateTime.Now.ToString("yyyy-MM-dd"), Process.GetCurrentProcess().Id);
                _logStream = new FileStream(_folderName + "//" + _fileName, FileMode.Append);
            }
            _logFile = new StreamWriter(_logStream);
        }

        public FileLogger(string folderName)
            : this(folderName, String.Format("log-{0}.txt", DateTime.Now.ToString("yyyy-MM-dd")))
        {
        }

        public static String GetAllUsersPublicFolder()
        {
            if (Environment.OSVersion.Version.Major > 5)
            {
                //OS version is greather than Win XP
                return Environment.ExpandEnvironmentVariables("%PUBLIC%");
            }
            else
            {
                //OS version is equal or lower than Win XP
                return Environment.ExpandEnvironmentVariables("%ALLUSERSPROFILE%");
            }
        }
        public void CreateLogFolder()
        {
            try
            {
                if (!Directory.Exists(_folderName))
                {
                    Directory.CreateDirectory(_folderName);
                }
            }
            catch (Exception expFileError)
            {
                throw (expFileError);
            }
        }

        public void RemoveLogFile()
        {
            try
            {
                if (File.Exists(_fileName))
                {
                    File.Delete(_fileName);
                }
            }
            catch (Exception expFileError)
            {
                throw (expFileError);
            }
        }

        public void Terminate()
        {
            _logFile.Close();
        }

        public void LogMsg(string logMessage, Log.Level level)
        {
            try
            {
                // log string to text file
                _logFile.WriteLine(level.ToString() + " " + logMessage);
                _logFile.Flush();
            }
            catch (IOException ioExp)
            {
                Log.Instance.UnregisterObserver(this);
                Log.Instance.LogMsg(String.Format("Unregistered observer {0}: exception {1}", this.ToString(), ioExp.Message), Log.Level.Error);
            }
        }

        public void Deactivate()
        {
            // whenever an exception within an observer has occured, it will not be used to log messages again
            Status = false;
        }

        public void ViewLog()
        {
            Process notePad = new Process();

            notePad.StartInfo.FileName = "notepad.exe";
            notePad.StartInfo.Arguments = _fileName;

            notePad.Start();
        }

        public override string ToString()
        {
            return String.Format("FileLogger object: m_FileName '{0}'", _fileName);
        }

        public String FileName
        {
            get
            {
                return _fileName;
            }
        }

        public bool Status
        {
            get
            {
                return _activated;
            }
            set
            {
                _activated = value;
            }
        }
    }
}
