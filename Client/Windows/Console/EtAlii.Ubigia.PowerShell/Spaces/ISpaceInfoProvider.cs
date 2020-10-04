namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System;

    public interface ISpaceInfoProvider 
    {
        string SpaceName { get; }
        Space Space { get; }
        Guid SpaceId { get; }
    }
}
