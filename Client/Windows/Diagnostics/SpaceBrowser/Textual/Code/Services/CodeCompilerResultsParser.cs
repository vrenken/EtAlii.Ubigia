namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;

    public class CodeCompilerResultsParser : ICodeCompilerResultsParser
    {
        public IEnumerable<TextualError> Parse(CompilerResults results)
        {
            return results.Errors.Cast<CompilerError>().Select(e =>
                    new TextualError
                    {
                        Text = e.ErrorText,
                        Line = e.Line,
                        Column = e.Column,
                    });
        }
    }
}
