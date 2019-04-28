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
        
        
//        // Get all spaces for the specified accountid
//        public IEnumerable<Root> GetForSpace(Guid spaceId)
        public override Task<RootMultipleResponse> GetMultiple(RootMultipleRequest request, ServerCallContext context)
        {
            var spaceId = request.SpaceId.ToLocal();
            
            var roots = _items
                .GetAll(spaceId)
                .ToWire();
            var response = new RootMultipleResponse();
            response.Roots.AddRange(roots);

            return Task.FromResult(response);
        }

        //public Root GetByName(string rootName)
        public override Task<RootSingleResponse> GetSingle(RootSingleRequest request, ServerCallContext context)
        {
            EtAlii.Ubigia.Api.Root root;
            var spaceId = request.SpaceId.ToLocal();

            switch (request)
            {
                case var _ when request.Id != null: // Get Item by id
                    root = _items.Get(spaceId, request.Id.ToLocal());
                    break;
                case var _ when request.Root != null: // Get Item by id
                    root = _items.Get(spaceId, request.Root.Id.ToLocal());
                    break;
                case var _ when request.Name != null: // Get Item by Name
                    root = _items.Get(spaceId, request.Name);
                    break;
                default:
                    throw new InvalidOperationException("Unable to serve a Root GET client request");                
            }

            var response = new RootSingleResponse
            {
                Root = root?.ToWire()
            };
            return Task.FromResult(response);
        }

        // Add item
        public override Task<RootSingleResponse> Post(RootPostSingleRequest request, ServerCallContext context)
        {
            var root = request.Root.ToLocal();
            var spaceId = request.SpaceId.ToLocal();
            
            root = _items.Add(spaceId, root);

            var response = new RootSingleResponse
            {
                Root = root.ToWire()
            };
            return Task.FromResult(response);
        }


        // Update item
        public override Task<RootSingleResponse> Put(RootSingleRequest request, ServerCallContext context)
        {
            var root = request.Root.ToLocal();
            var spaceId = request.SpaceId.ToLocal();
            
            root = _items.Update(spaceId, root.Id, root);

            var response = new RootSingleResponse
            {
                Root = root.ToWire()
            };
            return Task.FromResult(response);
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
