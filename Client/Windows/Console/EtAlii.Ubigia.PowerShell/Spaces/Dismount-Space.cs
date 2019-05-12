namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    [Cmdlet(VerbsData.Dismount, Nouns.Space, DefaultParameterSetName = "SpaceAccountSpaceCmdlet")]
    public class DismountSpace : SpaceTargetingCmdlet<Space>
    {
        protected override Task<Space> ProcessTask()
        {
            throw new System.NotImplementedException();
        }
    }
}
