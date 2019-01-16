namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;

    [Cmdlet(VerbsData.Dismount, Nouns.Space, DefaultParameterSetName = "SpaceAccountSpaceCmdlet")]
    public class DismountSpace : SpaceTargetingCmdlet
    {
    }
}
