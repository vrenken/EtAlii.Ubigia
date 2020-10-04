namespace EtAlii.Ubigia.PowerShell.Roots
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.PowerShell.Spaces;

#pragma warning disable S110 // For the powershell part we don't worry about a too deep inheritance chain.
    [Cmdlet(VerbsCommon.Get, Nouns.Roots, DefaultParameterSetName = "BySpaceName")]
    [Description("Gets the roots for the specified space")]
    public class GetRoots : SpaceTargetingCmdlet<IEnumerable<Root>>
    {
        protected override async Task<IEnumerable<Root>> ProcessTask()
        {
            //WriteDebug($"Getting roots for [{TargetSpace.Name}]")

            var roots = await PowerShellClient.Current.Fabric.Roots.GetAll();
            return roots;
        }
    }
}
