﻿namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.Linq;
    using Xunit;

    public class StructureFragmentParserMutationsTests
    {
        [Fact]
        public void StructureFragmentParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateStructureFragmentParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IStructureFragmentParser CreateStructureFragmentParser()
        {

            var container = new SchemaParserTestContainerFactory().Create();

            return container.GetInstance<IStructureFragmentParser>();
        }

        [Fact]
        public void StructureFragmentParser_Parse_ValueMutation_Key_Value_Single()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
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
            Assert.Equal(FragmentType.Mutation, query.Values[0].Type);
        }


        [Fact]
        public void StructureFragmentParser_Parse_ValueMutation_Node_Clear()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
            var text = @"Person @node(Person:Doe/John)
            {
               FirstName @node-clear()
            }";


            // Act.
            var node = parser.Parser.Do(text);
            var query = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(query);
            Assert.NotEmpty(query.Values);
            Assert.Equal(FragmentType.Mutation, query.Values[0].Type);
        }


        [Fact]
        public void StructureFragmentParser_Parse_ValueMutation_With_Multiple_ValueMutations_01()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
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
            Assert.Equal(FragmentType.Mutation, query.Values[0].Type);
            Assert.Equal(FragmentType.Mutation, query.Values[1].Type);
        }

        [Fact]
        public void StructureFragmentParser_Parse_ValueMutation_With_Multiple_ValueMutations_02()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
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
        public void StructureFragmentParser_Parse_ValueMutation_With_Annotations_00()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
            var text = @"Person @node(person:Stark/Tony)
            {
                ""age"" <= 22,
                ""firstname"" @node(),
                ""lastname"" @node(\#FamilyName),
                ""email"" <= ""admin@starkindustries.com"",
                ""phone"" <= ""+31 (909) 477-2353""
            }";

            // Act.
            var node = parser.Parser.Do(text);
            var structureMutation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(structureMutation);

            var valueFragment1 = structureMutation.Values.Single(v => v.Name == "firstname");
            Assert.NotNull(valueFragment1);
            Assert.Equal(FragmentType.Query, valueFragment1.Type);
            Assert.IsType<SelectNodeValueAnnotation>(valueFragment1.Annotation);
            Assert.Null(valueFragment1.Annotation.Source);

            var valueFragment2 = structureMutation.Values.Single(v => v.Name == "lastname");
            Assert.NotNull(valueFragment2);
            Assert.Equal(FragmentType.Query, valueFragment2.Type);
            Assert.IsType<SelectNodeValueAnnotation>(valueFragment2.Annotation);
            Assert.Equal(@"\#FamilyName", valueFragment2.Annotation.Source.ToString());
        }

        [Fact]
        public void StructureFragmentParser_Parse_ValueMutation_With_Annotations_01()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
            var text = @"Person @node(person:Stark/Tony)
            {
                age <= 22,
                firstname @node(),
                lastname @node(\#FamilyName),
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
        public void StructureFragmentParser_Parse_StructureMutation_01()
        {
            // Arrange.
            var parser = CreateStructureFragmentParser();
            var text = @"Friends @nodes-link(/Friends, Person:Banner/Peter, /Friends)
                        {
                            FirstName @node()
                            LastName @node(\#FamilyName)
                        }";

            // Act.
            var node = parser.Parser.Do(text);
            var structureMutation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(structureMutation);
            Assert.Equal(FragmentType.Mutation, structureMutation.Type);
            Assert.NotNull(structureMutation.Annotation);
            var linkAnnotation = Assert.IsType<LinkAndSelectMultipleNodesAnnotation>(structureMutation.Annotation);
            Assert.Equal("/Friends", linkAnnotation.Source.ToString());
            Assert.Equal("Person:Banner/Peter", linkAnnotation.Target.ToString());
            Assert.Equal("/Friends", linkAnnotation.TargetLink.ToString());

            var valueFragment1 = structureMutation.Values.Single(v => v.Name == "FirstName");
            Assert.NotNull(valueFragment1);
            Assert.Equal(FragmentType.Query, valueFragment1.Type);
            Assert.Null(valueFragment1.Annotation.Source);

            var valueFragment2 = structureMutation.Values.Single(v => v.Name == "LastName");
            Assert.NotNull(valueFragment2);
            Assert.Equal(@"\#FamilyName",valueFragment2.Annotation.Source.ToString());
        }
    }
}
