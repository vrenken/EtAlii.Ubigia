namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;

    public class FabricTestContext : IFabricTestContext
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> Transport { get; }

        public FabricTestContext(ITransportTestContext<InProcessInfrastructureHostTestContext> transport)
        {
            Transport = transport;
        }

        public async Task ConfigureFabricContextConfiguration(FabricContextConfiguration fabricContextConfiguration, bool openOnCreation)
        {
            var connection = await Transport.CreateDataConnectionToNewSpace(openOnCreation).ConfigureAwait(false);
            fabricContextConfiguration.Use(connection);
        }

        public async Task<IFabricContext> CreateFabricContext(bool openOnCreation)
        {
            var connection = await Transport.CreateDataConnectionToNewSpace(openOnCreation).ConfigureAwait(false);
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection)
                .Use(DiagnosticsConfiguration.Default);
            return new FabricContextFactory().Create(fabricContextConfiguration);
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
            var scope = new ExecutionScope(false);

            foreach (var child in hierarchy)
            {
                var previousLink = await fabric.Entries
                    .GetRelated(parent.Id, EntryRelation.Child, scope)
                    .SingleOrDefaultAsync(e => e.Type == EntryType.Add)
                    .ConfigureAwait(false);
                
                var updatedParent = await fabric.Entries.Prepare().ConfigureAwait(false);
                updatedParent.Type = parent.Type;
                updatedParent.Downdate = Relation.NewRelation(parent.Id);
                updatedParent = (IEditableEntry)await fabric.Entries.Change(updatedParent, scope).ConfigureAwait(false);

                var linkEntry = await fabric.Entries.Prepare().ConfigureAwait(false);
                linkEntry.Parent = Relation.NewRelation(updatedParent.Id);

                if (previousLink != null)
                {
                    linkEntry.Downdate = Relation.NewRelation(previousLink.Id);
                }
                linkEntry.Type = EntryType.Add;
                linkEntry = (IEditableEntry)await fabric.Entries.Change(linkEntry, scope).ConfigureAwait(false);

                var childEntry = await fabric.Entries.Prepare().ConfigureAwait(false);
                childEntry.Type = child;
                childEntry.Parent = Relation.NewRelation(linkEntry.Id);
                parent = (IEditableEntry)await fabric.Entries.Change(childEntry, scope).ConfigureAwait(false);
            }
            return parent;
        }

        #region start/stop

        public async Task Start(PortRange portRange)
        {
            await Transport.Start(portRange).ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await Transport.Stop().ConfigureAwait(false);
        }

        #endregion start/stop
    }
}