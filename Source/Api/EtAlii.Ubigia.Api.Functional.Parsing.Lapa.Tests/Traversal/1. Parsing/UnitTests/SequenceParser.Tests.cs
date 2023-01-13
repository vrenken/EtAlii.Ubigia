// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System;
using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class SequenceParserTests : IClassFixture<FunctionalUnitTestContext>, IAsyncLifetime
{
    private ISequenceParser _parser;
    private readonly FunctionalUnitTestContext _testContext;

    public SequenceParserTests(FunctionalUnitTestContext testContext)
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
    public void SequenceParser_Parse_Constants_01()
    {
        // Arrange.
        var text = "'First' <= 'Second' <= 'Third'";
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
    public void SequenceParser_Parse_Constants_02()
    {
        // Arrange.
        var text = @"""First"" <= ""Second"" <= ""Third""";
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
    public void SequenceParser_Parse_Constants_03()
    {
        // Arrange.
        var text = @"""First"" <= 'Second' <= ""Third""";
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
    public void SequenceParser_Parse_Variable_01()
    {
        // Arrange.
        var text = @"$first <= ""Second""";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 3);
        Assert.IsType<VariableSubject>(sequence.Parts.ElementAt(0));
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.IsType<StringConstantSubject>(sequence.Parts.ElementAt(2));
    }

    [Fact]
    public void SequenceParser_Parse_Variable_02()
    {
        // Arrange.
        var text = @"/'Second' <= $first <= ""Third""";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 5);
        Assert.IsType<AbsolutePathSubject>(sequence.Parts.ElementAt(0));
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.IsType<VariableSubject>(sequence.Parts.ElementAt(2));
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(3));
        Assert.IsType<StringConstantSubject>(sequence.Parts.ElementAt(4));
    }

    [Fact]
    public void SequenceParser_Parse_Variable_03_Invalid()
    {
        // Arrange.
        var text = @"""Second"" <= $first";
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
    public void SequenceParser_Parse_Variable_03()
    {
        // Arrange.
        var text = @"/""Second"" <= $first";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 3);
        Assert.IsType<AbsolutePathSubject>(sequence.Parts.ElementAt(0));
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.IsType<VariableSubject>(sequence.Parts.ElementAt(2));
    }

    [Fact]
    public void SequenceParser_Parse_Variable_04_Invalid()
    {
        // Arrange.
        var text = @"'Second' <= $first <= ""Third""";
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
    public void SequenceParser_Parse_Variable_04()
    {
        // Arrange.
        var text = @"/'Second' <= $first <= ""Third""";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 5);
        Assert.IsType<AbsolutePathSubject>(sequence.Parts.ElementAt(0));
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
        Assert.IsType<VariableSubject>(sequence.Parts.ElementAt(2));
        Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(3));
        Assert.IsType<StringConstantSubject>(sequence.Parts.ElementAt(4));
    }
}
