// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System.Threading.Tasks;

public interface IContentDefinitionRepository
{
    Task Store(Identifier identifier, ContentDefinition contentDefinition);
    Task Store(Identifier identifier, ContentDefinitionPart contentDefinitionPart);
    Task<ContentDefinition> Get(Identifier identifier);
}
