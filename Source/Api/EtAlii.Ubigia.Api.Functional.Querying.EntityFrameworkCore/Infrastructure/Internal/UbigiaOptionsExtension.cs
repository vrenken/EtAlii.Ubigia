// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage;

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Infrastructure.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class UbigiaOptionsExtension : IDbContextOptionsExtension
    {
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual string Storage => _storage;
        private string _storage;

        public virtual string Username => _username;
        private string _username;

        public virtual string Password => _password;
        private string _password;

        public virtual string Address => _address;
        private string _address;

        public virtual Type TransportType => _transportType;
        private Type _transportType;


        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual UbigiaDatabaseRoot DatabaseRoot => _databaseRoot;
        private UbigiaDatabaseRoot _databaseRoot;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual DbContextOptionsExtensionInfo Info => _info ??= new ExtensionInfo(this);
        private DbContextOptionsExtensionInfo _info;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaOptionsExtension()
        {
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected UbigiaOptionsExtension([NotNull] UbigiaOptionsExtension copyFrom)
        {
            _storage = copyFrom._storage;
            _username = copyFrom._username;
            _password = copyFrom._password;
            _address = copyFrom._address;
            _transportType = copyFrom._transportType;

            _databaseRoot = copyFrom._databaseRoot;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        protected virtual UbigiaOptionsExtension Clone()
            => new(this);

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual UbigiaOptionsExtension WithStorage([NotNull] string storage)
        {
            var clone = Clone();

            clone._storage = storage;

            return clone;
        }

        public virtual UbigiaOptionsExtension WithAddress([NotNull] string address)
        {
            var clone = Clone();

            clone._address = address;

            return clone;
        }

        public virtual UbigiaOptionsExtension WithUsername([NotNull] string username)
        {
            var clone = Clone();

            clone._username = username;

            return clone;
        }

        public UbigiaOptionsExtension WithTransport<TTransport>() where TTransport : ITransport
        {
            var clone = Clone();

            clone._transportType = typeof(TTransport);

            return clone;
        }

        public virtual UbigiaOptionsExtension WithPassword([NotNull] string password)
        {
            var clone = Clone();

            clone._password = password;

            return clone;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual UbigiaOptionsExtension WithDatabaseRoot([NotNull] UbigiaDatabaseRoot databaseRoot)
        {
            var clone = Clone();

            clone._databaseRoot = databaseRoot;

            return clone;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual void ApplyServices(IServiceCollection services)
            => services.AddEntityFrameworkUbigiaDatabase();

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual void Validate(IDbContextOptions options)
        {
        }

        private sealed class ExtensionInfo : DbContextOptionsExtensionInfo
        {
            private string _logFragment;

            public ExtensionInfo(IDbContextOptionsExtension extension)
                : base(extension)
            {
            }

            private new UbigiaOptionsExtension Extension
                => (UbigiaOptionsExtension)base.Extension;

            public override bool IsDatabaseProvider
                => true;

            public override string LogFragment
            {
                get
                {
                    if (_logFragment == null)
                    {
                        var builder = new StringBuilder();

                        builder.Append("StoreName=").Append(Extension._storage).Append(' ');
                        builder.Append("Address=").Append(Extension._address).Append(' ');
                        builder.Append("Username=").Append(Extension._username).Append(' ');
                        builder.Append("Password=").Append("<Password>").Append(' ');

                        _logFragment = builder.ToString();
                    }

                    return _logFragment;
                }
            }

            public override long GetServiceProviderHashCode()
                => Extension._databaseRoot?.GetHashCode() ?? 0L;

            public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
                => debugInfo["UbigiaDatabase:DatabaseRoot"]
                    = (Extension._databaseRoot?.GetHashCode() ?? 0L).ToString(CultureInfo.InvariantCulture);
        }
    }
}
