﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

public sealed class VariableSubject : Subject
{
    public readonly string Name;

    public VariableSubject(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"${Name}";
    }
}
