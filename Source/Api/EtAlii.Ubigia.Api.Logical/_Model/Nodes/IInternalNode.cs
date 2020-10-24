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