// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using Xunit;

    public class FunctionSubjectTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void FunctionSubject_Create()
        {
            // Arrange.

            // Act.
            var subject = new FunctionSubject("Rename");

            // Assert.
            Assert.NotNull(subject);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FunctionSubject_Create_One_Constant_Parameter()
        {
            // Arrange.

            // Act.
            var subject = new FunctionSubject("Rename", new ConstantFunctionSubjectArgument("First"));

            // Assert.
            Assert.NotNull(subject);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void FunctionSubject_Create_Two_Constant_Parameters()
        {
            // Arrange.

            // Act.
            var subject = new FunctionSubject("Rename", new ConstantFunctionSubjectArgument("First"), new ConstantFunctionSubjectArgument("Second"));

            // Assert.
            Assert.Equal("Rename", subject.Name);
            Assert.Equal(2, subject.Arguments.Length);
            Assert.IsType<ConstantFunctionSubjectArgument>(subject.Arguments[0]);
            Assert.Equal("First", ((ConstantFunctionSubjectArgument)subject.Arguments[0]).Value);
            Assert.IsType<ConstantFunctionSubjectArgument>(subject.Arguments[1]);
            Assert.Equal("Second", ((ConstantFunctionSubjectArgument)subject.Arguments[1]).Value);
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void FunctionSubject_Create_Two_Variable_Parameters()
        {
            // Arrange.

            // Act.
            var subject = new FunctionSubject("Rename", new VariableFunctionSubjectArgument("First"), new VariableFunctionSubjectArgument("Second"));

            // Assert.
            Assert.Equal("Rename", subject.Name);
            Assert.Equal(2, subject.Arguments.Length);
            Assert.IsType<VariableFunctionSubjectArgument>(subject.Arguments[0]);
            Assert.Equal("First", ((VariableFunctionSubjectArgument)subject.Arguments[0]).Name);
            Assert.IsType<VariableFunctionSubjectArgument>(subject.Arguments[1]);
            Assert.Equal("Second", ((VariableFunctionSubjectArgument)subject.Arguments[1]).Name);
        }
    }
}
