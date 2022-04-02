using System;

namespace Logger.Log
{
    public class LogLine
    {
        private readonly string _text;
        private DateTime DateTime { get; }

        public LogLine(string text)
        {
            _text = text;
            DateTime = DateTime.Now;
        }

        public string Date()
        {
            return $"{DateTime:dd-MM-yyyy}";
        }

        public override string ToString()
        {
            return $"[{DateTime:dd/MM/yyyy HH:mm:ss}] {_text}";
        }
    }
}