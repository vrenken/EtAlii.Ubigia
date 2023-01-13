// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Linq;
using System.Reflection;

public class ParameterSet
{
    public Parameter[] Parameters { get; }

    public TypeInfo[] ParameterTypeInfos { get; }

    public bool RequiresInput { get; }

    public ParameterSet(bool requiresInput, params Parameter[] parameters)
    {
        RequiresInput = requiresInput;
        Parameters = parameters;

        ParameterTypeInfos = parameters
            .Select(p => p?.Type.GetTypeInfo())
            .ToArray();
    }
}
