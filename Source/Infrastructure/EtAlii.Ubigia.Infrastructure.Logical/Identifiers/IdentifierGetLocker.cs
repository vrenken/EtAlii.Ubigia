// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical;

using System.Threading;

public class IdentifierGetLocker : IIdentifierGetLocker
{
    public SemaphoreSlim LockObject { get; } = new(1, 1);
}
