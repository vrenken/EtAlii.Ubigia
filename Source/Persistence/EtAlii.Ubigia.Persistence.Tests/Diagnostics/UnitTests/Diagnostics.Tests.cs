﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public class DiagnosticsTests
{
    [Fact]
    public void LoggingBlobPartRetriever_Create()
    {
        // Arrange.

        // Act.
        var loggingBlobPartRetriever = new LoggingBlobPartRetriever(null);

        // Assert.
        Assert.NotNull(loggingBlobPartRetriever);
    }
}
