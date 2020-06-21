namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface IRootInfoProvider
    {
        string RootName { get; }
        Root Root { get; }
        Guid RootId { get; }

        Storage TargetStorage { get; }
    }
}
