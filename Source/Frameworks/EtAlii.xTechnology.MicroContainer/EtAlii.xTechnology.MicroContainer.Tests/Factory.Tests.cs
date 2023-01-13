// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.MicroContainer.Tests;

using Xunit;

public class FactoryTests
{
    [Fact]
    public void Factory_Ctor()
    {
        // Arrange.

        // Act.
        var factory = new FactoryImplementation();

        // Assert.
        Assert.NotNull(factory);
    }

    [Fact]
    public void Factory_Create()
    {
        // Arrange.
        var factory = new FactoryImplementation();

        // Act.
        var instance = factory.Create();

        // Assert.
        Assert.NotNull(instance);
        Assert.IsType<FourthParent>(instance);
    }
}
