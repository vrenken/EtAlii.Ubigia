﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Collections.Generic;

public sealed class NodeEqualityComparer : IEqualityComparer<Node>
{
    // ReSharper disable once InconsistentNaming
    public static readonly NodeEqualityComparer Default = new();

    public bool Equals(Node x, Node y)
    {
        if (x is null && y is not null || x is not null && y is null)
        {
            return false;
        }
        if (x is null && y is null)
        {
            return true;
        }

        // Optimization for a common success case.
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (x.GetType() != y.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return x.Entry.Id == y.Entry.Id;
    }

    // Is this GetHashCode behavior correct?
    // More details can be found in the Github issue below:
    // https://github.com/vrenken/EtAlii.Ubigia/issues/75
    public int GetHashCode(Node obj)
    {
        return obj.Entry.Id.GetHashCode();
    }
}
