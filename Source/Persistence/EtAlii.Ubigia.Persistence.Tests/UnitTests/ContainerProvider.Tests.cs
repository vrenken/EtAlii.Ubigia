﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Tests;

using System;
using System.IO;
using EtAlii.Ubigia.Tests;
using Xunit;

[CorrelateUnitTests]
public class ContainerProviderTests
{
    private readonly TestIdentifierFactory _testIdentifierFactory = new();
    private readonly IContainerProvider _containerProvider = new DefaultContainerProvider();

    [Fact]
    public void ContainerProvider_FromIds()
    {
        // Arrange.
        var storage = Guid.NewGuid();
        var account = Guid.NewGuid();
        var space = Guid.NewGuid();

        // Act.
        var containerIdentifier = _containerProvider.ForEntry(storage, account, space);

        // Assert.
        Assert.Equal("Entries", containerIdentifier.Paths[0]);
        Assert.Equal(storage.ToString(), containerIdentifier.Paths[1]);
        Assert.Equal(account.ToString(), containerIdentifier.Paths[2]);
        Assert.Equal(space.ToString(), containerIdentifier.Paths[3]);
        Assert.Equal(4, containerIdentifier.Paths.Length);
    }

    [Fact]
    public void ContainerProvider_FromIdentifier_Without_EraPeriodMoment()
    {
        // Arrange.
        var id = _testIdentifierFactory.Create();

        // Act.
        var containerIdentifier = _containerProvider.FromIdentifier(id);

        // Assert.
        Assert.Equal("Entries", containerIdentifier.Paths[0]);
        Assert.Equal(id.Storage.ToString(), containerIdentifier.Paths[1]);
        Assert.Equal(id.Account.ToString(), containerIdentifier.Paths[2]);
        Assert.Equal(id.Space.ToString(), containerIdentifier.Paths[3]);
        Assert.Equal(id.Era.ToString(), containerIdentifier.Paths[4]);
        Assert.Equal(id.Period.ToString(), containerIdentifier.Paths[5]);
        Assert.Equal(id.Moment.ToString(), containerIdentifier.Paths[6]);
        Assert.Equal(7, containerIdentifier.Paths.Length);
    }

    [Fact]
    public void ContainerProvider_FromIdentifier_With_EraPeriodMoment()
    {
        // Arrange.
        var id = _testIdentifierFactory.Create();

        // Act.
        var containerIdentifier = _containerProvider.ForEntry(id.Storage, id.Account, id.Space, id.Era, id.Period, id.Moment);

        // Assert.
        Assert.Equal("Entries", containerIdentifier.Paths[0]);
        Assert.Equal(id.Storage.ToString(), containerIdentifier.Paths[1]);
        Assert.Equal(id.Account.ToString(), containerIdentifier.Paths[2]);
        Assert.Equal(id.Space.ToString(), containerIdentifier.Paths[3]);
        Assert.Equal(id.Era.ToString(), containerIdentifier.Paths[4]);
        Assert.Equal(id.Period.ToString(), containerIdentifier.Paths[5]);
        Assert.Equal(id.Moment.ToString(), containerIdentifier.Paths[6]);
        Assert.Equal(7, containerIdentifier.Paths.Length);
    }

    [Fact]
    public void ContainerProvider_FromIdentifier_With_EraPeriodMoment_AsStrings()
    {
        // Arrange.
        var id = _testIdentifierFactory.Create();

        // Act.
        var containerIdentifier = _containerProvider.ForEntry(id.Storage.ToString(), id.Account.ToString(), id.Space.ToString(), id.Era.ToString(), id.Period.ToString(), id.Moment.ToString());

        // Assert.
        Assert.Equal("Entries", containerIdentifier.Paths[0]);
        Assert.Equal(id.Storage.ToString(), containerIdentifier.Paths[1]);
        Assert.Equal(id.Account.ToString(), containerIdentifier.Paths[2]);
        Assert.Equal(id.Space.ToString(), containerIdentifier.Paths[3]);
        Assert.Equal(id.Era.ToString(), containerIdentifier.Paths[4]);
        Assert.Equal(id.Period.ToString(), containerIdentifier.Paths[5]);
        Assert.Equal(id.Moment.ToString(), containerIdentifier.Paths[6]);
        Assert.Equal(7, containerIdentifier.Paths.Length);
    }


