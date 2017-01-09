namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;

    public interface ICodeCompilerResultsParser
    {
        IEnumerable<TextualError> Parse(CompilerResults results);
    }
}