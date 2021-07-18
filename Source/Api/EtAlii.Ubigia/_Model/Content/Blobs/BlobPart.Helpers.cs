// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    // Should the EtAlii.Ubigia.BlobPart (or helpers in it) be in the fabric namespace?
    // https://github.com/vrenken/EtAlii.Ubigia/issues/76
    public partial class BlobPart
    {
        private static readonly Dictionary<Type, string> _blobPartNames = new();
        private static readonly object[] _emptyConstructorParameters = Array.Empty<object>();

        private static readonly object _lockObject = new();

        public static string GetName<T>()
            where T : BlobPart
        {
            lock (_lockObject)
            {
                var type = typeof(T);
                if (!_blobPartNames.TryGetValue(type, out var name))
                {
                    var constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .First(c => !c.IsStatic && c.GetParameters().Length == 0);
                    var instance = (T) constructor.Invoke(_emptyConstructorParameters);
                    name = instance.Name;
                    _blobPartNames[type] = name;
                }
                return name;
            }
        }

        public static string GetName(BlobPart blobPart)
        {
            return blobPart.Name;
        }

        public static void SetStored(BlobPart blobPart, bool stored)
        {
            blobPart.Stored = stored;
        }
    }
}
