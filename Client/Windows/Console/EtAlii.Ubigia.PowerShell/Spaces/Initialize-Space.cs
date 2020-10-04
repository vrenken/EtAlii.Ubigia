namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsData.Initialize, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class InitializeSpace : SpaceTargetingCmdlet<Space>
    {
        protected override Task<Space> ProcessTask()
        {
            return Task.FromResult(TargetSpace);
        } 
    }
}
