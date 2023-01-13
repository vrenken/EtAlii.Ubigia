// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Tests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Fabric;
using EtAlii.Ubigia.Api.Logical.Diagnostics;
using Xunit;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.MicroContainer;

[CorrelateUnitTests]
public class GraphPathTraverserSingleConnectionTests : IClassFixture<LogicalUnitTestContext>, IAsyncLifetime
{
    private readonly LogicalUnitTestContext _testContext;
    private FabricOptions _fabricOptions;

    public GraphPathTraverserSingleConnectionTests(LogicalUnitTestContext testContext)
    {
        _testContext = testContext;
    }

    public async Task InitializeAsync()
    {
        _fabricOptions = await _testContext.Fabric
            .CreateFabricOptions(true)
            .ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        await _fabricOptions.Connection
            .Close()
            .ConfigureAwait(false);
        _fabricOptions = null;
    }

    [Fact]
    public void GraphPathTraverser_SingleConnection_Create()
    {
        // Arrange.
        var logicalOptions = _fabricOptions
            .UseLogicalContext()
            .UseDiagnostics();

        // Act.
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);

        // Assert.
        Assert.NotNull(traverser);
    }



    [Fact]
    public async Task GraphPathTraverser_SingleConnection_Traverse_Time_BreadthFirst()
    {
        // Arrange.
        var scope = new ExecutionScope();
        const int depth = 5;

        var logicalOptions = _fabricOptions
            .UseLogicalContext()
            .UseDiagnostics();
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
        var fabricContext = logicalOptions.FabricContext;

        var communicationsRoot = await fabricContext.Roots
            .Get("Communication")
            .ConfigureAwait(false);
        var communicationsEntry = (IEditableEntry)await fabricContext.Entries
            .Get(communicationsRoot, scope)
            .ConfigureAwait(false);

        var hierarchyResult = await _testContext.Fabric
            .CreateHierarchy(fabricContext, communicationsEntry, depth)
            .ConfigureAwait(false);
        var hierarchy = hierarchyResult.Item2;

        var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
        graphPathBuilder.Add(communicationsEntry.Id);
        foreach (var item in hierarchy)
        {
            graphPathBuilder = graphPathBuilder
                .Add(GraphRelation.Children)
                .Add(item);
        }
        var path = graphPathBuilder.ToPath();

        // Act.
        var results = Observable.Create<IReadOnlyEntry>(output =>
        {
            traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
            return Disposable.Empty;
        }).ToHotObservable();
        var result = await results.ToArray();

        // Assert.
        Assert.Single(result);
        Assert.Equal(hierarchy[depth - 1], result.First().Type);
    }

    [Fact]
    public async Task GraphPathTraverser_SingleConnection_Traverse_Time_DepthFirst()
    {
        // Arrange.
        const int depth = 5;
        var scope = new ExecutionScope();

        var logicalOptions = _fabricOptions
            .UseLogicalContext()
            .UseDiagnostics();
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
        var fabricContext = logicalOptions.FabricContext;

        var communicationsRoot = await fabricContext.Roots
            .Get("Communication")
            .ConfigureAwait(false);
        var communicationsEntry = (IEditableEntry)await fabricContext.Entries
            .Get(communicationsRoot, scope)
            .ConfigureAwait(false);

        var hierarchyResult = await _testContext.Fabric
            .CreateHierarchy(fabricContext, communicationsEntry, depth)
            .ConfigureAwait(false);
        var hierarchy = hierarchyResult.Item2;

        var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
        graphPathBuilder.Add(communicationsEntry.Id);
        foreach (var item in hierarchy)
        {
            graphPathBuilder = graphPathBuilder
                .Add(GraphRelation.Children)
                .Add(item);
        }
        var path = graphPathBuilder.ToPath();

        // Act.
        var results = Observable.Create<IReadOnlyEntry>(output =>
        {
            traverser.Traverse(path, Traversal.DepthFirst, scope, output);
            return Disposable.Empty;
        }).ToHotObservable();
        var result = await results.ToArray();

        // Assert.
        Assert.Single(result);
        Assert.Equal(hierarchy[depth - 1], result.First().Type);
    }





    [Fact]
    public async Task GraphPathTraverser_SingleConnection_Traverse_Time_BreadthFirst_Wrong_Path()
    {
        // Arrange.
        const int depth = 5;
        var scope = new ExecutionScope();

        var logicalOptions = _fabricOptions
            .UseLogicalContext()
            .UseDiagnostics();
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
        var fabricContext = logicalOptions.FabricContext;

        var communicationsRoot = await fabricContext.Roots
            .Get("Communication")
            .ConfigureAwait(false);
        var communicationsEntry = (IEditableEntry)await fabricContext.Entries
            .Get(communicationsRoot, scope)
            .ConfigureAwait(false);

        var hierarchyResult = await _testContext.Fabric
            .CreateHierarchy(fabricContext, communicationsEntry, depth)
            .ConfigureAwait(false);
        var hierarchy = hierarchyResult.Item2;


        var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
        graphPathBuilder.Add(communicationsEntry.Id);
        hierarchy[3] = Guid.NewGuid().ToString();
        foreach (var item in hierarchy)
        {
            graphPathBuilder = graphPathBuilder
                .Add(GraphRelation.Children)
                .Add(item);
        }
        var path = graphPathBuilder.ToPath();

        // Act.
        var results = Observable.Create<IReadOnlyEntry>(output =>
        {
            traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
            return Disposable.Empty;
        }).ToHotObservable();
        var result = await results.ToArray();

        // Assert.
        Assert.Empty(result);
    }

    [Fact]
    public async Task GraphPathTraverser_SingleConnection_Traverse_Time_DepthFirst_Wrong_Path()
    {
        // Arrange.
        const int depth = 5;
        var scope = new ExecutionScope();

        var logicalOptions = _fabricOptions
            .UseLogicalContext()
            .UseDiagnostics();
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
        var fabricContext = logicalOptions.FabricContext;

        var communicationsRoot = await fabricContext.Roots
            .Get("Communication")
            .ConfigureAwait(false);
        var communicationsEntry = (IEditableEntry)await fabricContext.Entries
            .Get(communicationsRoot, scope)
            .ConfigureAwait(false);

        var hierarchyResult = await _testContext.Fabric
            .CreateHierarchy(fabricContext, communicationsEntry, depth)
            .ConfigureAwait(false);
        var hierarchy = hierarchyResult.Item2;

        var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
        graphPathBuilder.Add(communicationsEntry.Id);
        hierarchy[3] = Guid.NewGuid().ToString();
        foreach (var item in hierarchy)
        {
            graphPathBuilder = graphPathBuilder
                .Add(GraphRelation.Children)
                .Add(item);
        }
        var path = graphPathBuilder.ToPath();

        // Act.
        var results = Observable.Create<IReadOnlyEntry>(output =>
        {
            traverser.Traverse(path, Traversal.DepthFirst, scope, output);
            return Disposable.Empty;
        }).ToHotObservable();
        var result = await results.ToArray();

        // Assert.
        Assert.Empty(result);
    }

    [Fact]
    public async Task GraphPathTraverser_SingleConnection_Traverse_Time_BreadthFirst_Too_Short()
    {
        // Arrange.
        const int depth = 5;
        var scope = new ExecutionScope();

        var logicalOptions = _fabricOptions
            .UseLogicalContext()
            .UseDiagnostics();
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
        var fabricContext = logicalOptions.FabricContext;

        var communicationsRoot = await fabricContext.Roots
            .Get("Communication")
            .ConfigureAwait(false);
        var communicationsEntry = (IEditableEntry)await fabricContext.Entries
            .Get(communicationsRoot, scope)
            .ConfigureAwait(false);

        var hierarchyResult = await _testContext.Fabric
            .CreateHierarchy(fabricContext, communicationsEntry, depth)
            .ConfigureAwait(false);
        var hierarchy = hierarchyResult.Item2;

        var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
        graphPathBuilder.Add(communicationsEntry.Id);
        hierarchy = hierarchy.Take(depth - 1).ToArray();
        foreach (var item in hierarchy)
        {
            graphPathBuilder = graphPathBuilder
                .Add(GraphRelation.Children)
                .Add(item);
        }
        var path = graphPathBuilder.ToPath();

        // Act.
        var results = Observable.Create<IReadOnlyEntry>(output =>
        {
            traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
            return Disposable.Empty;
        }).ToHotObservable();
        var result = await results.ToArray();

        // Assert.
        Assert.Single(result);
        Assert.Equal(hierarchy[depth - 2], result.First().Type);
    }

    [Fact]
    public async Task GraphPathTraverser_SingleConnection_Traverse_Time_DepthFirst_Too_Short()
    {
        // Arrange.
        const int depth = 5;
        var scope = new ExecutionScope();

        var logicalOptions = _fabricOptions
            .UseLogicalContext()
            .UseDiagnostics();
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
        var fabricContext = logicalOptions.FabricContext;

        var communicationsRoot = await fabricContext.Roots
            .Get("Communication")
            .ConfigureAwait(false);
        var communicationsEntry = (IEditableEntry)await fabricContext.Entries
            .Get(communicationsRoot, scope)
            .ConfigureAwait(false);

        var hierarchyResult = await _testContext.Fabric
            .CreateHierarchy(fabricContext, communicationsEntry, depth)
            .ConfigureAwait(false);
        var hierarchy = hierarchyResult.Item2;

        var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
        graphPathBuilder.Add(communicationsEntry.Id);
        hierarchy = hierarchy.Take(depth - 1).ToArray();
        foreach (var item in hierarchy)
        {
            graphPathBuilder = graphPathBuilder
                .Add(GraphRelation.Children)
                .Add(item);
        }
        var path = graphPathBuilder.ToPath();

        // Act.
        var results = Observable.Create<IReadOnlyEntry>(output =>
        {
            traverser.Traverse(path, Traversal.DepthFirst, scope, output);
            return Disposable.Empty;
        }).ToHotObservable();
        var result = await results.ToArray();

        // Assert.
        Assert.Single(result);
        Assert.Equal(hierarchy[depth - 2], result.First().Type);
    }

    [Fact]
    public async Task GraphPathTraverser_SingleConnection_Traverse_Time_BreadthFirst_Too_Long()
    {
        // Arrange.
        const int depth = 5;
        var scope = new ExecutionScope();

        var logicalOptions = _fabricOptions
            .UseLogicalContext()
            .UseDiagnostics();
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
        var fabricContext = logicalOptions.FabricContext;

        var communicationsRoot = await fabricContext.Roots
            .Get("Communication")
            .ConfigureAwait(false);
        var communicationsEntry = (IEditableEntry)await fabricContext.Entries
            .Get(communicationsRoot, scope)
            .ConfigureAwait(false);

        var hierarchyResult = await _testContext.Fabric
            .CreateHierarchy(fabricContext, communicationsEntry, depth)
            .ConfigureAwait(false);
        var hierarchy = hierarchyResult.Item2;

        var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
        graphPathBuilder.Add(communicationsEntry.Id);
        var newHierarchy = new List<string>(hierarchy) { Guid.NewGuid().ToString() };
        hierarchy = newHierarchy.ToArray();
        foreach (var item in hierarchy)
        {
            graphPathBuilder = graphPathBuilder
                .Add(GraphRelation.Children)
                .Add(item);
        }
        var path = graphPathBuilder.ToPath();

        // Act.
        var results = Observable.Create<IReadOnlyEntry>(output =>
        {
            traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
            return Disposable.Empty;
        }).ToHotObservable();
        var result = await results.ToArray();

        // Assert.
        Assert.Empty(result);
    }

    [Fact]
    public async Task GraphPathTraverser_SingleConnection_Traverse_Time_DepthFirst_Too_Long()
    {
        // Arrange.
        const int depth = 5;
        var scope = new ExecutionScope();

        var logicalOptions = _fabricOptions
            .UseLogicalContext()
            .UseDiagnostics();
        var traverser = Factory.Create<IGraphPathTraverser>(logicalOptions);
        var fabricContext = logicalOptions.FabricContext;

        var communicationsRoot = await fabricContext.Roots
            .Get("Communication")
            .ConfigureAwait(false);
        var communicationsEntry = (IEditableEntry)await fabricContext.Entries
            .Get(communicationsRoot, scope)
            .ConfigureAwait(false);

        var hierarchyResult = await _testContext.Fabric
            .CreateHierarchy(fabricContext, communicationsEntry, depth)
            .ConfigureAwait(false);
        var hierarchy = hierarchyResult.Item2;

        var graphPathBuilder = (IGraphPathBuilder)new GraphPathBuilder();
        graphPathBuilder.Add(communicationsEntry.Id);
        var newHierarchy = new List<string>(hierarchy) { Guid.NewGuid().ToString() };
        hierarchy = newHierarchy.ToArray();
        foreach (var item in hierarchy)
        {
            graphPathBuilder = graphPathBuilder
                .Add(GraphRelation.Children)
                .Add(item);
        }
        var path = graphPathBuilder.ToPath();

        // Act.
        var results = Observable.Create<IReadOnlyEntry>(output =>
        {
            traverser.Traverse(path, Traversal.DepthFirst, scope, output);
            return Disposable.Empty;
        }).ToHotObservable();
        var result = await results.ToArray();

        // Assert.
        Assert.Empty(result);
    }
}
