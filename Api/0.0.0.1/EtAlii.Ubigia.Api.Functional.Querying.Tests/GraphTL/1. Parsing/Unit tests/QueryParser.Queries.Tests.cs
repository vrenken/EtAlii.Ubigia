namespace EtAlii.Ubigia.Api.Functional.Tests
{
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
        }

        [Fact]
        public void QueryParser_Parse_Query_Flat_03()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
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
            Assert.NotNull(parseResult.Query);
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
        }
        
        [Fact]
        public void QueryParser_Parse_Query_Flat_04()
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);

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
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);

            //Assert.Equal("id()", jsonNode.Children.ToArray()[1].Children.ToArray()[0].Annotation);
        }
        
        [Fact]
        public void QueryParser_Parse_Query_Nested_04()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var queryText = @"Person @nodes(Person:Stark/Tony)
                               {
                                    Data
                                    {
                                        FirstName @value()
                                        LastName @value(\#FamilyName)
                                    }
                               }";


            // Act.
            var parseResult = parser.Parse(queryText);

            // Assert.
            Assert.NotNull(parseResult);
            Assert.Empty(parseResult.Errors);
            Assert.NotNull(parseResult.Query);
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
            var structureQuery = (StructureQuery)parseResult.Query.Structure;
            Assert.Empty(structureQuery.Values); 
            Assert.Single(structureQuery.Children); 
            Assert.Equal("Data", structureQuery.Children[0].Name); 
            Assert.Null(structureQuery.Children[0].Annotation); 
            Assert.Equal(2, structureQuery.Children[0].Values.Length); 
            Assert.Equal("FirstName", structureQuery.Children[0].Values[0].Name); 
            Assert.Equal("LastName", structureQuery.Children[0].Values[1].Name); 
        }
        
        
        [Fact]
        public void QueryParser_Parse_Query_Nested_05()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var queryText = @"
            Person @node(person:Doe/John) 
            {
                age,
                name @node(\#FamilyName)
                {
                    first @value(/FirstName),
                    last @value()
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
            Assert.NotNull(parseResult.Query);
            Assert.NotNull(parseResult.Query.Structure);
            Assert.IsType<StructureQuery>(parseResult.Query.Structure);
            var structureQuery = (StructureQuery)parseResult.Query.Structure;
            Assert.Equal(4, structureQuery.Values.Length); 
            Assert.Single(structureQuery.Children); 
            Assert.Equal("name", structureQuery.Children[0].Name); 
            Assert.Equal("@Node(\\#FamilyName)", structureQuery.Children[0].Annotation.ToString()); 
            Assert.Equal(2, structureQuery.Children[0].Values.Length); 
            Assert.Equal("first", structureQuery.Children[0].Values[0].Name); 
            Assert.Equal("@Value(/FirstName)", structureQuery.Children[0].Values[0].Annotation.ToString()); 
            Assert.Equal("last", structureQuery.Children[0].Values[1].Name); 
            Assert.Equal("@Value()", structureQuery.Children[0].Values[1].Annotation.ToString()); 
            Assert.Equal("age", structureQuery.Values[0].Name); 
            Assert.Equal("company", structureQuery.Values[1].Name); 
            Assert.Equal("email", structureQuery.Values[2].Name); 
            Assert.Equal("phone", structureQuery.Values[3].Name); 

        }
    }
}
