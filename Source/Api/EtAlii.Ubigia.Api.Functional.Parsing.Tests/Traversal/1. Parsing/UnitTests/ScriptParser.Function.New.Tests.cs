// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System;
using System.Linq;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class ScriptParserFunctionNewTests : IClassFixture<FunctionalUnitTestContext>, IDisposable
{
    private IScriptParser _parser;

    public ScriptParserFunctionNewTests(FunctionalUnitTestContext testContext)
    {
        _parser = testContext.CreateScriptParser();
    }

    public void Dispose()
    {
        _parser = null;
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void ScriptParser_New_Blank()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var scriptText = new[]
        {
            "Person:Doe/John/Visits += new()",
        };

        // Act.
        var result = _parser.Parse(scriptText, scope);

        // Assert.
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var script = result.Script;
        Assert.NotNull(script);
        var sequence = script.Sequences.FirstOrDefault();
        Assert.NotNull(sequence);
        Assert.Equal(3, sequence.Parts.Length);
        Assert.IsType<RootedPathSubject>(sequence.Parts[0]);
        Assert.IsType<AddOperator>(sequence.Parts[1]);
        Assert.IsType<FunctionSubject>(sequence.Parts[2]);
        var functionSubject = (FunctionSubject)sequence.Parts[2];
        Assert.Equal("new", functionSubject.Name);
        Assert.Empty(functionSubject.Arguments);
    }


    [Fact]
    public void ScriptParser_New_Argument_01()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var scriptText = new[]
        {
            "Person:Doe/John/Visits += new('Vacation')",
        };

        // Act.
        var result = _parser.Parse(scriptText, scope);

        // Assert.
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var script = result.Script;
        Assert.NotNull(script);
        var sequence = script.Sequences.FirstOrDefault();
        Assert.NotNull(sequence);
        Assert.Equal(3, sequence.Parts.Length);
        Assert.IsType<RootedPathSubject>(sequence.Parts[0]);
        Assert.IsType<AddOperator>(sequence.Parts[1]);
        Assert.IsType<FunctionSubject>(sequence.Parts[2]);
        var functionSubject = (FunctionSubject)sequence.Parts[2];
        Assert.Equal("new", functionSubject.Name);
        Assert.NotEmpty(functionSubject.Arguments);
        Assert.IsType<ConstantFunctionSubjectArgument>(functionSubject.Arguments[0]);
        var argument = (ConstantFunctionSubjectArgument)functionSubject.Arguments[0];
        Assert.Equal("Vacation", argument.Value);
    }

    [Fact]
    public void ScriptParser_New_Argument_02()
    {
        // Arrange.
        var scope = new ExecutionScope();
        var scriptText = new[]
        {
            "Person:Doe/John/Visits += new(\"Vacation\")",
        };

        // Act.
        var result = _parser.Parse(scriptText, scope);

        // Assert.
        Assert.NotNull(result);
        Assert.Empty(result.Errors);
        var script = result.Script;
        Assert.NotNull(script);
        var sequence = script.Sequences.FirstOrDefault();
        Assert.NotNull(sequence);
        Assert.Equal(3, sequence.Parts.Length);
        Assert.IsType<RootedPathSubject>(sequence.Parts[0]);
        Assert.IsType<AddOperator>(sequence.Parts[1]);
        Assert.IsType<FunctionSubject>(sequence.Parts[2]);
        var functionSubject = (FunctionSubject)sequence.Parts[2];
        Assert.Equal("new", functionSubject.Name);
        Assert.NotEmpty(functionSubject.Arguments);
        Assert.IsType<ConstantFunctionSubjectArgument>(functionSubject.Arguments[0]);
        var argument = (ConstantFunctionSubjectArgument)functionSubject.Arguments[0];
        Assert.Equal("Vacation", argument.Value);
    }
}
