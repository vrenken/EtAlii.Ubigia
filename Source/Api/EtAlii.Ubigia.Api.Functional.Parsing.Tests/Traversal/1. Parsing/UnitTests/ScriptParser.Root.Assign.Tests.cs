// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System.Linq;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;

public partial class ScriptParserTests
{
    [Fact]
    public void ScriptParser_Root_Assign_Time_Root()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "root:time <= EtAlii.Ubigia.Roots.Time";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("time", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.Equal(new RootType("EtAlii.Ubigia.Roots.Time"), sequence.Parts.Skip(2).Cast<RootDefinitionSubject>().First().Type);
    }

    [Fact]
    public void ScriptParser_Root_Assign_Time_Root_And_Using_Short_RootType()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "root:time <= Time";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("time", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.Equal("Time", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
    }

    [Fact]
    public void ScriptParser_Root_Assign_Time_Root_Under_Other_Name()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "root:specialtime <= EtAlii.Ubigia.Roots.Time";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("specialtime", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.Equal(new RootType("EtAlii.Ubigia.Roots.Time"), sequence.Parts.Skip(2).Cast<RootDefinitionSubject>().First().Type);
    }

    [Fact]
    public void ScriptParser_Root_Assign_Time_Root_Under_Other_Name_And_Using_Short_RootType()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "root:specialtime <= Time";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("specialtime", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.Equal("Time", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
    }

    [Fact]
    public void ScriptParser_Root_Assign_Object_Root_Under_Other_Name()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "root:projects <= EtAlii.Ubigia.Roots.Object";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("projects", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.Equal(new RootType("EtAlii.Ubigia.Roots.Object"), sequence.Parts.Skip(2).Cast<RootDefinitionSubject>().First().Type);
    }


    [Fact]
    public void ScriptParser_Root_Assign_Object_Root_Under_Other_Name_And_Using_Short_RootType()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "root:projects <= Object";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("projects", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.Equal("Object", sequence.Parts.Skip(2).Cast<StringConstantSubject>().First().Value);
    }

    [Fact]
    public void ScriptParser_Root_Assign_Object_Root_Under_Other_Name_And_Schema()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "root:projects <= EtAlii.Ubigia.Roots.Object";//":/[Words]/[Number]"

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        Assert.Equal("projects", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.Equal(new RootType("EtAlii.Ubigia.Roots.Object"), sequence.Parts.Skip(2).Cast<RootDefinitionSubject>().First().Type);
        //Assert.Equal("/[WORDS]/[NUMBER]", sequence.Parts.Skip(2).Cast<RootDefinitionSubject>().First().Schema.ToString()); // TODO, should be types
    }
}
