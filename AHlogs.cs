using System;
using System.IO;

namespace AHHelperTools
{
    public class AHLogs
    {
        static string LOGPATH = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"ah-logs.txt";

        /// <summary>
        /// this will log to the filename  specified 
        /// by the tool. and with the current time.
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
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
        public static void Log(string message, string fileName = "ah-logs", Boolean withTime = true)
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
                    file.WriteLine(message);
                }
            }
        }

        /// <summary>
        /// logging Exception details
        /// </summary>
        /// <param name="ex"></param>
        public static void Log(Exception ex)
        {
            using (StreamWriter file = new StreamWriter(LOGPATH, true))
            {
                DateTime currentTime = DateTime.Now;
                Guid expIDTrackerr = Guid.NewGuid();

                file.WriteLine(string.Format("{0} - Exception Details: {1} - {2}", currentTime, ex.Message, expIDTrackerr));
                file.WriteLine(string.Format("{0} - Inner Exception: {1} - {2}", currentTime, ex.InnerException, expIDTrackerr));
                file.WriteLine(string.Format("{0} - Source: {1} - {2}", currentTime, ex.Source, expIDTrackerr));
                file.WriteLine(string.Format("{0} - Type: {1} - {2}", currentTime, ex.GetType(), expIDTrackerr));
            }
        }

        /// <summary>
        /// logging custom message with Exception details
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public static void Log(string message, Exception ex)
        {
            using (StreamWriter file = new StreamWriter(LOGPATH, true))
            {
                DateTime currentTime = DateTime.Now;
                Guid expIDTrackerr = Guid.NewGuid();
                
                file.WriteLine(string.Format("{0} - Message: {1} - {2}", currentTime, message, expIDTrackerr));
                file.WriteLine(string.Format("{0} - Exception Details: {1} - {2}", currentTime, ex.Message, expIDTrackerr));
                file.WriteLine(string.Format("{0} - Inner Exception: {1} - {2}", currentTime, ex.InnerException, expIDTrackerr));
                file.WriteLine(string.Format("{0} - Source: {1} - {2}", currentTime, ex.Source, expIDTrackerr));
                file.WriteLine(string.Format("{0} - Type: {1} - {2}", currentTime, ex.GetType(), expIDTrackerr));
            }
        }
    }
}
