namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class BlobHelper
    {
        private static readonly Dictionary<Type, string> _blobNames = new Dictionary<Type, string>();
        private static readonly object[] _emptyConstructorParameters = new object[] { };

        public static string GetName<T>()
            where T : BlobBase
        {
            var type = typeof(T);
            var name = String.Empty;

            if (!_blobNames.TryGetValue(type, out name))
            {
                var constructor = type.GetTypeInfo()
                                      .DeclaredConstructors
                                      .Where(c => !c.IsStatic)
                                      .Where(c => c.GetParameters().Length == 0)
                                      .First();
                var instance = (T)constructor.Invoke(_emptyConstructorParameters);
                name = instance.Name;
                _blobNames[type] = name;
            }
            return name;
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
