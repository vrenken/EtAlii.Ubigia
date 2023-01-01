// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

/// <summary>
/// A mutation based on setting a value from a variable.
/// </summary>
public class VariableMutationValue : MutationValue
{
    public string Name { get; init; }
}
