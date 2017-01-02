namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    public interface IRootContext
    {
        Root Add(string name); 
        void Remove(Guid id);
        Root Change(Guid rootId, string rootName);
        Root Get(string rootName);
        Root Get(Guid rootId);
        IEnumerable<Root> GetAll();

        event Action<Guid> Added;
        event Action<Guid> Changed;
        event Action<Guid> Removed;
    }
}
