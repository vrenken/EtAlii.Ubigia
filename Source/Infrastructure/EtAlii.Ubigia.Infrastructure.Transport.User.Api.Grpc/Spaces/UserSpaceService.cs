// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Grpc;
using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
using EtAlii.Ubigia.Infrastructure.Functional;
using global::Grpc.Core;

public class UserSpaceService : SpaceGrpcService.SpaceGrpcServiceBase, IUserSpaceService
{
    private readonly ISpaceRepository _items;
    private readonly ISimpleAuthenticationTokenVerifier _authenticationTokenVerifier;

    public UserSpaceService(ISpaceRepository items, ISimpleAuthenticationTokenVerifier authenticationTokenVerifier)
    {
        _items = items;
        _authenticationTokenVerifier = authenticationTokenVerifier;
    }

    //public Space GetByName(string spaceName)
    public override async Task<SpaceSingleResponse> GetSingle(SpaceSingleRequest request, ServerCallContext context)
    {
        var authenticationTokenHeader = context.RequestHeaders.Single(h => h.Key == GrpcHeader.AuthenticationTokenHeaderKey);
        var authenticationToken = authenticationTokenHeader.Value;
        var account = await _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System).ConfigureAwait(false);
        var accountId = account.Id;

        EtAlii.Ubigia.Space space;

        switch (request)
        {
            case var _ when request.Id != null: // Get Item by id
                space = await _items.Get(request.Id.ToLocal()).ConfigureAwait(false);
                break;
            case var _ when request.Space != null: // Get Item by id
                space = await _items.Get(request.Space.Id.ToLocal()).ConfigureAwait(false);
                break;
            case var _ when request.Name != null: // Get Item by id
                space = await _items.Get(accountId, request.Name).ConfigureAwait(false);
                break;
            default:
                throw new InvalidOperationException("Unable to serve a Space GET client request");
        }

        var response = new SpaceSingleResponse
        {
            Space = space.ToWire()
        };
        return response;
    }

    // Get all Items
    public override async Task GetMultiple(SpaceMultipleRequest request, IServerStreamWriter<SpaceMultipleResponse> responseStream, ServerCallContext context)
    {
        var authenticationTokenHeader = context.RequestHeaders.Single(h => h.Key == GrpcHeader.AuthenticationTokenHeaderKey);
        var authenticationToken = authenticationTokenHeader.Value;
        var account = await _authenticationTokenVerifier.Verify(authenticationToken, Role.User, Role.System).ConfigureAwait(false);
        var accountId = account.Id;

        var items = _items
            .GetAll(accountId)
            .ConfigureAwait(false);
        await foreach (var item in items)
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

        // A user can only create data spaces.
        var template = SpaceTemplate.Data;

        space = await _items.Add(space, template).ConfigureAwait(false);

        var response = new SpaceSingleResponse
        {
            Space = space.ToWire()
        };
        return response;
    }

    // Update item
    public override async Task<SpaceSingleResponse> Put(SpaceSingleRequest request, ServerCallContext context)
    {
        var space = request.Space.ToLocal();
        space = await _items.Update(space.Id, space).ConfigureAwait(false);

        var response = new SpaceSingleResponse
        {
            Space = space.ToWire()
        };
        return response;
    }

    // Delete Item
    public override async Task<SpaceSingleResponse> Delete(SpaceSingleRequest request, ServerCallContext context)
    {
        switch (request)
        {
            case var _ when request.Id != null: // Get Item by id
                await _items.Remove(request.Id.ToLocal()).ConfigureAwait(false);
                break;
            case var _ when request.Space != null: // Get Item by id
                await _items.Remove(request.Space.Id.ToLocal()).ConfigureAwait(false);
                break;
            default:
                throw new InvalidOperationException("Unable to serve a Space DELETE client request");
        }

        var response = new SpaceSingleResponse
        {
            Space = request.Space
        };
        return response;
    }
}
