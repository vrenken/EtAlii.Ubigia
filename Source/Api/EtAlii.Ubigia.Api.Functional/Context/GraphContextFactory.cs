// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphContextFactory : Factory<IGraphContext, FunctionalContextOptions, IGraphContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(FunctionalContextOptions options)
        {
            // Let's ensure that the function handler configuration is in fact valid.
            var functionHandlersProvider = options.FunctionHandlersProvider;
            var functionHandlerValidator = new FunctionHandlerValidator();
            functionHandlerValidator.Validate(functionHandlersProvider);

            // Let's ensure that the root handler configuration is in fact valid.
            var rootHandlerMappersProvider = options.RootHandlerMappersProvider;
            var rootHandlerMapperValidator = new RootHandlerMapperValidator();
            rootHandlerMapperValidator.Validate(rootHandlerMappersProvider);

            return System.Array.Empty<IScaffolding>();
            //new GraphContextScaffolding(configuration),
            //new ScriptsScaffolding(functionHandlersProvider, rootHandlerMappersProvider),

        }
    }
}
