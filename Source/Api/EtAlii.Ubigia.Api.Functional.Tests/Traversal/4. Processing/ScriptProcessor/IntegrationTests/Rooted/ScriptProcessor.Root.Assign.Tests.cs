﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Logical;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.MicroContainer;
using Xunit;

[CorrelateUnitTests]
public sealed class ScriptProcessorRootAssignTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly IScriptParser _parser;
    private readonly FunctionalUnitTestContext _testContext;

    public ScriptProcessorRootAssignTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
        _parser = testContext.CreateScriptParser();
    }


    [Fact]
    public async Task ScriptProcessor_Root_Assign_Time_Root_Existing()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        const string query = "root:time <= EtAlii.Ubigia.Roots.Time";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var act = new Func<Task>(async () =>
        {
            var lastSequence = await processor.Process(script, scope);
            var _ = await lastSequence.Output.ToArray();
        });

        // Assert.
        await Assert.ThrowsAsync<InvalidOperationException>(act).ConfigureAwait(false);
    }

    [Fact]
    public async Task ScriptProcessor_Root_Assign_Time_Root()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        const string query = "root:time2 <= EtAlii.Ubigia.Roots.Time";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        var root = await _testContext
            .GetRoot(logicalContext, "time2")
            .ConfigureAwait(false);
        Assert.NotNull(script);
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.NotNull(root);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal("time2", root.Name, StringComparer.OrdinalIgnoreCase);
        Assert.Equal(new RootType("EtAlii.Ubigia.Roots.Time"), root.Type);
    }

    [Fact]
    public async Task ScriptProcessor_Root_Assign_Time_Root_And_Using_Short_RootType()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        const string query = "root:time2 <= Time";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        var root = await _testContext
            .GetRoot(logicalContext, "time2")
            .ConfigureAwait(false);
        Assert.NotNull(script);
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.NotNull(root);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal("time2", root.Name, StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ScriptProcessor_Root_Assign_Time_Root_Under_Other_Name()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        const string query = "root:specialtime <= EtAlii.Ubigia.Roots.Time";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        var root = await _testContext
            .GetRoot(logicalContext, "specialtime")
            .ConfigureAwait(false);
        Assert.NotNull(script);
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.NotNull(root);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal("specialtime", root.Name, StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ScriptProcessor_Root_Assign_Time_Root_Under_Other_Name_And_Using_Short_RootType()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        const string query = "root:specialtime <= Time";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        var root = await _testContext
            .GetRoot(logicalContext, "specialtime")
            .ConfigureAwait(false);
        Assert.NotNull(script);
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.NotNull(root);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal("specialtime", root.Name, StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ScriptProcessor_Root_Assign_Object_Root_Under_Other_Name()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        const string query = "root:projects <= EtAlii.Ubigia.Roots.Object";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        var root = await _testContext
            .GetRoot(logicalContext, "projects")
            .ConfigureAwait(false);
        Assert.NotNull(script);
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.NotNull(root);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal("projects", root.Name, StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ScriptProcessor_Root_Assign_Object_Root_Under_Other_Name_And_Using_Short_RootType()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        const string query = "root:projects <= Object";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        var root = await _testContext
            .GetRoot(logicalContext, "projects")
            .ConfigureAwait(false);
        Assert.NotNull(script);
        Assert.NotNull(result);
        Assert.Empty(result);
        Assert.NotNull(root);
        // RCI2022: We want to make roots case insensitive.
        Assert.Equal("projects", root.Name, StringComparer.OrdinalIgnoreCase);
    }
}
