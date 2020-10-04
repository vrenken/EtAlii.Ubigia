namespace EtAlii.Ubigia
{
    using System;

    [Flags]
    public enum EntryRelation : byte
    {
        None = 0,
        Parent = 1,
        Child = 2,
        Previous = 4,
        Next = 8,
        Downdate = 16,
        Update = 32,
        Index = 64,
        Indexed = 128,
        All = byte.MaxValue,
    }
}
