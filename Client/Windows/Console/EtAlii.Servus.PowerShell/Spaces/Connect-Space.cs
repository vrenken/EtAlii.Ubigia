using EtAlii.Servus.Client.Windows;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EtAlii.Servus.PowerShell.Spaces
{
    [Cmdlet(VerbsCommunications.Connect, Nouns.Space)]
    public class Connect_Space : SpaceCmdlet
    {
        protected override void EndProcessing()
        {
        } 
    }
}
