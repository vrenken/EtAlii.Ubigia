namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface INode : INotifyPropertyChanged
    {
        Identifier Id { get; }
        Identifier Schema { get; }
        bool IsModified { get; }
        LinkCollection Links { get; }
        string Name { get; }
    }
}