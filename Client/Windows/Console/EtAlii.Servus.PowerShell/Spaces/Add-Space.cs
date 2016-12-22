namespace EtAlii.Servus.PowerShell.Spaces
{
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.PowerShell.Accounts;
    using System;
    using System.Linq;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Api.Transport;

    [Cmdlet(VerbsCommon.Add, Nouns.Space, DefaultParameterSetName = "bySpaceName")]
    public class Add_Space : AccountTargetingCmdlet, IAccountInfoProvider
    {
        [Parameter(Mandatory = true, Position = 0, HelpMessage = "The name of the space that should be added.")]
        public string SpaceName { get; set; }

        [Parameter(Mandatory = true, Position = 1, HelpMessage = "The template that should be added.")]
        public string Template { get; set; }


        protected override void ProcessRecord()
        {
            Space space = null;

            WriteDebug(String.Format("Adding space {0}", SpaceName));

            var task = Task.Run(async () =>
            {
                var template = SpaceTemplate.All.Single(t => t.Name == Template);
                space = await PowerShellClient.Current.ManagementConnection.Spaces.Add(TargetAccount.Id, SpaceName, template);
            });
            task.Wait();

            WriteObject(space);
        }
    }
}
