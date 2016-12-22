﻿namespace EtAlii.Servus.PowerShell
{
    using EtAlii.Servus.Api;
    using EtAlii.Servus.PowerShell.Spaces;
    using System.Management.Automation;

    [Cmdlet(VerbsData.Update, Nouns.Entry)]
    public class Update_Entry : SpaceTargetingCmdlet
    {
        [Parameter(Mandatory = false, Position = 60, ParameterSetName = "byTextContent", HelpMessage = "The text content that should be added to the entry as content.")]
        public string Text { get; set; }

        [Parameter(Mandatory = false, Position = 60, ParameterSetName = "byFileContent", HelpMessage = "The file that should be added to the entry as content.")]
        public string FileName { get; set; }



        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byChildEntry", HelpMessage = "The child entry that relates to the entry instance.")]
        public Entry Child { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byChildEntryId", HelpMessage = "The child entry ID that relates to the entry instance.")]
        public Identifier ChildId { get; set; }

        public Entry TargetChildEntry { get { return _targetChildEntry; } private set { _targetChildEntry = value; } }
        private Entry _targetChildEntry;


        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byNextEntry", HelpMessage = "The next entry that relates to the entry instance.")]
        public Entry Next { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byNextEntryId", HelpMessage = "The next entry ID that relates to the entry instance.")]
        public Identifier NextId { get; set; }

        public Entry TargetNextEntry { get { return _targetNextEntry; } private set { _targetNextEntry = value; } }
        private Entry _targetNextEntry;


        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byUpdateEntry", HelpMessage = "The update entry that relates to the entry instance.")]
        public Entry Update { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byUpdateEntryId", HelpMessage = "The update entry ID that relates to the entry instance.")]
        public Identifier UpdateId { get; set; }

        public Entry TargetUpdateEntry { get { return _targetUpdateEntry; } private set { _targetUpdateEntry = value; } }
        private Entry _targetUpdateEntry;
    }
}
