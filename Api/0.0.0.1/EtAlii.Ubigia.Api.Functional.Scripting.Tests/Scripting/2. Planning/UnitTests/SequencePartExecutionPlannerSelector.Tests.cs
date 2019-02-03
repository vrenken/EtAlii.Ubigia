// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting;
    using Xunit;


    
    public class SequenceExecutionPlannerTests : IAsyncLifetime
    {
        private IScriptParser _parser;

        public Task InitializeAsync()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            _parser = null;
            return Task.CompletedTask;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Create()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);

            // Act.
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);

            // Assert.
            Assert.NotNull(sequencePlanner);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Add()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Person/Vrenken += Tanja");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("/Person/Vrenken += \"Tanja\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Add_With_Variable()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Vrenken += Tanja");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Vrenken += \"Tanja\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Remove_With_Variable()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Vrenken -= Tanja");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Vrenken -= \"Tanja\"", executionPlan.ToString());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Assign_With_Variable()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Vrenken/Tanja <= { Gender:'Female'}");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Vrenken/Tanja <= Gender: Female", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Assign_With_Variable_From_Variable()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Vrenken/Tanja <= $var2");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Vrenken/Tanja <= $var2", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Assign_With_Variable_From_Variable_With_Comments()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Person/Vrenken/Tanja <= $var2 #These are comments");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("$var1 <= /Person/Vrenken/Tanja <= $var2", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Add_To_Path()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Person/Vrenken += Tanja");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("/Person/Vrenken += \"Tanja\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Remove_From_Path()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Person/Vrenken -= NoOne");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("/Person/Vrenken -= \"NoOne\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Remove_From_Path_With_Comments()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Person/Vrenken -= NoOne #These are comments");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.IsType<SequenceExecutionPlan>(executionPlan);
            Assert.Equal("/Person/Vrenken -= \"NoOne\"", executionPlan.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Only_Comments()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("#These are comments");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.Equal(SequenceExecutionPlan.Empty, executionPlan);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Only_Comments_With_Leading_Space()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("#These are comments");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.NotNull(executionPlan);
            Assert.Equal(SequenceExecutionPlan.Empty, executionPlan);
        }
    }
}