namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Reactive.Linq;

    internal class VariableSubjectFunctionParameterConverter : IVariableSubjectFunctionParameterConverter
    {
        private readonly IScriptProcessingContext _context;

        public VariableSubjectFunctionParameterConverter(IScriptProcessingContext context)
        {
            _context = context;
        }

        public object Convert(FunctionSubjectArgument argument)
        {
            var variableSubjectArgument = (VariableFunctionSubjectArgument)argument;

            object result = Observable.Empty<object>();
            var variableName = variableSubjectArgument.Name;
            ScopeVariable variable;
            if (_context.Scope.Variables.TryGetValue(variableName, out variable))
            {
                result = variable.Value;
            }
            else
            {
                //string message = string.Format("Variable [0] not set (subject: [0])", variableName, parameters.Target.ToString())
                //throw new ScriptParserException(message)
            }
            return result;
        }
    }
}
