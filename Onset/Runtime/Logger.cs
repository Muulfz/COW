using System;
using System.Collections.Generic;
using System.Text;
using Onset.Plugin;

namespace Onset.Runtime
{
    internal class Logger : ILogger
    {
        private readonly string _prefix;
        private readonly bool _isDebug;

        internal Logger(string prefix = null, bool isDebug = false)
        {
            _isDebug = isDebug;
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
            if(!_isDebug) return;
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
