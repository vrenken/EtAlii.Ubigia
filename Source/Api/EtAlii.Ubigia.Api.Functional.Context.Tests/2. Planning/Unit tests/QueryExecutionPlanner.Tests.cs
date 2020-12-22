namespace EtAlii.Ubigia.Api.Functional.Context.Tests
{
    using System.Linq;
    using EtAlii.xTechnology.MicroContainer;
    using Xunit;

    public class QueryExecutionPlannerTests
    {

        private ISchemaExecutionPlanner CreatePlanner()
        {
            var configuration = new SchemaProcessorConfiguration();
            var container = new Container();
            new SchemaExecutionPlanningScaffolding().Register(container);
            new SchemaProcessingScaffolding(configuration).Register(container);
            return container.GetInstance<ISchemaExecutionPlanner>();
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
            var parser = new SchemaParserFactory().Create(new SchemaParserConfiguration());
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
                    first @node(/FirstName),
                    last @node()
                }
            }";

            var parseResult = parser.Parse(queryText);
            var query = parseResult.Schema;

            // Act.
            var executionPlans = planner.Plan(query);

            // Assert.
            var fragmentMetadata = executionPlans.FirstOrDefault()?.Metadata;

            Assert.NotNull(fragmentMetadata);
            Assert.Equal("Person @node(person:Doe/John)", fragmentMetadata.ToString());
            Assert.Equal(5, fragmentMetadata.Children.Length);
            Assert.Equal("age", fragmentMetadata.Children[0].ToString());
            Assert.Equal("company", fragmentMetadata.Children[1].ToString());
            Assert.Equal("email", fragmentMetadata.Children[2].ToString());
            Assert.Equal("phone", fragmentMetadata.Children[3].ToString());
            Assert.Equal("name @node(\\#FamilyName)", fragmentMetadata.Children[4].ToString());

            Assert.NotNull(executionPlans);
            Assert.Equal(8, executionPlans.Length);
            Assert.Equal("Person @node(person:Doe/John)", executionPlans[0].ToString());
            Assert.Equal("age", executionPlans[1].ToString());
            Assert.Equal("company", executionPlans[2].ToString());
            Assert.Equal("email", executionPlans[3].ToString());
            Assert.Equal("phone", executionPlans[4].ToString());
            Assert.Equal("name @node(\\#FamilyName)", executionPlans[5].ToString());
            Assert.Equal("first @node(/FirstName)", executionPlans[6].ToString());
            Assert.Equal("last @node()", executionPlans[7].ToString());

        }
    }
}
