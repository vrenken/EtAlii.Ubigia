namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;

    internal partial class SubjectProcessingScaffolding
    {
        private readonly IFunctionHandlersProvider _functionHandlersProvider;

        private IFunctionHandlersProvider GetFunctionHandlersProvider(Container container)
        {

            var defaultFunctionHandlers = container.GetInstance<IFunctionHandlerFactory>().CreateDefaults();

            var functionHandlers = defaultFunctionHandlers
                .Concat(_functionHandlersProvider.FunctionHandlers)
                .ToArray();

            var doubles = functionHandlers
                .GroupBy(fh => fh.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToArray();
            if (doubles.Any())
            {
                var message = $"Double registered function handlers detected: {String.Join(", ", doubles)}";
                throw new ScriptParserException(message);   
            }

            return new FunctionHandlersProvider(functionHandlers); 
        }
    }
}
