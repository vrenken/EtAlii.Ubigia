﻿namespace EtAlii.Ubigia.Infrastructure.Transport
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
            if (create == null)
            {
                throw new ArgumentException(nameof(create));
            }

            _create = create;
        }
    }
}