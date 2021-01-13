namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.Linq;
    using Xunit;

    public partial class SchemaParserTests
    {
        [Fact]
        public void SchemaParser_Parse_Mutation_With_Comment_And_Object_Multiple_Lines_01()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_With_Comment_And_Object_Multiple_Lines_02()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Values.Single().Type);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Without_Comment_And_Object_Multiple_Lines_01()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Values.Single().Type);
        }


        [Fact]
        public void SchemaParser_Parse_Mutation_Without_Comment_And_Object_Multiple_Lines_02()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Values.Single().Type);
        }


        [Fact]
        public void SchemaParser_Parse_Mutation_NonRooted_Select()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var schemaText = @"-- Comment goes here.
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
            var parseResult = parser.Parse(schemaText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.Equal("Person",parseResult.Schema.Structure.Annotation.Source.Parts[1].ToString());
            Assert.Equal("Stark",parseResult.Schema.Structure.Annotation.Source.Parts[3].ToString());
            Assert.Equal("Tony",parseResult.Schema.Structure.Annotation.Source.Parts[5].ToString());
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);

            var ageStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "age");
            Assert.NotNull(ageStructure);
            Assert.Equal(FragmentType.Mutation, ageStructure.Type);
            Assert.Equal("22", ageStructure.Mutation);

            var emailStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "email");
            Assert.NotNull(emailStructure);
            Assert.Equal(FragmentType.Mutation, emailStructure.Type);
            Assert.Equal("sabrina.stephenson@isotronic.io", emailStructure.Mutation);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Annotated_Root()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.NotNull(parseResult.Schema.Structure.Annotation);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);

            var ageStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "age");
            Assert.NotNull(ageStructure);
            Assert.Equal(FragmentType.Mutation, ageStructure.Type);
            Assert.Equal(22, ageStructure.Mutation);

            var emailStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "email");
            Assert.NotNull(emailStructure);
            Assert.Equal(FragmentType.Mutation, emailStructure.Type);
            Assert.Equal("sabrina.stephenson@isotronic.io", emailStructure.Mutation);
        }


        [Fact]
        public void SchemaParser_Parse_Mutation_Rooted_Select()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);

            var ageStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "age");
            Assert.NotNull(ageStructure);
            Assert.Equal(FragmentType.Mutation, ageStructure.Type);
            Assert.Equal("22", ageStructure.Mutation);

            var emailStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "email");
            Assert.NotNull(emailStructure);
            Assert.Equal(FragmentType.Mutation, emailStructure.Type);
            Assert.Equal("sabrina.stephenson@isotronic.io", emailStructure.Mutation);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Flat()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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

            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);

            var ageStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "age");
            Assert.NotNull(ageStructure);
            Assert.Equal(FragmentType.Mutation, ageStructure.Type);
            Assert.Equal("22", ageStructure.Mutation);

            var emailStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "email");
            Assert.NotNull(emailStructure);
            Assert.Equal(FragmentType.Mutation, emailStructure.Type);
            Assert.Equal("sabrina.stephenson@isotronic.io", emailStructure.Mutation);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Flat_Annotated()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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


            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);

            var ageStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "age");
            Assert.NotNull(ageStructure);
            Assert.Equal(FragmentType.Mutation, ageStructure.Type);
            Assert.Equal("22", ageStructure.Mutation);

            var emailStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "email");
            Assert.NotNull(emailStructure);
            Assert.Equal(FragmentType.Mutation, emailStructure.Type);
            Assert.Equal("sabrina.stephenson@isotronic.io", emailStructure.Mutation);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Nested()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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

            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);

            var ageStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "age");
            Assert.NotNull(ageStructure);
            Assert.Equal(FragmentType.Mutation, ageStructure.Type);
            Assert.Equal(22, ageStructure.Mutation);

            var emailStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "email");
            Assert.NotNull(emailStructure);
            Assert.Equal(FragmentType.Mutation, emailStructure.Type);
            Assert.Equal("sabrina.stephenson@isotronic.io", emailStructure.Mutation);
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_Annotated_Element_00()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var annotatedRootPersonText = @"
            Person @node(person:Stark/Tony)
            {
                ""age"" <= 22,
                ""firstname"" @node(),
                ""lastname"" @node(\\),
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

            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);

            var ageStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "age");
            Assert.NotNull(ageStructure);
            Assert.Equal(FragmentType.Mutation, ageStructure.Type);
            Assert.Equal(22, ageStructure.Mutation);

            var emailStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "email");
            Assert.NotNull(emailStructure);
            Assert.Equal(FragmentType.Mutation, emailStructure.Type);
            Assert.Equal("admin@starkindustries.com", emailStructure.Mutation);
        }


        [Fact]
        public void SchemaParser_Parse_Mutation_Annotated_Element_01()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var annotatedRootPersonText = @"
            Person
            {
                ""age"" <= 22,
                ""firstname"" @node(),
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

            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);

            var ageStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "age");
            Assert.NotNull(ageStructure);
            Assert.Equal(FragmentType.Mutation, ageStructure.Type);
            Assert.Equal(22, ageStructure.Mutation);

            var emailStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "email");
            Assert.NotNull(emailStructure);
            Assert.Equal(FragmentType.Mutation, emailStructure.Type);
            Assert.Equal("sabrina.stephenson@isotronic.io", emailStructure.Mutation);
        }
        [Fact]
        public void SchemaParser_Parse_Mutation_Annotated_Element_02()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var annotatedRootPersonText = @"
            Person
            {
                ""age"" <= 22,
                ""name""
                {
                    ""first"" @node(),
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

            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Mutation, parseResult.Schema.Structure.Type);

            var ageStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "age");
            Assert.NotNull(ageStructure);
            Assert.Equal(FragmentType.Mutation, ageStructure.Type);
            Assert.Equal(22, ageStructure.Mutation);

            var emailStructure = parseResult.Schema.Structure.Values.SingleOrDefault(v => v.Name == "email");
            Assert.NotNull(emailStructure);
            Assert.Equal(FragmentType.Mutation, emailStructure.Type);
            Assert.Equal("sabrina.stephenson@isotronic.io", emailStructure.Mutation);
        }
    }
}
