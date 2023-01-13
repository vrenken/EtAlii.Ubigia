// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class ScriptParserFunctionRenameTests : IClassFixture<FunctionalUnitTestContext>, IDisposable
{
    private IScriptParser _parser;

    public ScriptParserFunctionRenameTests(FunctionalUnitTestContext testContext)
    {
        _parser = testContext.CreateScriptParser();
    }

    public void Dispose()
    {
        _parser = null;
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void ScriptParser_Rename()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var scriptText = new[]
        {
            "$v0 <= /Documents/Files+=/Images",
            "$v1 <= rename($v0, 'Photos')",
            "/Documents/Files",
        };

        // Act.
        var script = _parser.Parse(scriptText, scope).Script;

        // Assert.
        Assert.NotNull(script);
        //Assert.NotNull(script)
        //Assert.True(script.Sequences.Count() == 1)
    }
}
