namespace EtAlii.Servus.PowerShell.Spaces
{
    using System.Management.Automation;

    [Cmdlet(VerbsData.Dismount, Nouns.Space, DefaultParameterSetName = "SpaceAccountSpaceCmdlet")]
    public class Dismount_Space : SpaceTargetingCmdlet
    {
    }
}
