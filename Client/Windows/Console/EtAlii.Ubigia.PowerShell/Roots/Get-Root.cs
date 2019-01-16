namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, Nouns.Root, DefaultParameterSetName = "byRootName")]
    public class GetRoot : RootTargetingCmdlet
    {
        protected override void ProcessRecord()
        {
            WriteObject(TargetRoot);
        } 
    }
}
