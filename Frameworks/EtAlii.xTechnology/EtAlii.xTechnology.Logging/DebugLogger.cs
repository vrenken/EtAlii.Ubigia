
namespace EtAlii.xTechnology.Logging
{
    using System.Diagnostics;

    public class DebugLogger : ILogger
    {
        public LogLevel Level
        {
            get { return _level; }
            set { _level = value; }
        }
        private LogLevel _level = LogLevel.Info;

        public void Verbose(string message)
        {
            if (_level >= LogLevel.Verbose)
            {
                Debug.WriteLine(message);
            }
        }

        public void Verbose(string message, params object[] args)
        {
            if (_level >= LogLevel.Verbose)
            {
                message = string.Format(message, args);
                Debug.WriteLine(message);
            }
        }

        public void Info(string message)
        {
            if (_level >= LogLevel.Info)
            {
                Debug.WriteLine(message);
            }
        }

        public void Info(string message, params object[] args)
        {
            if (_level >= LogLevel.Info)
            {
                message = string.Format(message, args);
                Debug.WriteLine(message);
            }
        }

        public void Warning(string message)
        {
            if (_level >= LogLevel.Warning)
            {
                Debug.WriteLine(message);
            }
        }

        public void Warning(string message, params object[] args)
        {
            if (_level >= LogLevel.Warning)
            {
                message = string.Format(message, args);
                Debug.WriteLine(message);
            }
        }

        public void Critical(string message, System.Exception e, params object[] args)
        {
            if (_level >= LogLevel.Critical)
            {
                message = string.Format(message, args);
                Debug.WriteLine(message);
            }
        }

        public void Critical(string message, System.Exception e)
        {
            if (_level >= LogLevel.Critical)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
