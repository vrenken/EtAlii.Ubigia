namespace EtAlii.Ubigia.PowerShell.Spaces
{
    using System.Linq;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Accounts;

    [Cmdlet(VerbsCommon.Add, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class AddSpace : AccountTargetingCmdlet<Space>
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The name of the space that should be added.")]
        public string SpaceName { get; set; }

        [Parameter(Mandatory = true, Position = 1, HelpMessage = "The template that should be added.")]
        public string Template { get; set; }


        protected override async Task<Space> ProcessTask()
        {
            //WriteDebug($"Adding space {SpaceName}")

            var template = SpaceTemplate.All.Single(t => t.Name == Template);
            var space = await PowerShellClient.Current.ManagementConnection.Spaces.Add(TargetAccount.Id, SpaceName, template);

            return space;
        }
    }
}
