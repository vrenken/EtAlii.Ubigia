namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Core;

    public partial class UserEntryService
    {
        // Get a new prepared entry for the specified spaceId
        public override Task<EntrySingleResponse> Post(EntryPostRequest request, ServerCallContext context)
        {
            var spaceId = request.SpaceId.ToLocal();
            var entry = _items.Prepare(spaceId);
            var response = new EntrySingleResponse
            {
                Entry = entry.ToWire()
            };
            return Task.FromResult(response);
        }
    }
}
