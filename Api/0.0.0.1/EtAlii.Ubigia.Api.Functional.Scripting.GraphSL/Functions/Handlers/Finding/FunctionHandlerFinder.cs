namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;

    internal class FunctionHandlerFinder : IFunctionHandlerFinder
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;

        public FunctionHandlerFinder(IFunctionHandlersProvider functionHandlersProvider)
        {
            _functionHandlersProvider = functionHandlersProvider;
        }

        public IFunctionHandler Find(FunctionSubject functionSubject)
        {
            var functionHandler = _functionHandlersProvider.FunctionHandlers.SingleOrDefault(fhc => String.Equals(fhc.Name, functionSubject.Name, StringComparison.OrdinalIgnoreCase));
            if (functionHandler == null)
            {
                var message = $"No function found with name '{functionSubject.Name}'";
                throw new ScriptProcessingException(message);
            }
            return functionHandler;
        }

    }
}