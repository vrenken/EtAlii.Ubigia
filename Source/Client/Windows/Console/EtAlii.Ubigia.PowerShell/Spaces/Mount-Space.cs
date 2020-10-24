namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsData.Mount, Nouns.Space, DefaultParameterSetName = "byStorageName")]
    public class MountSpace : SpaceTargetingCmdlet<Space>
    {
        protected override Task<Space> ProcessTask()
        {
            throw new System.NotImplementedException();
        }
    }
}
