namespace EtAlii.Servus.Api.Data
{
    using EtAlii.Servus.Api;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A stubbed data client that can be used to manage roots.
    /// </summary>
    public class RootDataClientStub : IRootDataClient 
    {
        public Root Add(string name)
        {
            return null;
        }
        
        public void Remove(Guid id)
        {
        }

        public Root Change(Guid rootId, string rootName)
        {
            return null;
        }

        public Root Get(string rootName)
        {
            return null;
        }

        public Root Get(Guid rootId)
        {
            return null;
        }

        public IEnumerable<Root> GetAll()
        {
            return null;
        }

        public void Connect()
        {
        }

        public void Disconnect()
        {
        }
    }
}
