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
                TypeId.Range => typeof(Range),
                TypeId.None => null,
                _ => throw new NotSupportedException($"TypeId is not supported: {typeId}")
            };
        }

        public static TypeId ToTypeId(object value)
        {
            return value switch
            {
                null => TypeId.None,
                string => TypeId.String,
                char => TypeId.Char,
                bool => TypeId.Boolean,
                sbyte => TypeId.SByte,
                byte => TypeId.Byte,
                short => TypeId.Int16,
                int => TypeId.Int32,
                long => TypeId.Int64,
                ushort => TypeId.UInt16,
                uint => TypeId.UInt32,
                ulong => TypeId.UInt64,
                float => TypeId.Single,
                double => TypeId.Double,
                decimal => TypeId.Decimal,
                DateTime => TypeId.DateTime,
                TimeSpan => TypeId.TimeSpan,
                Guid => TypeId.Guid,
                Version => TypeId.Version,
                Range => TypeId.Range,
                _ => throw new NotSupportedException("Type is not supported: " + value.GetType().Name)
            };
        }
    }
}
