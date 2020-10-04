namespace EtAlii.Ubigia.Api.Fabric
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
            return new Guid(bytes);
        }
    }
}