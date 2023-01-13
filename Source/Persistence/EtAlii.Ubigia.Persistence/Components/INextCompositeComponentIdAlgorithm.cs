// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

internal interface INextCompositeComponentIdAlgorithm
{
    ulong Create(ContainerIdentifier containerIdentifier);
}
