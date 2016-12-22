using System;

namespace EtAlii.xTechnology.Logging
{
    public interface ILogger
    {
        LogLevel Level { get; set; }
        void Verbose(string message);
        void Verbose(string message, params object[] args);
        void Info(string message);
        void Info(string message, params object[] args);
        void Warning(string message);
        void Warning(string message, params object[] args);
        void Critical(string message, Exception e, params object[] args);
        void Critical(string message, Exception e);
    }
}
