namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;
    using System.Linq;
    using Xunit;

    public partial class RootHandlerPathMatcherTests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_Random()
        {
            // Arrange.
            var seed = new Random().Next();
            var random = new Random(seed);
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.DayFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.HourFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MinuteFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.SecondFormatter)
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
            Console.WriteLine("Seed: " + seed);
            Console.WriteLine("Template: " + String.Join("", template.Select(t => t.ToString())));
            Console.WriteLine("Path: " + String.Join("", path.Select(t => t.ToString())));

            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.True(match != MatchResult.NoMatch);
            Assert.Equal(String.Join("", path.Select(m => m.ToString())), String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_01()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.DayFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.HourFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MinuteFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.SecondFormatter)
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
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2016/11/21/23/45/15", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_02()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.DayFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.HourFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MinuteFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.SecondFormatter)
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
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013/01/02/03/04/05", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_03()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.DayFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.HourFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MinuteFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.SecondFormatter)
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
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013/1/2/3/4/5", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }



        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_04()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.DayFormatter)
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
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013/01/02", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("/03/04/05", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_05()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.MonthFormatter), new ParentPathSubjectPart(),
                    new TypedPathSubjectPart(TypedPathFormatter.Time.DayFormatter)
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
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013/1/2", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("/3/4/5", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_06()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                    new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
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
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("/1/2/3/4/5", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_07()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(),
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
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
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/2013", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("/1/2/3/4/5", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_08()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new ParentPathSubjectPart(),
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ParentPathSubjectPart(),
                new ConstantPathSubjectPart("2013")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("/2013", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_09()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("2013")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("2013", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_10()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("1")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("1", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_11()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("12")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("12", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_12()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("123")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("123", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_13()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("1234")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("1234", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_14()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("234")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("234", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_15()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("34")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.NotEqual(MatchResult.NoMatch, match);
            Assert.Equal("34", String.Join("", match.Match.Select(m => m.ToString())));//, "Match")
            Assert.Equal("", String.Join("", match.Rest.Select(m => m.ToString())));//, "Rest")
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void RootHandlerPathMatcher_Time_16()
        {
            // Arrange.
            var rootHandlerPathMatcher = CreateRootHandlerPathMatcher();
            var scriptscope = new ScriptScope();
            var template = new PathSubjectPart[] {
                new TypedPathSubjectPart(TypedPathFormatter.Time.YearFormatter)
            };
            var path = new PathSubjectPart[]
            {
                new ConstantPathSubjectPart("4021")
            };
            var rootHandler = new TestRootHandler(template);

            // Act.
            var match = rootHandlerPathMatcher.Match(scriptscope, rootHandler, path);

            // Assert.
            Assert.Equal(MatchResult.NoMatch, match);
        }
    }
}