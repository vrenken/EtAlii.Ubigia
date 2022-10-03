// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;

    public static class TypeIdConverter
    {
        public static Type ToType(TypeId typeId)
        {
            return typeId switch
            {
                TypeId.String => typeof(string),
                TypeId.Char => typeof(char),
                TypeId.Boolean => typeof(bool),
                TypeId.SByte => typeof(sbyte),
                TypeId.Byte => typeof(byte),
                TypeId.Int16 => typeof(short),
                TypeId.Int32 => typeof(int),
                TypeId.Int64 => typeof(long),
                TypeId.UInt16 => typeof(ushort),
                TypeId.UInt32 => typeof(uint),
                TypeId.UInt64 => typeof(ulong),
                TypeId.Single => typeof(float),
                TypeId.Double => typeof(double),
                TypeId.Decimal => typeof(decimal),
                TypeId.DateTime => typeof(DateTime),
                TypeId.TimeSpan => typeof(TimeSpan),
                TypeId.Guid => typeof(Guid),
                TypeId.Version => typeof(Version),
                TypeId.None => null,
                _ => throw new NotSupportedException($"TypeId is not supported: {typeId}")
            };
        }

        public static TypeId ToTypeId(object value)
        {
            return value switch
            {
                null => TypeId.None,
                string _ => TypeId.String,
                char _ => TypeId.Char,
                bool _ => TypeId.Boolean,
                sbyte _ => TypeId.SByte,
                byte _ => TypeId.Byte,
                short _ => TypeId.Int16,
                int _ => TypeId.Int32,
                long _ => TypeId.Int64,
                ushort _ => TypeId.UInt16,
                uint _ => TypeId.UInt32,
                ulong _ => TypeId.UInt64,
                float _ => TypeId.Single,
                double _ => TypeId.Double,
                decimal _ => TypeId.Decimal,
                DateTime _ => TypeId.DateTime,
                TimeSpan _ => TypeId.TimeSpan,
                Guid _ => TypeId.Guid,
                Version _ => TypeId.Version,
                _ => throw new NotSupportedException("Type is not supported: " + value.GetType().Name)
            };
        }
    }
}
