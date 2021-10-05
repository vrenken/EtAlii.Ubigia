// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class LocalStorageGetter : ILocalStorageGetter
    {
        private readonly LogicalContextOptions _options;
        private readonly Storage _localStorage;

        public LocalStorageGetter(LogicalContextOptions options)
        {
            _options = options;
            _localStorage = new Storage
            {
                Id = Guid.NewGuid(), // TODO: This should be a system-specific Guid, persisted on disk somehow.
                Address = _options.StorageAddress.ToString(),
                Name = _options.Name,
            };
        }

        /// <inheritdoc />
        public Storage GetLocal(IList<Storage> items)
        {
            var local = items?.FirstOrDefault(item => item.Name == _options.Name);
            return local ?? GetLocal();
        }

        private Storage GetLocal() =>  _localStorage;
    }
}
