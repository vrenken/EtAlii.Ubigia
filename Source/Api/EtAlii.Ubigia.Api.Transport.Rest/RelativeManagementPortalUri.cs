// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest
{
    /// <summary>
    /// Constants that represent relative URI parts needed to access the management portal.
    /// </summary>
    public static class RelativeManagementPortalUri
    {
        public const string MicrosoftGraphSettings = "admin/settings/microsoft/graph";
        public const string GooglePeopleApiSettings = "admin/settings/google/peopleapi";

        public const string StoragesAdministration = "admin/storages";
        public const string AccountsAdministration = "admin/accounts";
        public const string AccountAdministration = "admin/account/edit";
        public const string SpacesAdministration = "admin/spaces";
    }
}
