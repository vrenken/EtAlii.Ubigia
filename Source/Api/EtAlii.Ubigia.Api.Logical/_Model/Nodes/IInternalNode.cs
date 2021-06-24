// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    internal interface IInternalNode : INode
    {
        PropertyDictionary GetProperties();
        //void SetProperties(IPropertiesDictionary properties)
        
        IReadOnlyEntry Entry { get; } //set ]
        //void ClearIsModified()

        void Update(PropertyDictionary properties, IReadOnlyEntry entry);
    }
}