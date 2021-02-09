namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.Linq;
    using Xunit;

    public partial class SchemaParserTests
    {
        [Fact]
        public void SchemaParser_Parse_Query_Flat_01()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);

            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Flat_02()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Flat_03()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person
            {
                age
                first
                last
                company
                email
                phone
            }";


            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Flat_04()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Flat_Annotated_01()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person = @node(Person:Doe/John)
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Flat_Annotated_02()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person = @node(Person:Doe/John)
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Flat_Annotated_03()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person = @node(Person:Doe/John)
            {
                age, first, last, company, email, phone
            }";


            // Act.
            var parseResult = parser.Parse(normalPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }


        [Fact]
        public void SchemaParser_Parse_Query_Nested_01()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person = @node(person:Doe/John)
            {
                ""age"",
                ""name"" = @node(\LastName)
                {
                    ""first"" = @node(/FirstName),
                    ""last"" = @node()
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Nested_02()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person = @node(person:Doe/John)
            {
                ""age"",
                ""name""
                {
                    ""first"" = @node(),
                    ""last"" = @node(\\LastName)
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);

            var childStructure = parseResult.Schema.Structure.Children.SingleOrDefault();
            Assert.NotNull(childStructure);
            Assert.Equal(FragmentType.Query, childStructure.Type);
            Assert.Equal("name", childStructure.Name);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Nested_03()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var normalPersonText = @"
            Person = @node(person:Doe/John)
            {
                age,
                name = @node(\LastName)
                {
                    first = @node(/FirstName),
                    last = @node()
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);

            var childStructure = parseResult.Schema.Structure.Children.SingleOrDefault();
            Assert.NotNull(childStructure);
            Assert.Equal(FragmentType.Query, childStructure.Type);
            Assert.Equal("name", childStructure.Name);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Annotated_Root_No_Values()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var annotatedRootPersonText = @"
            Person = @node(person:Stephenson/Sabrina)
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);

            var childStructure = parseResult.Schema.Structure.Children.SingleOrDefault();
            Assert.NotNull(childStructure);
            Assert.Equal(FragmentType.Query, childStructure.Type);
            Assert.Equal("name", childStructure.Name);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Annotated_Element_No_Values_01()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var annotatedRootPersonText = @"
            Person
            {
                ""age"",
                ""firstname"" = @node(),
                ""company"",
                ""email"",
                ""phone""
            }";


            // Act.
            var parseResult = parser.Parse(annotatedRootPersonText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);
            Assert.Equal("Person", parseResult.Schema.Structure.Name);
        }


        [Fact]
        public void SchemaParser_Parse_Query_Annotated_Element_No_Values_02()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var annotatedRootPersonText = @"
            Person
            {
                ""age"",
                ""name""
                {
                    ""first"" = @node(),
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
            Assert.NotNull(parseResult.Schema);
            Assert.NotNull(parseResult.Schema.Structure);
            Assert.Equal(FragmentType.Query, parseResult.Schema.Structure.Type);

            var childStructure = parseResult.Schema.Structure.Children.SingleOrDefault();
            Assert.NotNull(childStructure);
            Assert.Equal(FragmentType.Query, childStructure.Type);
            Assert.Equal("name", childStructure.Name);
        }

        [Fact]
        public void SchemaParser_Parse_Query_Nested_04()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var queryText = @"Person = @nodes(Person:Stark/Tony)
                               {
                                    Data
                                    {
                                        FirstName = @node()
                                        LastName = @node(\#FamilyName)
                                    }
                               }";


            // Act.
            var parseResult = parser.Parse(queryText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            var structureFragment = parseResult.Schema.Structure;
            Assert.NotNull(structureFragment);
            Assert.Equal(FragmentType.Query, structureFragment.Type);
            Assert.Empty(structureFragment.Values);
            Assert.Single(structureFragment.Children);
            Assert.Equal("Data", structureFragment.Children[0].Name);
            Assert.Null(structureFragment.Children[0].Annotation);
            Assert.Equal(2, structureFragment.Children[0].Values.Length);
            Assert.Equal("FirstName", structureFragment.Children[0].Values[0].Name);
            Assert.Equal(FragmentType.Query, structureFragment.Children[0].Values[0].Type);
            Assert.Equal("LastName", structureFragment.Children[0].Values[1].Name);
            Assert.Equal(FragmentType.Query, structureFragment.Children[0].Values[1].Type);
        }


        [Fact]
        public void SchemaParser_Parse_Query_Nested_05()
        {
            // Arrange.
            var parser = new TestSchemaParserFactory().Create();
            var queryText = @"
            Person = @node(person:Doe/John)
            {
                age,
                name = @node(\#FamilyName)
                {
                    first = @node(/FirstName),
                    last = @node()
                },
                company,
                email,
                phone
            }";

            // Act.
            var parseResult = parser.Parse(queryText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Schema);
            var structureFragment = parseResult.Schema.Structure;
            Assert.NotNull(structureFragment);
            Assert.Equal(FragmentType.Query, structureFragment.Type);
            Assert.Equal(4, structureFragment.Values.Length);
            Assert.Single(structureFragment.Children);
            Assert.Equal("name", structureFragment.Children[0].Name);
            Assert.Equal(FragmentType.Query, structureFragment.Children[0].Type);
            Assert.Equal("@node(\\#FamilyName)", structureFragment.Children[0].Annotation.ToString());
            Assert.Equal(2, structureFragment.Children[0].Values.Length);
            Assert.Equal("first", structureFragment.Children[0].Values[0].Name);
            Assert.Equal(FragmentType.Query, structureFragment.Children[0].Values[0].Type);
            Assert.Equal("@node(/FirstName)", structureFragment.Children[0].Values[0].Annotation.ToString());
            Assert.Equal("last", structureFragment.Children[0].Values[1].Name);
            Assert.Equal(FragmentType.Query, structureFragment.Children[0].Values[1].Type);
            Assert.Equal("@node()", structureFragment.Children[0].Values[1].Annotation.ToString());
            Assert.Equal("age", structureFragment.Values[0].Name);
            Assert.Equal(FragmentType.Query, structureFragment.Values[0].Type);
            Assert.Equal("company", structureFragment.Values[1].Name);
            Assert.Equal(FragmentType.Query, structureFragment.Values[1].Type);
            Assert.Equal("email", structureFragment.Values[2].Name);
            Assert.Equal(FragmentType.Query, structureFragment.Values[2].Type);
            Assert.Equal("phone", structureFragment.Values[3].Name);
            Assert.Equal(FragmentType.Query, structureFragment.Values[3].Type);

        }
    }
}
