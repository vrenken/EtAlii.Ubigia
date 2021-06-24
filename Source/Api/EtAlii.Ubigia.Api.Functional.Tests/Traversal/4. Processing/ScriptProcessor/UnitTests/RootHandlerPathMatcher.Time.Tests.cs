// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;
    using Xunit.Abstractions;

    public partial class RootHandlerPathMatcherTests
    {
        private readonly ITestOutputHelper _output;

        public RootHandlerPathMatcherTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_Random()
        {
            // Arrange.
            var seed = new Random().Next();
            var random = new Random(seed);
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimePathFormatter.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.DayFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.HourFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MinuteFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.SecondFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart(random.Next(0, 2100).ToString()), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart(random.Next(0, 12).ToString()), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart(random.Next(0, 31).ToString()), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart(random.Next(0, 23).ToString()), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart(random.Next(0, 59).ToString()), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart(random.Next(0, 59).ToString())
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            _output.WriteLine("Seed: " + seed);
            _output.WriteLine("Template: " + string.Join("", template.Select(t => t.ToString())));
            _output.WriteLine("Path: " + string.Join("", path.Select(t => t.ToString())));

            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
            Assert.Equal(string.Join("", path.Select(m => m.ToString())), string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_01()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimePathFormatter.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.DayFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.HourFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MinuteFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.SecondFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("2016"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("11"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("21"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("23"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("45"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("15")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2016/11/21/23/45/15", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_02()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimePathFormatter.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.DayFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.HourFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MinuteFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.SecondFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("2013"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("01"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("02"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("03"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("04"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("05")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013/01/02/03/04/05", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_03()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimePathFormatter.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.DayFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.HourFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MinuteFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.SecondFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("2013"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("1"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("2"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("3"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("4"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("5")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013/1/2/3/4/5", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_04()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimePathFormatter.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.DayFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("2013"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("01"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("02"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("03"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("04"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("05")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013/01/02", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("/03/04/05", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_05()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimePathFormatter.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TimePathFormatter.DayFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("2013"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("1"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("2"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("3"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("4"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("5")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013/1/2", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("/3/4/5", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_06()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("2013"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("1"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("2"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("3"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("4"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("5")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("/1/2/3/4/5", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_07()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(),
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("2013"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("1"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("2"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("3"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("4"), new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("5")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/2013", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("/1/2/3/4/5", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_08()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(),
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("2013")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/2013", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_09()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("2013")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_10()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("1")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("1", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_11()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("12")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("12", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_12()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("123")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("123", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_13()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("1234")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("1234", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_14()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("234")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("234", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_15()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("34")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("34", string.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", string.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public async Task RootHandlerPathMatcher_Time_16()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptScope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TimePathFormatter.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("4021")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = await rootHandlerPathMatcher.Match(scriptScope, rootHandler, path).ConfigureAwait(false);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
    }
}
