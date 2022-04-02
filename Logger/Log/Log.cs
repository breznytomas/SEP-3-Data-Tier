using System;
using System.IO;

namespace Logger.Log
{
    public static class Log
    {
        public static void AddLog(string text)
        {
            var logLine = new LogLine(text);
            AddToFile(logLine);
            Console.WriteLine(logLine);
        }

        private static void AddToFile(LogLine logLine)
        {
            if (logLine == null) return;
            
            var filename = $"Log-{logLine.Date()}.txt";
            using var output = File.AppendText(filename);
            output.WriteLine(logLine);
        }
    }
}