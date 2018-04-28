namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.CodeDom.Compiler;

    public interface ICodeCompiler
    {
        CompilerResults Compile(params string[] sources);
    }
}