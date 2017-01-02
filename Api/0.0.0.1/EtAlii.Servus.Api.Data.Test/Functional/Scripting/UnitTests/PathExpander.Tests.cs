namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data;
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    [TestClass]
    public class PathExpander_Tests
    {
        private IPathExpander _pathExpander;
        private ScriptScope _scope;

        [TestInitialize]
        public void Initialize()
        {
            _scope = new ScriptScope();

            var identifierComponentExpander = new IdentifierComponentExpander ();
            var variableComponentExpander = new VariableComponentExpander(_scope);
            var nameComponentExpander = new NameComponentExpander();
            _pathExpander = new PathExpander(identifierComponentExpander, variableComponentExpander, nameComponentExpander);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _pathExpander = null;
            _scope = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PathExpander_New()
        {
            // Arrange.
            var scope = new ScriptScope();
            var identifierComponentExpander = new IdentifierComponentExpander();
            var variableComponentExpander = new VariableComponentExpander(scope);
            var nameComponentExpander = new NameComponentExpander();

            // Act.
            var pathExpander = new PathExpander(identifierComponentExpander, variableComponentExpander, nameComponentExpander);

            // Assert.
            Assert.IsNotNull(pathExpander);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PathExpander_Expand_NameComponent_1()
        {
            // Arrange.
            var startIdentifier = Identifier.Empty;
            var components = new PathComponent[]
            {
                new NameComponent("first"),
            };
            var path = new Path(components);

            // Act.
            var result = _pathExpander.Expand(path, out startIdentifier);

            // Assert.
            Assert.AreEqual(Identifier.Empty, startIdentifier);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("first", result.First());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PathExpander_Expand_NameComponent_3()
        {
            // Arrange.
            var startIdentifier = Identifier.Empty;
            var components = new PathComponent[]
            {
                new NameComponent("first"),
                new NameComponent("second"),
                new NameComponent("third"),
            };
            var path = new Path(components);

            // Act.
            var result = _pathExpander.Expand(path, out startIdentifier);

            // Assert.
            Assert.AreEqual(Identifier.Empty, startIdentifier);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("first", result.First());
            Assert.AreEqual("second", result.Skip(1).First());
            Assert.AreEqual("third", result.Last());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PathExpander_Expand_VariableComponent_1()
        {
            // Arrange.
            _scope.Variables["var1"] = new ScopeVariable("first", "first");

            var startIdentifier = Identifier.Empty;
            var components = new PathComponent[]
            {
                new VariableComponent("var1"),
            };
            var path = new Path(components);

            // Act.
            var result = _pathExpander.Expand(path, out startIdentifier);

            // Assert.
            Assert.AreEqual(Identifier.Empty, startIdentifier);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("first", result.First());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PathExpander_Expand_VariableComponent_3()
        {
            // Arrange.
            _scope.Variables["var1"] = new ScopeVariable("first", "first");
            _scope.Variables["var2"] = new ScopeVariable("second", "second");
            _scope.Variables["var3"] = new ScopeVariable("third", "third");

            var startIdentifier = Identifier.Empty;
            var components = new PathComponent[]
            {
                new VariableComponent("var1"),
                new VariableComponent("var2"),
                new VariableComponent("var3"),
            };
            var path = new Path(components);

            // Act.
            var result = _pathExpander.Expand(path, out startIdentifier);

            // Assert.
            Assert.AreEqual(Identifier.Empty, startIdentifier);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("first", result.First());
            Assert.AreEqual("second", result.Skip(1).First());
            Assert.AreEqual("third", result.Last());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PathExpander_Expand_VariableComponent_3_With_Root()
        {
            // Arrange.
            var identifier = Identifier.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 0, 0, 2);
            var parent = Identifier.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 0, 0, 1);
            var relation = Relation.NewRelation(parent);
            var entry = new Entry(identifier, relation, "test");
            var node = new Node(entry);
            _scope.Variables["var1"] = new ScopeVariable(node, "first");
            _scope.Variables["var2"] = new ScopeVariable("second", "second");
            _scope.Variables["var3"] = new ScopeVariable("third", "third");

            var startIdentifier = Identifier.Empty;
            var components = new PathComponent[]
            {
                new VariableComponent("var1"),
                new VariableComponent("var2"),
                new VariableComponent("var3"),
            };
            var path = new Path(components);

            // Act.
            var result = _pathExpander.Expand(path, out startIdentifier);

            // Assert.
            Assert.AreNotEqual(Identifier.Empty, startIdentifier);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("second", result.First());
            Assert.AreEqual("third", result.Skip(1).First());
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void PathExpander_Expand_VariableComponent_3_With_False_Root()
        {
            // Arrange.
            var identifier = Identifier.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 0, 0, 2);
            var parent = Identifier.Create(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 0, 0, 1);
            var relation = Relation.NewRelation(parent);
            var entry = new Entry(identifier, relation, "test");
            _scope.Variables["var1"] = new ScopeVariable("", "first");
            _scope.Variables["var2"] = new ScopeVariable(entry, "second");
            _scope.Variables["var3"] = new ScopeVariable("third", "third");

            var startIdentifier = Identifier.Empty;
            var components = new PathComponent[]
            {
                new VariableComponent("var1"),
                new VariableComponent("var2"),
                new VariableComponent("var3"),
            };
            var path = new Path(components);

            // Act.
            var act = new System.Action(() =>
            {
                var result = _pathExpander.Expand(path, out startIdentifier);
            });

            // Assert.
            ExceptionAssert.Throws<InvalidOperationException>(act);
        }
    }

}