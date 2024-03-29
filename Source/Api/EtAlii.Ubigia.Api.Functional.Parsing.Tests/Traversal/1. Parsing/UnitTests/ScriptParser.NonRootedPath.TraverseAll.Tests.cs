﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System.Linq;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;

public partial class ScriptParserNonRootedPathTests
{
    [Fact]
    public void ScriptParser_NonRootedPath_Parse_Traverse_All_Hierarchical_Childs()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/2018/08/22//";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        var subject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(0).First());
        Assert.Equal("2018", subject.Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(2).First());
        Assert.Equal("08", subject.Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(4).First());
        Assert.Equal("22", subject.Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<AllParentsPathSubjectPart>(subject.Parts.Skip(6).First());
    }

    [Fact]
    public void ScriptParser_NonRootedPath_Parse_Traverse_All_Hierarchical_Parents()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = @"/2018/08/22\\";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        var subject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(0).First());
        Assert.Equal("2018", subject.Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(2).First());
        Assert.Equal("08", subject.Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(4).First());
        Assert.Equal("22", subject.Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<AllChildrenPathSubjectPart>(subject.Parts.Skip(6).First());
    }


    [Fact]
    public void ScriptParser_NonRootedPath_Parse_Traverse_All_Sequential_Next()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/Device/Canon/BT342/Feed/123>>";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        var subject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(0).First());
        Assert.Equal("Device", subject.Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(2).First());
        Assert.Equal("Canon", subject.Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(4).First());
        Assert.Equal("BT342", subject.Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(6).First());
        Assert.Equal("Feed", subject.Parts.Skip(7).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(8).First());
        Assert.Equal("123", subject.Parts.Skip(9).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<AllNextPathSubjectPart>(subject.Parts.Skip(10).First());
    }

    [Fact]
    public void ScriptParser_NonRootedPath_Parse_Traverse_All_Sequential_Previous()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const string query = "/Device/Canon/BT342/Feed/123<<";

        // Act.
        var result = _parser.Parse(query, scope);

        // Assert.
        var script = result.Script;
        Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
        var sequence = script.Sequences.First();
        var subject = sequence.Parts.Skip(1).Cast<AbsolutePathSubject>().First();
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(0).First());
        Assert.Equal("Device", subject.Parts.Skip(1).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(2).First());
        Assert.Equal("Canon", subject.Parts.Skip(3).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(4).First());
        Assert.Equal("BT342", subject.Parts.Skip(5).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(6).First());
        Assert.Equal("Feed", subject.Parts.Skip(7).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<ParentPathSubjectPart>(subject.Parts.Skip(8).First());
        Assert.Equal("123", subject.Parts.Skip(9).Cast<ConstantPathSubjectPart>().First().Name);
        Assert.IsType<AllPreviousPathSubjectPart>(subject.Parts.Skip(10).First());
    }
}
