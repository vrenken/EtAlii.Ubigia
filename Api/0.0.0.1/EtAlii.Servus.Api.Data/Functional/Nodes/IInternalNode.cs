namespace EtAlii.Servus.Api
{
    using System.Collections.Generic;

    internal interface IInternalNode : INode
    {
        IDictionary<string, object> GetProperties();
        //void SetProperties(IDictionary<string, object> properties);
        
        IReadOnlyEntry Entry { get; } //set; }
        //void ClearIsModified();

        void Update(IDictionary<string, object> properties, IReadOnlyEntry entry);
    }
}