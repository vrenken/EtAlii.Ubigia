namespace EtAlii.Servus.Api.Transport
{
    public static class RelativeUri 
    {
        // TODO: refactor to RelativeUri.Admin.*, RelativeUri.Data.* and RelativeUri.User.*
        public const string Authenticate = "authenticate";

        public const string MicrosoftGraphSettingsAdmin = "admin/settings/microsoft/graph";
        public const string MicrosoftGraphSettingsUser = "settings/microsoft/graph";

        public const string GooglePeopleApiSettingsAdmin = "admin/settings/google/peopleapi";
        public const string GooglePeopleApiSettingsUser = "settings/google/peopleapi";

        public const string StoragesAdministration = "admin/storages";
        public const string AccountsAdministration = "admin/accounts";
        public const string AccountAdministration = "admin/account/edit";
        public const string SpacesAdministration = "admin/spaces";

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

        // TODO: Could have a better name?
        public const string UserData = "/datastream";
        public const string AdminData = "/adminstream";
    }
}
