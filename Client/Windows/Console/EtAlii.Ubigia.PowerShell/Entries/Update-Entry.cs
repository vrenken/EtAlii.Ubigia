namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System.Management.Automation;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.PowerShell.Spaces;

    [Cmdlet(VerbsData.Update, Nouns.Entry)]
    public class UpdateEntry : SpaceTargetingCmdlet<Entry>
    {
        [Parameter(Mandatory = false, Position = 60, ParameterSetName = "byTextContent", HelpMessage = "The text content that should be added to the entry as content.")]
        public string Text { get; set; }

        [Parameter(Mandatory = false, Position = 60, ParameterSetName = "byFileContent", HelpMessage = "The file that should be added to the entry as content.")]
        public string FileName { get; set; }



        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byChildEntry", HelpMessage = "The child entry that relates to the entry instance.")]
        public Entry Child { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byChildEntryId", HelpMessage = "The child entry ID that relates to the entry instance.")]
        public Identifier ChildId { get; set; }

        //public Entry TargetChildEntry [ get private set ] 


        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byNextEntry", HelpMessage = "The next entry that relates to the entry instance.")]
        public Entry Next { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byNextEntryId", HelpMessage = "The next entry ID that relates to the entry instance.")]
        public Identifier NextId { get; set; }

        //public Entry TargetNextEntry [ get private set ]


        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byUpdateEntry", HelpMessage = "The update entry that relates to the entry instance.")]
        public Entry Update { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byUpdateEntryId", HelpMessage = "The update entry ID that relates to the entry instance.")]
        public Identifier UpdateId { get; set; }

        //public Entry TargetUpdateEntry [ get private set ]
        
        protected override Task<Entry> ProcessTask()
        {
            throw new System.NotImplementedException();
        }
    }
}
