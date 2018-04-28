using SingletonLogger.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DiceThrows
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log _logger = Log.Instance;
            FileLogger _FileLogger = new FileLogger(Utils.FilesOperations.AppLogFolder);
            //Register the observer
            _logger.RegisterObserver(_FileLogger);

            Application.Run(new MainForm());
        }
    }
}
