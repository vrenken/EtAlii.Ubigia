﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class SequenceParserPathsTemporalTests : IClassFixture<FunctionalUnitTestContext>, IAsyncLifetime
{
    private ISequenceParser _parser;
    private readonly FunctionalUnitTestContext _testContext;

    public SequenceParserPathsTemporalTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    public async Task InitializeAsync()
    {
        _parser = await _testContext
            .CreateComponentOnNewSpace<ISequenceParser>()
            .ConfigureAwait(false);
    }

    public Task DisposeAsync()
    {
        _parser = null;
        return Task.CompletedTask;
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_01()
    {
        // Arrange.
        var text = "/First/Second/Third{";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(7, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<DowndatePathSubjectPart>(pathSubject.Parts.ElementAt(6));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_02()
    {
        // Arrange.
        var text = "/First/Second/Third/{";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(8, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        Assert.IsType<DowndatePathSubjectPart>(pathSubject.Parts.ElementAt(7));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_03()
    {
        // Arrange.
        var text = "/First/Second{/Third/";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(8, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<DowndatePathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_04()
    {
        // Arrange.
        var text = "/First/{Second/Third/";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(8, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<DowndatePathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_05()
    {
        // Arrange.
        var text = "/First/{{Second/Third/";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(8, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<AllDowndatesPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_06()
    {
        // Arrange.
        var text = "/First/Second/Third{{";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(7, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<AllDowndatesPathSubjectPart>(pathSubject.Parts.ElementAt(6));
    }


    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_07()
    {
        // Arrange.
        var text = "/First/Second/Third{{{";
        var validator = new TraversalValidator();

        // Act.
        var act = new Action(() =>
        {
            var sequence = _parser.Parse(text);
            validator.Validate(sequence);
        });

        // Assert.
        Assert.Throws<ScriptParserException>(act);
    }



    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Downdate_08()
    {
        // Arrange.
        var text = "/First/{{{Second/Third";
        var validator = new TraversalValidator();

        // Act.
        var act = new Action(() =>
        {
            var sequence = _parser.Parse(text);
            validator.Validate(sequence);
        });

        // Assert.
        Assert.Throws<ScriptParserException>(act);
    }


    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Update_01()
    {
        // Arrange.
        var text = "/First/Second/Third}";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(7, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<UpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(6));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Update_02()
    {
        // Arrange.
        var text = "/First/Second/Third/}";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(8, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        Assert.IsType<UpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(7));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Update_03()
    {
        // Arrange.
        var text = "/First/Second}/Third/";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(8, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<UpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Update_04()
    {
        // Arrange.
        var text = "/First/}Second/Third/";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(8, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<UpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Update_05()
    {
        // Arrange.
        var text = "/First/}}Second/Third/";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(8, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<AllUpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(7));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Update_07()
    {
        // Arrange.
        var text = "/First/Second/Third}}";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(7, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<AllUpdatesPathSubjectPart>(pathSubject.Parts.ElementAt(6));
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Update_08()
    {
        // Arrange.
        var text = "/First/Second/Third}}}";
        var validator = new TraversalValidator();

        // Act.
        var act = new Action(() =>
        {
            var sequence = _parser.Parse(text);
            validator.Validate(sequence);
        });

        // Assert.
        Assert.Throws<ScriptParserException>(act);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Hierarchical_Update_09()
    {
        // Arrange.
        var text = "/First/}}}Second/Third";
        var validator = new TraversalValidator();

        // Act.
        var act = new Action(() =>
        {
            var sequence = _parser.Parse(text);
            validator.Validate(sequence);
        });

        // Assert.
        Assert.Throws<ScriptParserException>(act);
    }

}
