﻿namespace EtAlii.Servus.PowerShell.Entries
{
    using EtAlii.Servus.Client.Model;

    public interface IEntryInfoProvider
    {
        Entry Entry { get; }
        Identifier EntryId { get; }

        Storage TargetStorage { get; }
    }
}
