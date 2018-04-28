namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;

    public interface IErrorWriter
    {
        void Write(IScriptViewModel viewModel, Exception e, List<TextualError> errors);
    }
}