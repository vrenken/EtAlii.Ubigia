namespace EtAlii.Ubigia.Client.Windows.Diagnostics
{
    using Microsoft.CSharp;
    using System;
    using System.Linq;
    using System.CodeDom.Compiler;
    using System.Reflection;

    public class CodeCompiler
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
            CompilerParameters result = new CompilerParameters
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
