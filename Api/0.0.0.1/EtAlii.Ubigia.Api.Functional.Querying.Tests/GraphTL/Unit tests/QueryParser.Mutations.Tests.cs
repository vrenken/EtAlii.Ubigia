namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using Xunit;

    public partial class QueryParserTests 
    {
        [Fact]
        public void QueryParser_Parse_Mutation_With_Comment_And_Object_Multiple_Lines_01()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"-- This is a comment 
            Data
            { 
                ""key"" <= ""value"" 
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Mutation_With_Comment_And_Object_Multiple_Lines_02()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"-- This is a comment 
            Data @node(Person:Doe/John)
            { 
                ""key"" <= ""value"" 
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Mutation_Without_Comment_And_Object_Multiple_Lines_01()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"Data
            { 
                ""key"" <= ""value"" 
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        
        [Fact]
        public void QueryParser_Parse_Mutation_Without_Comment_And_Object_Multiple_Lines_02()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Data
            { 
                ""key"" <= ""value"" 
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        
        [Fact]
        public void QueryParser_Parse_Mutation_NonRooted_Select()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"-- Comment goes here.
            Person @node(/Person/Stark/Tony)
            {
                ""age"" <= ""22"",
                ""first"" <= ""Sabrina"",
                ""last"" <= ""Stephenson"",
                ""company"" <= ""ISOTRONIC"",
                ""email"" <= ""sabrina.stephenson@isotronic.io"",
                ""phone"" <= ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
            Assert.Equal("Person",parseResult.Query.Structure.Annotation.Path.Parts[1].ToString());
            Assert.Equal("Stark",parseResult.Query.Structure.Annotation.Path.Parts[3].ToString());
            Assert.Equal("Tony",parseResult.Query.Structure.Annotation.Path.Parts[5].ToString());
            Assert.IsType<StructureMutation>(parseResult.Query.Structure);
            var structureMutation = (StructureMutation)parseResult.Query.Structure; 
            Assert.Equal("22",structureMutation.Values.Cast<ValueMutation>().Single(v => v.Name == "age").Value);
            Assert.Equal("sabrina.stephenson@isotronic.io",structureMutation.Values.Cast<ValueMutation>().Single(v => v.Name == "email").Value);
        }

        [Fact]
        public void QueryParser_Parse_Mutation_Annotated_Root()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            Person @node(person:Stephenson/Sabrina)
            {
                ""age"" <= 22,
                ""name"" 
                {
                    ""first"" <= ""Sabrina"",
                    ""last"" <= ""Stephenson""
                },
                ""company"" <= ""ISOTRONIC"",
                ""email"" <= ""sabrina.stephenson@isotronic.io"",
                ""phone"" <= ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(annotatedRootPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
            Assert.IsType<Annotation>(parseResult.Query.Structure.Annotation);
            //Assert.Equal("traverse(person:Stephenson/Sabrina)",jsonNode.Annotation);
        }

        
        [Fact]
        public void QueryParser_Parse_Mutation_Rooted_Select()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"-- Comment goes here.
            Person @node(Person:Start/Tony)
            {
                age <= ""22"",
                first <= ""Sabrina"",
                last <= ""Stephenson"",
                company <= ""ISOTRONIC"",
                email <= ""sabrina.stephenson@isotronic.io"",
                phone <= ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Mutation_Flat()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person 
            {
                ""age"" <= ""22"",
                ""first"" <= ""Sabrina"",
                ""last"" <= ""Stephenson"",
                ""company"" <= ""ISOTRONIC"",
                ""email"" <= ""sabrina.stephenson@isotronic.io"",
                ""phone"" <= ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Mutation_Flat_Annotated()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person @node(Person:Doe/John)
            {
                ""age"" <= ""22"",
                ""first"" <= ""John"",
                ""last"" <= ""Doe"",
                ""company"" <= ""ISOTRONIC"",
                ""email"" <= ""sabrina.stephenson@isotronic.io"",
                ""phone"" <= ""+31 (909) 477-2353""
            }";
            
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Mutation_Nested()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var normalPersonText = @"
            Person 
            {
                ""age"" <= 22,
                ""name"" 
                {
                    ""first"" <= ""Sabrina"",
                    ""last"" <= ""Stephenson""
                },
                ""company"" <= ""ISOTRONIC"",
                ""email"" <= ""sabrina.stephenson@isotronic.io"",
                ""phone"" <= ""+31 (909) 477-2353""
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
        }

        [Fact]
        public void QueryParser_Parse_Mutation_Annotated_Element_00()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            Person @node(person:Stark/Tony)
            {
                ""age"" <= 22,
                ""firstname"" <= @value(),
                ""lastname"" <= @node(\\),
                ""email"" <= ""admin@starkindustries.com"",
                ""phone"" <= ""+31 (909) 477-2353""
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
        public void QueryParser_Parse_Mutation_Annotated_Element_01()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            Person 
            {
                ""age"" <= 22,
                ""firstname"" <= @value(),
                ""company"" <= ""ISOTRONIC"",
                ""email"" <= ""sabrina.stephenson@isotronic.io"",
                ""phone"" <= ""+31 (909) 477-2353""
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
        public void QueryParser_Parse_Mutation_Annotated_Element_02()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var annotatedRootPersonText = @"
            Person 
            {
                ""age"" <= 22,
                ""name"" 
                {
                    ""first"" <= @value(),
                    ""last"" <= ""Stephenson""
                },
                ""company"" <= ""ISOTRONIC"",
                ""email"" <= ""sabrina.stephenson@isotronic.io"",
                ""phone"" <= ""+31 (909) 477-2353""
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
