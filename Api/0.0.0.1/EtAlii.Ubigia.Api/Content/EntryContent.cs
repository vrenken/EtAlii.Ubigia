namespace EtAlii.Ubigia.Api
{
    using System;

    [Flags]
    public enum EntryContent : byte
    {
        Definition = 1,
        PartDefinition = 2,
        Part = 3,
    }
}
