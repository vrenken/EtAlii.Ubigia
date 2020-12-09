namespace EtAlii.Ubigia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class BlobHelper
    {
        private static readonly Dictionary<Type, string> _blobNames = new();
        private static readonly object[] _emptyConstructorParameters = Array.Empty<object>(); 

        private static readonly object _lockObject = new();
        
        public static string GetName<T>()
            where T : BlobBase
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


        public static string GetName(BlobBase blob)
        {
            return blob.Name;
        }

        public static void SetStored(BlobBase blob, bool stored)
        {
            blob.Stored = stored;
        }

        public static void SetSummary(BlobBase blob, BlobSummary summary)
        {
            blob.Summary = summary;
        }
    }
}
