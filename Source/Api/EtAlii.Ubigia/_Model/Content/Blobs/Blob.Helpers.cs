// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

public partial class Blob
{
    private static readonly ConcurrentDictionary<Type, string> _blobNames = new();
    private static readonly object[] _emptyConstructorParameters = Array.Empty<object>();

    private static readonly object _lockObject = new();

    public static string GetName<T>()
        where T : Blob
    {
        lock(_lockObject)
        {
            var type = typeof(T);
            if (!_blobNames.TryGetValue(type, out var name))
            {
                var constructor = type.GetTypeInfo()
                    .DeclaredConstructors
                    .Where(c => !c.IsStatic)
                    .First(c => c.GetParameters().Length == 0);
                var instance = (T)constructor.Invoke(_emptyConstructorParameters);
                name = instance.Name;
                _blobNames[type] = name;
            }
            return name;
        }
    }


    public static string GetName(Blob blob)
    {
        return blob.Name;
    }

    public static void SetStored(Blob blob, bool stored)
    {
        blob.Stored = stored;
    }

    public static void SetSummary(Blob blob, BlobSummary summary)
    {
        blob.Summary = summary;
    }

    public static void SetTotalParts(Blob blob, ulong totalParts)
    {
        blob.TotalParts = totalParts;
    }
}
