// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

public interface IContentPartCalculator
{
    ulong GetRequiredParts(ulong totalBytes);
    ulong GetPartSize(ulong totalBytes);
    ulong GetPart(ulong totalBytes, ulong positionInBytes);
}
