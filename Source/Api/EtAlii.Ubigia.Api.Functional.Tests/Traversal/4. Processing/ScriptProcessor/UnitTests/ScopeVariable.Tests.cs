// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;

public sealed class ScopeVariableTests
{
    [Fact]
    public async Task ScopeVariable_New()
    {
        // Arrange.

        // Act.
        var variable = new ScopeVariable("value", "source");

        // Assert.
        Assert.Equal("value", await variable.Value.SingleAsync());
        Assert.Equal("source", variable.Source);
        Assert.Equal(typeof(string), await variable.Value.Select(v => v.GetType()).SingleAsync());
    }

    [Fact]
    public void ScopeVariable_New_From_Null()
    {
        // Arrange.
        ScopeVariable variable = null;

        // Act.
        var act = new Action(() =>
        {
            variable = new ScopeVariable(null, "source");
        });

        // Assert.
        Assert.Throws<ArgumentNullException>(act);
        Assert.Null(variable);
    }

    [Fact]
    public async Task ScopeVariable_New_From_Observable_String()
    {
        // Arrange.

        // Act.
        var variable = new ScopeVariable(Observable.Return<object>("Test"), "source");

        // Assert.
        Assert.NotNull(await variable.Value.SingleAsync());
        Assert.Equal("source", variable.Source);
        Assert.Equal(typeof(string), await variable.Value.Select(v => v.GetType()).SingleAsync());
    }

    [Fact]
    public async Task ScopeVariable_New_From_Observable_Int()
    {
        // Arrange.

        // Act.
        var variable = new ScopeVariable(Observable.Return<object>(1), "source");

        // Assert.
        Assert.NotNull(await variable.Value.SingleAsync());
        Assert.Equal("source", variable.Source);
        Assert.Equal(typeof(int), await variable.Value.Select(v => v.GetType()).SingleAsync());
    }
}
