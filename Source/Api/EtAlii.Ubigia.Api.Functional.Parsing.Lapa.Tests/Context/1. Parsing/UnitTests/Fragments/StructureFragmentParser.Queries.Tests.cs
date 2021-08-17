// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Context;
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using EtAlii.Ubigia.Api.Functional.Traversal.Tests;
    using Xunit;
    using EtAlii.Ubigia.Tests;

    [CorrelateUnitTests]
    public class StructureQueryParserTests : IClassFixture<TraversalUnitTestContext>
    {
        private readonly TraversalUnitTestContext _testContext;

        public StructureQueryParserTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task StructureFragmentParser_Create()
        {
            // Arrange.

            // Act.
            var parser = await new LapaSchemaParserComponentTestFactory()
                .Create<IStructureFragmentParser>(_testContext)
                .ConfigureAwait(false);

            // Assert.
            Assert.NotNull(parser);
        }

        [Fact]
        public async Task StructureFragmentParser_Parse_Query_With_Multiple_ValueQueries_01()
        {
            // Arrange.
            var (parser, nodeValidator) = await new LapaSchemaParserComponentTestFactory()
                .Create<IStructureFragmentParser, INodeValidator>(_testContext)
                .ConfigureAwait(false);
            var text = @"Person @node(Person:Stark/Tony)
            {
                key1,
                key2
            }";


            // Act.
            var node = parser.Parser.Do(text);
            var structureFragment = parser.Parse(node, nodeValidator);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(structureFragment);
            Assert.Equal(FragmentType.Query, structureFragment.Type);

            Assert.NotEmpty(structureFragment.Values);
            Assert.Equal(FragmentType.Query, structureFragment.Values[0].Type);
            Assert.Equal(FragmentType.Query, structureFragment.Values[1].Type);
        }

        [Fact]
        public async Task StructureFragmentParser_Parse_Query_With_Multiple_ValueQueries_02()
        {
            // Arrange.
            var (parser, nodeValidator) = await new LapaSchemaParserComponentTestFactory()
                .Create<IStructureFragmentParser, INodeValidator>(_testContext)
                .ConfigureAwait(false);
            var text = @"Person @node(Person:Stark/Tony)
            {
                ""key1"",
                ""key2""
            }";


            // Act.
            var node = parser.Parser.Do(text);
            var structureFragment = parser.Parse(node, nodeValidator);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(structureFragment);
            Assert.Equal(FragmentType.Query, structureFragment.Type);

            Assert.NotEmpty(structureFragment.Values);
            Assert.Equal(FragmentType.Query, structureFragment.Values[0].Type);
            Assert.Equal(FragmentType.Query, structureFragment.Values[1].Type);
        }

        [Fact]
        public async Task StructureFragmentParser_Parse_Query_With_Two_Distinct_Results()
        {
            // Arrange.
            var (parser, nodeValidator) = await new LapaSchemaParserComponentTestFactory()
                .Create<IStructureFragmentParser, INodeValidator>(_testContext)
                .ConfigureAwait(false);
            var text = @"Data
            {
                Person @nodes(Person:Doe/*)
                {
                    FirstName @node(),
                    LastName @node(\#FamilyName),
                    NickName,
                    Friends @nodes(/Friends/)
                    {
                        FirstName @node(),
                        LastName @node(\#FamilyName)
                    }
                },
                Location @nodes(location:DE/Berlin//)
                {
                    FirstName @node(),
                    LastName @node(\#FamilyName),
                    NickName,
                    Friends @nodes(/Friends/)
                    {
                        FirstName @node(),
                        LastName @node(\#FamilyName)
                    }
                }
            }";


            // Act.
            var node = parser.Parser.Do(text);
            var structureFragment = parser.Parse(node, nodeValidator);

            // Assert.
            Assert.NotNull(node);

            Assert.NotNull(structureFragment);
            Assert.Equal(FragmentType.Query, structureFragment.Type);

            Assert.Empty(structureFragment.Values);
            Assert.NotEmpty(structureFragment.Children);
            Assert.Equal(FragmentType.Query, structureFragment.Children[0].Type);
            Assert.Equal(FragmentType.Query, structureFragment.Children[1].Type);
        }
    }
}
