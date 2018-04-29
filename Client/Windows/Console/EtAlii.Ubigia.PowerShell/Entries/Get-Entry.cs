namespace EtAlii.Ubigia.PowerShell.Entries
{
    using EtAlii.Ubigia.Api;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Get, Nouns.Entry)]
    public class GetEntry : Cmdlet
    {
        [Parameter(Mandatory = false, Position = 0)]
        public Identifier Identifier;

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
