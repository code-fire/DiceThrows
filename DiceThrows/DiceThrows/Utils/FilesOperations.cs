using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceThrows.Utils
{
    public static class FilesOperations
    {
        private static string _appPublicFolder = String.Concat(GetAllUsersPublicFolder(), "\\Documents\\", "DiceThrows");
        public static string AppLogFolder = String.Concat(GetAllUsersPublicFolder(), "\\Documents\\", "DiceThrows\\", "Log");
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
        public static void PrepareFolders()
        {
            if (!Directory.Exists(_appPublicFolder))
            {
                Directory.CreateDirectory(_appPublicFolder);
            }

            if (!Directory.Exists(AppLogFolder))
            {
                Directory.CreateDirectory(AppLogFolder);
            }
        }
    }
}
