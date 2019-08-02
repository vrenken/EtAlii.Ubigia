namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using Xunit;

    public partial class SchemaParserTests 
    {
        [Fact]
        public void SchemaParser_Parse_Mutation_With_Comment_And_Object_Multiple_Lines_01()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_With_Comment_And_Object_Multiple_Lines_02()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Without_Comment_And_Object_Multiple_Lines_01()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
            var normalPersonText = @"Data
            { 
                ""key"" <= ""value"" 
            }";
            
            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);
        }

        
        [Fact]
        public void SchemaParser_Parse_Mutation_Without_Comment_And_Object_Multiple_Lines_02()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);
        }

        
        [Fact]
        public void SchemaParser_Parse_Mutation_NonRooted_Select()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.Equal("Person",parseResult.Schema.Structure.Annotation.Path.Parts[1].ToString());
            Assert.Equal("Stark",parseResult.Schema.Structure.Annotation.Path.Parts[3].ToString());
            Assert.Equal("Tony",parseResult.Schema.Structure.Annotation.Path.Parts[5].ToString());
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);
            var structureMutation = (StructureMutation)parseResult.Schema.Structure; 
            Assert.Equal("22",structureMutation.Values.Cast<ValueMutation>().Single(v => v.Name == "age").Value);
            Assert.Equal("sabrina.stephenson@isotronic.io",structureMutation.Values.Cast<ValueMutation>().Single(v => v.Name == "email").Value);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Annotated_Root()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.IsType<Annotation>(parseResult.Schema.Structure.Annotation);
            //Assert.Equal("traverse(person:Stephenson/Sabrina)",jsonNode.Annotation);
        }

        
        [Fact]
        public void SchemaParser_Parse_Mutation_Rooted_Select()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Flat()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Flat_Annotated()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Nested()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Annotated_Element_00()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);

            //Assert.Equal("id()", jsonNode.Children.ToArray()[1].Annotation);
        }
        
        
        [Fact]
        public void SchemaParser_Parse_Mutation_Annotated_Element_01()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);

            //Assert.Equal("id()", jsonNode.Children.ToArray()[1].Annotation);
        }
        [Fact]
        public void SchemaParser_Parse_Mutation_Annotated_Element_02()
        {
            // Arrange.
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.IsType<StructureMutation>(parseResult.Schema.Structure);

            //Assert.Equal("id()", jsonNode.Children.ToArray()[1].Children.ToArray()[0].Annotation);
        }
    }
}
