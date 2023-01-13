// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using Xunit;

public sealed class ParentPathSubjectPartTests
{

    [Fact]
    public void ParentPathSubjectPart_ToString()
    {
        // Arrange.
        var part = new ParentPathSubjectPart();

        // Act.
        var result = part.ToString();

        // Assert.
        Assert.Equal("/", result);
    }
}
