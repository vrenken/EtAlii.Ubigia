using Gibraltar.Agent;
using System;

namespace EtAlii.xTechnology.Logging
{
    using System.Diagnostics;

    [DebuggerStepThrough]
    public class Logger : ILogger
    {
        private readonly string _name;
        private readonly string _category;

        private static string _productName;
        private static string _applicationName;
        private static Version _applicationVersion;
        private static bool _hasCustomNaming;

        private const int _framesToSkip = 1;

        public LogLevel Level { get { return _level; } set { _level = value; } }
        private LogLevel _level = LogLevel.Info;

        internal Logger(string name, string category)
        {
            _category = category;
            _name = name;
        }

        public static void StartSession()
        {
            Log.StartSession(_framesToSkip, null);
        }

        public static void StartSession(string productName, string applicationName, Version applicationVersion)
        {
            _hasCustomNaming = true;
            _productName = productName;
            _applicationName = applicationName;
            _applicationVersion = applicationVersion;

            Log.Initializing += OnLogInitializing;
            Log.StartSession(_framesToSkip, null);
        }

        private static void OnLogInitializing(object sender, LogInitializingEventArgs e)
        {
            e.Configuration.Publisher.ProductName = _productName;
            e.Configuration.Publisher.ApplicationName = _applicationName;
            e.Configuration.Publisher.ApplicationVersion = _applicationVersion;
        }

        public static void EndSession()
        {
            Log.EndSession( SessionStatus.Normal, _framesToSkip, null);
            if (_hasCustomNaming)
            {
                Log.Initializing -= OnLogInitializing;
            }
        }

        public static void ReportUnhandledException(Exception e)
        {
            Log.ReportException(e, "Unhandled", true, false);
        }

        public void Warning(string message)
        {
            if (_level >= LogLevel.Warning)
            {
                Log.Write(LogMessageSeverity.Warning, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Warning(string message, params object[] args)
        {
            if (_level >= LogLevel.Warning)
            {
                Log.Write(LogMessageSeverity.Warning, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }

        public void Warning(string message, Exception e)
        {
            if (_level >= LogLevel.Warning)
            {
                Log.Write(LogMessageSeverity.Warning, _name, _framesToSkip, e, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Warning(string message, Exception e, params object[] args)
        {
            if (_level >= LogLevel.Warning)
            {
                Log.Write(LogMessageSeverity.Warning, _name, _framesToSkip, e, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }

        public void Verbose(string message)
        {
            if (_level >= LogLevel.Verbose)
            {
                Log.Write(LogMessageSeverity.Verbose, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Verbose(string message, params object[] args)
        {
            if (_level >= LogLevel.Verbose)
            {
                Log.Write(LogMessageSeverity.Verbose, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }

        public void Info(string message)
        {
            if (_level >= LogLevel.Info)
            {
                Log.Write(LogMessageSeverity.Information, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Info(string message, params object[] args)
        {
            if (_level >= LogLevel.Info)
            {
                Log.Write(LogMessageSeverity.Information, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }

        public void Critical(string message, Exception e)
        {
            if (_level >= LogLevel.Critical)
            {
                Log.Write(LogMessageSeverity.Critical, _name, _framesToSkip, e, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Critical(string message, Exception e, params object[] args)
        {
            if (_level >= LogLevel.Critical)
            {
                Log.Write(LogMessageSeverity.Critical, _name, _framesToSkip, e, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }
    }
}

