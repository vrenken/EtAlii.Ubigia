namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System;
    using System.Collections.Generic;

    public interface IErrorWriter
    {
        void Write(IScriptViewModel viewModel, Exception e, List<TextualError> errors);
    }
}