    [Fact]
    public void ContainerProvider_FromIdentifier_With_EraPeriodMoment_AsStrings_Partially()
    {
        // Arrange.
        var id = _testIdentifierFactory.Create();

        // Act.
        var containerIdentifier = _containerProvider.ForEntry(id.Storage.ToString(), id.Account.ToString(), id.Space.ToString(), id.Era, id.Period, id.Moment);

        // Assert.
        Assert.Equal("Entries", containerIdentifier.Paths[0]);
        Assert.Equal(id.Storage.ToString(), containerIdentifier.Paths[1]);
        Assert.Equal(id.Account.ToString(), containerIdentifier.Paths[2]);
        Assert.Equal(id.Space.ToString(), containerIdentifier.Paths[3]);
        Assert.Equal(id.Era.ToString(), containerIdentifier.Paths[4]);
        Assert.Equal(id.Period.ToString(), containerIdentifier.Paths[5]);
        Assert.Equal(id.Moment.ToString(), containerIdentifier.Paths[6]);
        Assert.Equal(7, containerIdentifier.Paths.Length);
    }


    [Fact]
    public void ContainerProvider_NotEquals_True()
    {
        // Arrange.
        var firstId = _testIdentifierFactory.Create();
        var secondId = _testIdentifierFactory.Create();

        // Act.
        var firstContainerIdentifier = _containerProvider.FromIdentifier(firstId);
        var secondContainerIdentifier = _containerProvider.FromIdentifier(secondId);

        // Assert.
        Assert.True(firstContainerIdentifier != secondContainerIdentifier);
    }

    [Fact]
    public void ContainerProvider_NotEquals_False()
    {
        // Arrange.
        var firstId = _testIdentifierFactory.Create();
        var secondId = Identifier.Create(firstId.Storage, firstId.Account, firstId.Space, firstId.Era, firstId.Period, firstId.Moment);

        // Act.
        var firstContainerIdentifier = _containerProvider.FromIdentifier(firstId);
        var secondContainerIdentifier = _containerProvider.FromIdentifier(secondId);

        // Assert.
        Assert.False(firstContainerIdentifier != secondContainerIdentifier);
    }

    [Fact]
    public void ContainerProvider_Equals_True()
    {
        // Arrange.
        var firstId = _testIdentifierFactory.Create();
        var secondId = Identifier.Create(firstId.Storage, firstId.Account, firstId.Space, firstId.Era, firstId.Period, firstId.Moment);

        // Act.
        var firstContainerIdentifier = _containerProvider.FromIdentifier(firstId);
        var secondContainerIdentifier = _containerProvider.FromIdentifier(secondId);

        // Assert.
        Assert.True(firstContainerIdentifier == secondContainerIdentifier);
    }

    [Fact]
    public void ContainerProvider_Equals_False()
    {
        // Arrange.
        var firstId = _testIdentifierFactory.Create();
        var secondId = _testIdentifierFactory.Create();

        // Act.
        var firstContainerIdentifier = _containerProvider.FromIdentifier(firstId);
        var secondContainerIdentifier = _containerProvider.FromIdentifier(secondId);

        // Assert.
        Assert.False(firstContainerIdentifier == secondContainerIdentifier);
    }

    [Fact]
    public void ContainerProvider_FromIdentifier_TrimTime_True()
    {
        // Arrange.
        var id = _testIdentifierFactory.Create();

        // Act.
        var containerIdentifier = _containerProvider.FromIdentifier(id, true);

        // Assert.
        Assert.Equal("Entries", containerIdentifier.Paths[0]);
        Assert.Equal(id.Storage.ToString(), containerIdentifier.Paths[1]);
        Assert.Equal(id.Account.ToString(), containerIdentifier.Paths[2]);
        Assert.Equal(id.Space.ToString(), containerIdentifier.Paths[3]);
        Assert.Equal(4, containerIdentifier.Paths.Length);
    }

