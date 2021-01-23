namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using global::Grpc.Core;

    public class UserRootService : RootGrpcService.RootGrpcServiceBase, IUserRootService
    {
        private readonly IRootRepository _items;

        public UserRootService(IRootRepository items)
        {
            _items = items;
        }


        /// <summary>
        /// Get all spaces for the specified account id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="responseStream"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GetMultiple(RootMultipleRequest request, IServerStreamWriter<RootMultipleResponse> responseStream, ServerCallContext context)
        {
            var spaceId = request.SpaceId.ToLocal();

            var roots = _items
                .GetAll(spaceId)
                .ConfigureAwait(false);
            await foreach (var root in roots.ConfigureAwait(false))
            {
                var response = new RootMultipleResponse
                {
                    Root = root.ToWire()
                };
                await responseStream
                    .WriteAsync(response)
                    .ConfigureAwait(false);
            }
        }

        //public Root GetByName(string rootName)
        public override async Task<RootSingleResponse> GetSingle(RootSingleRequest request, ServerCallContext context)
        {
            EtAlii.Ubigia.Root root;
            var spaceId = request.SpaceId.ToLocal();

            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    root = await _items.Get(spaceId, request.Id.ToLocal()).ConfigureAwait(false);
                    break;
                case var _ when request.Root != null: // Get Item by id
                    root = await _items.Get(spaceId, request.Root.Id.ToLocal()).ConfigureAwait(false);
                    break;
                case var _ when request.Name != null: // Get Item by Name
                    root = await _items.Get(spaceId, request.Name).ConfigureAwait(false);
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Root GET client request");
            }

            var response = new RootSingleResponse
            {
                Root = root?.ToWire()
            };
            return response;
        }

        // Add item
        public override async Task<RootSingleResponse> Post(RootPostSingleRequest request, ServerCallContext context)
        {
            var root = request.Root.ToLocal();
            var spaceId = request.SpaceId.ToLocal();

            root = await _items
                .Add(spaceId, root)
                .ConfigureAwait(false);

            var response = new RootSingleResponse
            {
                Root = root.ToWire()
            };

            return response;
        }


        // Update item
        public override async Task<RootSingleResponse> Put(RootSingleRequest request, ServerCallContext context)
        {
            var root = request.Root.ToLocal();
            var spaceId = request.SpaceId.ToLocal();

            root = await _items
                .Update(spaceId, root.Id, root)
                .ConfigureAwait(false);

            var response = new RootSingleResponse
            {
                Root = root.ToWire()
            };
            return response;
        }


        // Delete Item
        public override Task<RootSingleResponse> Delete(RootSingleRequest request, ServerCallContext context)
        {
            var spaceId = request.SpaceId.ToLocal();

            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    _items.Remove(spaceId, request.Id.ToLocal());
                    break;
                case var _ when request.Root != null: // Get Item by id
                    _items.Remove(spaceId, request.Root.Id.ToLocal());
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Root DELETE client request");
            }

            var response = new RootSingleResponse
            {
                Root = request.Root
            };
            return Task.FromResult(response);
        }
    }
}
