namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Select, Nouns.Entry)]
    public class SelectEntry : Cmdlet
    {
        [Parameter(Mandatory = false, Position = 1)]
        public bool Newest;

        [Parameter(Mandatory = false, Position = 2)]
        public bool Oldest;

        protected override void BeginProcessing()
        {
        }

        protected override void EndProcessing()
        {
        }
    }
}
