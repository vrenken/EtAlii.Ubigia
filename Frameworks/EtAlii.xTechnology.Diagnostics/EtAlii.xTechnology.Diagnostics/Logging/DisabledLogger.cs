namespace EtAlii.xTechnology.Diagnostics
{
    public class DisabledLogger : ILogger
    {
        public LogLevel Level
        {
            get => LogLevel.None;
            set { /* No action. */ }
        }

        public void Verbose(string message)
        {
        }

        public void Verbose(string message, params object[] args)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(string message, params object[] args)
        {
        }

        public void Warning(string message)
        {
        }

        public void Warning(string message, params object[] args)
        {
        }

        public void Critical(string message, System.Exception e, params object[] args)
        {
        }

        public void Critical(string message, System.Exception e)
        {
        }
    }
}
