namespace EtAlii.Ubigia.PowerShell.Roots
{
    using EtAlii.Ubigia.PowerShell.Spaces;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;

    [Cmdlet(VerbsCommon.Add, Nouns.Root, DefaultParameterSetName = "bySpaceName")]
    public class Add_Root : SpaceTargetingCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The name of the root that should be added.")]
        public string RootName { get; set; }

        protected override void ProcessRecord()
        {
            Root root = null;
            var task = Task.Run(async () =>
            {
                root = await PowerShellClient.Current.Fabric.Roots.Add(RootName);
            });
            task.Wait();

            WriteObject(root);
        }
    }
}
