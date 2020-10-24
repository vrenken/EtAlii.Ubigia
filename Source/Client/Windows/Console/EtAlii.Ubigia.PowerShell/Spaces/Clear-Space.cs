namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Clear, Nouns.Space, DefaultParameterSetName = "bySpaceName", SupportsShouldProcess = true)]
    public class ClearSpace : SpaceTargetingCmdlet<Space>
    {
        protected override Task<Space> ProcessTask()
        {
            throw new System.NotImplementedException();
        }
    }
}
