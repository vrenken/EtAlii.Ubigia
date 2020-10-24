namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System;
    using System.CodeDom.Compiler;
    using System.Linq;

    public class CodeRunner
    {
        public object Run(CompilerResults compilerResults)
        {
            object result = null;
            var assembly = compilerResults.CompiledAssembly;

            if (assembly != null)
            {
                var className = "ExpressionEvaluator";
                var instance = assembly.CreateInstance("Lab.ExpressionEvaluator");

                var modules = assembly.GetModules(false);

                var type = (from t in modules[0].GetTypes()
                             where t.Name == className
                             select t).FirstOrDefault();

                var method = (from m in type.GetMethods()
                                     where m.Name == "Evaluate"
                                     select m).FirstOrDefault();

                result = method.Invoke(instance, null);
            }
            else
            {
                throw new InvalidOperationException("Unable to load Evaluator assembly");
            }

            return result;
        }
    }
}
