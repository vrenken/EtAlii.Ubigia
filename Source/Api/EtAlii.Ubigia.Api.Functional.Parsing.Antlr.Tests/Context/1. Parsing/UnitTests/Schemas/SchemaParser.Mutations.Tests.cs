namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Functional.Context;
    using Xunit;

    public partial class SchemaParserTests
    {
        [Fact]
        public void SchemaParser_Parse_Nested_Mutation()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var text = @"
            Person = @nodes(Person:Doe/John)
            {
                FirstName = @node()
                LastName = @node(\#FamilyName)
                NickName
                Birthdate
                Friends = @nodes-link(/Friends, Person:Banner/Peter, /Friends)
                {
                    FirstName = @node()
                    LastName = @node(\#FamilyName)
                }
            }";

            // Act.
            var parseResult = parser.Parse(text);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            var structureFragment = parseResult.Schema.Structure;
            Assert.NotNull(structureFragment);
            Assert.Equal(FragmentType.Query, structureFragment.Type);
            Assert.Single(structureFragment.Children);
            var childStructure = structureFragment.Children[0];

            Assert.Equal(FragmentType.Mutation, childStructure.Type);
            Assert.NotNull(childStructure.Annotation);
            Assert.NotNull(childStructure.Annotation.Source);
            Assert.IsType<LinkAndSelectMultipleNodesAnnotation>(childStructure.Annotation);
            var linkAnnotation = (LinkAndSelectMultipleNodesAnnotation)childStructure.Annotation;
            Assert.Equal("/Friends", linkAnnotation.Source.ToString());
            Assert.Equal("Person:Banner/Peter", linkAnnotation.Target.ToString());
            Assert.Equal("/Friends", linkAnnotation.TargetLink.ToString());

            var valueFragment1 = childStructure.Values.Single(v => v.Name == "FirstName");
            Assert.NotNull(valueFragment1);
            Assert.Equal(FragmentType.Query,valueFragment1.Type);
            Assert.Null(valueFragment1.Annotation.Source);

            var valueFragment2 = childStructure.Values.Single(v => v.Name == "LastName");
            Assert.NotNull(valueFragment2);
            Assert.Equal(FragmentType.Query,valueFragment2.Type);
            Assert.Equal(@"\#FamilyName",valueFragment2.Annotation.Source.ToString());
        }

        [Fact]
        public void SchemaParser_Parse_Mutation_With_Comment_And_Object_Multiple_Lines_01()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"-- This is a comment
            Data
            {
                ""key"" = ""value""
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
            Data = @node(Person:Doe/John)
            {
                ""key"" = ""value""
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
                ""key"" = ""value""
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
                ""key"" = ""value""
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
            Person = @node(/Person/Stark/Tony)
            {
                ""age"" = ""22"",
                ""first"" = ""Sabrina"",
                ""last"" = ""Stephenson"",
                ""company"" = ""ISOTRONIC"",
                ""email"" = ""sabrina.stephenson@isotronic.io"",
                ""phone"" = ""+31 (909) 477-2353""
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
            Person = @node(person:Stephenson/Sabrina)
            {
                ""age"" = 22,
                ""name""
                {
                    ""first"" = ""Sabrina"",
                    ""last"" = ""Stephenson""
                },
                ""company"" = ""ISOTRONIC"",
                ""email"" = ""sabrina.stephenson@isotronic.io"",
                ""phone"" = ""+31 (909) 477-2353""
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
            Person = @node(Person:Start/Tony)
            {
                age = ""22"",
                first = ""Sabrina"",
                last = ""Stephenson"",
                company = ""ISOTRONIC"",
                email = ""sabrina.stephenson@isotronic.io"",
                phone = ""+31 (909) 477-2353""
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
                ""age"" = ""22"",
                ""first"" = ""Sabrina"",
                ""last"" = ""Stephenson"",
                ""company"" = ""ISOTRONIC"",
                ""email"" = ""sabrina.stephenson@isotronic.io"",
                ""phone"" = ""+31 (909) 477-2353""
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
            Person = @node(Person:Doe/John)
            {
                ""age"" = ""22"",
                ""first"" = ""John"",
                ""last"" = ""Doe"",
                ""company"" = ""ISOTRONIC"",
                ""email"" = ""sabrina.stephenson@isotronic.io"",
                ""phone"" = ""+31 (909) 477-2353""
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
                ""age"" = 22,
                ""name""
                {
                    ""first"" = ""Sabrina"",
                    ""last"" = ""Stephenson""
                },
                ""company"" = ""ISOTRONIC"",
                ""email"" = ""sabrina.stephenson@isotronic.io"",
                ""phone"" = ""+31 (909) 477-2353""
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
            Person = @node(person:Stark/Tony)
            {
                ""age"" = 22,
                ""firstname"" = @node(),
                ""lastname"" = @node(\\),
                ""email"" = ""admin@starkindustries.com"",
                ""phone"" = ""+31 (909) 477-2353""
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
                ""age"" = 22,
                ""firstname"" = @node(),
                ""company"" = ""ISOTRONIC"",
                ""email"" = ""sabrina.stephenson@isotronic.io"",
                ""phone"" = ""+31 (909) 477-2353""
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
                ""age"" = 22,
                ""name""
                {
                    ""first"" = @node(),
                    ""last"" = ""Stephenson""
                },
                ""company"" = ""ISOTRONIC"",
                ""email"" = ""sabrina.stephenson@isotronic.io"",
                ""phone"" = ""+31 (909) 477-2353""
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
