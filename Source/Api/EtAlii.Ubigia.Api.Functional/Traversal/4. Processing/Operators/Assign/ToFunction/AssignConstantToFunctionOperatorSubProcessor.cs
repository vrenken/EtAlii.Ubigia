// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System.Threading.Tasks;

    internal class AssignConstantToFunctionOperatorSubProcessor : IAssignConstantToFunctionOperatorSubProcessor
    {
        private readonly IFunctionContext _functionContext;
        private readonly IParameterSetFinder _parameterSetFinder;
        private readonly IFunctionHandlerFinder _functionHandlerFinder;
        private readonly IArgumentSetFinder _argumentSetFinder;

        public AssignConstantToFunctionOperatorSubProcessor(
            IFunctionContext functionContext,
            IParameterSetFinder parameterSetFinder,
            IFunctionHandlerFinder functionHandlerFinder,
            IArgumentSetFinder argumentSetFinder)
        {
            _functionContext = functionContext;
            _parameterSetFinder = parameterSetFinder;
            _functionHandlerFinder = functionHandlerFinder;
            _argumentSetFinder = argumentSetFinder;
        }

        public Task Assign(OperatorParameters parameters)
        {
            var functionSubject = (FunctionSubject)parameters.LeftSubject;

            // Find matching argument set.
            var argumentSet = _argumentSetFinder.Find(functionSubject, parameters.Scope);
            // Find function handler.
            var functionHandler = _functionHandlerFinder.Find(functionSubject);
            // And one single parameter set with the exact same parameters.
            var parameterSet = _parameterSetFinder.Find(functionSubject, functionHandler, argumentSet);

            functionHandler.Process(_functionContext, parameterSet, argumentSet, parameters.RightInput, parameters.Scope, parameters.Output, false);
            return Task.CompletedTask;
        }
    }
}
