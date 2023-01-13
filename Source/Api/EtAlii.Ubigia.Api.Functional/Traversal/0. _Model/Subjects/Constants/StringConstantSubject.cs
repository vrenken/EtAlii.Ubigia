// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

public sealed class StringConstantSubject : ConstantSubject
{
    public readonly string Value;

    public StringConstantSubject(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"\"{Value}\"";
    }
}
