namespace EtAlii.Ubigia.Client.Windows.Diagnostics
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

        public void Write(IScriptViewModel viewModel, Exception e, List<TextualError> errors)
        {
            _statusWriter.Write(viewModel, $"Execution failed: {e.Message}");
            errors.Add(new TextualError { Text = e.Message, Line = 0, Column = 0 });
        }
    }
}