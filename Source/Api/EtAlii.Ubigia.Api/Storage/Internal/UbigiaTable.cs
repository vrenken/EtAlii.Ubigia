// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EtAlii.Ubigia.Api.Internal;
using EtAlii.Ubigia.Api.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        private readonly IPrincipalKeyValueFactory<TKey> _keyValueFactory;
        private readonly bool _sensitiveLoggingEnabled;
        private readonly Dictionary<TKey, object[]> _rows;
        private readonly IList<(int, ValueConverter)> _valueConverters;
        private readonly IList<(int, ValueComparer)> _valueComparers;

        private Dictionary<int, IUbigiaIntegerValueGenerator> _integerGenerators;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public UbigiaTable([NotNull] IEntityType entityType, [CanBeNull] IUbigiaTable baseTable, bool sensitiveLoggingEnabled)
        {
            EntityType = entityType;
            BaseTable = baseTable;
            _keyValueFactory = entityType.FindPrimaryKey().GetPrincipalKeyValueFactory<TKey>();
            _sensitiveLoggingEnabled = sensitiveLoggingEnabled;
            _rows = new Dictionary<TKey, object[]>(_keyValueFactory.EqualityComparer);

            foreach (var property in entityType.GetProperties())
            {
                var converter = property.GetValueConverter()
                    ?? property.FindTypeMapping()?.Converter;

                if (converter != null)
                {
                    if (_valueConverters == null)
                    {
                        _valueConverters = new List<(int, ValueConverter)>();
                    }

                    _valueConverters.Add((property.GetIndex(), converter));
                }

                var comparer = property.GetKeyValueComparer();
                if (!comparer.IsDefault())
                {
                    if (_valueComparers == null)
                    {
                        _valueComparers = new List<(int, ValueComparer)>();
                    }

                    _valueComparers.Add((property.GetIndex(), comparer));
                }
            }
        }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IUbigiaTable BaseTable { get; }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IEntityType EntityType { get; }

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual UbigiaIntegerValueGenerator<TProperty> GetIntegerValueGenerator<TProperty>(
            IProperty property,
            IReadOnlyList<IUbigiaTable> tables)
        {
            if (_integerGenerators == null)
            {
                _integerGenerators = new Dictionary<int, IUbigiaIntegerValueGenerator>();
            }

            var propertyIndex = property.GetIndex();
            if (!_integerGenerators.TryGetValue(propertyIndex, out var generator))
            {
                generator = new UbigiaIntegerValueGenerator<TProperty>(propertyIndex);
                _integerGenerators[propertyIndex] = generator;

                foreach (var table in tables)
                {
                    foreach (var row in table.Rows)
                    {
                        generator.Bump(row);
                    }
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
        public virtual IEnumerable<object[]> Rows
            => _rows.Values;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual IReadOnlyList<object[]> SnapshotRows()
        {
            var rows = _rows.Values.ToList();
            var rowCount = rows.Count;
            var properties = EntityType.GetProperties().ToList();
            var propertyCount = properties.Count;

            for (var rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                var snapshotRow = new object[propertyCount];
                Array.Copy(rows[rowIndex], snapshotRow, propertyCount);

                if (_valueConverters != null)
                {
                    foreach (var (index, converter) in _valueConverters)
                    {
                        snapshotRow[index] = converter.ConvertFromProvider(snapshotRow[index]);
                    }
                }

                if (_valueComparers != null)
                {
                    foreach (var (index, comparer) in _valueComparers)
                    {
                        snapshotRow[index] = comparer.Snapshot(snapshotRow[index]);
                    }
                }

                rows[rowIndex] = snapshotRow;
            }

            return rows;
        }

        private static List<ValueComparer> GetKeyComparers(IEnumerable<IProperty> properties)
            => properties.Select(p => p.GetKeyValueComparer()).ToList();

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual void Create(IUpdateEntry entry)
        {
            var row = entry.EntityType.GetProperties()
                .Select(p => SnapshotValue(p, p.GetKeyValueComparer(), entry))
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

            if (_rows.TryGetValue(key, out var row))
            {
                var properties = entry.EntityType.GetProperties().ToList();
                var concurrencyConflicts = new Dictionary<IProperty, object>();

                for (var index = 0; index < properties.Count; index++)
                {
                    IsConcurrencyConflict(entry, properties[index], row[index], concurrencyConflicts);
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
            if (property.IsConcurrencyToken)
            {
                var comparer = property.GetKeyValueComparer();
                var originalValue = entry.GetOriginalValue(property);

                if ((comparer != null && !comparer.Equals(rowValue, originalValue))
                    || (comparer == null && !StructuralComparisons.StructuralEqualityComparer.Equals(rowValue, originalValue)))
                {
                    concurrencyConflicts.Add(property, rowValue);

                    return true;
                }
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

            if (_rows.TryGetValue(key, out var row))
            {
                var properties = entry.EntityType.GetProperties().ToList();
                var comparers = GetKeyComparers(properties);
                var valueBuffer = new object[properties.Count];
                var concurrencyConflicts = new Dictionary<IProperty, object>();

                for (var index = 0; index < valueBuffer.Length; index++)
                {
                    if (IsConcurrencyConflict(entry, properties[index], row[index], concurrencyConflicts))
                    {
                        continue;
                    }

                    valueBuffer[index] = entry.IsModified(properties[index])
                        ? SnapshotValue(properties[index], comparers[index], entry)
                        : row[index];
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

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public virtual void BumpValueGenerators(object[] row)
        {
            if (BaseTable != null)
            {
                BaseTable.BumpValueGenerators(row);
            }

            if (_integerGenerators != null)
            {
                foreach (var generator in _integerGenerators.Values)
                {
                    generator.Bump(row);
                }
            }
        }

        private TKey CreateKey(IUpdateEntry entry)
            => _keyValueFactory.CreateFromCurrentValues(entry);

        private static object SnapshotValue(IProperty property, ValueComparer comparer, IUpdateEntry entry)
        {
            var value = SnapshotValue(comparer, entry.GetCurrentValue(property));

            var converter = property.GetValueConverter()
                ?? property.FindTypeMapping()?.Converter;

            if (converter != null)
            {
                value = converter.ConvertToProvider(value);
            }

            return value;
        }

        private static object SnapshotValue(ValueComparer comparer, object value)
            => comparer == null ? value : comparer.Snapshot(value);

        /// <summary>
        ///     Throws an exception indicating that concurrency conflicts were detected.
        /// </summary>
        /// <param name="entry"> The update entry which resulted in the conflict(s). </param>
        /// <param name="concurrencyConflicts"> The conflicting properties with their associated database values. </param>
        protected virtual void ThrowUpdateConcurrencyException(
            [NotNull] IUpdateEntry entry,
            [NotNull] Dictionary<IProperty, object> concurrencyConflicts)
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
