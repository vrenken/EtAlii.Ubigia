namespace EtAlii.Servus.Client.Windows.Diagnostics
{
    using System;
    using System.Linq;
    using System.CodeDom.Compiler;
    using System.Reflection;
    using System.Threading;

    public class ScriptProcessor
    {
        public object Process(CompilerResults compilerResults)
        {
            object result = null;
           
            Thread.Sleep(5000);

            return result;
        }
    }
}
