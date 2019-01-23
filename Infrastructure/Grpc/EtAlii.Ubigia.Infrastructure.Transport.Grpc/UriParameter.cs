﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Grpc
{
    // Origin: EtAlii.Ubigia.Api.Transport.WebApi

    // Idea: These parameters should be used in the RequiredRequiredFromQueryAttribute usages.
    public static class UriParameter 
    {
        public const string EntryId = "entryId";
        public const string EntryIds = "entryIds";
        public const string EntryRelations = "entryRelations";
        public const string EntriesWithRelation = "entriesWithRelation";

        public const string SpaceName = "spaceName";
        public const string SpaceId = "spaceId";
        public const string SpaceTemplate = "spaceTemplate";

        public const string RootId = "rootId";
        public const string RootName = "rootName";

        public const string AccountId = "accountId";
        public const string AccountName = "accountName";
        public const string AccountTemplate = "accountTemplate";

        public const string StorageId = "storageId";
        public const string StorageName = "storageName";

        public const string ContentDefinitionPartId = "contentDefinitionPartId";
        public const string ContentPartId = "contentPartId";
    }
}
