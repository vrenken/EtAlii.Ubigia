// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using EtAlii.Servus.Api.Tests;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public partial class SequenceParser_Paths_Conditional_Tests
    {
        private ISequenceParser _parser;

        [TestInitialize]
        public void Initialize()
        {
            var container = new Container();

            new ConstantHelpersScaffolding().Register(container); 
            new ScriptParserScaffolding().Register(container);
            new SequenceParsingScaffolding().Register(container);
            new OperatorParsingScaffolding().Register(container);
            new SubjectParsingScaffolding(FunctionHandlersProvider.Empty).Register(container);

            _parser = container.GetInstance<ISequenceParser>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _parser = null;
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_01()
        {
            // Arrange.
            var text = "/Contacts/*/.Nickname=\"Johnny\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(1, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_02()
        {
            // Arrange.
            var text = "/Contacts/*/.Nickname=\'Johnny\'";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(1, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_03()
        {
            // Arrange.
            var text = "/Contacts/*/.Nickname=\"Johnny\"&Birthdate=28-07-1978";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(2, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_04()
        {
            // Arrange.
            var text = "/Contacts/*/.Nickname=\"Johnny\"&Birthdate=1978-07-28";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(2, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_05()
        {
            // Arrange.
            var text = "/Contacts/*/.Nickname=\"Johnny\"&Birthdate=1978-07-28&IsMale=true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_06()
        {
            // Arrange.
            var text = "/Contacts/.Type=\"Family\"/.Nickname=\"Johnny\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(3).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(1, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Type", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Family", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(1, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_07()
        {
            // Arrange.
            var text = "/Contacts/.Type=/.Nickname=\"Johnny\"";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(3).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(1, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Type", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.IsNull(conditionalPathSubjectPart.Conditions[0].Value);
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(1, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_MultiLine_01()
        {
            // Arrange.
            var text = "/Contacts/*/.Nickname=\"Johnny\"\n" +
                       " & Birthdate=28-07-1978\n" +
                       " & IsMale = true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_MultiLine_02()
        {
            // Arrange.
            var text = "/Contacts/*/.\n" +
                       " Nickname=\"Johnny\" & \n" +
                       " Birthdate=28-07-1978 & \n"+
                       " IsMale = true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_MultiLine_03()
        {
            // Arrange.
            var text = "/Contacts/*/John.Nickname=\"Johnny\"\n" +
                       " & Birthdate=28-07-1978\n" +
                       " & IsMale = true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(7, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(6), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }


        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_MultiLine_04()
        {
            // Arrange.
            var text = "/Contacts/*/.Nickname=\"Johnny\" & \n" +
                       "Birthdate=28-07-1978 & \n" +
                       "IsMale = true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(6, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(5).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_11()
        {
            // Arrange.
            var text = "/Contacts/*/John.Nickname=\"Johnny\"&Birthdate=1978-07-28&IsMale=true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(7, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(6), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(ConditionType.Equal, conditionalPathSubjectPart.Conditions[1].Type);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_12()
        {
            // Arrange.
            var text = "/Contacts/*/John.Nickname=\"Johnny\"&Birthdate>1978-07-28&IsMale=true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(7, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(6), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(ConditionType.MoreThan, conditionalPathSubjectPart.Conditions[1].Type);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_13()
        {
            // Arrange.
            var text = "/Contacts/*/John.Nickname=\"Johnny\"&Birthdate>=1978-07-28&IsMale=true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(7, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(6), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(ConditionType.MoreThanOrEqual, conditionalPathSubjectPart.Conditions[1].Type);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_14()
        {
            // Arrange.
            var text = "/Contacts/*/John.Nickname=\"Johnny\"&Birthdate!=1978-07-28&IsMale=true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(7, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(6), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(ConditionType.NotEqual, conditionalPathSubjectPart.Conditions[1].Type);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_15()
        {
            // Arrange.
            var text = "/Contacts/*/John.Nickname=\"Johnny\"&Birthdate<1978-07-28&IsMale=true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(7, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(6), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(ConditionType.LessThan, conditionalPathSubjectPart.Conditions[1].Type);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }

        [TestMethod, TestCategory(TestAssembly.Category)]
        public void SequenceParser_Parse_PathSubject_Conditional_16()
        {
            // Arrange.
            var text = "/Contacts/*/John.Nickname=\"Johnny\"&Birthdate<=1978-07-28&IsMale=true";

            // Act.
            var sequence = _parser.Parse(text);

            // Assert.
            Assert.IsNotNull(sequence);
            Assert.IsTrue(sequence.Parts.Count() == 2);

            var pathSubject = sequence.Parts.Skip(1).First() as PathSubject;
            Assert.IsNotNull(pathSubject);
            Assert.AreEqual(7, pathSubject.Parts.Count());
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(0), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(1), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(2), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(3), typeof(WildcardPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(4), typeof(IsParentOfPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(5), typeof(ConstantPathSubjectPart));
            Assert.IsInstanceOfType(pathSubject.Parts.ElementAt(6), typeof(ConditionalPathSubjectPart));
            var conditionalPathSubjectPart = pathSubject.Parts.Skip(6).Cast<ConditionalPathSubjectPart>().First();
            Assert.AreEqual(3, conditionalPathSubjectPart.Conditions.Length);
            Assert.AreEqual("Nickname", conditionalPathSubjectPart.Conditions[0].Property);
            Assert.AreEqual("Johnny", conditionalPathSubjectPart.Conditions[0].Value);
            Assert.AreEqual("Birthdate", conditionalPathSubjectPart.Conditions[1].Property);
            Assert.AreEqual(ConditionType.LessThanOrEqual, conditionalPathSubjectPart.Conditions[1].Type);
            Assert.AreEqual(new DateTime(1978, 07, 28), conditionalPathSubjectPart.Conditions[1].Value);
            Assert.AreEqual("IsMale", conditionalPathSubjectPart.Conditions[2].Property);
            Assert.AreEqual(true, conditionalPathSubjectPart.Conditions[2].Value);
        }

    }
}