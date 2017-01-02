namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    // TODO: Should be in the fabric namespace.

    public static class BlobPartHelper
    {
        private static readonly Dictionary<Type, string> _blobPartNames = new Dictionary<Type, string>();
        private static readonly object[] _emptyConstructorParameters = new object[] { };

        public static string GetName<T>()
            where T : BlobPartBase
        {
            var type = typeof(T);
            var name = String.Empty;

            if (!_blobPartNames.TryGetValue(type, out name))
            {
                var constructor = type.GetTypeInfo()
                                      .DeclaredConstructors
                                      .First(c => !c.IsStatic && c.GetParameters().Length == 0); 
                var instance = (T)constructor.Invoke(_emptyConstructorParameters);
                name = instance.Name;
                _blobPartNames[type] = name;
            }
            return name;
        }

        public static string GetName(IBlobPart blobPart)
        {
            return ((BlobPartBase)blobPart).Name;
        }

        public static void SetId(IBlobPart blobPart, UInt32 id)
        {
            ((BlobPartBase)blobPart).Id = id;
        }

        public static void SetStored(IBlobPart blobPart, bool stored)
        {
            ((BlobPartBase)blobPart).Stored = stored;
        }
    }
}
