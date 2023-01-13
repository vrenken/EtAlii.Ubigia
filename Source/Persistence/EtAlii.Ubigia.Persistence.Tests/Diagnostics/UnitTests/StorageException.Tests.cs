// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using System;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public class StorageExceptionTests
{
    [Fact]
    public void StorageException_Create_1()
    {
        // Arrange.
        var innerException = new Exception();
        // Act.
        var exception = new StorageException("Test message", innerException);

        // Assert.
        Assert.Equal("Test message", exception.Message);
        Assert.Equal(innerException, exception.InnerException);
    }

    [Fact]
    public void StorageException_Create_2()
    {
        // Arrange.

        // Act.
        var exception = new StorageException("Test message");

        // Assert.
        Assert.Equal("Test message", exception.Message);
    }
}
