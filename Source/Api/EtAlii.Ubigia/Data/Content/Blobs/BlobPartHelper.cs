namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    // TODO: Should be in the fabric namespace.

    public static class BlobPartHelper
    {
        private static readonly Dictionary<Type, string> _blobPartNames = new(); 
        private static readonly object[] _emptyConstructorParameters = Array.Empty<object>();

        private static readonly object _lockObject = new();

        public static string GetName<T>()
            where T : BlobPartBase
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

        public static string GetName(IBlobPart blobPart)
        {
            return ((BlobPartBase)blobPart).Name;
        }

        public static void SetId(IBlobPart blobPart, uint id)
        {
            ((BlobPartBase)blobPart).Id = id;
        }

        public static void SetStored(IBlobPart blobPart, bool stored)
        {
            ((BlobPartBase)blobPart).Stored = stored;
        }
    }
}
