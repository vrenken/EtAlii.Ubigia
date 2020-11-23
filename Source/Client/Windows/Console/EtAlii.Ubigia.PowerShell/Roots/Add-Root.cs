namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Spaces;

    [Cmdlet(VerbsCommon.Add, Nouns.Root, DefaultParameterSetName = "bySpaceName")]
    public class AddRoot : SpaceTargetingCmdlet<Root>
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The name of the root that should be added.")]
        public string RootName { get; set; }

        protected override async Task<Root> ProcessTask()
        {
            var root = await PowerShellClient.Current.Fabric.Roots.Add(RootName).ConfigureAwait(false);
            return root;
        }
    }
}
