// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Serialization
{
    internal enum TypeId : byte
    {
        None = 0,
        String,
        Char,
        Boolean,
        SByte,
        Byte,
        Int16,
        Int32,
        Int64,
        UInt16,
        UInt32,
        UInt64,
        Single,
        Double,
        Decimal,
        DateTime,
        TimeSpan,
        Guid,
        Version,
    }
}