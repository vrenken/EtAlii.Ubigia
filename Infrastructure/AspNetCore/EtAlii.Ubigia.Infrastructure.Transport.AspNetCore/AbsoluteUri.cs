namespace EtAlii.Ubigia.Infrastructure.Transport.AspNetCore
{
    // Origin: EtAlii.Ubigia.Api.Transport.WebApi

    public static class AbsoluteUri 
    {
        // TODO: refactor to RelativeUri.Admin.*, RelativeUri.Data.* and RelativeUri.User.*

        public static class Admin
        {
            public static class Portal
            {
                public const string BasePath = "/admin/portal";

                public const string MicrosoftGraphSettings = "admin/portal/settings/microsoft/graph";
                public const string GooglePeopleApiSettings = "admin/portal/settings/google/peopleapi";

                public const string StoragesAdministration = "admin/portal/storages";
                public const string AccountsAdministration = "admin/portal/accounts";
                public const string AccountAdministration = "admin/portal/account/edit";
                public const string SpacesAdministration = "admin/portal/spaces";
            }

            public static class Api
            {
                public static class Rest
                {
                    public const string BasePath = "/admin/api/rest";

                    public const string Accounts = "admin/api/rest/account";
                    public const string Storages = "admin/api/rest/storage";
                    public const string Spaces = "admin/api/rest/space";
                }

                public static class SignalR
                {
                    public const string BasePath = "/admin/api/stream";
                }
            }
        }

        public static class User
        {
            public static class Portal
            {
                public const string BaseUrl = "/user/portal";

                public const string MicrosoftGraphSettings = "user/portal/settings/microsoft/graph";
                public const string GooglePeopleApiSetting = "user/portal/settings/google/peopleapi";
            }

            public static class Api
            {
                public static class Rest
                {
                    public const string BaseUrl = "/user/api/rest";

                    public const string Entry = "user/api/rest/entry";
                    public const string RelatedEntries = "user/api/rest/relatedentries";
                    public const string Entries = "user/api/rest/entries";

                    public const string Roots = "user/api/rest/root";
                    public const string Content = "user/api/rest/content";
                    public const string Properties = "user/api/rest/properties";
                    public const string ContentDefinition = "user/api/rest/contentdefinition";
                }

                public static class SignalR
                {
                    public const string BaseUrl = "/user/api/stream";
                }

            }
        }

        public const string Authenticate = "authenticate";
    }
}
