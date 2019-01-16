namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;

    [Cmdlet(VerbsData.Mount, Nouns.Space, DefaultParameterSetName = "byStorageName")]
    public class MountSpace : SpaceTargetingCmdlet
    {
    }
}
