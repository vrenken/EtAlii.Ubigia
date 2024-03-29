﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Fabric;

public class ContentDefinitionQueryHandler : IContentDefinitionQueryHandler
{
    private readonly IFabricContext _fabric;

    public ContentDefinitionQueryHandler(IFabricContext fabric)
    {
        _fabric = fabric;
    }

    public async Task<ContentDefinition> Execute(ContentDefinitionQuery query)
    {
        var contentDefinition = await _fabric.Content.RetrieveDefinition(query.Identifier).ConfigureAwait(false);
        if (contentDefinition == null)
        {
            var size = query.SizeInBytes;
            var newContentDefinition = ContentDefinition.Create(0, size, Array.Empty<ContentDefinitionPart>());
            Blob.SetTotalParts(newContentDefinition, query.RequiredParts);

            await _fabric.Content.StoreDefinition(query.Identifier, newContentDefinition).ConfigureAwait(false);
            contentDefinition = newContentDefinition;
        }

        return contentDefinition;
    }
}
