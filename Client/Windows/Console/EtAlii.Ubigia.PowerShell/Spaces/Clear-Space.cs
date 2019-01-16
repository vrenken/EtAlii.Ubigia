namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Clear, Nouns.Space, DefaultParameterSetName = "bySpaceName", SupportsShouldProcess = true)]
    public class ClearSpace : SpaceTargetingCmdlet
    {
    }
}
