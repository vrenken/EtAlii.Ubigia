﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Scripting.Tests
{
    using System.Linq;
    using Xunit;

    public partial class ScriptParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_Add_Time_MMDDHHMMSS_FormattedString_SingleQuotes_Absolute()
        {
            // Arrange.
            const string query = "time:'2016' += /12/04/13/38/22";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("2016", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("/12/04/13/38/22", pathSubject.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_Add_Time_MMDDHHMMSS_FormattedString_SingleQuotes_Relative()
        {
            // Arrange.
            const string query = "time:'2016' += 12/04/13/38/22";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("2016", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<RelativePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("12/04/13/38/22", pathSubject.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_Add_Time_DDHHMMSS_FormattedString_SingleQuotes_Absolute()
        {
            // Arrange.
            const string query = "time:'2016-12' += /04/13/38/22";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("2016-12", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("/04/13/38/22", pathSubject.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_Add_Time_DDHHMMSS_FormattedString_SingleQuotes_Relative()
        {
            // Arrange.
            const string query = "time:'2016-12' += 04/13/38/22";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("2016-12", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<RelativePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("04/13/38/22", pathSubject.ToString());
        }


        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_Add_Time_SS_FormattedString_SingleQuotes_Absolute()
        {
            // Arrange.
            const string query = "time:'2016-12-04 13:38' += /22";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("2016-12-04 13:38", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<AbsolutePathSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("/22", pathSubject.ToString());
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_Add_Time_SS_FormattedString_SingleQuotes_Relative()
        {
            // Arrange.
            const string query = "time:'2016-12-04 13:38' += 22";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            var rootedPathSubject = sequence.Parts.Skip(0).Cast<RootedPathSubject>().First();
            Assert.Equal("time", rootedPathSubject.Root);
            Assert.Single(rootedPathSubject.Parts);
            Assert.IsType<ConstantPathSubjectPart>(rootedPathSubject.Parts[0]);
            Assert.Equal("2016-12-04 13:38", rootedPathSubject.Parts.Cast<ConstantPathSubjectPart>().First().Name);
            Assert.IsType<AddOperator>(sequence.Parts.ElementAt(1));
            var pathSubject = sequence.Parts.Skip(2).Cast<StringConstantSubject>().First();
            Assert.NotNull(pathSubject);
            Assert.Equal("22", pathSubject.Value);
        }
    }
}