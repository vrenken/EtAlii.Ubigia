namespace EtAlii.Ubigia.Api.Logical
{
    using System.ComponentModel;

    public interface INode : INotifyPropertyChanged
    {
        Identifier Id { get; }
        string Type { get; }
        bool IsModified { get; }
    }
}