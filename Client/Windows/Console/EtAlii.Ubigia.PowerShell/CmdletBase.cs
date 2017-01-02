namespace EtAlii.Ubigia.PowerShell
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommunications.Connect, Nouns.Space)]
    public abstract class CmdletBase : System.Management.Automation.Cmdlet
    {
        protected CmdletBase()
        {
        }
    }
}
