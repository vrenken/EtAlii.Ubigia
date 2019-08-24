﻿namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
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
                    FirstName @value(),
                    LastName @value(\#FamilyName),
                    NickName,
                    Friends @nodes(/Friends/)  
                }
                Location @nodes(location:DE/Berlin//)
                {
                    FirstName @value(),
                    LastName @value(\#FamilyName),
                    NickName,
                    Friends @nodes(/Friends/)  
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
