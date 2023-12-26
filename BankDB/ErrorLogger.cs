using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Configuration;


namespace BankDB
{
    public static class ErrorLogger
    {

        public static void Log(string message)
        {        
            // Log to the text file
            // Specify the path to the log file
            string filePath = "../../../BankDB/log/errorHistory.txt";
            try
            {
                // Log to the text file using StreamWriter
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as file not found or permission issues
                Console.WriteLine("Error writing to the log file: " + ex.Message);
            }
        }
    }
}
