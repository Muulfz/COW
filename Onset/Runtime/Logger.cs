using System;
using System.Collections.Generic;
using System.Text;

namespace Onset.Runtime
{
    internal class Logger : ILogger
    {
        private readonly string _prefix;

        internal Logger(string prefix = null)
        {
            if (prefix != null)
            {
                _prefix = "[COW: " + prefix + "] ";
            }
            else
            {
                _prefix = "[COW] ";
            }
        }

        public void Info(string message)
        {
            Print("INFO", message);
        }

        public void Warn(string message)
        {
            Print("WARN", message);
        }

        public void Success(string message)
        {
            Print("SUCCESS", message);
        }

        public void Debug(string message)
        {
            Print("DEBUG", message);
        }

        public void Fatal(string message)
        {
            Print("FATAL", message);
        }

        public void Error(string message, Exception exception)
        {
            Print("ERROR", message + "\n" + message + "\n" + exception.Message + "\n" + exception.StackTrace + (exception.InnerException != null ? "\n" + exception.InnerException.Message + "\n" + exception.InnerException.StackTrace : ""));
        }

        private void Print(string label, string message)
        {
            Wrapper.LogConsole(_prefix + label + " > " + message);
        }
    }
}
