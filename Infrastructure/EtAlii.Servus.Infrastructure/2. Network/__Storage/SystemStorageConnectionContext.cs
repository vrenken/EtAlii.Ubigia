namespace EtAlii.Servus.Infrastructure
{
    using System;
    using EtAlii.Servus.Api;

    public class SystemStorageConnectionContext : ISystemStorageConnectionContext
    {
        public Storage Storage { get { return _storage; } }
        private Storage _storage;

        public void Initialize(Storage storage)
        {
            _storage = storage;
        }
    }
}