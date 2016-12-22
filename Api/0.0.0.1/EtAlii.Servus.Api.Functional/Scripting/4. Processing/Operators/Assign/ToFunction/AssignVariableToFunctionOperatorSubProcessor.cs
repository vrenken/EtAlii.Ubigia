namespace EtAlii.Servus.Api.Functional
{
    internal class AssignVariableToFunctionOperatorSubProcessor : IAssignVariableToFunctionOperatorSubProcessor
    {
        private readonly IFunctionContext _functionContext;
        private readonly IParameterSetFinder _parameterSetFinder;
        private readonly IFunctionHandlerFinder _functionHandlerFinder;
        private readonly IArgumentSetFinder _argumentSetFinder;

        public AssignVariableToFunctionOperatorSubProcessor(
            IFunctionContext functionContext,
            IFunctionHandlerFinder functionHandlerFinder, 
            IParameterSetFinder parameterSetFinder, 
            IArgumentSetFinder argumentSetFinder)
        {
            _functionContext = functionContext;
            _functionHandlerFinder = functionHandlerFinder;
            _parameterSetFinder = parameterSetFinder;
            _argumentSetFinder = argumentSetFinder;
        }

        public void Assign(OperatorParameters parameters)
        {
            var functionSubject = (FunctionSubject)parameters.LeftSubject;

            // Find matching argument set.
            var argumentSet = _argumentSetFinder.Find(functionSubject);
            // Find function handler.
            var functionHandler = _functionHandlerFinder.Find(functionSubject);
            // And one single parameter set with the exact same parameters.
            var parameterSet = _parameterSetFinder.Find(functionSubject, functionHandler, argumentSet);

            functionHandler.Process(_functionContext, parameterSet, argumentSet, parameters.RightInput, parameters.Scope, parameters.Output, false);
        }
    }
}