﻿namespace EtAlii.Ubigia
{
    using System;

    public interface IBlobPart
    {
        bool Stored { get; }
        UInt64 Id { get; } 
    }
}
