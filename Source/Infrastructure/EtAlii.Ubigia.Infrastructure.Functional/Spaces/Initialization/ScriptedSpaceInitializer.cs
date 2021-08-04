// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Logical;

    internal class ScriptedSpaceInitializer : ISpaceInitializer
    {
        private readonly ISystemConnectionCreationProxy _systemConnectionCreationProxy;

        public ScriptedSpaceInitializer(ISystemConnectionCreationProxy systemConnectionCreationProxy)
        {
            _systemConnectionCreationProxy = systemConnectionCreationProxy;
        }

        public async Task Initialize(Space space, SpaceTemplate template)
        {
            var systemConnection = _systemConnectionCreationProxy.Request();
            var managementConnection = await systemConnection.OpenManagementConnection().ConfigureAwait(false);
            var spaceConnection = await managementConnection.OpenSpace(space).ConfigureAwait(false);

            var options = new FunctionalContextOptions()
                .UseAntlrTraversalParser()
                .UseCaching(true)
                .UseTraversalCaching(true)
                .Use(spaceConnection);
            var scriptContext = new TraversalContextFactory().Create(options);

            var rootsToCreate = template.RootsToCreate;

            foreach (var rootToCreate in rootsToCreate)
            {
                var scope = new ScriptScope();
                var createScript = scriptContext.Parse($"root:{rootToCreate} <= Object");
                var processingResult = await scriptContext.Process(createScript.Script, scope);
                await processingResult.Output.LastOrDefaultAsync();
            }
        }
    }
}
