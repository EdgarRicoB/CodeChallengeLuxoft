using CodeChallengeLuxoft.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallengeLuxoft.Utilities
{
    public class Logger
    {
        /// <summary>
        /// Method to log into a file if it does not exist it creates the file
        /// </summary>
        /// <param name="message">Message to be logged</param>
        /// <param name="level">Log level</param>
        /// <param name="fileName">Path to the file</param>
        public static void log_message(string message, string level, string fileName = "")
        {
            //if the file name is not sent we use the name stored in the configurations
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = Configurations.LogName;
                if (fileName == null)
                {
                    throw new Exception("The configuration for the log Path/Name is not correctly setup");
                }
            }

            //Append the log message to the file and if the file does not exist it will be created
            using (StreamWriter writer = File.AppendText(fileName))
            {
                writer.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}][{level}] {message}");
            }
        }
    }
}
