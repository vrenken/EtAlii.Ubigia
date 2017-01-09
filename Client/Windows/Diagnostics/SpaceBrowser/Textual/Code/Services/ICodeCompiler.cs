namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using System.CodeDom.Compiler;

    public interface ICodeCompiler
    {
        CompilerResults Compile(params string[] sources);
    }
}