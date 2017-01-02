using EtAlii.Servus.Client.Model;
using EtAlii.Servus.Client.Windows;
using EtAlii.Servus.PowerShell.Storages;
using EtAlii.Servus.PowerShell.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EtAlii.Servus.PowerShell.Spaces
{
    public class SpaceUserCmdlet : SpaceCmdlet, IUserInfoProvider
    {
        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byUser", HelpMessage = "The user on which the action should be applied.")]
        public User User { get; set; }

        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byUserName", HelpMessage = "The name of the user on which the action should be applied.")]
        public string UserName { get; set; }

        [Parameter(Mandatory = false, Position = 90, ParameterSetName = "byUserId", HelpMessage = "The ID of the user on which the action should be applied.")]
        public Guid UserId { get; set; }

        public User TargetUser { get { return _targetUser; } private set { _targetUser = value; } }
        private User _targetUser;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            TargetUser = UserResolver.Get(this);

            if (TargetUser == null)
            {
                ThrowTerminatingError(new ErrorRecord(new InvalidOperationException(), ErrorId.NoStorage, ErrorCategory.InvalidData, null));
            }
            WriteDebug(String.Format("Using user [{0}]", TargetUser.Name));

        }
    }
}
