
namespace EtAlii.Servus.PowerShell.Roots
{
    using EtAlii.Servus.Client.Model;
    using System;

    public interface IRootInfoProvider
    {
        string RootName { get; }
        Root Root { get; }
        Guid RootId { get; }

        Storage TargetStorage { get; }
    }
}
