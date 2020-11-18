namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;

    public partial class UserEntryService
    {
        public override Task<EntrySingleResponse> GetSingle(EntrySingleRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var entryRelations = request.EntryRelations.ToLocal();
            var entry = _items.Get(entryId, entryRelations);
            
            var response = new EntrySingleResponse
            {
                Entry = entry.ToWire()
            };
            return Task.FromResult(response);
        }

        public override async Task GetMultiple(EntryMultipleRequest request, IServerStreamWriter<EntryMultipleResponse> responseStream, ServerCallContext context)
        {
            var entryIds = request.EntryIds.ToLocal();
            var entryRelations = request.EntryRelations.ToLocal();
            var entries = _items.Get(entryIds, entryRelations); // TODO: AsyncEnumerable

            foreach (var entry in entries)
            {
                var response = new EntryMultipleResponse
                {
                    Entry = entry.ToWire()
                };
                await responseStream.WriteAsync(response);
            }
        }

        public override async Task GetRelated(EntryRelatedRequest request, IServerStreamWriter<EntryMultipleResponse> responseStream, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var entryRelations = request.EntryRelations.ToLocal();
            var entriesWithRelation = request.EntriesWithRelation.ToLocal();
            var entries = _items.GetRelated(entryId, entriesWithRelation, entryRelations);

            await foreach (var entry in entries)
            {
                var response = new EntryMultipleResponse
                {
                    Entry = entry.ToWire()
                };
                await responseStream.WriteAsync(response);
            }
        }
    }
}
