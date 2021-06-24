// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class LocalStorageGetter : ILocalStorageGetter
    {
        private readonly ILogicalContextConfiguration _configuration;
        private readonly Lazy<Storage> _localStorage;
         
        public LocalStorageGetter(ILogicalContextConfiguration configuration)
        {
            _configuration = configuration;
            _localStorage = new Lazy<Storage>(() => new Storage
            {
                Id = Guid.NewGuid(),
                Address = _configuration.StorageAddress.ToString(),
                Name = _configuration.Name,
            });
        }

        public Storage GetLocal(IList<Storage> items)
        {
            var local = items?.FirstOrDefault(item => item.Name == _configuration.Name);
            return local ?? GetLocal();
        }

        private Storage GetLocal()
        {
            return _localStorage.Value;
        }
    }
}