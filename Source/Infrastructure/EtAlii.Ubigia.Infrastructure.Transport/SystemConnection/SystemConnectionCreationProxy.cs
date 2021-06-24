// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SystemConnectionCreationProxy : ISystemConnectionCreationProxy
    {
        private Func<ISystemConnection> _create;

        public ISystemConnection Request()
        {
            if (_create == null)
            {
                throw new NotSupportedException("This SystemConnectionCreationProxy instance has no Create function assigned.");
            }
            return _create();
        }

        public void Initialize(Func<ISystemConnection> create)
        {
            _create = create ?? throw new ArgumentException("No system connection create function specified", nameof(create));
        }
    }
}