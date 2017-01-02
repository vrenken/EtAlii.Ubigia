namespace EtAlii.Servus.Api.Functional.Tests
{
    using EtAlii.Servus.Api.Tests;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FunctionSubject_Tests
    {
        [TestInitialize]
        public void Initialize()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FunctionSubject_Create()
        {
            // Arrange.

            // Act.
            var subject = new FunctionSubject("Rename");

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FunctionSubject_Create_One_Constant_Parameter()
        {
            // Arrange.

            // Act.
            var subject = new FunctionSubject("Rename", new ConstantFunctionSubjectArgument("First"));

            // Assert.
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FunctionSubject_Create_Two_Constant_Parameters()
        {
            // Arrange.

            // Act.
            var subject = new FunctionSubject("Rename", new ConstantFunctionSubjectArgument("First"), new ConstantFunctionSubjectArgument("Second"));

            // Assert.
            Assert.AreEqual("Rename", subject.Name);
            Assert.AreEqual(2, subject.Arguments.Length);
            Assert.IsInstanceOfType(subject.Arguments[0], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("First", ((ConstantFunctionSubjectArgument)subject.Arguments[0]).Value);
            Assert.IsInstanceOfType(subject.Arguments[1], typeof(ConstantFunctionSubjectArgument));
            Assert.AreEqual("Second", ((ConstantFunctionSubjectArgument)subject.Arguments[1]).Value);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void FunctionSubject_Create_Two_Variable_Parameters()
        {
            // Arrange.

            // Act.
            var subject = new FunctionSubject("Rename", new VariableFunctionSubjectArgument("First"), new VariableFunctionSubjectArgument("Second"));

            // Assert.
            Assert.AreEqual("Rename", subject.Name);
            Assert.AreEqual(2, subject.Arguments.Length);
            Assert.IsInstanceOfType(subject.Arguments[0], typeof(VariableFunctionSubjectArgument));
            Assert.AreEqual("First", ((VariableFunctionSubjectArgument)subject.Arguments[0]).Name);
            Assert.IsInstanceOfType(subject.Arguments[1], typeof(VariableFunctionSubjectArgument));
            Assert.AreEqual("Second", ((VariableFunctionSubjectArgument)subject.Arguments[1]).Name);
        }
    }
}