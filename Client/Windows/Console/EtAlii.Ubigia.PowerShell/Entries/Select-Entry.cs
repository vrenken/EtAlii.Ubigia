namespace EtAlii.Ubigia.PowerShell
{
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Select, Nouns.Entry)]
    public class Select_Entry : Cmdlet
    {
        [Parameter(Mandatory = false, Position = 1)]
        public bool newest;

        [Parameter(Mandatory = false, Position = 2)]
        public bool oldest;

        protected override void BeginProcessing()
        {
        }

        protected override void EndProcessing()
        {
        }
    }
}
