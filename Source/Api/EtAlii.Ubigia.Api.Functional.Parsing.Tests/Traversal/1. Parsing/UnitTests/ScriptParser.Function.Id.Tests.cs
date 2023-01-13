// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System;
using System.Linq;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class ScriptParserFunctionIdTests : IClassFixture<FunctionalUnitTestContext>, IDisposable
{
    private IScriptParser _parser;

    public ScriptParserFunctionIdTests(FunctionalUnitTestContext testContext)
    {
        _parser = testContext.CreateScriptParser();
    }

    public void Dispose()
    {
        _parser = null;
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void ScriptParser_Function_Id_Assign()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string text = "id() <= /Hierarchy";

        // Act.
        var result = _parser.Parse(text, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Empty(part.Arguments);
    }


    [Fact]
    public void ScriptParser_Function_Id_Variable()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id($path)";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<VariableFunctionSubjectArgument>(part.Arguments[0]);
        Assert.Equal("path", ((VariableFunctionSubjectArgument)part.Arguments[0]).Name);
    }

    [Fact]
    public void ScriptParser_Function_Id_Constant_SingleQuoted()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id('/Hierarchy')";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
        Assert.Equal("/Hierarchy", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
    }

    [Fact]
    public void ScriptParser_Function_Id_Constant_SingleQuoted_Special_Characters()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id('/Hierarchy äëöüáéóúâêôû')";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
        Assert.Equal("/Hierarchy äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
    }




    [Fact]
    public void ScriptParser_Function_Id_Constant_DoubleQuoted()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id(\"/Hierarchy\")";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
        Assert.Equal("/Hierarchy", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
    }

    [Fact]
    public void ScriptParser_Function_Id_Constant_DoubleQuoted_Special_Characters()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id(\"/Hierarchy äëöüáéóúâêôû\")";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<ConstantFunctionSubjectArgument>(part.Arguments[0]);
        Assert.Equal("/Hierarchy äëöüáéóúâêôû", ((ConstantFunctionSubjectArgument)part.Arguments[0]).Value);
    }


    [Fact]
    public void ScriptParser_Function_Id_Path_01()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id(/Hierarchy)";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
        Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
        Assert.Equal("Hierarchy", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]).Name);
    }


    [Fact]
    public void ScriptParser_Function_Id_Path_02()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id(/Hierarchy/Child)";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
        Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
        Assert.Equal("Hierarchy", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]).Name);
        Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[2]);
        Assert.Equal("Child", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[3]).Name);
    }

    [Fact]
    public void ScriptParser_Function_Id_Path_03()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id(/Hierarchy/Child/$var/MoreChildren)";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<NonRootedPathFunctionSubjectArgument>(part.Arguments[0]);
        Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]);
        Assert.Equal("Hierarchy", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[1]).Name);
        Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[2]);
        Assert.Equal("Child", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[3]).Name);
        Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[4]);
        Assert.Equal("var", ((VariablePathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[5]).Name);
        Assert.IsType<ParentPathSubjectPart>(((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[6]);
        Assert.Equal("MoreChildren", ((ConstantPathSubjectPart)((NonRootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[7]).Name);
    }


    [Fact]
    public void ScriptParser_Function_Id_Path_04()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id(time:now)";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<RootedPathFunctionSubjectArgument>(part.Arguments[0]);
        Assert.Equal("now", ((ConstantPathSubjectPart)((RootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]).Name);
    }

    [Fact]
    public void ScriptParser_Function_Id_Path_05()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id(time:\"2016-02-19\")";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<RootedPathFunctionSubjectArgument>(part.Arguments[0]);
        Assert.Equal("2016-02-19", ((ConstantPathSubjectPart)((RootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]).Name);
    }


    [Fact]
    public void ScriptParser_Function_Id_Path_06()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "id(time:'2016-02-19')";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        Assert.True(script.Sequences.Count() == 1);
        var sequence = script.Sequences.First();
        var part = sequence.Parts.Skip(1).First() as FunctionSubject;
        Assert.NotNull(part);
        Assert.Equal("id", part.Name);
        Assert.Single(part.Arguments);
        Assert.IsType<RootedPathFunctionSubjectArgument>(part.Arguments[0]);
        Assert.Equal("2016-02-19", ((ConstantPathSubjectPart)((RootedPathFunctionSubjectArgument)part.Arguments[0]).Subject.Parts[0]).Name);
    }
}
