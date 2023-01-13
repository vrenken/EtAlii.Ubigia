// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Grpc;
using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Serialization;
using global::Grpc.Core;

public class UserPropertiesService : PropertiesGrpcService.PropertiesGrpcServiceBase, IUserPropertiesService
{
    private readonly IPropertiesRepository _items;
    private readonly ISerializer _serializer;

    public UserPropertiesService(IPropertiesRepository items, ISerializer serializer)
    {
        _items = items;
        _serializer = serializer;
    }

    public override Task<PropertiesGetResponse> Get(PropertiesGetRequest request, ServerCallContext context)
    {
        var entryId = request.EntryId.ToLocal();
        var propertyDictionary = _items.Get(entryId);

        var response = new PropertiesGetResponse
        {
            PropertyDictionary = propertyDictionary.ToWire(_serializer)
        };
        return Task.FromResult(response);
    }

    public override Task<PropertiesPostResponse> Post(PropertiesPostRequest request, ServerCallContext context)
    {
        var entryId = request.EntryId.ToLocal();
        var propertyDictionary = request.PropertyDictionary.ToLocal(_serializer);
        _items.Store(entryId, propertyDictionary);

        var response = new PropertiesPostResponse();
        return Task.FromResult(response);
    }
}
