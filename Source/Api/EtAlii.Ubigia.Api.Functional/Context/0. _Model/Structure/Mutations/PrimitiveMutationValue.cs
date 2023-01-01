// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context;

/// <summary>
/// A mutation based on setting a value using a primitive.
/// </summary>
public class PrimitiveMutationValue : MutationValue
{
    public object Value { get; init; }
}
