namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.Collections.Generic;

    public interface IErrorWriter
    {
        void Write(IExecutionStatusProvider statusProvider, Exception e, List<TextualError> errors);
    }
}