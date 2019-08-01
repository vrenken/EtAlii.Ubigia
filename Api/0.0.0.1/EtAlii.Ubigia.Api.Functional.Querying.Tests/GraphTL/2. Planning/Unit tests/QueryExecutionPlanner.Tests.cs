﻿namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public partial class QueryExecutionPlannerTests 
    {
        
        private IQueryExecutionPlanner CreatePlanner()
        {
            var configuration = new QueryProcessorConfiguration();
            var container = new Container();
            new QueryExecutionPlanningScaffolding().Register(container);
            new QueryProcessingScaffolding(configuration).Register(container);
            return container.GetInstance<IQueryExecutionPlanner>();
        }
        
        [Fact]
        public void QueryExecutionPlanner_Create()
        {
            // Arrange.
            
            // Act.
            var planner = CreatePlanner();

            // Assert.
            Assert.NotNull(planner);
        }

        [Fact]
        public void QueryExecutionPlanner_Plan_Simple_00()
        {
            // Arrange.
            var parser = new QueryParserFactory().Create(new QueryParserConfiguration());
            var planner = CreatePlanner();

            var queryText = @"
            Person @node(person:Doe/John) 
            {
                age,
                company,
                email,
                phone,
                name @node(\#FamilyName)
                {
                    first @value(/FirstName),
                    last @value()
                }
            }";

            var parseResult = parser.Parse(queryText);
            var query = parseResult.Query;
            
            // Act.
            var executionPlans = planner.Plan(query);

            // Assert.
            var fragmentMetadata = query.Structure.Metadata;

            Assert.NotNull(fragmentMetadata);
            Assert.Equal("Person @Node(person:Doe/John)", fragmentMetadata.ToString());
            Assert.Equal(5, fragmentMetadata.Children.Length);
            Assert.Equal("age", fragmentMetadata.Children[0].ToString());
            Assert.Equal("company", fragmentMetadata.Children[1].ToString());
            Assert.Equal("email", fragmentMetadata.Children[2].ToString());
            Assert.Equal("phone", fragmentMetadata.Children[3].ToString());
            Assert.Equal("name @Node(\\#FamilyName)", fragmentMetadata.Children[4].ToString());
            
            Assert.NotNull(executionPlans);
            Assert.Equal(8, executionPlans.Length);
            Assert.Equal("Person @Node(person:Doe/John)", executionPlans[0].ToString());
            Assert.Equal("age", executionPlans[1].ToString());
            Assert.Equal("company", executionPlans[2].ToString());
            Assert.Equal("email", executionPlans[3].ToString());
            Assert.Equal("phone", executionPlans[4].ToString());
            Assert.Equal("name @Node(\\#FamilyName)", executionPlans[5].ToString());
            Assert.Equal("first @Value(/FirstName)", executionPlans[6].ToString());
            Assert.Equal("last @Value()", executionPlans[7].ToString());

        }
    }
}
