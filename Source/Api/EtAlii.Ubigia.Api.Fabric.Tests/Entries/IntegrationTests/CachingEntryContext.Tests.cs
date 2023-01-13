// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests;

using DataConnectionStub = EtAlii.Ubigia.Api.Transport.DataConnectionStub;
using EtAlii.Ubigia.Api.Fabric;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class CachingEntryContextTests
{
    [Fact]
    public void CachingEntryContext_Create()
    {
        // Arrange.
        var entryContext = new EntryContext(new DataConnectionStub());

        // Act.
        var context = new CachingEntryContext(entryContext);

        // Assert.
        Assert.NotNull(context);
    }
}
