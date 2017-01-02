namespace EtAlii.Servus.Api.Functional
{
    internal class AssignVariableToFunctionOperatorSubProcessor : IAssignVariableToFunctionOperatorSubProcessor
    {
        private readonly IFunctionSubjectParameterConverterSelector _parameterConverterSelector;
        private readonly IFunctionContext _functionContext;

        public AssignVariableToFunctionOperatorSubProcessor(
            IFunctionSubjectParameterConverterSelector parameterConverterSelector,
            IFunctionContext functionContext)
        {
            _parameterConverterSelector = parameterConverterSelector;
            _functionContext = functionContext;
        }

        public void Assign(OperatorParameters parameters)
        {
            var functionSubject = (FunctionSubject)parameters.LeftSubject;

            var argumentSet = functionSubject.GetArgumentSet(_parameterConverterSelector);

            // And one single parameter set with the exact same parameters.
            var parameterSet = functionSubject.FindParameterSet(argumentSet);

            functionSubject.FunctionHandler.Process(_functionContext, parameterSet, argumentSet, parameters.RightInput, parameters.Scope, parameters.Output, false);
        }
    }
}