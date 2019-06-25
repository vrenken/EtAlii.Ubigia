namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using Xunit;

    public partial class QueryParserTests 
    {
        [Fact]
        public void QueryParser_Parse_Query_Flat_01()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person 
            {
                ""age"",
                ""first"",
                ""last"",
                ""company"",
                ""email"",
                ""phone""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Query_Flat_02()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person 
            {
                age,
                first,
                last,
                company,
                email,
                phone
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Query_Flat_03()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person 
            {
                age, first, last, company, email, phone
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }
        
        [Fact]
        public void QueryParser_Parse_Query_Flat_Annotated_01()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person @node(Person:Doe/John)
            {
                ""age"",
                ""first"",
                ""last"",
                ""company"",
                ""email"",
                ""phone""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }
        
        [Fact]
        public void QueryParser_Parse_Query_Flat_Annotated_02()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person @node(Person:Doe/John)
            {
                age,
                first,
                last,
                company,
                email,
                phone
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }
        
        [Fact]
        public void QueryParser_Parse_Query_Flat_Annotated_03()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person @node(Person:Doe/John)
            {
                age, first, last, company, email, phone
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        
        [Fact]
        public void QueryParser_Parse_Query_Nested_01()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person @node(person:Doe/John) 
            {
                ""age"",
                ""name"" @node(\LastName)
                {
                    ""first"" @value(/FirstName),
                    ""last"" @value()
                },
                ""company"",
                ""email"",
                ""phone""
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Query_Nested_02()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person @node(person:Doe/John) 
            {
                ""age"",
                ""name"" 
                {
                    ""first"" @value(),
                    ""last"" @value(\\LastName)
                },
                ""company"",
                ""email"",
                ""phone""
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Query_Nested_03()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person @node(person:Doe/John) 
            {
                age,
                name @node(\LastName)
                {
                    first @value(/FirstName),
                    last @value()
                },
                company,
                email,
                phone
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Query_Annotated_Root_No_Values()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            Person @node(person:Stephenson/Sabrina)
            {
                ""age"",
                ""name"" 
                {
                    ""first"",
                    ""last""
                },
                ""company"",
                ""email"",
                ""phone""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(annotatedRootPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
            //Assert.Equal("traverse(person:Stephenson/Sabrina)",jsonNode.Annotation);
        }

        [Fact]
        public void QueryParser_Parse_Query_Annotated_Element_No_Values_01()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            Person 
            {
                ""age"",
                ""firstname"" @value(),
                ""company"",
                ""email"",
                ""phone""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(annotatedRootPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);

            //Assert.Equal("id()", jsonNode.Children.ToArray()[1].Annotation);
        }


        [Fact]
        public void QueryParser_Parse_Query_Annotated_Element_No_Values_02()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            Person 
            {
                ""age"",
                ""name"" 
                {
                    ""first"" @value(),
                    ""last""
                },
                ""company"",
                ""email"",
                ""phone""
            }";


            // Act.
            var parseResult = parser.Parse(annotatedRootPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);

            //Assert.Equal("id()", jsonNode.Children.ToArray()[1].Children.ToArray()[0].Annotation);
        }

    }
}
