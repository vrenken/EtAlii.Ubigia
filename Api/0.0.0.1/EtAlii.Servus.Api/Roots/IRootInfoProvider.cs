
namespace EtAlii.Servus.Api
{
    using System;

    public interface IRootInfoProvider
    {
        string RootName { get; }
        Root Root { get; }
        Guid RootId { get; }

        Storage TargetStorage { get; }
    }
}
