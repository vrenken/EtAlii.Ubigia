namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Get, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class GetSpace : SpaceTargetingCmdlet<Space>
    {
        protected override Task<Space> ProcessTask()
        {
            return Task.FromResult(TargetSpace);
        } 
    }
}
