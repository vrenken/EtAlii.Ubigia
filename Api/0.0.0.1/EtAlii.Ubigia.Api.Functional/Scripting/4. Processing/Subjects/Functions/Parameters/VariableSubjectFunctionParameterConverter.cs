namespace EtAlii.Ubigia.Api.Functional
{
    using System.Reactive.Linq;

    internal class VariableSubjectFunctionParameterConverter : IVariableSubjectFunctionParameterConverter
    {
        private readonly IProcessingContext _context;

        public VariableSubjectFunctionParameterConverter(IProcessingContext context)
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
                //string message = String.Format("Variable {0} not set (subject: {0})", variableName, parameters.Target.ToString());
                //throw new ScriptParserException(message);
            }
            return result;
        }
    }
}
