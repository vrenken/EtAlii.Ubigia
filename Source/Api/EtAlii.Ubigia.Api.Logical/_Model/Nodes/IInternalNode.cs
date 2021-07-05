// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    internal interface IInternalNode : INode
    {
        PropertyDictionary GetProperties();
        IReadOnlyEntry Entry { get; }
    }
}
