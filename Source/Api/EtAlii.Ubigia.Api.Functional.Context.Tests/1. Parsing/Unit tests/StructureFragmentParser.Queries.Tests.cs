namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using Xunit;

    public class StructureQueryParserTests
    {
        [Fact]
        public void StructureFragmentParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateStructureFragmentParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IStructureFragmentParser CreateStructureFragmentParser()
        {
            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<IStructureFragmentParser>();
        }

        [Fact]
        public void StructureFragmentParser_Parse_Query_With_Multiple_ValueQueries_01()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
            var text = @"Person @node(Person:Stark/Tony)
            {
                key1,
                key2
            }";


            // Act.
            var node = parser.Parser.Do(text);
            var structureFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(structureFragment);
            Assert.Equal(FragmentType.Query, structureFragment.Type);

            Assert.NotEmpty(structureFragment.Values);
            Assert.Equal(FragmentType.Query, structureFragment.Values[0].Type);
            Assert.Equal(FragmentType.Query, structureFragment.Values[1].Type);
        }

        [Fact]
        public void StructureFragmentParser_Parse_Query_With_Multiple_ValueQueries_02()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
            var text = @"Person @node(Person:Stark/Tony)
            {
                ""key1"",
                ""key2""
            }";


            // Act.
            var node = parser.Parser.Do(text);
            var structureFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(structureFragment);
            Assert.Equal(FragmentType.Query, structureFragment.Type);

            Assert.NotEmpty(structureFragment.Values);
            Assert.Equal(FragmentType.Query, structureFragment.Values[0].Type);
            Assert.Equal(FragmentType.Query, structureFragment.Values[1].Type);
        }

        [Fact]
        public void StructureFragmentParser_Parse_Query_With_Two_Distinct_Results()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
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
            var structureFragment = parser.Parse(node);

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
