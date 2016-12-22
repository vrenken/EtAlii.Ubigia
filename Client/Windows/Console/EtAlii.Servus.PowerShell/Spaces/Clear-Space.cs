namespace EtAlii.Servus.PowerShell.Spaces
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Clear, Nouns.Space, DefaultParameterSetName = "bySpaceName", SupportsShouldProcess = true)]
    public class Clear_Space : SpaceTargetingCmdlet
    {
    }
}
