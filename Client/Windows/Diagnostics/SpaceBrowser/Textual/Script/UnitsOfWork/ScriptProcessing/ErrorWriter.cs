namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Shapes;
    using Xceed.Wpf.DataGrid;

    class ErrorWriter : IErrorWriter
    {
        private readonly IStatusWriter _statusWriter;

        public ErrorWriter(IStatusWriter statusWriter)
        {
            _statusWriter = statusWriter;
        }

        public void Write(ScriptViewModel viewModel, Exception e, List<TextualError> errors)
        {
            _statusWriter.Write(viewModel, $"Execution failed: {e.Message}");
            errors.Add(new TextualError { Text = e.Message, Line = 0, Column = 0 });
        }
    }
}