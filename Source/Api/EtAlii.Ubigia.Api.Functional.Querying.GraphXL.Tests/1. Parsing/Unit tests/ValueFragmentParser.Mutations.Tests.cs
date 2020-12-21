namespace EtAlii.Ubigia.Api.Functional.Querying.Tests
{
    using EtAlii.Ubigia.Api.Functional.Traversal;
    using Xunit;

    public class ValueFragmentParserMutationsTests
    {
        [Fact]
        public void ValueFragmentParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateValueFragmentParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private INodeValueFragmentParser CreateValueFragmentParser() => new SchemaParserTestContainerFactory().Create().GetInstance<INodeValueFragmentParser>();

        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Space()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "firstname <= \"John\"";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("John",valueFragment.Mutation);
        }

        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Tab()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "firstname\t<=\t\"John\"";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("John",valueFragment.Mutation);
        }

        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Compact()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "firstname<=\"John\"";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.Null(annotation);
            Assert.NotNull(valueFragment);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("John",valueFragment.Mutation);
        }

        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_From_Annotation_Space()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "Location @node-set( Location:NL/Overijssel/Enschede/Oldebokhoek/52 )";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectNodeValueAnnotation>(annotation);
            Assert.Equal("Location",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", ((AssignAndSelectNodeValueAnnotation)annotation).Subject.ToString());
        }

        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_From_Annotation_Compact()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "Location @node-set(Location:NL/Overijssel/Enschede/Oldebokhoek/52)";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectNodeValueAnnotation>(annotation);
            Assert.Equal("Location",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", ((AssignAndSelectNodeValueAnnotation)annotation).Subject.ToString());
        }


        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_From_Annotation_Tab()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "Location\t@node-set(\tLocation:NL/Overijssel/Enschede/Oldebokhoek/52\t)";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectNodeValueAnnotation>(annotation);
            Assert.Equal("Location",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Equal("Location:NL/Overijssel/Enschede/Oldebokhoek/52", ((AssignAndSelectNodeValueAnnotation)annotation).Subject.ToString());
        }

        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Assignment_From_Constant_Space()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "FirstName @node-set(\"John\")";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectNodeValueAnnotation>(annotation);
            Assert.Equal("FirstName",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Null(annotation.Source);
            var subject = Assert.IsType<StringConstantSubject>(((AssignAndSelectNodeValueAnnotation)annotation).Subject);
            Assert.Equal("John", subject.Value);
        }

        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Assignment_From_Constant_Tab()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "FirstName @node-set(\t\"John\"\t)";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectNodeValueAnnotation>(annotation);
            Assert.Equal("FirstName",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Null(annotation.Source);
            var subject = Assert.IsType<StringConstantSubject>(((AssignAndSelectNodeValueAnnotation)annotation).Subject);
            Assert.Equal("John", subject.Value);
        }
        [Fact]
        public void ValueFragmentParser_Parse_Value_Mutation_Assignment_From_Constant_Compact()
        {
            // Arrange.
            var parser = CreateValueFragmentParser();
            var text = "FirstName @node-set(\"John\")";

            // Act.
            var node = parser.Parser.Do(text);
            var valueFragment = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            var annotation = valueFragment.Annotation;
            Assert.NotNull(annotation);
            Assert.IsType<AssignAndSelectNodeValueAnnotation>(annotation);
            var subject = Assert.IsType<StringConstantSubject>(((AssignAndSelectNodeValueAnnotation)annotation).Subject);
            Assert.Equal("John", subject.Value);
            Assert.Equal("FirstName",valueFragment.Name);
            Assert.Equal(FragmentType.Mutation, valueFragment.Type);
            Assert.Null(annotation.Source);
        }
    }
}
