//namespace EtAlii.Servus.Api.Functional
//{
//    internal class VariableSubjectFunctionParameterConverter2 : IVariableSubjectFunctionParameterConverter2
//    {
//        private readonly IProcessingContext _context;

//        public VariableSubjectFunctionParameterConverter2(IProcessingContext context)
//        {
//            _context = context;
//        }

//        public object Convert(FunctionSubjectArgument argument)
//        {
//            var variableSubjectArgument = (VariableFunctionSubjectArgument)argument;

//            object result = null;
//            var variableName = variableSubjectArgument.Name;
//            ScopeVariable variable;
//            if (_context.Scope.Variables.TryGetValue(variableName, out variable))
//            {
//                result = variable.Value;
//            }
//            else
//            {
//                //string message = String.Format("Variable {0} not set (subject: {0})", variableName, parameters.Target.ToString());
//                //throw new ScriptParserException(message);
//            }
//            return result;
//        }
//    }
//}
