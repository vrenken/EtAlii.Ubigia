// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.SignalR
{
    public static class SignalRHub
    {
        public const string Authentication = "/Authentication";

		public const string Account = "/Account";
		public const string Space = "/Space";

		public const string Root = "/Root";
		public const string Property = "/Properties";
        public const string Entry = "/Entry";
        public const string Content = "/Content";
        public const string ContentDefinition = "/ContentDefinition";
    }
}
