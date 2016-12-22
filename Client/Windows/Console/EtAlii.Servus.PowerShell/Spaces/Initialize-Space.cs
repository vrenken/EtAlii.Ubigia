namespace EtAlii.Servus.PowerShell.Spaces
{
    using System.Management.Automation;

    [Cmdlet(VerbsData.Initialize, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class Initialize_Space : SpaceTargetingCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(TargetSpace);
        } 
    }
}
