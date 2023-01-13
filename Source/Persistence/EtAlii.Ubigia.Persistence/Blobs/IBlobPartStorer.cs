// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

public interface IBlobPartStorer
{
    void Store(ContainerIdentifier container, BlobPart blobPart);
}
