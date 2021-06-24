// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    public static class InvalidInfrastructureOperation
    {
        public const string NoConnection = "No connection";
        public const string ConnectionAlreadyClosed = "The connection is already closed";
        public const string ConnectionAlreadyOpen = "The connection is already open";
        public const string ConnectionAlreadyOpenUsingAnotherAddress = "The connection is already open using another address";
        public const string StorageAlreadyOpen = "The storage is already open";
        public const string SpaceAlreadyOpen = "The space is already open";
        public const string UnableToConnectUsingAccount = "Unable to connect using the specified account";
        public const string UnableToConnectToSpace = "Unable to connect to the specified space";
        public const string UnableToConnectToStorage = "Unable to connect to the specified storage";
        public const string AlreadySubscribedToNotifications = "Already connected to the notifications system";
        public const string NotSubscribedToNotifications = "Not subscribed to the notifications system";
        public const string UnableToAuthorize = "Authentication failed";
        public const string NoWayToAuthenticate = "No way to authenticate";

        public const string AlreadySubscribedToTransport = "Already connected to transport";
        public const string NotSubscribedToTransport = "Not subscribed to the transport";
    }
}
