// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Logical;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public sealed class ScriptProcessorRootedPathMediaPathTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly IScriptParser _parser;
    private readonly FunctionalUnitTestContext _testContext;

    public ScriptProcessorRootedPathMediaPathTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
        _parser = testContext.CreateScriptParser();
    }

    [Fact]
    public async Task ScriptProcessor_RootedPath_Media_Select_ManufacturerDeviceModelDeviceTypeId_Path()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var logicalOptions = await _testContext.Logical
            .CreateLogicalOptionsWithConnection(true)
            .ConfigureAwait(false);
        var addQueries = new[]
        {
            "Media:+=Canon/PowerShot/Gtx123/000",
        };

        var addQuery = string.Join("\r\n", addQueries);
        var selectQuery1 = "/Media/Canon/PowerShot/Gtx123/000";
        var selectQuery2 = "Media:Canon/PowerShot/Gtx123/000";

        var addScript = _parser.Parse(addQuery, scope).Script;
        var selectScript1 = _parser.Parse(selectQuery1, scope).Script;
        var selectScript2 = _parser.Parse(selectQuery2, scope).Script;

        var processor = _testContext.CreateScriptProcessor(logicalOptions);

        // Act.
        var lastSequence = await processor.Process(addScript, scope);
        var addResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
        lastSequence = await processor.Process(selectScript1, scope);
        var firstResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();
        lastSequence = await processor.Process(selectScript2, scope);
        var secondResult = await lastSequence.Output.Cast<Node>().SingleOrDefaultAsync();

        // Assert.
        Assert.NotNull(addResult);
        Assert.NotNull(firstResult);
        Assert.NotNull(secondResult);
        Assert.Equal(addResult.Id, firstResult.Id);
        Assert.Equal(addResult.Id, secondResult.Id);
    }
}
