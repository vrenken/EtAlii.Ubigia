// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class SequenceExecutionPlannerTests : IClassFixture<TraversalUnitTestContext>, IDisposable
    {
        private IScriptParser _parser;
        private readonly TraversalUnitTestContext _testContext;

        public SequenceExecutionPlannerTests(TraversalUnitTestContext testContext)
        {
            _testContext = testContext;
            _parser = new TestScriptParserFactory().Create(_testContext.ClientConfiguration);
        }

        public void Dispose()
        {
            _parser = null;
            GC.SuppressFinalize(this);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Create()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);

            // Act.
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);

            // Assert.
            Assert.NotNull(sequencePlanner);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Simple_Add()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Person/Banner += Tanja", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("/Person/Banner += \"Tanja\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Simple_Add_With_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Banner += Tanja", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Banner += \"Tanja\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Simple_Remove_With_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Banner -= Tanja", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Banner -= \"Tanja\"", executionPlan.ToString());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Simple_Assign_With_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Banner/Tanja <= { Gender:'Female'}", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Banner/Tanja <= Gender: Female", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Simple_Assign_With_Variable_From_Variable()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Banner/Tanja <= $var2", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Banner/Tanja <= $var2", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Simple_Assign_With_Variable_From_Variable_With_Comments()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Banner/Tanja <= $var2 --These are comments", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Banner/Tanja <= $var2", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Simple_Add_To_Path()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Person/Banner += Tanja", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("/Person/Banner += \"Tanja\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Simple_Remove_From_Path()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Person/Banner -= NoOne", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("/Person/Banner -= \"NoOne\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Simple_Remove_From_Path_With_Comments()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Person/Banner -= NoOne --These are comments", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("/Person/Banner -= \"NoOne\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Only_Comments()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("--These are comments", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.Equal(SequenceExecutionPlan.Empty, executionPlan);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task SequenceExecutionPlanner_Plan_Only_Comments_With_Leading_Space()
        {
            // Arrange.
            var scope = new ExecutionScope();
            var sequencePartExecutionPlannerSelector = await TestSequencePartExecutionPlannerSelector
                .Create(_testContext.Logical)
                .ConfigureAwait(false);
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("--These are comments", scope);

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.Equal(SequenceExecutionPlan.Empty, executionPlan);
        }
    }
}
