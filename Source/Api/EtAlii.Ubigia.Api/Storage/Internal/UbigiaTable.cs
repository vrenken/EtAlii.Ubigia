// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using EtAlii.Ubigia.Api.Internal;
using EtAlii.Ubigia.Api.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Utilities;

namespace EtAlii.Ubigia.Api.Storage.Internal
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class UbigiaTable<TKey> : IUbigiaTable
    {
        // WARNING: The Ubigia provider is using EF internal code here. This should not be copied by other providers. See #15096
        private readonly IPrincipalKeyValueFactory<TKey> _keyValueFactory;
        private readonly bool _sensitiveLoggingEnabled;
        private readonly Dictionary<TKey, object[]> _rows;

        private Dictionary<int, IUbigiaIntegerValueGenerator> _integerGenerators;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaTable(
            // WARNING: The Ubigia provider is using EF internal code here. This should not be copied by other providers. See #15096
            [NotNull] IPrincipalKeyValueFactory<TKey> keyValueFactory,
            bool sensitiveLoggingEnabled)
        {
            _keyValueFactory = keyValueFactory;
            _sensitiveLoggingEnabled = sensitiveLoggingEnabled;
#pragma warning disable EF1001 // Internal API
            _rows = new Dictionary<TKey, object[]>(keyValueFactory.EqualityComparer);
#pragma warning restore EF1001 // Internal API
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual UbigiaIntegerValueGenerator<TProperty> GetIntegerValueGenerator<TProperty>(IProperty property)
        {
            if (_integerGenerators == null)
            {
                _integerGenerators = new Dictionary<int, IUbigiaIntegerValueGenerator>();
            }

            // WARNING: The Ubigia provider is using EF internal code here. This should not be copied by other providers. See #15096
#pragma warning disable EF1001 // Internal API
            var propertyIndex = Microsoft.EntityFrameworkCore.Metadata.Internal.PropertyBaseExtensions.GetIndex(property);
#pragma warning restore EF1001 // Internal API
            if (!_integerGenerators.TryGetValue(propertyIndex, out var generator))
            {
                generator = new UbigiaIntegerValueGenerator<TProperty>(propertyIndex);
                _integerGenerators[propertyIndex] = generator;

                foreach (var row in _rows.Values)
                {
                    generator.Bump(row);
                }
            }

            return (UbigiaIntegerValueGenerator<TProperty>)generator;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IReadOnlyList<object[]> SnapshotRows()
            => _rows.Values.ToList();

        private static List<ValueComparer> GetStructuralComparers(IEnumerable<IProperty> properties)
            => properties.Select(GetStructuralComparer).ToList();

        private static ValueComparer GetStructuralComparer(IProperty p)
            => p.GetStructuralValueComparer() ?? p.FindTypeMapping()?.StructuralComparer;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual void Create(IUpdateEntry entry)
        {
            var row = entry.EntityType.GetProperties()
                .Select(p => SnapshotValue(p, GetStructuralComparer(p), entry))
                .ToArray();

            _rows.Add(CreateKey(entry), row);

            BumpValueGenerators(row);
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual void Delete(IUpdateEntry entry)
        {
            var key = CreateKey(entry);

            if (_rows.ContainsKey(key))
            {
                var properties = entry.EntityType.GetProperties().ToList();
                var concurrencyConflicts = new Dictionary<IProperty, object>();

                for (var index = 0; index < properties.Count; index++)
                {
                    IsConcurrencyConflict(entry, properties[index], _rows[key][index], concurrencyConflicts);
                }

                if (concurrencyConflicts.Count > 0)
                {
                    ThrowUpdateConcurrencyException(entry, concurrencyConflicts);
                }

                _rows.Remove(key);
            }
            else
            {
                throw new DbUpdateConcurrencyException(UbigiaStrings.UpdateConcurrencyException, new[] { entry });
            }
        }

        private static bool IsConcurrencyConflict(
            IUpdateEntry entry,
            IProperty property,
            object rowValue,
            Dictionary<IProperty, object> concurrencyConflicts)
        {
            if (property.IsConcurrencyToken
                && !StructuralComparisons.StructuralEqualityComparer.Equals(
                    rowValue,
                    entry.GetOriginalValue(property)))
            {
                concurrencyConflicts.Add(property, rowValue);

                return true;
            }

            return false;
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual void Update(IUpdateEntry entry)
        {
            var key = CreateKey(entry);

            if (_rows.ContainsKey(key))
            {
                var properties = entry.EntityType.GetProperties().ToList();
                var comparers = GetStructuralComparers(properties);
                var valueBuffer = new object[properties.Count];
                var concurrencyConflicts = new Dictionary<IProperty, object>();

                for (var index = 0; index < valueBuffer.Length; index++)
                {
                    if (IsConcurrencyConflict(entry, properties[index], _rows[key][index], concurrencyConflicts))
                    {
                        continue;
                    }

                    valueBuffer[index] = entry.IsModified(properties[index])
                        ? SnapshotValue(properties[index], comparers[index], entry)
                        : _rows[key][index];
                }

                if (concurrencyConflicts.Count > 0)
                {
                    ThrowUpdateConcurrencyException(entry, concurrencyConflicts);
                }

                _rows[key] = valueBuffer;

                BumpValueGenerators(valueBuffer);
            }
            else
            {
                throw new DbUpdateConcurrencyException(UbigiaStrings.UpdateConcurrencyException, new[] { entry });
            }
        }

        private void BumpValueGenerators(object[] row)
        {
            if (_integerGenerators != null)
            {
                foreach (var generator in _integerGenerators.Values)
                {
                    generator.Bump(row);
                }
            }
        }

        // WARNING: The Ubigia provider is using EF internal code here. This should not be copied by other providers. See #15096
#pragma warning disable EF1001 // Internal API
        private TKey CreateKey(IUpdateEntry entry) => _keyValueFactory.CreateFromCurrentValues((InternalEntityEntry)entry);
#pragma warning restore EF1001 // Internal API

        private static object SnapshotValue(IProperty property, ValueComparer comparer, IUpdateEntry entry)
            => SnapshotValue(comparer, entry.GetCurrentValue(property));

        private static object SnapshotValue(ValueComparer comparer, object value)
            => comparer == null ? value : comparer.Snapshot(value);

        /// <summary>
        ///     Throws an exception indicating that concurrency conflicts were detected.
        /// </summary>
        /// <param name="entry"> The update entry which resulted in the conflict(s). </param>
        /// <param name="concurrencyConflicts"> The conflicting properties with their associated database values. </param>
        protected virtual void ThrowUpdateConcurrencyException(
            [NotNull] IUpdateEntry entry, [NotNull] Dictionary<IProperty, object> concurrencyConflicts)
        {
            Check.NotNull(entry, nameof(entry));
            Check.NotNull(concurrencyConflicts, nameof(concurrencyConflicts));

            if (_sensitiveLoggingEnabled)
            {
                throw new DbUpdateConcurrencyException(
                    UbigiaStrings.UpdateConcurrencyTokenExceptionSensitive(
                        entry.EntityType.DisplayName(),
                        entry.BuildCurrentValuesString(entry.EntityType.FindPrimaryKey().Properties),
                        entry.BuildOriginalValuesString(concurrencyConflicts.Keys),
                        "{"
                        + string.Join(
                            ", ",
                            concurrencyConflicts.Select(
                                c => c.Key.Name + ": " + Convert.ToString(c.Value, CultureInfo.InvariantCulture)))
                        + "}"),
                    new[] { entry });
            }

            throw new DbUpdateConcurrencyException(
                UbigiaStrings.UpdateConcurrencyTokenException(
                    entry.EntityType.DisplayName(),
                    concurrencyConflicts.Keys.Format()),
                new[] { entry });
        }
    }
}
