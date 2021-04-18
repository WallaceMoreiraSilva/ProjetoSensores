using System;

namespace SensoresAPP.Interfaces
{
    public interface ILog
    {
        bool IsFatalEnabled
        {
            get;
        }

        bool IsWarnEnabled
        {
            get;
        }

        bool IsInfoEnabled
        {
            get;
        }

        bool IsDebugEnabled
        {
            get;
        }

        bool IsErrorEnabled
        {
            get;
        }

        void Debug(string message);

        void Debug(string message, Exception exception);

        void DebugFormat(string format, params object[] args);

        void Error(string message, Exception exception);

        void Error(string message);

        void ErrorFormat(string format, params object[] args);

        void Fatal(string message);

        void Fatal(string message, Exception exception);

        void FatalFormat(string format, params object[] args);

        void Info(string message, Exception exception);

        void Info(string message);

        void InfoFormat(string format, params object[] args);

        void Warn(string message);

        void Warn(string message, Exception exception);

        void WarnFormat(string format, params object[] args);
    }
}