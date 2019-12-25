using System;
using System.Collections.Generic;
using System.Text;

namespace Onset
{
    public interface ILogger
    {
        void Info(string message);

        void Warn(string message);

        void Success(string message);

        void Debug(string message);

        void Fatal(string message);

        void Error(string message, Exception exception);
    }
}
