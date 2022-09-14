using System;
using System.Globalization;
using System.IO;

namespace Silent_guardian
{
    public static class FileLogger
    {
        static object lockFile = new object();

        public static void logError(string errorMsg)
        {
            var currentDate = DateTime.Now;

            string LogPath = Path.Combine(AppContext.BaseDirectory,"log");
            string LogName = currentDate.ToString("yyMMdd") +".txt";

            lock (lockFile) 
            {
                Directory.CreateDirectory(LogPath);
                File.AppendAllText(Path.Combine(LogPath, LogName), "[" + currentDate.ToString("HH:mm:ss") + "] " + errorMsg + Environment.NewLine);
            }

        }
    }
}