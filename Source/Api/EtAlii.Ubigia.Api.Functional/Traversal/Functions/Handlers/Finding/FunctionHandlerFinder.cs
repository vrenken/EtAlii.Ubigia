// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
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
            var functionHandler = _functionHandlersProvider.FunctionHandlers.SingleOrDefault(fhc => string.Equals(fhc.Name, functionSubject.Name, StringComparison.OrdinalIgnoreCase));
            if (functionHandler == null)
            {
                var message = $"No function found with name '{functionSubject.Name}'";
                throw new ScriptProcessingException(message);
            }
            return functionHandler;
        }

    }
}
