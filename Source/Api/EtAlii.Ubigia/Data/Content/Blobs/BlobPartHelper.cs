namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    // TODO: Should be in the fabric namespace.

    public static class BlobPartHelper
    {
        private static readonly Dictionary<Type, string> BlobPartNames = new Dictionary<Type, string>(); 
        private static readonly object[] EmptyConstructorParameters = Array.Empty<object>();

        private static readonly object LockObject = new object();

        public static string GetName<T>()
            where T : BlobPartBase
        {
            lock (LockObject)
            {
                var type = typeof(T);
                if (!BlobPartNames.TryGetValue(type, out var name))
                {
                    var constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .First(c => !c.IsStatic && c.GetParameters().Length == 0);
                    var instance = (T) constructor.Invoke(EmptyConstructorParameters);
                    name = instance.Name;
                    BlobPartNames[type] = name;
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
