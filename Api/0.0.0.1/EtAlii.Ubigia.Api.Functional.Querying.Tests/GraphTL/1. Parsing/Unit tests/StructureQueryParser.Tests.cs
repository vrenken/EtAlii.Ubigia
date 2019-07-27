namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using Xunit;

    public class StructureQueryParserTests 
    {
        [Fact]
        public void StructureQueryParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateStructureQueryParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IStructureQueryParser CreateStructureQueryParser()
        {
            var container = new QueryParserTestContainerFactory().Create();

            return container.GetInstance<IStructureQueryParser>();
        }

        [Fact]
        public void StructureQueryParser_Parse_Query_With_Multiple_ValueQueries_01()
        {
            // Arrange.
            var parser = CreateStructureQueryParser();
            var text = @"Person @node(Person:Stark/Tony)
            {
                key1,
                key2
            }";
            
            
            // Act.
            var node = parser.Parser.Do(text);
            var query = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(query);
            Assert.NotEmpty(query.Values);
            Assert.IsType<ValueQuery>(query.Values[0]);
            Assert.IsType<ValueQuery>(query.Values[1]);
        }        

        [Fact]
        public void StructureQueryParser_Parse_Query_With_Multiple_ValueQueries_02()
        {
            // Arrange.
            var parser = CreateStructureQueryParser();
            var text = @"Person @node(Person:Stark/Tony)
            {
                ""key1"",
                ""key2""
            }";
            
            
            // Act.
            var node = parser.Parser.Do(text);
            var query = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(query);
            Assert.NotEmpty(query.Values);
            Assert.IsType<ValueQuery>(query.Values[0]);
            Assert.IsType<ValueQuery>(query.Values[1]);
        }  

        [Fact]
        public void StructureQueryParser_Parse_Query_With_Two_Distinct_Results()
        {
            // Arrange.
            var parser = CreateStructureQueryParser();
            var text = @"Data
            {
                Person @nodes(Person:Doe/*)
                {
                    FirstName @value(),
                    LastName @value(\LastName),
                    NickName,
                    Friends @nodes(/Friends)  
                }
                Location @nodes(location:DE/Berlin//)
                {
                    FirstName @value(),
                    LastName @value(\LastName),
                    NickName,
                    Friends @nodes(/Friends)  
                }
            }";
            
            
            // Act.
            var node = parser.Parser.Do(text);
            var query = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(query);
            Assert.Empty(query.Values);
            Assert.NotEmpty(query.Children);
            Assert.IsType<StructureQuery>(query.Children[0]);
            Assert.IsType<StructureQuery>(query.Children[1]);
        }  
    }
}
