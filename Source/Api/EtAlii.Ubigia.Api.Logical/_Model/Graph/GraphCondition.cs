﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;
using System.Diagnostics;

[DebuggerStepThrough]
[DebuggerDisplay("{" + nameof(Description) + "}")]
public sealed class GraphCondition : GraphPathPart
{
    private string Description { get; }
    public Predicate<PropertyDictionary> Predicate { get; }

    public GraphCondition(Predicate<PropertyDictionary> predicate, string description)
    {
        Predicate = predicate;
        Description = description;
    }

    public override string ToString()
    {
        return Description;
    }
}