    [Fact]
    public void ContainerProvider_FromIdentifier_TrimTime_False()
    {
        // Arrange.
        var id = _testIdentifierFactory.Create();

        // Act.
        var containerIdentifier = _containerProvider.FromIdentifier(id);

        // Assert.
        Assert.Equal("Entries", containerIdentifier.Paths[0]);
        Assert.Equal(id.Storage.ToString(), containerIdentifier.Paths[1]);
        Assert.Equal(id.Account.ToString(), containerIdentifier.Paths[2]);
        Assert.Equal(id.Space.ToString(), containerIdentifier.Paths[3]);
        Assert.Equal(id.Era.ToString(), containerIdentifier.Paths[4]);
        Assert.Equal(id.Period.ToString(), containerIdentifier.Paths[5]);
        Assert.Equal(id.Moment.ToString(), containerIdentifier.Paths[6]);
        Assert.Equal(7, containerIdentifier.Paths.Length);
    }

    [Fact]
    public void ContainerProvider_FromEmptyPaths_ToString()
    {
        // Arrange.

        // Act.
        var containerId = ContainerIdentifier.FromPaths();

        // Assert.
        Assert.Equal($"{nameof(ContainerIdentifier)}.Empty", containerId.ToString());
    }

    [Fact]
    public void ContainerProvider_Single_Id_ToString()
    {
        // Arrange.
        var id = Guid.NewGuid().ToString();

        // Act.
        var containerId = ContainerIdentifier.FromPaths(id);

        // Assert.
        Assert.NotEqual(ContainerIdentifier.Empty, containerId);
        Assert.Equal(id, containerId.ToString());
    }

    [Fact]
    public void ContainerProvider_Multiple_Ids_ToString()
    {
        // Arrange.
        var first = Guid.NewGuid().ToString();
        var second = Guid.NewGuid().ToString();

        // Act.
        var containerId = ContainerIdentifier.FromPaths(first, second);

        // Assert.
        Assert.NotEqual(ContainerIdentifier.Empty, containerId);
        Assert.Equal(string.Join(Path.DirectorySeparatorChar, first, second), containerId.ToString());
    }

    [Fact]
    public void ContainerProvider_Paths()
    {
        // Arrange.
        var first = Guid.NewGuid().ToString();
        var second = Guid.NewGuid().ToString();

        // Act.
        var containerId = ContainerIdentifier.FromPaths(first, second);

        // Assert.
        Assert.NotEqual(ContainerIdentifier.Empty, containerId);
        Assert.Equal(first, containerId.Paths[0]);
        Assert.Equal(second, containerId.Paths[1]);
    }

    [Fact]
    public void ContainerProvider_Combine()
    {
        // Arrange.
        var first = Guid.NewGuid().ToString();
        var second = Guid.NewGuid().ToString();
        var containerId = ContainerIdentifier.FromPaths(first);

        // Act.
        containerId = ContainerIdentifier.Combine(containerId, second);

        // Assert.
        Assert.NotEqual(ContainerIdentifier.Empty, containerId);
        Assert.Equal(string.Join(Path.DirectorySeparatorChar, first, second), containerId.ToString());
    }


    [Fact]
    public void ContainerProvider_Empty_ToString()
    {
        // Arrange.

        // Act.
        var result = ContainerIdentifier.Empty.ToString();

        // Assert.
        Assert.Equal("ContainerIdentifier.Empty", result);
    }


    [Fact]
    public void ContainerProvider_Comparison_With_Right_Null()
    {
        // Arrange.
        var id = _testIdentifierFactory.Create();
        var first = _containerProvider.FromIdentifier(id);

        // Act.
        var result = first.Equals(null);

        // Assert.
        Assert.False(result, "A ContainerIdentifier should not match with null");
    }

    [Fact]
    public void ContainerProvider_Comparison_With_Self()
    {
        // Arrange.
        var id = _testIdentifierFactory.Create();
        var first = _containerProvider.FromIdentifier(id);
        var second = first as object;

        // Act.
        var result = first.Equals(second);

        // Assert.
        Assert.True(result, "A ContainerProvider generated ContainerIdentifier should also match with itself wrapped as object.");
    }
}
