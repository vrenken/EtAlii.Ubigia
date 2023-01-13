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
public class SequenceParserPathsConditionalTests : IClassFixture<FunctionalUnitTestContext>, IAsyncLifetime
{
    private ISequenceParser _parser;
    private readonly FunctionalUnitTestContext _testContext;

    public SequenceParserPathsConditionalTests(FunctionalUnitTestContext testContext)
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
    public void SequenceParser_Parse_PathSubject_Conditional_01()
    {
        // Arrange.
        var text = "/Person/*/.NickName=\"Johnny\"";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Single(conditionalPathSubjectPart.Conditions);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
    }


    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_02()
    {
        // Arrange.
        var text = "/Person/*/.NickName=\'Johnny\'";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Single(conditionalPathSubjectPart.Conditions);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
    }


    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_03()
    {
        // Arrange.
        var text = "/Person/*/.NickName=\"Johnny\"&Birthdate=27-06-1977";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(2, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
    }


    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_04()
    {
        // Arrange.
        var text = "/Person/*/.NickName=\"Johnny\"&Birthdate=1977-06-27";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(2, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_05()
    {
        // Arrange.
        var text = "/Person/*/.NickName=\"Johnny\"&Birthdate=1977-06-27&IsMale=true";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }


    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_06()
    {
        // Arrange.
        var text = "/Person/.Type=\"Family\"/.NickName=\"Johnny\"";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(3).Cast<ConditionalPathSubjectPart>().First();
        Assert.Single(conditionalPathSubjectPart.Conditions);
        Assert.Equal("Type", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Family", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Single(conditionalPathSubjectPart.Conditions);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_07()
    {
        // Arrange.
        var text = "/Person/.Type=/.NickName=\"Johnny\"";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(3).Cast<ConditionalPathSubjectPart>().First();
        Assert.Single(conditionalPathSubjectPart.Conditions);
        Assert.Equal("Type", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Null(conditionalPathSubjectPart.Conditions[0].Value);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Single(conditionalPathSubjectPart.Conditions);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_MultiLine_01()
    {
        // Arrange.
        var text = "/Person/*/.NickName=\"Johnny\"\n" +
                   " & Birthdate=27-06-1977\n" +
                   " & IsMale = true";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_MultiLine_02()
    {
        // Arrange.
        var text = "/Person/*/.\n" +
                   " NickName=\"Johnny\" & \n" +
                   " Birthdate=27-06-1977 & \n"+
                   " IsMale = true";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_MultiLine_03()
    {
        // Arrange.
        var text = "/Person/*/John.NickName=\"Johnny\"\n" +
                   " & Birthdate=27-06-1977\n" +
                   " & IsMale = true";

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
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }


    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_MultiLine_04()
    {
        // Arrange.
        var text = "/Person/*/.NickName=\"Johnny\" & \n" +
                   "Birthdate=27-06-1977 & \n" +
                   "IsMale = true";

        // Act.
        var sequence = _parser.Parse(text);

        // Assert.
        Assert.NotNull(sequence);
        Assert.True(sequence.Parts.Length == 2);

        var pathSubject = sequence.Parts.Skip(1).First() as AbsolutePathSubject;
        Assert.NotNull(pathSubject);
        Assert.Equal(6, pathSubject.Parts.Length);
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(0));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(1));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(2));
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_11()
    {
        // Arrange.
        var text = "/Person/*/John.NickName=\"Johnny\"&Birthdate=1977-06-27&IsMale=true";

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
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(ConditionType.Equal, conditionalPathSubjectPart.Conditions[1].Type);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_12()
    {
        // Arrange.
        var text = "/Person/*/John.NickName=\"Johnny\"&Birthdate>1977-06-27&IsMale=true";

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
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(ConditionType.MoreThan, conditionalPathSubjectPart.Conditions[1].Type);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_13()
    {
        // Arrange.
        var text = "/Person/*/John.NickName=\"Johnny\"&Birthdate>=1977-06-27&IsMale=true";

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
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(ConditionType.MoreThanOrEqual, conditionalPathSubjectPart.Conditions[1].Type);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_14()
    {
        // Arrange.
        var text = "/Person/*/John.NickName=\"Johnny\"&Birthdate!=1977-06-27&IsMale=true";

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
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(ConditionType.NotEqual, conditionalPathSubjectPart.Conditions[1].Type);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_15()
    {
        // Arrange.
        var text = "/Person/*/John.NickName=\"Johnny\"&Birthdate<1977-06-27&IsMale=true";

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
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(ConditionType.LessThan, conditionalPathSubjectPart.Conditions[1].Type);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }

    [Fact]
    public void SequenceParser_Parse_PathSubject_Conditional_16()
    {
        // Arrange.
        var text = "/Person/*/John.NickName=\"Johnny\"&Birthdate<=1977-06-27&IsMale=true";

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
        Assert.IsType<WildcardPathSubjectPart>(pathSubject.Parts.ElementAt(3));
        Assert.IsType<ParentPathSubjectPart>(pathSubject.Parts.ElementAt(4));
        Assert.IsType<ConstantPathSubjectPart>(pathSubject.Parts.ElementAt(5));
        Assert.IsType<ConditionalPathSubjectPart>(pathSubject.Parts.ElementAt(6));
        var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
        Assert.Equal(3, conditionalPathSubjectPart.Conditions.Length);
        Assert.Equal("NickName", conditionalPathSubjectPart.Conditions[0].Property);
        Assert.Equal("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        Assert.Equal("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
        Assert.Equal(ConditionType.LessThanOrEqual, conditionalPathSubjectPart.Conditions[1].Type);
        Assert.Equal(new DateTime(1977, 06, 27), conditionalPathSubjectPart.Conditions[1].Value);
        Assert.Equal("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
        Assert.True((bool)conditionalPathSubjectPart.Conditions[2].Value);
    }

}
