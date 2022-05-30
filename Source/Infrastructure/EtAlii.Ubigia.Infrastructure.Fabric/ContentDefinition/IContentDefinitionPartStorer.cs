﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System.Threading.Tasks;

    public interface IContentDefinitionPartStorer
    {
        Task Store(in Identifier identifier, ContentDefinitionPart contentDefinitionPart);
    }
}
