﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Tests;
using EtAlii.Ubigia.Tests;
using Xunit;

public class TransportUnitTestContext : IAsyncLifetime
{
    public ITransportTestContext TransportTestContext { get; private set; }

    public ByteArrayComparer ByteArrayComparer { get; }
    public ContentComparer ContentComparer { get; }
    public PropertyDictionaryComparer PropertyDictionaryComparer { get; }

    public TestContentDefinitionFactory TestContentDefinitionFactory { get; }
    public TestContentFactory TestContentFactory { get; }
    public TestPropertiesFactory TestPropertiesFactory { get; }

    public TestIdentifierFactory TestIdentifierFactory { get; }

    public TransportUnitTestContext()
    {
        TestContentDefinitionFactory = new TestContentDefinitionFactory();
        TestContentFactory = new TestContentFactory();
        TestPropertiesFactory = new TestPropertiesFactory();
        TestIdentifierFactory = new TestIdentifierFactory();

        ByteArrayComparer = new ByteArrayComparer();
        ContentComparer = new ContentComparer(ByteArrayComparer);
        PropertyDictionaryComparer = new PropertyDictionaryComparer();
    }

    public async Task InitializeAsync()
    {
        TransportTestContext = new TransportTestContext().Create();
        await TransportTestContext.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        await TransportTestContext.Stop().ConfigureAwait(false);
        TransportTestContext = null;
    }
}
