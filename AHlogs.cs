using System;
using System.IO;

namespace AHHelperTools
{
    public class AHlogs
    {
        static string LOGPATH = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"ah-logs.txt";

        /// <summary>
        /// this will log to the filename  specified 
        /// by the tool. and with the current time.
        /// </summary>
        /// <param name="message"></param>
        public static void log(string message)
        {
            using (StreamWriter file = new StreamWriter(LOGPATH, true))
            {
                DateTime currentTime = DateTime.Now;
                file.WriteLine(currentTime + " - " + message);
            }
        }

        /// <summary>
        /// Log to a file that you name.
        /// choose to add time or not 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fileName"></param>
        /// <param name="withTime"></param>
        public static void log(string message, string fileName = "ah-logs", Boolean withTime = true)
        {
            if (fileName != "ah-logs")
            {
                LOGPATH = LOGPATH.Replace("ah-logs", fileName);
            }
            using (StreamWriter file = new StreamWriter(LOGPATH, true))
            {
                if (withTime)
                {
                    DateTime currentTime = DateTime.Now;
                    file.WriteLine(currentTime + " - " + message);
                }
                else
                {
                    DateTime currentTime = DateTime.Now;
                    file.WriteLine(message);
                }
            }
        }
    }
}
