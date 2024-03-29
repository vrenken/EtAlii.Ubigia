﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System.Threading.Tasks;

internal partial class SystemContentDataClient
{
    /// <inheritdoc />
    public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
    {
        await _functionalContext.ContentDefinition.Store(identifier, contentDefinition).ConfigureAwait(false);
        MarkAsStored(contentDefinition);

        //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString())
        //await _client.Post(address, contentDefinition)

        //MarkAsStored(contentDefinition)
    }

    /// <inheritdoc />
    public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
    {
        await _functionalContext.ContentDefinition
            .Store(identifier, contentDefinitionPart)
            .ConfigureAwait(false);
        MarkAsStored(contentDefinitionPart);

        //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString(), UriParameter.ContentDefinitionPartId, contentDefinitionPart.Id.ToString())
        //await _client.Post(address, contentDefinitionPart)

        //MarkAsStored(contentDefinitionPart)
    }

    /// <inheritdoc />
    public async Task<ContentDefinition> RetrieveDefinition(Identifier identifier)
    {
        return await _functionalContext.ContentDefinition
            .Get(identifier)
            .ConfigureAwait(false);

        //var address = _addressFactory.Create(DataConnection.Storage, RelativeUri.ContentDefinition, UriParameter.EntryId, identifier.ToString())
        //var contentDefinition = await _client.Get<ContentDefinition>(address)
        //return contentDefinition
    }

    private void MarkAsStored(ContentDefinition contentDefinition)
    {
        Blob.SetStored(contentDefinition, true);

        foreach (var contentDefinitionPart in contentDefinition.Parts)
        {
            MarkAsStored(contentDefinitionPart);
        }
    }

    private void MarkAsStored(ContentDefinitionPart contentDefinitionPart)
    {
        BlobPart.SetStored(contentDefinitionPart, true);
    }
}
