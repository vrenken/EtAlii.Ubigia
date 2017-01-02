namespace EtAlii.Servus.Api.Fabric.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport.Tests;

    public class FabricTestContext : IFabricTestContext
    {
        public ITransportTestContext Transport { get { return _transport; } }
        private readonly ITransportTestContext _transport;

        public FabricTestContext(ITransportTestContext transport)
        {
            _transport = transport;
        }

        public async Task<IFabricContext> CreateFabricContext(bool openOnCreation)
        {
            var connection = await _transport.CreateDataConnection(openOnCreation);
            var fabricContextConfiguration = new FabricContextConfiguration()
                .Use(connection);
            return new FabricContextFactory().Create(fabricContextConfiguration);
        }

        public async Task<Tuple<IEditableEntry, string[]>> CreateHierarchy(IFabricContext fabric, IEditableEntry parent, int depth)//, out string[] hierarchy)
        {
            var hierarchy = new string[depth];
            for (int i = 0; i < depth; i++)
            {
                hierarchy[i] = Guid.NewGuid().ToString();
            };

            var entry = await CreateHierarchy(fabric, parent, hierarchy);

            return new Tuple<IEditableEntry, string[]>(entry, hierarchy);
        }

        private async Task<IEditableEntry> CreateHierarchy(IFabricContext fabric, IEditableEntry parent, params string[] hierarchy)
        {
            var scope = new ExecutionScope(false);

            foreach (var child in hierarchy)
            {
                var entries = await fabric.Entries.GetRelated(parent.Id, EntryRelation.Child, scope);
                var previousLink = entries.SingleOrDefault(e => e.Type == EntryType.Add);

                
                var updatedParent = await fabric.Entries.Prepare();
                updatedParent.Type = parent.Type;
                updatedParent.Downdate = Relation.NewRelation(parent.Id);
                updatedParent = (IEditableEntry)await fabric.Entries.Change(updatedParent, scope);

                var linkEntry = await fabric.Entries.Prepare();
                linkEntry.Parent = Relation.NewRelation(updatedParent.Id);

                if (previousLink != null)
                {
                    linkEntry.Downdate = Relation.NewRelation(previousLink.Id);
                }
                linkEntry.Type = EntryType.Add;
                linkEntry = (IEditableEntry)await fabric.Entries.Change(linkEntry, scope);

                var childEntry = await fabric.Entries.Prepare();
                childEntry.Type = child;
                childEntry.Parent = Relation.NewRelation(linkEntry.Id);
                parent = (IEditableEntry)await fabric.Entries.Change(childEntry, scope);
            }
            return parent;
        }

        #region start/stop

        public async Task Start()
        {
            await _transport.Start();
        }

        public async Task Stop()
        {
            await _transport.Stop();
        }

        #endregion start/stop
    }
}