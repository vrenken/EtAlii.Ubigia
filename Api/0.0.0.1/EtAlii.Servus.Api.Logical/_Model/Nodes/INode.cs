namespace EtAlii.Servus.Api.Logical
{
    using System.ComponentModel;

    public interface INode : INotifyPropertyChanged
    {
        Identifier Id { get; }
        string Type { get; }
        //Identifier Schema { get; }
        bool IsModified { get; }
        //LinkCollection Links { get; }
        //string Name { get; }
    }
}