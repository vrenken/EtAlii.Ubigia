// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

/// <summary>
/// An abstract class that all different fragments in a GCL schema inherit from.
/// </summary>
public abstract class Fragment
{
    /// <summary>
    /// The name of the Fragment.
    /// </summary>
    public string Name {get;}

    /// <summary>
    /// The type of the fragment.
    /// </summary>
    public FragmentType Type { get; }

    protected Fragment(
        string name,
        FragmentType fragmentType)
    {
        Name = name;
        Type = fragmentType;
    }
}
