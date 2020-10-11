namespace EtAlii.xTechnology.Diagnostics.Serilog
{
    using System;
    using System.Diagnostics;
    using global::Serilog;

    [DebuggerStepThrough]
    public class SerilogLogger : global::EtAlii.xTechnology.Diagnostics.ILogger
    {
        private readonly ILogger _logger;

        internal SerilogLogger(string name, string category)
        {
            _logger = Log
                .ForContext("name", name)
                .ForContext("category", category);
        }

        public void Warning(string message)
        {
            _logger.Warning(message);
        }

        public void Warning(string message, params object[] args)
        {
            _logger.Warning(message, args);

            // if (Level >= LogLevel.Warning)
            // {
            //     Log.Write(LogMessageSeverity.Warning, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message, args);
            // }
        }

        public void Warning(string message, Exception e)
        {
            _logger.Warning(e, message);

            // if (Level >= LogLevel.Warning)
            // {
            //     Log.Write(LogMessageSeverity.Warning, _name, _framesToSkip, e, LogWriteMode.Queued, null, _category, null, message);
            // }
        }

        public void Warning(string message, Exception e, params object[] args)
        {
            _logger.Warning(e, message, args);

            // if (Level >= LogLevel.Warning)
            // {
            //     Log.Write(LogMessageSeverity.Warning, _name, _framesToSkip, e, LogWriteMode.Queued, null, _category, null, message, args);
            // }
        }

        public void Verbose(string message)
        {
            _logger.Verbose(message);

            // if (Level >= LogLevel.Verbose)
            // {
            //     Log.Write(LogMessageSeverity.Verbose, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message);
            // }
        }

        public void Verbose(string message, params object[] args)
        {
            _logger.Verbose(message, args);
            //
            // if (Level >= LogLevel.Verbose)
            // {
            //     Log.Write(LogMessageSeverity.Verbose, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message, args);
            // }
        }

        public void Info(string message)
        {
            _logger.Information(message);

            // if (Level >= LogLevel.Info)
            // {
            //     Log.Write(LogMessageSeverity.Information, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message);
            // }
        }

        public void Info(string message, params object[] args)
        {
            _logger.Information(message, args);

            // if (Level >= LogLevel.Info)
            // {
            //     Log.Write(LogMessageSeverity.Information, _name, _framesToSkip, null, LogWriteMode.Queued, null, _category, null, message, args);
            // }
        }

        public void Critical(string message, Exception e)
        {
            _logger.Error(e, message);

            // if (Level >= LogLevel.Critical)
            // {
            //     Log.Write(LogMessageSeverity.Critical, _name, _framesToSkip, e, LogWriteMode.Queued, null, _category, null, message);
            // }
        }

        public void Critical(string message, Exception e, params object[] args)
        {
            _logger.Error(e, message, args);

            // if (Level >= LogLevel.Critical)
            // {
            //     Log.Write(LogMessageSeverity.Critical, _name, _framesToSkip, e, LogWriteMode.Queued, null, _category, null, message, args);
            // }
        }
    }
}

