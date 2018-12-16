namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    public interface ICodeCompilerResultsParser
    {
        IEnumerable<TextualError> Parse(CompilerResults results);
    }
}