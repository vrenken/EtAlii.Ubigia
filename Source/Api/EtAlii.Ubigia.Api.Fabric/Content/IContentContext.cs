// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Threading.Tasks;

public interface IContentContext
{
    Task StoreDefinition(Identifier identifier, ContentDefinition contentDefinition);
    Task StoreDefinition(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
    Task<ContentDefinition> RetrieveDefinition(Identifier identifier);

    Task Store(Identifier identifier, Content content);
    Task Store(Identifier identifier, ContentPart contentPart);
    Task<Content> Retrieve(Identifier identifier);
    Task<ContentPart> Retrieve(Identifier identifier, ulong contentPartId);
}
