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
                public const string BaseUrl = "/admin/portal";

                public const string MicrosoftGraphSettings = "admin/portal/settings/microsoft/graph";
                public const string GooglePeopleApiSettings = "admin/portal/settings/google/peopleapi";

                public const string StoragesAdministration = "admin/portal/storages";
                public const string AccountsAdministration = "admin/portal/accounts";
                public const string AccountAdministration = "admin/portal/account/edit";
                public const string SpacesAdministration = "admin/portal/spaces";
            }

            public static class Api
            {
                public const string BaseUrl = "/admin/api";

                public const string Accounts = "admin/api/account";
                public const string Storages = "admin/api/storage";
                public const string Spaces = "admin/api/space";
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
                public const string BaseUrl = "/user/api";

                public const string Entry = "user/api/entry";
                public const string RelatedEntries = "user/api/relatedentries";
                public const string Entries = "user/api/entries";

                public const string Roots = "user/api/root";
                public const string Content = "user/api/content";
                public const string Properties = "user/api/properties";
                public const string ContentDefinition = "user/api/contentdefinition";
            }
        }

        public const string Authenticate = "authenticate";
    }
}
