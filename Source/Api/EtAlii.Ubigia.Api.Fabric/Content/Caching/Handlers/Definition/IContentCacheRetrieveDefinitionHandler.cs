﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric;

using System.Threading.Tasks;

internal interface IContentCacheRetrieveDefinitionHandler
{
    Task<ContentDefinition> Handle(Identifier identifier);
}
