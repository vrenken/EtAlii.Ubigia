namespace EtAlii.Servus.Api
{
    using EtAlii.Servus.Client.Model;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class InvalidInfrastructureOperation 
    {
        public const string NoConnection = "No connection";
        public const string ConnectionAlreadyClosed = "The connection is already closed";
        public const string ConnectionAlreadyOpen = "The connection is already open";
        public const string SpaceAlreadyOpen = "The space is already open";
        public const string UnableToConnectUsingAccount = "Unable to connect using the specified account";
        public const string UnableToConnectToSpace = "Unable to connect to the specified space";
    }
}
