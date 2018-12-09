namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;

    class ErrorWriter : IErrorWriter
    {
        private readonly IStatusWriter _statusWriter;

        public ErrorWriter(IStatusWriter statusWriter)
        {
            _statusWriter = statusWriter;
        }

        public void Write(IExecutionStatusProvider statusProvider, Exception e, List<TextualError> errors)
        {
            _statusWriter.Write(statusProvider, $"Execution failed: {e.Message}");
            errors.Add(new TextualError { Text = e.Message, Line = 0, Column = 0 });
        }
    }
}