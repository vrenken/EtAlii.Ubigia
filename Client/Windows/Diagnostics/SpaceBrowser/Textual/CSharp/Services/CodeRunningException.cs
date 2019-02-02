namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class CodeRunningException : Exception
    {
        private CodeRunningException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public CodeRunningException(string message)
            : base(message)
        {
        }
    }
}