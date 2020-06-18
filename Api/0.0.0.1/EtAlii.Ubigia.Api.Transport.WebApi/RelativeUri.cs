namespace EtAlii.Ubigia.Api.Transport.WebApi
{
    public static class RelativeUri 
    {
		// TODO: refactor to RelativeUri.Admin.*, RelativeUri.Data.* and RelativeUri.User.*

		public const string ApiRest = "Api/Rest/";
	    public const string Authenticate = "authenticate";

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
            public const string Accounts = "account";

            public const string Information = "information";

            public const string Storages = "storage";
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
