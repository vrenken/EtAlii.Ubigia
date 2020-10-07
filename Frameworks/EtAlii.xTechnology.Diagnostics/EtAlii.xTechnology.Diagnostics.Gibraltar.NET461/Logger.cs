namespace EtAlii.xTechnology.Diagnostics
{
    using System;
    using System.Diagnostics;
    using Gibraltar.Agent;

    [DebuggerStepThrough]
    public class Logger : ILogger
    {
        private readonly string _name;
        private readonly string _category;

        private static string _productName;
        private static string _applicationName;
        private static Version _applicationVersion;
        private static bool _hasCustomNaming;

        private const int FramesToSkip = 1;

        public LogLevel Level { get; set; } = LogLevel.Info;

        internal Logger(string name, string category)
        {
            _category = category;
            _name = name;
        }

        public static void StartSession()
        {
            Log.StartSession(FramesToSkip, null);
        }

        public static void StartSession(string productName, string applicationName, Version applicationVersion)
        {
            _hasCustomNaming = true;
            _productName = productName;
            _applicationName = applicationName;
            _applicationVersion = applicationVersion;

            Log.Initializing += OnLogInitializing;
            Log.StartSession(FramesToSkip, null);
        }

        private static void OnLogInitializing(object sender, LogInitializingEventArgs e)
        {
            e.Configuration.Publisher.ProductName = _productName;
            e.Configuration.Publisher.ApplicationName = _applicationName;
            e.Configuration.Publisher.ApplicationVersion = _applicationVersion;
        }

        public static void EndSession()
        {
            Log.EndSession( SessionStatus.Normal, FramesToSkip, null);
            if (_hasCustomNaming)
            {
                Log.Initializing -= OnLogInitializing;
            }
        }

        //public static void ReportUnhandledException(Exception e)
        //{
        //    Log.ReportException(e, "Unhandled", true, false);
        //}

        public void Warning(string message)
        {
            if (Level >= LogLevel.Warning)
            {
                Log.Write(LogMessageSeverity.Warning, _name, FramesToSkip, null, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Warning(string message, params object[] args)
        {
            if (Level >= LogLevel.Warning)
            {
                Log.Write(LogMessageSeverity.Warning, _name, FramesToSkip, null, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }

        public void Warning(string message, Exception e)
        {
            if (Level >= LogLevel.Warning)
            {
                Log.Write(LogMessageSeverity.Warning, _name, FramesToSkip, e, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Warning(string message, Exception e, params object[] args)
        {
            if (Level >= LogLevel.Warning)
            {
                Log.Write(LogMessageSeverity.Warning, _name, FramesToSkip, e, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }

        public void Verbose(string message)
        {
            if (Level >= LogLevel.Verbose)
            {
                Log.Write(LogMessageSeverity.Verbose, _name, FramesToSkip, null, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Verbose(string message, params object[] args)
        {
            if (Level >= LogLevel.Verbose)
            {
                Log.Write(LogMessageSeverity.Verbose, _name, FramesToSkip, null, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }

        public void Info(string message)
        {
            if (Level >= LogLevel.Info)
            {
                Log.Write(LogMessageSeverity.Information, _name, FramesToSkip, null, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Info(string message, params object[] args)
        {
            if (Level >= LogLevel.Info)
            {
                Log.Write(LogMessageSeverity.Information, _name, FramesToSkip, null, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }

        public void Critical(string message, Exception e)
        {
            if (Level >= LogLevel.Critical)
            {
                Log.Write(LogMessageSeverity.Critical, _name, FramesToSkip, e, LogWriteMode.Queued, null, _category, null, message);
            }
        }

        public void Critical(string message, Exception e, params object[] args)
        {
            if (Level >= LogLevel.Critical)
            {
                Log.Write(LogMessageSeverity.Critical, _name, FramesToSkip, e, LogWriteMode.Queued, null, _category, null, message, args);
            }
        }
    }
}

