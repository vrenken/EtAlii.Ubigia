namespace EtAlii.Servus.Api.Data.UnitTests
{
    using EtAlii.Servus.Api.Data.Tests;
    using EtAlii.Servus.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class ScopeVariable_Tests
    {
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScopeVariable_New()
        {
            // Arrange.

            // Act.
            var variable = new ScopeVariable("value", "source");

            // Assert.
            Assert.AreEqual("value", variable.Value);
            Assert.AreEqual("source", variable.Source);
            Assert.AreEqual(typeof(string), variable.Type);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScopeVariable_New_No_Type()
        {
            // Arrange.

            // Act.
            var act = new Action(() =>
            {
                var variable = new ScopeVariable("value", "source", null);
            });

            // Assert.
            ExceptionAssert.Throws<ArgumentNullException>(act);
        }
        
        [TestMethod, TestCategory(TestAssembly.Category)]
        public void ScopeVariable_New_No_Value()
        {
            // Arrange.

            // Act.
            var variable = new ScopeVariable(null, "source", typeof(string));

            // Assert.
            Assert.IsNull(variable.Value);
            Assert.AreEqual("source", variable.Source);
            Assert.AreEqual(typeof(string), variable.Type);
        }
    }
}