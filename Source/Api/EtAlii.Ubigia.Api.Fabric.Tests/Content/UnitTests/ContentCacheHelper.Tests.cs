// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Tests;

using EtAlii.Ubigia.Api.Fabric;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class ContentCacheHelperTests
{
    [Fact]
    public void ContentCacheHelper_Create()
    {
        // Arrange.
        var cacheProvider = new ContentCacheProvider();

        // Act.
        var helper = new ContentCacheHelper(cacheProvider);

        // Assert.
        Assert.NotNull(helper);
    }
}
