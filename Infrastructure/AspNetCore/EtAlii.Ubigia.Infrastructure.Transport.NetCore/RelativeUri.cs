namespace EtAlii.Ubigia.Infrastructure.Transport.NetCore
{
    // Origin: EtAlii.Ubigia.Api.Transport.WebApi
    // TODO: Merge with RelativeUri in transport.

    public static class RelativeUri
    {
        //public static string ApiRest = "Api/Rest/"
        public const string Authenticate = "authenticate";

        // TODO: refactor to RelativeUri.Admin.*, RelativeUri.Data.* and RelativeUri.User.*

        public static class Admin
        {
            public static class Portal
            {

                public const string MicrosoftGraphSettings = "settings/microsoft/graph";
                public const string GooglePeopleApiSettings = "settings/google/peopleapi";

                public const string StoragesAdministration = "storages";
                public const string AccountsAdministration = "accounts";
                public const string AccountAdministration = "account/edit";
                public const string SpacesAdministration = "spaces";
            }

            public static class Api
            {
                public const string Information = "information";
                public const string Accounts = "account";
                public const string Storages = "storage";
                public const string Spaces = "space";
                public const string Roots = "root";
            }
        }

        public static class User
        {
            public static class Portal
            {
                public const string MicrosoftGraphSettings = "settings/microsoft/graph";
                public const string GooglePeopleApiSetting = "settings/google/peopleapi";
            }

            public static class Api
            {
	            public const string Storages = "storage";
	            public const string Accounts = "account";
	            public const string Spaces = "space";

				public const string Entry = "entry";
                public const string RelatedEntries = "relatedentries";
                public const string Entries = "entries";

                public const string Roots = "root";
                public const string Content = "content";
                public const string Properties = "properties";
                public const string ContentDefinition = "contentdefinition";
            }
        }
    }
}
