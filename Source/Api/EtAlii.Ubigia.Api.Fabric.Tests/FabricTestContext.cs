// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class FabricTestContext : IFabricTestContext
    {
        public ITransportTestContext Transport { get; }

        public IConfigurationRoot ClientConfiguration => Transport.Host.ClientConfiguration;
        public IConfigurationRoot HostConfiguration => Transport.Host.HostConfiguration;

        public FabricTestContext(ITransportTestContext<InProcessInfrastructureHostTestContext> transport)
        {
            Transport = transport;
        }

        public async Task<FabricOptions> CreateFabricOptions(bool openOnCreation)
        {
            var fabricOptions = await Transport
                .CreateDataConnectionToNewSpace(openOnCreation)
                .UseFabricContext()
                .UseDiagnostics()
                .ConfigureAwait(false);
            return fabricOptions;
        }

        public async Task<Tuple<IEditableEntry, string[]>> CreateHierarchy(IFabricContext fabric, IEditableEntry parent, int depth)//, out string[] hierarchy)
        {
            var hierarchy = new string[depth];
            for (var i = 0; i < depth; i++)
            {
                hierarchy[i] = Guid.NewGuid().ToString();
            }

            var entry = await CreateHierarchy(fabric, parent, hierarchy).ConfigureAwait(false);

            return new Tuple<IEditableEntry, string[]>(entry, hierarchy);
        }

        private async Task<IEditableEntry> CreateHierarchy(IFabricContext fabric, IEditableEntry parent, params string[] hierarchy)
        {
            var scope = new ExecutionScope();

            foreach (var child in hierarchy)
            {
                var previousLink = await fabric.Entries
                    .GetRelated(parent.Id, EntryRelations.Child, scope)
                    .SingleOrDefaultAsync(e => e.Type == EntryType.Add)
                    .ConfigureAwait(false);

                var updatedParent = await fabric.Entries
                    .Prepare()
                    .ConfigureAwait(false);
                updatedParent.Type = parent.Type;
                updatedParent.Downdate = Relation.NewRelation(parent.Id);
                updatedParent = (IEditableEntry)await fabric.Entries
                    .Change(updatedParent, scope)
                    .ConfigureAwait(false);

                var linkEntry = await fabric.Entries.Prepare().ConfigureAwait(false);
                linkEntry.Parent = Relation.NewRelation(updatedParent.Id);

                if (previousLink != null)
                {
                    linkEntry.Downdate = Relation.NewRelation(previousLink.Id);
                }
                linkEntry.Type = EntryType.Add;
                linkEntry = (IEditableEntry)await fabric.Entries
                    .Change(linkEntry, scope)
                    .ConfigureAwait(false);

                var childEntry = await fabric.Entries
                    .Prepare()
                    .ConfigureAwait(false);
                childEntry.Type = child;
                childEntry.Parent = Relation.NewRelation(linkEntry.Id);
                parent = (IEditableEntry)await fabric.Entries
                    .Change(childEntry, scope)
                    .ConfigureAwait(false);
            }
            return parent;
        }

        public async Task Start(PortRange portRange)
        {
            await Transport
                .Start(portRange)
                .ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await Transport
                .Stop()
                .ConfigureAwait(false);
        }
    }
}
