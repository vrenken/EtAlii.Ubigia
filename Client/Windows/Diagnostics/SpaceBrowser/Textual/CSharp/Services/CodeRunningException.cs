namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;

    public class CodeRunningException : Exception
    {
        public CodeRunningException(string message)
            : base(message)
        {
        }
    }
}