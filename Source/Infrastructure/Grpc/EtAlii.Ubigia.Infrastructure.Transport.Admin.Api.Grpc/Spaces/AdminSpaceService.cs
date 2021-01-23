namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public class AdminSpaceService : SpaceGrpcService.SpaceGrpcServiceBase, IAdminSpaceService
    {
		private readonly ISpaceRepository _items;

		public AdminSpaceService(ISpaceRepository items)
		{
			_items = items;
		}

        //public Space GetByName(string spaceName)
        public override Task<SpaceSingleResponse> GetSingle(SpaceSingleRequest request, ServerCallContext context)
        {
            EtAlii.Ubigia.Space space;

            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    space = _items.Get(request.Id.ToLocal());
                    break;
                case var _ when request.Space != null: // Get Item by id
                    space = _items.Get(request.Space.Id.ToLocal());
                    break;
                //case var _ when !string.IsNullOrWhiteSpace(request.Name): // Get Item by id
                //    space = _items.Get(request.a.Name)
                //    break
                default:
                    throw new InvalidOperationException("Unable to serve a Space GET client request");
            }

            var response = new SpaceSingleResponse
            {
                Space = space.ToWire()
            };
            return Task.FromResult(response);
        }

        // Get all Items
        public override async Task GetMultiple(SpaceMultipleRequest request, IServerStreamWriter<SpaceMultipleResponse> responseStream, ServerCallContext context)
        {
            var accountId = request.AccountId.ToLocal();

            var items = _items.GetAll(accountId);
            await foreach (var item in items.ConfigureAwait(false))
            {
                var response = new SpaceMultipleResponse
                {
                    Space = item.ToWire()
                };
                await responseStream.WriteAsync(response).ConfigureAwait(false);
            }
        }

        // Add item
        public override async Task<SpaceSingleResponse> Post(SpacePostSingleRequest request, ServerCallContext context)
        {
            var space = request.Space.ToLocal();
            var spaceTemplate = request.Template;
            var template = SpaceTemplate.All.Single(t => t.Name == spaceTemplate);

            space = await _items.Add(space, template).ConfigureAwait(false);

            var response = new SpaceSingleResponse
            {
                Space = space.ToWire()
            };
            return response;
        }

        // Update item
        public override Task<SpaceSingleResponse> Put(SpaceSingleRequest request, ServerCallContext context)
        {
            var space = request.Space.ToLocal();
            space = _items.Update(space.Id, space);

            var response = new SpaceSingleResponse
            {
                Space = space.ToWire()
            };
            return Task.FromResult(response);
        }

        // Delete Item
        public override Task<SpaceSingleResponse> Delete(SpaceSingleRequest request, ServerCallContext context)
        {
            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    _items.Remove(request.Id.ToLocal());
                    break;
                case var _ when request.Space != null: // Get Item by id
                    _items.Remove(request.Space.Id.ToLocal());
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Space DELETE client request");
            }

            var response = new SpaceSingleResponse
            {
                Space = request.Space
            };
            return Task.FromResult(response);
        }
    }
}
