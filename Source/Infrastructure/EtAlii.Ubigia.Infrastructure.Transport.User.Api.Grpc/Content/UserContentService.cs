// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Grpc;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Grpc;
using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
using EtAlii.Ubigia.Infrastructure.Functional;
using global::Grpc.Core;

public class UserContentService : ContentGrpcService.ContentGrpcServiceBase, IUserContentService
{
    private readonly IContentRepository _items;

    public UserContentService(IContentRepository items)
    {
        _items = items;
    }

    public override async Task<ContentGetResponse> Get(ContentGetRequest request, ServerCallContext context)
    {
        var entryId = request.EntryId.ToLocal();
        var content = await _items
            .Get(entryId)
            .ConfigureAwait(false);

        var response = new ContentGetResponse
        {
            Content = content.ToWire()
        };
        return response;
    }

    public override async Task<ContentPartGetResponse> GetPart(ContentPartGetRequest request, ServerCallContext context)
    {
        var entryId = request.EntryId.ToLocal();
        var contentPartId = request.ContentPartId;

        var contentPart = await _items
            .Get(entryId, contentPartId)
            .ConfigureAwait(false);
        var response = new ContentPartGetResponse
        {
            ContentPart = contentPart.ToWire()
        };
        return response;
    }

    public override async Task<ContentPostResponse> Post(ContentPostRequest request, ServerCallContext context)
    {
        var entryId = request.EntryId.ToLocal();
        var content = request.Content.ToLocal();

        await _items
            .Store(entryId, content)
            .ConfigureAwait(false);

        var response = new ContentPostResponse();
        return response;
    }

    public override async Task<ContentPartPostResponse> PostPart(ContentPartPostRequest request, ServerCallContext context)
    {
        var entryId = request.EntryId.ToLocal();
        var contentPartId = request.ContentPartId;
        var contentPart = request.ContentPart.ToLocal();

        if (contentPartId != contentPart.Id)
        {
            throw new InvalidOperationException("ContentPartId does not match");
        }

        await _items
            .Store(entryId, contentPart )
            .ConfigureAwait(false);

        var response = new ContentPartPostResponse();
        return response;
    }
}
