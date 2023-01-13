// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests;

using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Functional.Context;
using EtAlii.Ubigia.Api.Functional.Tests;
using EtAlii.Ubigia.Api.Functional.Traversal;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class StructureFragmentParserMutationsTests : IClassFixture<FunctionalUnitTestContext>
{
    private readonly FunctionalUnitTestContext _testContext;

    public StructureFragmentParserMutationsTests(FunctionalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    [Fact]
    public async Task StructureFragmentParser_Create()
    {
        // Arrange.

        // Act.
        var parser = await _testContext
            .CreateComponentOnNewSpace<IStructureFragmentParser>()
            .ConfigureAwait(false);

        // Assert.
        Assert.NotNull(parser);
    }

    [Fact(Skip = "Not working yet - Should we get rid of the LAPA parser?")]
    public async Task StructureFragmentParser_Parse_ValueMutation_Key_Value_Single()
    {
        // Arrange.
        var (parser, nodeValidator) = await _testContext
            .CreateComponentOnNewSpace<IStructureFragmentParser, INodeValidator>()
            .ConfigureAwait(false);
        var text = @"Person = @node(Person:Stark/Tony)
            {
                key = ""value""
            }";

        // Act.
        var node = parser.Parser.Do(text);
        var query = parser.Parse(node, nodeValidator);

        // Assert.
        Assert.NotNull(node);
        Assert.NotNull(query);
        Assert.NotEmpty(query.Values);
        Assert.Equal(FragmentType.Mutation, query.Values[0].Type);
    }


    [Fact]
    public async Task StructureFragmentParser_Parse_ValueMutation_Node_Clear()
    {
        // Arrange.
        var (parser, nodeValidator) = await _testContext
            .CreateComponentOnNewSpace<IStructureFragmentParser, INodeValidator>()
            .ConfigureAwait(false);
        var text = @"Person = @node(Person:Doe/John)
            {
               FirstName = @node-clear()
            }";

        // Act.
        var node = parser.Parser.Do(text);
        var query = parser.Parse(node, nodeValidator);

        // Assert.
        Assert.NotNull(node);
        Assert.NotNull(query);
        Assert.NotEmpty(query.Values);
        Assert.Equal(FragmentType.Mutation, query.Values[0].Type);
    }

    [Fact(Skip = "Not working yet - Should we get rid of the LAPA parser?")]
    public async Task StructureFragmentParser_Parse_ValueMutation_With_Multiple_ValueMutations_01()
    {
        // Arrange.
        var (parser, nodeValidator) = await _testContext
            .CreateComponentOnNewSpace<IStructureFragmentParser, INodeValidator>()
            .ConfigureAwait(false);
        var text = @"Person = @node(Person:Stark/Tony)
            {
                key1 = ""value1"",
                key2 = ""value2""
            }";

        // Act.
        var node = parser.Parser.Do(text);
        var query = parser.Parse(node, nodeValidator);

        // Assert.
        Assert.NotNull(node);
        Assert.NotNull(query);
        Assert.NotEmpty(query.Values);
        Assert.Equal(FragmentType.Mutation, query.Values[0].Type);
        Assert.Equal(FragmentType.Mutation, query.Values[1].Type);
    }

    [Fact(Skip = "Not working yet - Should we get rid of the LAPA parser?")]
    public async Task StructureFragmentParser_Parse_ValueMutation_With_Multiple_ValueMutations_02()
    {
        // Arrange.
        var (parser, nodeValidator) = await _testContext
            .CreateComponentOnNewSpace<IStructureFragmentParser, INodeValidator>()
            .ConfigureAwait(false);
        var text = @"Person = @node(Person:Stark/Tony)
            {
                age = 22,
                first = ""Sabrina"",
                last = ""Stephenson"",
                company = ""ISOTRONIC"",
                email = ""sabrina.stephenson@isotronic.io"",
                phone = ""+31 (909) 477-2353""
            }";

        // Act.
        var node = parser.Parser.Do(text);
        var @object = parser.Parse(node, nodeValidator);

        // Assert.
        Assert.NotNull(node);
        Assert.NotNull(@object);
    }

    [Fact(Skip = "Not working yet - Should we get rid of the LAPA parser?")]
    public async Task StructureFragmentParser_Parse_ValueMutation_With_Annotations_00()
    {
        // Arrange.
        var (parser, nodeValidator) = await _testContext
            .CreateComponentOnNewSpace<IStructureFragmentParser, INodeValidator>()
            .ConfigureAwait(false);
        var text = @"Person = @node(person:Stark/Tony)
            {
                ""age"" = 22,
                ""firstname"" = @node(),
                ""lastname"" = @node(\#FamilyName),
                ""email"" = ""admin@starkindustries.com"",
                ""phone"" = ""+31 (909) 477-2353""
            }";

        // Act.
        var node = parser.Parser.Do(text);
        var structureMutation = parser.Parse(node, nodeValidator);

        // Assert.
        Assert.NotNull(node);
        Assert.NotNull(structureMutation);

        var valueFragment1 = structureMutation.Values.Single(v => v.Name == "firstname");
        Assert.NotNull(valueFragment1);
        Assert.Equal(FragmentType.Query, valueFragment1.Type);
        Assert.IsType<SelectValueAnnotation>(valueFragment1.Annotation);
        Assert.Null(valueFragment1.Annotation.Source);

        var valueFragment2 = structureMutation.Values.Single(v => v.Name == "lastname");
        Assert.NotNull(valueFragment2);
        Assert.Equal(FragmentType.Query, valueFragment2.Type);
        Assert.IsType<SelectValueAnnotation>(valueFragment2.Annotation);
        Assert.Equal(@"\#FamilyName", valueFragment2.Annotation.Source.ToString());
    }

    [Fact(Skip = "Not working yet - Should we get rid of the LAPA parser?")]
    public async Task StructureFragmentParser_Parse_ValueMutation_With_Annotations_01()
    {
        // Arrange.
        var (parser, nodeValidator) = await _testContext
            .CreateComponentOnNewSpace<IStructureFragmentParser, INodeValidator>()
            .ConfigureAwait(false);
        var text = @"Person = @node(person:Stark/Tony)
            {
                age = 22,
                firstname = @node(),
                lastname = @node(\#FamilyName),
                email = ""admin@starkindustries.com"",
                phone = ""+31 (909) 477-2353""
            }";

        // Act.
        var node = parser.Parser.Do(text);
        var structureMutation = parser.Parse(node, nodeValidator);

        // Assert.
        Assert.NotNull(node);
        Assert.NotNull(structureMutation);
    }

    [Fact]
    public async Task StructureFragmentParser_Parse_StructureMutation_01()
    {
        // Arrange.
        var (parser, nodeValidator) = await _testContext
            .CreateComponentOnNewSpace<IStructureFragmentParser, INodeValidator>()
            .ConfigureAwait(false);
        var text = @"Friends = @nodes-link(/Friends, Person:Banner/Peter, /Friends)
                        {
                            FirstName = @node()
                            LastName = @node(\#FamilyName)
                        }";

        // Act.
        var node = parser.Parser.Do(text);
        var structureMutation = parser.Parse(node, nodeValidator);

        // Assert.
        Assert.NotNull(node);
        Assert.NotNull(structureMutation);
        Assert.Equal(FragmentType.Mutation, structureMutation.Type);
        Assert.NotNull(structureMutation.Annotation);
        var linkAnnotation = Assert.IsType<LinkAndSelectMultipleNodesAnnotation>(structureMutation.Annotation);
        Assert.Equal("/Friends", linkAnnotation.Source.ToString());
        Assert.Equal("Person:Banner/Peter", linkAnnotation.Target.ToString());
        Assert.Equal("/Friends", linkAnnotation.TargetLink.ToString());

        var valueFragment1 = structureMutation.Values.Single(v => v.Name == "FirstName");
        Assert.NotNull(valueFragment1);
        Assert.Equal(FragmentType.Query, valueFragment1.Type);
        Assert.Null(valueFragment1.Annotation.Source);

        var valueFragment2 = structureMutation.Values.Single(v => v.Name == "LastName");
        Assert.NotNull(valueFragment2);
        Assert.Equal(@"\#FamilyName",valueFragment2.Annotation.Source.ToString());
    }
}
