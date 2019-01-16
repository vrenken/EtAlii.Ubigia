namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;

    [Cmdlet(VerbsData.Initialize, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class InitializeSpace : SpaceTargetingCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(TargetSpace);
        } 
    }
}
