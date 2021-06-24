// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Parsing.Tests
{
    using EtAlii.Ubigia.Api.Functional.Context;
    using Xunit;

    public class NodeAnnotationsParserTests
    {
        [Fact]
        public void NodeAnnotationsParser_Create()
        {
            // Arrange.

            // Act.
            var parser = CreateNodeAnnotationsParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private INodeAnnotationsParser CreateNodeAnnotationsParser()
        {
            var container = new LapaSchemaParserTestContainerFactory().Create();

            return container.GetInstance<INodeAnnotationsParser>();
        }

        [Fact]
        public void NodeAnnotationsParser_Parse_Node_Person()
        {
            // Arrange.
            var parser = CreateNodeAnnotationsParser();
            var text = "@node(Person:Stark/Tony)";


            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(annotation);
            Assert.IsType<SelectSingleNodeAnnotation>(annotation);
            Assert.Equal("Person:Stark/Tony",annotation.Source?.ToString());
        }

        [Fact]
        public void NodeAnnotationsParser_Parse_Node_Person_Add_Relative_Path()
        {
            // Arrange.
            var parser = CreateNodeAnnotationsParser();
            var text = "@node-link(/Friends, Person:Doe/Jane, /Friends)";


            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(annotation);
            Assert.IsType<LinkAndSelectSingleNodeAnnotation>(annotation);
            Assert.Equal("/Friends",annotation.Source?.ToString());
            Assert.Equal("Person:Doe/Jane",((LinkAndSelectSingleNodeAnnotation)annotation).Target?.ToString());
            Assert.Equal("/Friends",((LinkAndSelectSingleNodeAnnotation)annotation).TargetLink?.ToString());
        }

        [Fact]
        public void NodeAnnotationsParser_Parse_Node_Person_Add_Whitespaces()
        {
            // Arrange.
            var parser = CreateNodeAnnotationsParser();
            var text = "@node-add(Person:Potts, Pepper)";


            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(annotation);
            Assert.IsType<AddAndSelectSingleNodeAnnotation>(annotation);
            Assert.Equal("Person:Potts",annotation.Source?.ToString());
            Assert.Equal("Pepper",((AddAndSelectSingleNodeAnnotation)annotation).Name);
        }

        [Fact]
        public void NodeAnnotationsParser_Parse_Node_Person_Add_Tabs()
        {
            // Arrange.
            var parser = CreateNodeAnnotationsParser();
            var text = "@node-add(Person:Potts\t, \tPepper)";


            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(annotation);
            Assert.IsType<AddAndSelectSingleNodeAnnotation>(annotation);
            Assert.Equal("Person:Potts",annotation.Source?.ToString());
            Assert.Equal("Pepper",((AddAndSelectSingleNodeAnnotation)annotation).Name);
        }
        [Fact]
        public void NodeAnnotationsParser_Parse_Node_Person_Add_Compact()
        {
            // Arrange.
            var parser = CreateNodeAnnotationsParser();
            var text = "@node-add(Person:Potts, Pepper)";


            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(annotation);
            Assert.IsType<AddAndSelectSingleNodeAnnotation>(annotation);
            Assert.Equal("Person:Potts",annotation.Source?.ToString());
            Assert.Equal("Pepper",((AddAndSelectSingleNodeAnnotation)annotation).Name);
        }

        [Fact]
        public void NodeAnnotationsParser_Parse_Node_Person_Remove_Whitespaces()
        {
            // Arrange.
            var parser = CreateNodeAnnotationsParser();
            var text = "@node-remove(Person:Potts, Pepper)";


            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(annotation);
            Assert.IsType<RemoveAndSelectSingleNodeAnnotation>(annotation);
            Assert.Equal("Person:Potts",annotation.Source?.ToString());
            Assert.Equal("Pepper", ((RemoveAndSelectSingleNodeAnnotation)annotation).Name);
        }

        [Fact]
        public void NodeAnnotationsParser_Parse_Node_Person_Remove_Tabs()
        {
            // Arrange.
            var parser = CreateNodeAnnotationsParser();
            var text = "@node-remove(Person:Potts\t, \tPepper)";


            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(annotation);
            Assert.IsType<RemoveAndSelectSingleNodeAnnotation>(annotation);
            Assert.Equal("Person:Potts",annotation.Source?.ToString());
            Assert.Equal("Pepper",((RemoveAndSelectSingleNodeAnnotation)annotation).Name);
        }

        [Fact]
        public void NodeAnnotationsParser_Parse_Node_Person_Remove_Compact()
        {
            // Arrange.
            var parser = CreateNodeAnnotationsParser();
            var text = "@node-remove(Person:Potts, Pepper)";


            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(annotation);
            Assert.IsType<RemoveAndSelectSingleNodeAnnotation>(annotation);
            Assert.Equal("Person:Potts",annotation.Source?.ToString());
            Assert.Equal("Pepper",((RemoveAndSelectSingleNodeAnnotation)annotation).Name);
        }

        [Fact]
        public void NodeAnnotationsParser_Parse_Nodes_Persons()
        {
            // Arrange.
            var parser = CreateNodeAnnotationsParser();
            var text = @"@nodes(Person:Doe/*)";


            // Act.
            var node = parser.Parser.Do(text);
            var annotation = parser.Parse(node);

            // Assert.
            Assert.NotNull(node);
            //Assert.Empty(node.Errors);
            Assert.NotNull(annotation);
            Assert.IsType<SelectMultipleNodesAnnotation>(annotation);
            Assert.Equal("Person:Doe/*",annotation.Source.ToString());
        }
    }
}
