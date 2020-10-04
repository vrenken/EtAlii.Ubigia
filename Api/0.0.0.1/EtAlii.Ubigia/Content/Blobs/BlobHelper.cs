namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class BlobHelper
    {
        private static readonly Dictionary<Type, string> BlobNames = new Dictionary<Type, string>();
        private static readonly object[] EmptyConstructorParameters = { };

        private static readonly object LockObject = new object();
        
        public static string GetName<T>()
            where T : BlobBase
        {
            lock(LockObject)
            {
                var type = typeof(T);
                if (!BlobNames.TryGetValue(type, out var name))
                {
                    var constructor = type.GetTypeInfo()
                        .DeclaredConstructors
                        .Where(c => !c.IsStatic)
                        .First(c => c.GetParameters().Length == 0);
                    var instance = (T)constructor.Invoke(EmptyConstructorParameters);
                    name = instance.Name;
                    BlobNames[type] = name;
                }
                return name;
            }
        }


        public static string GetName(IBlob blob)
        {
            return ((BlobBase)blob).Name;
        }

        public static void SetStored(IBlob blob, bool stored)
        {
            ((BlobBase)blob).Stored = stored;
        }

        public static void SetSummary(IBlob blob, BlobSummary summary)
        {
            ((BlobBase)blob).Summary = summary;
        }
    }
}
