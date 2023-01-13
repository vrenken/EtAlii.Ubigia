// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Logical;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.MicroContainer;
using Xunit;

[CorrelateUnitTests]
public sealed class ScriptProcessorLogicalRemoveTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;
    private readonly IScriptParser _parser;

    public ScriptProcessorLogicalRemoveTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
        _parser = testContext.CreateScriptParser();
    }

    [Fact]
    public async Task ScriptProcessor_Logical_Remove_1()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        var root = await _testContext
            .GetRoot(logicalContext, "Person")
            .ConfigureAwait(false);
        var entry = await _testContext
            .GetEntry(logicalContext, root.Identifier, scope)
            .ConfigureAwait(false);
        await _testContext.Logical
            .CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName")
            .ConfigureAwait(false);
        var selectQuery = "<= /Person/LastName/";
        var selectScript = _parser.Parse(selectQuery, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);
        var lastSequence = await processor.Process(selectScript, scope);
        dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();
        var removeScript = _parser.Parse("/Person/LastName -= SurName", scope).Script;

        // Act.
        lastSequence = await processor.Process(removeScript, scope);
        await lastSequence.Output.ToArray();
        lastSequence = await processor.Process(selectScript, scope);
        dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

        // Assert.
        Assert.NotNull(beforeResult);
        Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
        Assert.Null(afterResult);
    }

    [Fact]
    public async Task ScriptProcessor_Logical_Remove_2()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        var root = await _testContext
            .GetRoot(logicalContext, "Person")
            .ConfigureAwait(false);
        var entry = await _testContext
            .GetEntry(logicalContext, root.Identifier, scope)
            .ConfigureAwait(false);
        await _testContext.Logical
            .CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName")
            .ConfigureAwait(false);
        var selectQuery = "<= /Person/LastName/";
        var selectScript = _parser.Parse(selectQuery, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);
        var lastSequence = await processor.Process(selectScript, scope);
        dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

        // Act.
        var removeScript = _parser.Parse("/Person/LastName -= /SurName", scope).Script;
        lastSequence = await processor.Process(removeScript, scope);
        await lastSequence.Output.ToArray();
        lastSequence = await processor.Process(selectScript, scope);
        dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

        // Assert.
        Assert.NotNull(beforeResult);
        Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
        Assert.Null(afterResult);
    }

    [Fact]
    public async Task ScriptProcessor_Logical_Remove_With_Variable_1()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        var root = await _testContext
            .GetRoot(logicalContext, "Person")
            .ConfigureAwait(false);
        var entry = await _testContext
            .GetEntry(logicalContext, root.Identifier, scope)
            .ConfigureAwait(false);
        await _testContext.Logical
            .CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName")
            .ConfigureAwait(false);
        var selectQuery = "<= /Person/LastName/";
        var selectScript = _parser.Parse(selectQuery, scope).Script;
        scope = new ExecutionScope();
        var processor = _testContext.CreateScriptProcessor(logicalOptions);
        var lastSequence = await processor.Process(selectScript, scope);
        dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

        // Act.
        var removeScript = _parser.Parse("/Person/LastName -= $var", scope).Script;
        scope.Variables["var"] = new ScopeVariable("SurName", "Variable");
        lastSequence = await processor.Process(removeScript, scope);
        await lastSequence.Output.ToArray();
        lastSequence = await processor.Process(selectScript, scope);
        dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

        // Assert.
        Assert.NotNull(beforeResult);
        Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
        Assert.Null(afterResult);
    }

    [Fact]
    public async Task ScriptProcessor_Logical_Remove_With_Variable_2()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        var root = await _testContext
            .GetRoot(logicalContext, "Person")
            .ConfigureAwait(false);
        var entry = await _testContext
            .GetEntry(logicalContext, root.Identifier, scope)
            .ConfigureAwait(false);
        await _testContext.Logical
            .CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName")
            .ConfigureAwait(false);
        var selectQuery = "<= /Person/LastName/";
        var selectScript = _parser.Parse(selectQuery, scope).Script;
        scope = new ExecutionScope();
        var processor = _testContext.CreateScriptProcessor(logicalOptions);
        var lastSequence = await processor.Process(selectScript, scope);
        dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

        // Act.
        var removeScript = _parser.Parse("$var <= \"SurName\"\r\n/Person/LastName -= $var", scope).Script;
        scope.Variables["var"] = new ScopeVariable("SurName", "Variable");
        lastSequence = await processor.Process(removeScript, scope);
        await lastSequence.Output.ToArray();
        lastSequence = await processor.Process(selectScript, scope);
        dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

        // Assert.
        Assert.NotNull(beforeResult);
        Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
        Assert.Null(afterResult);
    }

    [Fact]
    public async Task ScriptProcessor_Logical_Remove_With_Variable_3()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);

#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
        await using var logicalContext = Factory.Create<ILogicalContext>(logicalOptions);
#pragma warning restore CA2007

        var root = await _testContext
            .GetRoot(logicalContext, "Person")
            .ConfigureAwait(false);
        var entry = await _testContext
            .GetEntry(logicalContext, root.Identifier, scope)
            .ConfigureAwait(false);
        await _testContext.Logical.CreateHierarchy(logicalContext, (IEditableEntry)entry, "LastName", "SurName").ConfigureAwait(false);
        var selectQuery = "<= /Person/LastName/";
        var selectScript = _parser.Parse(selectQuery, scope).Script;
        scope = new ExecutionScope();
        var processor = _testContext.CreateScriptProcessor(logicalOptions);
        var lastSequence = await processor.Process(selectScript, scope);
        dynamic beforeResult = await lastSequence.Output.SingleOrDefaultAsync();

        // Act.
        var removeScript = _parser.Parse("$var <= /Person/LastName/SurName\r\n/Person/LastName -= $var", scope).Script;
        scope.Variables["var"] = new ScopeVariable("SurName", "Variable");
        lastSequence = await processor.Process(removeScript, scope);
        await lastSequence.Output.ToArray();
        lastSequence = await processor.Process(selectScript, scope);
        dynamic afterResult = await lastSequence.Output.SingleOrDefaultAsync();

        // Assert.
        Assert.NotNull(beforeResult);
        Assert.NotEqual(Identifier.Empty, ((Node)beforeResult).Id);
        Assert.Null(afterResult);
    }
}
