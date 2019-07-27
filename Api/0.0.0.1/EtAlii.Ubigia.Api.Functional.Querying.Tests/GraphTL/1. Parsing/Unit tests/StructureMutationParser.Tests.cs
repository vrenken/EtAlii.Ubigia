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

            var container = new QueryParserTestContainerFactory().Create();

            return container.GetInstance<IStructureMutationParser>();
        }
        
        [Fact]
        public void StructureMutationParser_Parse_Mutation_Key_Value_Single()
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
        public void StructureMutationParser_Parse_Mutation_With_Multiple_ValueMutations_01()
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
        public void StructureMutationParser_Parse_Mutation_With_Multiple_ValueMutations_02()
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
        public void StructureMutationParser_Parse_Mutation_With_Annotations_00()
        {
            // Arrange.
            var parser = CreateStructureMutationParser();
            var text = @"Person @node(person:Stark/Tony)
            {
                ""age"" <= 22,
                ""firstname"" <= @value(),
                ""lastname"" <= @node(\\),
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
            Assert.Equal(AnnotationType.Node,valueMutation2.Annotation.Type);
            Assert.Equal(@"\\",valueMutation2.Annotation.Path.ToString());
        }
        
        [Fact]
        public void StructureMutationParser_Parse_Mutation_With_Annotations_01()
        {
            // Arrange.
            var parser = CreateStructureMutationParser();
            var text = @"Person @node(person:Stark/Tony)
            {
                age <= 22,
                firstname <= @value(),
                lastname <= @node(\\),
                email <= ""admin@starkindustries.com"",
                phone <= ""+31 (909) 477-2353""
            }";
            
            // Act.
            var node = parser.Parser.Do(text);
            var @object = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(@object);
        }
        
    }
}
