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

        public override Task<EntryMultipleResponse> GetMultiple(EntryMultipleRequest request, ServerCallContext context)
        {
            var entryIds = request.EntryIds.ToLocal();
            var entryRelations = request.EntryRelations.ToLocal();
            var entries = _items.Get(entryIds, entryRelations);

            var response = new EntryMultipleResponse();
            response.Entries.AddRange(entries.ToWire());
            return Task.FromResult(response);
        }
        public override Task<EntryMultipleResponse> GetRelated(EntryRelatedRequest request, ServerCallContext context)
        {
            var entryId = request.EntryId.ToLocal();
            var entryRelations = request.EntryRelations.ToLocal();
            var entriesWithRelation = request.EntriesWithRelation.ToLocal();
            var entries = _items.GetRelated(entryId, entriesWithRelation, entryRelations);

            var response = new EntryMultipleResponse();
            response.Entries.AddRange(entries.ToWire());
            return Task.FromResult(response);
        }
    }
}
