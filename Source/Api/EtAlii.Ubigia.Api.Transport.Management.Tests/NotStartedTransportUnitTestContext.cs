// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests;

using System;
using EtAlii.Ubigia.Api.Tests;

public class NotStartedTransportUnitTestContext : IDisposable
{
    public ITransportTestContext Transport { get; private set; }

    public NotStartedTransportUnitTestContext()
    {
        Transport = new TransportTestContext().Create();
    }

    public void Dispose()
    {
        Transport = null;
        GC.SuppressFinalize(this);
    }
}
