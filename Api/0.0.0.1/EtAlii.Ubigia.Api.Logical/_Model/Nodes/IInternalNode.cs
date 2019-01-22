namespace EtAlii.Ubigia.Api.Logical
{
    internal interface IInternalNode : INode
    {
        PropertyDictionary GetProperties();
        
        IReadOnlyEntry Entry { get; }

        void Update(PropertyDictionary properties, IReadOnlyEntry entry);
    }
}