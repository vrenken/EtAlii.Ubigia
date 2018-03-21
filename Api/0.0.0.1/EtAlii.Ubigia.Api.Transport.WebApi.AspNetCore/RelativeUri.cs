namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    public static class RelativeUri 
    {
        // TODO: refactor to RelativeUri.Admin.*, RelativeUri.Data.* and RelativeUri.User.*

        public static class Admin
        {
            public const string MicrosoftGraphSettings = "admin/settings/microsoft/graph";
            public const string GooglePeopleApiSettings = "admin/settings/google/peopleapi";

            public const string StoragesAdministration = "admin/storages";
            public const string AccountsAdministration = "admin/accounts";
            public const string AccountAdministration = "admin/account/edit";
            public const string SpacesAdministration = "admin/spaces";
        }

        public static class User
        {
            public const string MicrosoftGraphSettings = "settings/microsoft/graph";
            public const string GooglePeopleApiSetting = "settings/google/peopleapi";
        }

        public static class Data
        {
            public const string Accounts = "data/account";

            public const string Storages = "data/storage";
            public const string Spaces = "data/space";

            public const string Entry = "data/entry";
            public const string RelatedEntries = "data/relatedentries";
            public const string Entries = "data/entries";

            public const string Roots = "data/root";
            public const string Content = "data/content";
            public const string Properties = "data/properties";
            public const string ContentDefinition = "data/contentdefinition";
        }

        public const string Authenticate = "authenticate";
    }
}
