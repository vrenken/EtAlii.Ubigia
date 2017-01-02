// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SequenceExecutionPlanner_Tests
    {
        private IScriptParser _parser;

        [TestInitialize]
        public void Initialize()
        {
            var diagnostics = TestDiagnostics.Create();
            var scriptParserConfiguration = new ScriptParserConfiguration()
                .Use(diagnostics);
            _parser = new ScriptParserFactory().Create(scriptParserConfiguration);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Create()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);

            // Act.
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);

            // Assert.
            Assert.IsNotNull(sequencePlanner);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Add()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Contacts/Vrenken += Tanja");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.IsNotNull(executionPlan);
            Assert.IsInstanceOfType(executionPlan, typeof(SequenceExecutionPlan));
            Assert.AreEqual("/Contacts/Vrenken += \"Tanja\"", executionPlan.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Add_With_Variable()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Contacts/Vrenken += Tanja");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.IsNotNull(executionPlan);
            Assert.IsInstanceOfType(executionPlan, typeof(SequenceExecutionPlan));
            Assert.AreEqual("$var1 <= /Contacts/Vrenken += \"Tanja\"", executionPlan.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Remove_With_Variable()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Contacts/Vrenken -= Tanja");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.IsNotNull(executionPlan);
            Assert.IsInstanceOfType(executionPlan, typeof(SequenceExecutionPlan));
            Assert.AreEqual("$var1 <= /Contacts/Vrenken -= \"Tanja\"", executionPlan.ToString());
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Assign_With_Variable()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Contacts/Vrenken/Tanja <= { Gender:'Female'}");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.IsNotNull(executionPlan);
            Assert.IsInstanceOfType(executionPlan, typeof(SequenceExecutionPlan));
            Assert.AreEqual("$var1 <= /Contacts/Vrenken/Tanja <= Gender: Female", executionPlan.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Assign_With_Variable_From_Variable()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Contacts/Vrenken/Tanja <= $var2");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.IsNotNull(executionPlan);
            Assert.IsInstanceOfType(executionPlan, typeof(SequenceExecutionPlan));
            Assert.AreEqual("$var1 <= /Contacts/Vrenken/Tanja <= $var2", executionPlan.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Assign_With_Variable_From_Variable_With_Comments()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("$var1 <= /Contacts/Vrenken/Tanja <= $var2 #These are comments");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.IsNotNull(executionPlan);
            Assert.IsInstanceOfType(executionPlan, typeof(SequenceExecutionPlan));
            Assert.AreEqual("$var1 <= /Contacts/Vrenken/Tanja <= $var2", executionPlan.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Add_To_Path()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Contacts/Vrenken += Tanja");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.IsNotNull(executionPlan);
            Assert.IsInstanceOfType(executionPlan, typeof(SequenceExecutionPlan));
            Assert.AreEqual("/Contacts/Vrenken += \"Tanja\"", executionPlan.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Remove_From_Path()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Contacts/Vrenken -= NoOne");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.IsNotNull(executionPlan);
            Assert.IsInstanceOfType(executionPlan, typeof(SequenceExecutionPlan));
            Assert.AreEqual("/Contacts/Vrenken -= \"NoOne\"", executionPlan.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceExecutionPlanner_Plan_Simple_Remove_From_Path_With_Comments()
        {
            // Arrange.
            var sequencePartExecutionPlannerSelector = TestSequencePartExecutionPlannerSelector.Create();
            var executionPlanCombinerSelector = TestExecutionPlanCombinerSelector.Create(sequencePartExecutionPlannerSelector);
            var sequencePlanner = new SequenceExecutionPlanner(sequencePartExecutionPlannerSelector, executionPlanCombinerSelector);
            var parseResult = _parser.Parse("/Contacts/Vrenken -= NoOne #These are comments");

            // Act.
            var executionPlan = sequencePlanner.Plan(parseResult.Script.Sequences.First());

            // Assert.
            Assert.IsNotNull(executionPlan);
            Assert.IsInstanceOfType(executionPlan, typeof(SequenceExecutionPlan));
            Assert.AreEqual("/Contacts/Vrenken -= \"NoOne\"", executionPlan.ToString());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
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
            Assert.IsNotNull(executionPlan);
            Assert.AreEqual(SequenceExecutionPlan.Empty, executionPlan);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
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
            Assert.IsNotNull(executionPlan);
            Assert.AreEqual(SequenceExecutionPlan.Empty, executionPlan);
        }
    }
}