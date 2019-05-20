namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using Xunit;

    public class QueryParserTests 
    {
        [Fact]
        public void QueryParser_Create()
        {
            // Arrange.
            
            // Act.
            //var jsonNode = new QueryParser();

            // Assert.
            //Assert.NotNull(jsonNode);
        }
        
        [Fact]
        public void QueryParser_Parse_Rooted_Select()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"-- Comment goes here.
            @select(Person:Start/Tony)
            {
                ""age"": ""22"",
                ""first"": ""Sabrina"",
                ""last"": ""Stephenson""
                ""company"": ""ISOTRONIC"",
                ""email"": ""sabrina.stephenson@isotronic.io"",
                ""phone"": ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_NonRooted_Select()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"-- Comment goes here.
            @select(/Person/Start/Tony)
            {
                ""age"": ""22"",
                ""first"": ""Sabrina"",
                ""last"": ""Stephenson""
                ""company"": ""ISOTRONIC"",
                ""email"": ""sabrina.stephenson@isotronic.io"",
                ""phone"": ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Text_Flat()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            {
                ""age"": ""22"",
                ""first"": ""Sabrina"",
                ""last"": ""Stephenson""
                ""company"": ""ISOTRONIC"",
                ""email"": ""sabrina.stephenson@isotronic.io"",
                ""phone"": ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }
   
        [Fact]
        public void QueryParser_Parse_Text_Nested()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            {
                ""age"": 22,
                ""name"": 
                {
                    ""first"": ""Sabrina"",
                    ""last"": ""Stephenson""
                },
                ""company"": ""ISOTRONIC"",
                ""email"": ""sabrina.stephenson@isotronic.io"",
                ""phone"": ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Comment()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"-- This is a comment";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Comment_And_Object_Single_Line()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"-- This is a comment { ""key"": ""value"" }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Comment_And_Object_Multiple_Lines()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"-- This is a comment 
            { 
                ""key"": ""value"" 
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Text_Annotated_Root()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            --@select(person:Stephenson/Sabrina)
            {
                ""age"": 22,
                ""name"": 
                {
                    ""first"": ""Sabrina"",
                    ""last"": ""Stephenson""
                },
                ""company"": ""ISOTRONIC"",
                ""email"": ""sabrina.stephenson@isotronic.io"",
                ""phone"": ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(annotatedRootPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
            Assert.IsType<Annotation>(parseResult.Query.ObjectDefinition.Annotation);
            //Assert.Equal("traverse(person:Stephenson/Sabrina)",jsonNode.Annotation);
        }

        [Fact]
        public void QueryParser_Parse_Text_Annotated_Root_No_Values()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            @select(person:Stephenson/Sabrina)
            {
                ""age"",
                ""name"": 
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
        public void QueryParser_Parse_Text_Annotated_Element_01()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            {
                ""age"": 22,
                ""firstname"": @id(),
                ""company"": ""ISOTRONIC"",
                ""email"": ""sabrina.stephenson@isotronic.io"",
                ""phone"": ""+31 (909) 477-2353""
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
        public void QueryParser_Parse_Text_Annotated_Element_No_Values_01()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            {
                ""age"",
                ""firstname"": @id(),
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
        public void QueryParser_Parse_Text_Annotated_Element_02()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            {
                ""age"": 22,
                ""name"": 
                {
                    ""first"": @id(),
                    ""last"": ""Stephenson""
                },
                ""company"": ""ISOTRONIC"",
                ""email"": ""sabrina.stephenson@isotronic.io"",
                ""phone"": ""+31 (909) 477-2353""
            }";


            // Act.
            var parseResult = parser.Parse(annotatedRootPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);

            //Assert.Equal("id()", jsonNode.Children.ToArray()[1].Children.ToArray()[0].Annotation);
        }

        [Fact]
        public void QueryParser_Parse_Text_Annotated_Element_No_Values_02()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            {
                ""age"",
                ""name"": 
                {
                    ""first"": @id(),
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
