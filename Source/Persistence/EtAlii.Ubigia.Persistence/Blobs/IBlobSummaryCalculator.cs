// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence;

using System.Threading.Tasks;

public interface IBlobSummaryCalculator
{
    Task<BlobSummary> Calculate<T>(ContainerIdentifier container)
        where T: Blob;
}
