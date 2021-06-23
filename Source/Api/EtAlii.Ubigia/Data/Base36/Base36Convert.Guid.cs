// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    public static partial class Base36Convert
    {
        public static string ToString(Guid guid)
        {
            var bytes = guid.ToByteArray();
            return ToString(bytes);
        }

        public static Guid ToGuid(string base36String)
        {
            var bytes = ToBytes(base36String);
#if NETSTANDARD2_0
            return new Guid(bytes.ToArray());
#elif NETSTANDARD2_1
            return new Guid(bytes);
#else
            This won't work
#endif
        }
    }
}
