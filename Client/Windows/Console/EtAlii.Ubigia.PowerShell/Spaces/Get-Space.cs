namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class Get_Space : SpaceTargetingCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(TargetSpace);
        } 
    }
}
