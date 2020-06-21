namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System;
    using EtAlii.Ubigia.Api;

    public interface ISpaceInfoProvider 
    {
        string SpaceName { get; }
        Space Space { get; }
        Guid SpaceId { get; }
    }
}
