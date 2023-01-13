// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Diagnostics.Tests;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EtAlii.Ubigia.Diagnostics.Profiling;
using Xunit;
using EtAlii.Ubigia.Tests;

[CorrelateUnitTests]
public class ProfilerTests
{

    [Fact]
    public void Profiler_Create_Root()
    {
        // Arrange.

        // Act.
        var profiler = new Profiler(ProfilingAspects.Functional.TraversalContext);

        // Assert.
        Assert.NotNull(profiler);
    }

    [Fact]
    public void Profiler_Create_Child()
    {
        // Arrange.
        var root = new Profiler(ProfilingAspects.Functional.TraversalContext);

        // Act.
        var profiler = new Profiler(root, ProfilingAspects.Functional.ScriptProcessor);

        // Assert.
        Assert.NotNull(profiler);
    }

    [Fact]
    public void Profiler_Create_Child_And_Profile_01()
    {
        // Arrange.
        var root = new Profiler(ProfilingAspects.Functional.TraversalContext) {Aspects = ProfilingAspects.Functional.All};
        var profiler = root.Create(ProfilingAspects.Functional.ScriptProcessor);

        ProfilingResult result = null;
        root.ProfilingStarted += r => result = r;

        // Act.
        profiler.Begin("Execution");

        // Assert.
        Assert.NotNull(result);
    }

    [Fact]
    public void Profiler_Create_Child_And_Profile_02()
    {
        // Arrange.
        var root = new Profiler(ProfilingAspects.Functional.TraversalContext) {Aspects = ProfilingAspects.Functional.All};

        var results = new List<ProfilingResult>();
        root.ProfilingStarted += r => results.Add(r);

        var profiler1 = root.Create(ProfilingAspects.Functional.ScriptProcessor);
        var profiler2 = root.Create(ProfilingAspects.Functional.ScriptSequenceProcessor);

        // Act.
        profiler1.Begin("Execution");
        profiler2.Begin("Execution");

        // Assert.
        Assert.Single(results);
        Assert.Single(results[0].Children);
    }

    [Fact]
    public void Profiler_Create_Child_And_Profile_03()
    {
        // Arrange.
        var root = new Profiler(ProfilingAspects.Functional.TraversalContext) {Aspects = ProfilingAspects.Functional.All};

        var results = new List<ProfilingResult>();
        root.ProfilingStarted += r => results.Add(r);

        var profiler1 = root.Create(ProfilingAspects.Functional.ScriptProcessor);
        var profiler2 = profiler1.Create(ProfilingAspects.Functional.ScriptSequenceProcessor);

        // Act.
        var profile = profiler1.Begin("Execution");
        profiler2.Begin("Execution");

        // Assert.
        Assert.NotNull(profile);
        Assert.Single(results);
        Assert.Single(results[0].Children);
    }

    [Fact]
    public void Profiler_Create_Child_And_Profile_04()
    {
        // Arrange.
        var root = new Profiler(ProfilingAspects.Functional.TraversalContext) {Aspects = ProfilingAspects.Functional.All};

        var results = new List<ProfilingResult>();
        root.ProfilingStarted += r => results.Add(r);

        var profiler1 = root.Create(ProfilingAspects.Functional.ScriptProcessor);
        var profiler2 = profiler1.Create(ProfilingAspects.Functional.ScriptSequenceProcessor);

        // Act.
        var profile = profiler1.Begin("Execution");
        profiler1.End(profile);
        profiler2.Begin("Execution");

        // Assert.
        Assert.Equal(2, results.Count);
        Assert.Empty(results[0].Children);
    }

    [Fact]
    public void Profiler_Start()
    {
        // Arrange.
        var root = new Profiler(ProfilingAspects.Functional.TraversalContext);
        var profiler = new Profiler(root, ProfilingAspects.Functional.ScriptProcessor)
        {
            Aspects = ProfilingAspects.Functional.All
        };

        // Act.
        var profile = profiler.Begin("Execution");

        // Assert.
        Assert.NotNull(profile[ProfilingProperty.Started]);
        Assert.NotEqual(DateTime.MinValue, profile[ProfilingProperty.Started]);
        Assert.Null(profile[ProfilingProperty.DurationTotal]);
    }

    [Fact]
    public void Profiler_Start_Stop()
    {
        // Arrange.
        var root = new Profiler(ProfilingAspects.Functional.TraversalContext);
        var profiler = new Profiler(root, ProfilingAspects.Functional.ScriptProcessor)
        {
            Aspects = ProfilingAspects.Functional.All
        };

        // Act.
        var profile = profiler.Begin("Execution");
        Task.Delay(1000).Wait();
        profiler.End(profile);

        // Assert.
        Assert.NotNull(profile[ProfilingProperty.Started]);
        Assert.NotEqual(DateTime.MinValue, profile[ProfilingProperty.Started]);
        Assert.NotNull(profile[ProfilingProperty.Stopped]);
        Assert.NotEqual(DateTime.MinValue, profile[ProfilingProperty.Stopped]);
        Assert.NotNull(profile[ProfilingProperty.DurationTotal]);
        Assert.True((double)profile[ProfilingProperty.DurationTotal] > 500);
    }
}
