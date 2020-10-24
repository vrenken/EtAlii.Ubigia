namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.CodeDom.Compiler;
    using Microsoft.CSharp;

    public class CodeCompiler : ICodeCompiler
    {
        public CompilerResults Compile(params string[] sources)
        {
            using (CodeDomProvider provider = new CSharpCodeProvider())
            {
                var parameters = CreateCompilerParameters();
                var result = provider.CompileAssemblyFromSource(parameters, sources);
                return result;
            }
        }

        private CompilerParameters CreateCompilerParameters()
        {
            var result = new CompilerParameters
            {
                CompilerOptions = "/target:library",
                GenerateExecutable = false,
                GenerateInMemory = true
            };

            result.ReferencedAssemblies.Add("System.dll");
            result.ReferencedAssemblies.Add("System.Core.dll");

            return result;
        }
    }
}
