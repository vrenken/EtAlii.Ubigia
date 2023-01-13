// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System.Threading.Tasks;

public interface IContentRepository
{
    Task Store(Identifier identifier, Content content, ContentPart[] contentParts);
    Task Store(Identifier identifier, Content content);
    Task Store(Identifier identifier, ContentPart contentPart);
    Task<Content> Get(Identifier identifier);
    Task<ContentPart> Get(Identifier identifier, ulong contentPartId);
}
