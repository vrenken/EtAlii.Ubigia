namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using System.Linq;
    using System.CodeDom.Compiler;
    using System.Reflection;

    public class CodeRunner
    {
        public object Run(CompilerResults compilerResults)
        {
            object result = null;
            var assembly = compilerResults.CompiledAssembly;

            if (assembly != null)
            {
                string className = "ExpressionEvaluator";
                object instance = assembly.CreateInstance("Lab.ExpressionEvaluator");

                Module[] modules = assembly.GetModules(false);

                var type = (from t in modules[0].GetTypes()
                             where t.Name == className
                             select t).FirstOrDefault();

                MethodInfo method = (from m in type.GetMethods()
                                     where m.Name == "Evaluate"
                                     select m).FirstOrDefault();

                result = method.Invoke(instance, null);
            }
            else
            {
                throw new CodeRunningException("Unable to load Evaluator assembly");
            }

            return result;
        }
    }
}
