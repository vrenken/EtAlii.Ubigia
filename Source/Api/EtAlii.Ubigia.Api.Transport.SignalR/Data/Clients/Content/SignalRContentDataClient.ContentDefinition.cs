// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.SignalR;

using System.Threading.Tasks;

internal partial class SignalRContentDataClient
{
    public async Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition)
    {
        await _invoker.Invoke(_contentDefinitionConnection, SignalRHub.ContentDefinition, "Post", identifier, contentDefinition).ConfigureAwait(false);

        MarkAsStored(contentDefinition);
    }

    public async Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart)
    {
        await _invoker.Invoke(_contentDefinitionConnection, SignalRHub.ContentDefinition, "PostPart", identifier, contentDefinitionPart.Id, contentDefinitionPart).ConfigureAwait(false);

        MarkAsStored(contentDefinitionPart);
    }

    public async Task<ContentDefinition> RetrieveDefinition(Identifier identifier)
    {
        return await _invoker.Invoke<ContentDefinition>(_contentDefinitionConnection, SignalRHub.ContentDefinition, "Get", identifier).ConfigureAwait(false);
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
