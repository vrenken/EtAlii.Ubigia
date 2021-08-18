// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Antlr;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Microsoft.Extensions.Configuration;

    internal class ScriptedSpaceInitializer : ISpaceInitializer
    {
        private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;
        private readonly IConfigurationRoot _configurationRoot;

        public ScriptedSpaceInitializer(ISystemConnectionCreationProxy systemConnectionCreationProxy, IConfigurationRoot configurationRoot)
        {
            _systemConnectionCreationProxy = systemConnectionCreationProxy;
            _configurationRoot = configurationRoot;
        }

        public async Task Initialize(Space space, SpaceTemplate template)
        {
            var systemConnection = _systemConnectionCreationProxy.Request();
            var managementConnection = await systemConnection.OpenManagementConnection().ConfigureAwait(false);
            var spaceConnection = await managementConnection.OpenSpace(space).ConfigureAwait(false);

            var options = new FunctionalOptions(_configurationRoot)
                .UseAntlrParsing()
                .UseCaching(true)
                .UseTraversalCaching(true)
                .Use(spaceConnection);

            var scriptContext = Factory.Create<ITraversalContext, IFunctionalExtension>(options);

            var rootsToCreate = template.RootsToCreate;

            var scope = new ExecutionScope();
            foreach (var rootToCreate in rootsToCreate)
            {
                var createScript = scriptContext.Parse($"root:{rootToCreate} <= Object", scope);
                var processingResult = await scriptContext.Process(createScript.Script, scope);
                await processingResult.Output.LastOrDefaultAsync();
            }
        }
    }
}
