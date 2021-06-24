// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.ComponentModel;

    public interface INode : INotifyPropertyChanged
    {
        Identifier Id { get; }
        string Type { get; }
        //Identifier Schema [ get ]
        bool IsModified { get; }
        //LinkCollection Links [ get ]
        //string Name [ get ]
    }
}
