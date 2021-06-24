// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IContentDefinitionStorer
    {
        void Store(in Identifier identifier, ContentDefinition contentDefinition);
    }
}