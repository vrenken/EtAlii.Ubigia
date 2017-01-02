namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Reactive.Linq;

    internal class FunctionSubjectProcessor : IFunctionSubjectProcessor
    {
        private readonly IFunctionSubjectParameterConverterSelector _parameterConverterSelector;
        private readonly IFunctionContext _functionContext;

        public FunctionSubjectProcessor(
            IFunctionSubjectParameterConverterSelector parameterConverterSelector,
            IFunctionContext functionContext)
        {
            _parameterConverterSelector = parameterConverterSelector;
            _functionContext = functionContext;
        }

        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var functionSubject = (FunctionSubject)subject;

            var argumentSet = functionSubject.GetArgumentSet(_parameterConverterSelector);

            //if(argumentSet.Arguments.Any(a => a == null))
            //{
            //    throw new ScriptProcessingException("Empty arguments are not supported");
            //}

            // And one single parameter set with the exact same parameters.
            var parameterSet = functionSubject.FindParameterSet(argumentSet);

            functionSubject.FunctionHandler.Process(_functionContext, parameterSet, argumentSet, Observable.Empty<object>(), scope, output, true);
        }
    }
}
