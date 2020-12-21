// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable S1121 // Assignments should not be made from within sub-expressions

namespace EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Storage.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore.Diagnostics;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.Internal;
    using EtAlii.Ubigia.Api.Functional.Querying.EntityFrameworkCore.ValueGeneration.Internal;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Update;
    using Microsoft.EntityFrameworkCore.Utilities;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class UbigiaStore : IUbigiaStore
    {
        private readonly IUbigiaTableFactory _tableFactory;
        private readonly bool _useNameMatching;

        private readonly object _lock = new();

        private Dictionary<object, IUbigiaTable> _tables;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaStore(
            [NotNull] IUbigiaTableFactory tableFactory,
            bool useNameMatching)
        {
            _tableFactory = tableFactory;
            _useNameMatching = useNameMatching;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual UbigiaIntegerValueGenerator<TProperty> GetIntegerValueGenerator<TProperty>(
            IProperty property)
        {
            lock (_lock)
            {
                var entityType = property.DeclaringEntityType;

                return EnsureTable(entityType).GetIntegerValueGenerator<TProperty>(
                    property,
                    entityType.GetDerivedTypesInclusive().Select(type => EnsureTable(type)).ToArray());
            }
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual bool EnsureCreated(
            IUpdateAdapterFactory updateAdapterFactory,
            IDiagnosticsLogger<DbLoggerCategory.Update> updateLogger)
        {
            lock (_lock)
            {
                var valuesSeeded = _tables == null;
                if (valuesSeeded)
                {
                    // ReSharper disable once AssignmentIsFullyDiscarded
                    _tables = CreateTables();

                    var updateAdapter = updateAdapterFactory.CreateStandalone();
                    var entries = new List<IUpdateEntry>();
                    foreach (var entityType in updateAdapter.Model.GetEntityTypes())
                    {
                        foreach (var targetSeed in entityType.GetSeedData())
                        {
                            var entry = updateAdapter.CreateEntry(targetSeed, entityType);
                            entry.EntityState = EntityState.Added;
                            entries.Add(entry);
                        }
                    }

                    ExecuteTransaction(entries, updateLogger);
                }

                return valuesSeeded;
            }
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual bool Clear()
        {
            lock (_lock)
            {
                if (_tables == null)
                {
                    return false;
                }

                _tables = null;

                return true;
            }
        }

        private static Dictionary<object, IUbigiaTable> CreateTables()
            => new();

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IReadOnlyList<UbigiaTableSnapshot> GetTables(IEntityType entityType)
        {
            var data = new List<UbigiaTableSnapshot>();
            lock (_lock)
            {
                if (_tables != null)
                {
                    foreach (var et in entityType.GetDerivedTypesInclusive().Where(et => !et.IsAbstract()))
                    {
                        var key = _useNameMatching ? (object)et.FullName() : et;
                        if (_tables.TryGetValue(key, out var table))
                        {
                            data.Add(new UbigiaTableSnapshot(et, table.SnapshotRows()));
                        }
                    }
                }
            }

            return data;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual int ExecuteTransaction(
            IList<IUpdateEntry> entries,
            IDiagnosticsLogger<DbLoggerCategory.Update> updateLogger)
        {
            var rowsAffected = 0;

            lock (_lock)
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < entries.Count; i++)
                {
                    var entry = entries[i];
                    var entityType = entry.EntityType;

                    Check.DebugAssert(!entityType.IsAbstract(), "entityType is abstract");

                    var table = EnsureTable(entityType);

                    if (entry.SharedIdentityEntry != null)
                    {
                        if (entry.EntityState == EntityState.Deleted)
                        {
                            continue;
                        }

                        table.Delete(entry);
                    }

                    switch (entry.EntityState)
                    {
                        case EntityState.Added:
                            table.Create(entry);
                            break;
                        case EntityState.Deleted:
                            table.Delete(entry);
                            break;
                        case EntityState.Modified:
                            table.Update(entry);
                            break;
                    }

                    rowsAffected++;
                }
            }

            updateLogger.ChangesSaved(entries, rowsAffected);

            return rowsAffected;
        }

        // Must be called from inside the lock
        private IUbigiaTable EnsureTable(IEntityType entityType)
        {
            if (_tables == null)
            {
                _tables = CreateTables();
            }

            IUbigiaTable baseTable = null;

            var entityTypes = entityType.GetAllBaseTypesInclusive();
            foreach (var currentEntityType in entityTypes)
            {
                var key = _useNameMatching ? (object)currentEntityType.FullName() : currentEntityType;
                if (!_tables.TryGetValue(key, out var table))
                {
                    _tables.Add(key, table = _tableFactory.Create(currentEntityType, baseTable));
                }

                baseTable = table;
            }

            return _tables[_useNameMatching ? (object)entityType.FullName() : entityType];
        }
    }
}
