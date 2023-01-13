// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public sealed class ScriptProcessorNonRootedPathFunctionRename : IClassFixture<FunctionalUnitTestContext>
{
    private readonly IScriptParser _parser;
    private readonly FunctionalUnitTestContext _testContext;

    public ScriptProcessorNonRootedPathFunctionRename(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
        _parser = testContext.CreateScriptParser();
    }

    [Fact]
    public async Task ScriptProcessor_NonRootedPath_Function_Rename_Path_01()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);
        const string query = "/Person += Doe/John\r\n$path <= /Person/Doe/John\r\nrename($path, 'Jane')";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.IsType<object[]>(result);
        Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
        Assert.Equal("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
    }

    [Fact]
    public async Task ScriptProcessor_NonRootedPath_Function_Rename_Path_02()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);
        const string query = "/Person += Doe/John\r\n$path <= /Person/Doe/John\r\nrename(/Person/Doe/John, 'Jane')";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.IsType<object[]>(result);
        Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
        Assert.Equal("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
    }

    [Fact]
    public async Task ScriptProcessor_NonRootedPath_Function_Rename_Path_03()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);
        const string query = "/Person += Doe/John\r\n$jane <= 'Jane'\r\n$path <= /Person/Doe/John\r\nrename(/Person/Doe/John, $jane)";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.IsType<object[]>(result);
        Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
        Assert.Equal("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
    }

    [Fact]
    public async Task ScriptProcessor_NonRootedPath_Function_Rename_Path_04()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);
        const string query = "/Person += Doe/John\r\n$jane <= 'Jane'\r\n$path <= /Person/Doe/John\r\nrename($path, $jane)";
        var script = _parser.Parse(query, scope).Script;
        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(script, scope);
        var result = await lastSequence.Output.ToArray();

        // Assert.
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.IsType<object[]>(result);
        Assert.IsAssignableFrom<IReadOnlyEntry>(result[0]);
        Assert.Equal("Jane", result.Cast<IReadOnlyEntry>().Single().Type);
    }

}
