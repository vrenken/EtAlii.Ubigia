﻿namespace EtAlii.Ubigia.PowerShell.Entries
{
    using System;
    using System.Management.Automation;
    using System.Threading.Tasks;

    [Cmdlet(VerbsCommon.Add, Nouns.Entry)]
    public class AddEntry : TaskCmdlet<Entry>
    {
        #region Content related properties

        [Parameter(Mandatory = false, Position = 60, ParameterSetName = "byTextContent", HelpMessage = "The text content that should be added to the entry as content.")]
        public string Text { get; set; }

        [Parameter(Mandatory = false, Position = 60, ParameterSetName = "byFileContent", HelpMessage = "The file that should be added to the entry as content.")]
        public string FileName { get; set; }

        #endregion Content related properties

        #region Root related properties

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byRoot", HelpMessage = "The root that relates to the entry instance.")]
        public Root Root { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byRootName", HelpMessage = "The name of the root that relates to the entry instance.")]
        public string RootName { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byRootId", HelpMessage = "The ID of the root that relates to the entry instance.")]
        public Guid RootId { get; set; }

        public Root TargetRoot { get; } = null;

        #endregion Root related properties

        #region Previous related properties

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byPrevious", HelpMessage = "The previous entry that relates to the entry instance.")]
        public Entry Previous { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byPreviousId", HelpMessage = "The ID of the previous entry that relates to the entry instance.")]
        public Identifier PreviousId { get; set; }

        public Entry TargetPrevious { get; } = null;

        #endregion Previous related properties

        #region Parent related properties

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byParent", HelpMessage = "The parent entry that relates to the entry instance.")]
        public Entry Parent { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byParentId", HelpMessage = "The ID of the parent entry that relates to the entry instance.")]
        public Identifier ParentId { get; set; }

        public Entry TargetParent { get; } = null;

        #endregion Parent related properties

        #region Update related properties

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byUpdate", HelpMessage = "The obsolete entry that relates to the entry instance.")]
        public Entry Update { get; set; }

        [Parameter(Mandatory = false, Position = 80, ParameterSetName = "byUpdateId", HelpMessage = "The ID of the obsolete entry that relates to the entry instance.")]
        public Identifier UpdateId { get; set; }

        public Entry TargetUpdate { get; } = null;

        #endregion Update related properties

        protected override Task<Entry> ProcessTask()
        {
            throw new NotImplementedException();
        }
    }
}