namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using Xunit;

    public class StructureMutationParserTests 
    {
        [Fact]
        public void StructureMutationParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateStructureMutationParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IStructureMutationParser CreateStructureMutationParser()
        {

            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<IStructureMutationParser>();
        }
        
        [Fact]
        public void StructureMutationParser_Parse_ValueMutation_Key_Value_Single()
        {
            // Arrange.
            var parser = CreateStructureMutationParser();
            var text = @"Person @node(Person:Stark/Tony)
            {
                key <= ""value""
            }";
            
            
            // Act.
            var node = parser.Parser.Do(text);
            var query = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(query);
            Assert.NotEmpty(query.Values);
            Assert.IsType<ValueMutation>(query.Values[0]);
        }
        
                
        [Fact]
        public void StructureMutationParser_Parse_ValueMutation_With_Multiple_ValueMutations_01()
        {
            // Arrange.
            var parser = CreateStructureMutationParser();
            var text = @"Person @node(Person:Stark/Tony)
            {
                key1 <= ""value1"",
                key2 <= ""value2""
            }";
            
            
            // Act.
            var node = parser.Parser.Do(text);
            var query = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(query);
            Assert.NotEmpty(query.Values);
            Assert.IsType<ValueMutation>(query.Values[0]);
            Assert.IsType<ValueMutation>(query.Values[1]);
        }        

        [Fact]
        public void StructureMutationParser_Parse_ValueMutation_With_Multiple_ValueMutations_02()
        {
            // Arrange.
            var parser = CreateStructureMutationParser();
            var text = @"Person @node(Person:Stark/Tony)
            {
                age <= ""22"",
                first <= ""Sabrina"",
                last <= ""Stephenson"",
                company <= ""ISOTRONIC"",
                email <= ""sabrina.stephenson@isotronic.io"",
                phone <= ""+31 (909) 477-2353""
            }";
            
            // Act.
            var node = parser.Parser.Do(text);
            var @object = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(@object);
        }
        
        [Fact]
        public void StructureMutationParser_Parse_ValueMutation_With_Annotations_00()
        {
            // Arrange.
            var parser = CreateStructureMutationParser();
            var text = @"Person @node(person:Stark/Tony)
            {
                ""age"" <= 22,
                ""firstname"" <= @value(),
                ""lastname"" <= @value(\#FamilyName),
                ""email"" <= ""admin@starkindustries.com"",
                ""phone"" <= ""+31 (909) 477-2353""
            }";
            
            // Act.
            var node = parser.Parser.Do(text);
            var structureMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(structureMutation);
            var valueMutation1 = structureMutation.Values.Single(v => v.Name == "firstname") as ValueMutation; 
            var valueMutation2 = structureMutation.Values.Single(v => v.Name == "lastname") as ValueMutation; 
            Assert.NotNull(valueMutation1);
            Assert.Equal(AnnotationType.Value,valueMutation1.Annotation.Type);
            Assert.Null(valueMutation1.Annotation.Path);
            Assert.NotNull(valueMutation2);
            Assert.Equal(AnnotationType.Value,valueMutation2.Annotation.Type);
            Assert.Equal(@"\#FamilyName",valueMutation2.Annotation.Path.ToString());
        }
        
        [Fact]
        public void StructureMutationParser_Parse_ValueMutation_With_Annotations_01()
        {
            // Arrange.
            var parser = CreateStructureMutationParser();
            var text = @"Person @node(person:Stark/Tony)
            {
                age <= 22,
                firstname <= @value(),
                lastname <= @value(\#FamilyName),
                email <= ""admin@starkindustries.com"",
                phone <= ""+31 (909) 477-2353""
            }";
            
            // Act.
            var node = parser.Parser.Do(text);
            var structureMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(structureMutation);
        }
                
        [Fact]
        public void StructureMutationParser_Parse_StructureMutation_01()
        {
            // Arrange.
            var parser = CreateStructureMutationParser();
            var text = @"Friends @nodes(/Friends += Person:Vrenken/Peter)
                        {
                            FirstName @value()
                            LastName @value(\#FamilyName)
                        }";
            
            // Act.
            var node = parser.Parser.Do(text);
            var structureMutation = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(structureMutation);
            Assert.NotNull(structureMutation.Annotation);
            Assert.NotNull(structureMutation.Annotation.Path);
            Assert.NotNull(structureMutation.Annotation.Operator);
            Assert.NotNull(structureMutation.Annotation.Subject);
            Assert.Equal(AnnotationType.Nodes,structureMutation.Annotation.Type);
            Assert.Equal("/Friends", structureMutation.Annotation.Path.ToString());
            Assert.Equal(" += ", structureMutation.Annotation.Operator.ToString());
            Assert.Equal("Person:Vrenken/Peter", structureMutation.Annotation.Subject.ToString());
            
            var valueQuery1 = structureMutation.Values.Single(v => v.Name == "FirstName") as ValueQuery; 
            Assert.NotNull(valueQuery1);
            Assert.Equal(AnnotationType.Value,valueQuery1.Annotation.Type);
            Assert.Null(valueQuery1.Annotation.Path);

            var valueQuery2 = structureMutation.Values.Single(v => v.Name == "LastName") as ValueQuery; 
            Assert.NotNull(valueQuery2);
            Assert.Equal(AnnotationType.Value,valueQuery2.Annotation.Type);
            Assert.Equal(@"\#FamilyName",valueQuery2.Annotation.Path.ToString());


        }
    }
}
