﻿namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public class StructureQueryParserTests 
    {
        [Fact]
        public void StructureQueryParser_Create()
        {
            // Arrange.
            
            // Act.
            var parser = CreateStructureQueryParser();

            // Assert.
            Assert.NotNull(parser);
        }

        private IStructureQueryParser CreateStructureQueryParser()
        {

            var scaffoldings = new IScaffolding[]
            {
                new PathSubjectParsingScaffolding(),
                new ConstantHelpersScaffolding(),
                new QueryParserScaffolding(),
            };
            
            var container = new Container();

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            return container.GetInstance<IStructureQueryParser>();
        }

        [Fact]
        public void StructureQueryParser_Parse_Query_With_Multiple_ValueQueries_01()
        {
            // Arrange.
            var parser = CreateStructureQueryParser();
            var text = @"Person @node(Person:Stark/Tony)
            {
                key1,
                key2
            }";
            
            
            // Act.
            var node = parser.Parser.Do(text);
            var query = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(query);
            Assert.NotEmpty(query.Values);
            Assert.IsType<ValueQuery>(query.Values[0]);
            Assert.IsType<ValueQuery>(query.Values[1]);
        }        

        [Fact]
        public void StructureQueryParser_Parse_Query_With_Multiple_ValueQueries_02()
        {
            // Arrange.
            var parser = CreateStructureQueryParser();
            var text = @"Person @node(Person:Stark/Tony)
            {
                ""key1"",
                ""key2""
            }";
            
            
            // Act.
            var node = parser.Parser.Do(text);
            var query = parser.Parse(node);
            
            // Assert.
            Assert.NotNull(node);
            Assert.NotNull(query);
            Assert.NotEmpty(query.Values);
            Assert.IsType<ValueQuery>(query.Values[0]);
            Assert.IsType<ValueQuery>(query.Values[1]);
        }        
    }
}
