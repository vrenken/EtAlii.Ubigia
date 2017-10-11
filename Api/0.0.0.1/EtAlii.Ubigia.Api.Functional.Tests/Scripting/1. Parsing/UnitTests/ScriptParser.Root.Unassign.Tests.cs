﻿// Copyright (c) Peter Vrenken. All rights reserved. See License.txt in the project root for license information.

namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System.Linq;
    using Xunit;


    public partial class ScriptParserTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_Unassign_Time_Root()
        {
            // Arrange.
            const string query = "root:time <= ";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("time", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal(2,sequence.Parts.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_Unassign_Time_Root_Under_Other_Name()
        {
            // Arrange.
            const string query = "root:specialtime <= ";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("specialtime", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal(2, sequence.Parts.Length);
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void ScriptParser_Root_Unassign_Object_Root()
        {
            // Arrange.
            const string query = "root:projects <= ";

            // Act.
            var result = _parser.Parse(query);

            // Assert.
            var script = result.Script;
            Assert.False(result.Errors.Any(), result.Errors.Select(e => e.Message).FirstOrDefault());
            var sequence = script.Sequences.First();
            Assert.Equal("projects", sequence.Parts.Skip(0).Cast<RootSubject>().First().Name);
            Assert.IsType<AssignOperator>(sequence.Parts.ElementAt(1));
            Assert.Equal(2, sequence.Parts.Length);
        }
    }
}