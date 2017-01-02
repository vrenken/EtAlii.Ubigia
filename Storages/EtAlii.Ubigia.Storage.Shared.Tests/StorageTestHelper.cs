namespace EtAlii.Ubigia.Storage.Tests
{
    using EtAlii.Ubigia.Api;
    using System;

    public static class StorageTestHelper
    {
        public static Guid[] CreateIds(int count)
        {
            var result = new Guid[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = Guid.NewGuid();
            }
            return result;
        }

        public static SimpleTestItem CreateSimpleTestItem()
        {
            return new SimpleTestItem
            {
                Name = Guid.NewGuid().ToString(),
                Value = Guid.NewGuid()
            };
        }

        public static SimpleTestItem[] CreateSimpleTestItems(int count)
        {
            var result = new SimpleTestItem[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = new SimpleTestItem
                {
                    Name = Guid.NewGuid().ToString(),
                    Value = Guid.NewGuid()
                };
            }
            return result;
        }

        //public static Entry CreateEntry()
        //{
        //    var storageId = Guid.NewGuid();
        //    var accountId = Guid.NewGuid();
        //    var spaceId = Guid.NewGuid();
        //    return CreateEntry(storageId, accountId, spaceId, 0, 0, 0);
        //}

        public static Entry CreateEntry(Guid storageId, Guid accountId, Guid spaceId, UInt64 era, UInt64 period, UInt64 moment)
        {
            var previousIdentifier = Identifier.NewIdentifier(storageId, accountId, spaceId);
            previousIdentifier = Identifier.NewIdentifier(previousIdentifier, era, period, moment);
            var identifier = Identifier.NewIdentifier(previousIdentifier, era, period, moment + 1);
            return Entry.NewEntry(identifier, Relation.NewRelation(previousIdentifier));
        }

        public static ChildrenComponent CreateChildrenComponent(Guid storageId, Guid accountId, Guid spaceId, UInt64 era, UInt64 period, UInt64 moment)
        {
            var identifier = Identifier.NewIdentifier(storageId, accountId, spaceId);
            identifier = Identifier.NewIdentifier(identifier, era, period, moment);
            return new ChildrenComponent { Relations = new Relation[] { Relation.NewRelation(identifier) } };
        }

        public static ParentComponent CreateParentComponent(Guid storageId, Guid accountId, Guid spaceId, UInt64 era, UInt64 period, UInt64 moment)
        {
            var identifier = Identifier.NewIdentifier(storageId, accountId, spaceId);
            identifier = Identifier.NewIdentifier(identifier, era, period, moment);
            var relation = Relation.NewRelation(identifier);
            return new ParentComponent { Relation = relation };
        }

        public static PreviousComponent CreatePreviousComponent(Guid storageId, Guid accountId, Guid spaceId, UInt64 era, UInt64 period, UInt64 moment)
        {
            var identifier = Identifier.NewIdentifier(storageId, accountId, spaceId);
            identifier = Identifier.NewIdentifier(identifier, era, period, moment);
            var relation = Relation.NewRelation(identifier);
            return new PreviousComponent { Relation = relation };
        }

        public static NextComponent CreateNextComponent(Guid storageId, Guid accountId, Guid spaceId, UInt64 era, UInt64 period, UInt64 moment)
        {
            var identifier = Identifier.NewIdentifier(storageId, accountId, spaceId);
            identifier = Identifier.NewIdentifier(identifier, era, period, moment);
            var relation = Relation.NewRelation(identifier);
            return new NextComponent { Relation = relation };
        }

        public static UpdatesComponent CreateUpdatesComponent(Guid storageId, Guid accountId, Guid spaceId, UInt64 era, UInt64 period, UInt64 moment)
        {
            var identifier = Identifier.NewIdentifier(storageId, accountId, spaceId);
            identifier = Identifier.NewIdentifier(identifier, era, period, moment);
            return new UpdatesComponent { Relations = new Relation[] { Relation.NewRelation(identifier) } };
        }

        public static DowndateComponent CreateDowndateComponent(Guid storageId, Guid accountId, Guid spaceId, UInt64 era, UInt64 period, UInt64 moment)
        {
            var identifier = Identifier.NewIdentifier(storageId, accountId, spaceId);
            identifier = Identifier.NewIdentifier(identifier, era, period, moment);
            var relation = Relation.NewRelation(identifier);
            return new DowndateComponent { Relation = relation };
        }

        public static EtAlii.Ubigia.Storage.ContainerIdentifier CreateLongContainerIndentifier(IContainerProvider containerProvider)
        {
            return containerProvider.ForEntry(
                Guid.NewGuid(),         // Storage
                Guid.NewGuid(),         // Account
                Guid.NewGuid(),         // Space
                (ulong.MaxValue - 1),   // Era
                (ulong.MaxValue - 1),   // Period
                (ulong.MaxValue - 1));  // Moment
        }

        public static ContainerIdentifier[] CreateSimpleContainerIdentifiers(int count)
        {
            var result = new ContainerIdentifier[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = CreateSimpleContainerIdentifier();
            }
            return result;
        }

        public static ContainerIdentifier CreateSimpleContainerIdentifier(string id = null)
        {
            string[] paths;
            if (!String.IsNullOrEmpty(id))
            {
                paths = new string[]
                {
                    id,
                    Guid.NewGuid().ToString(),
                };
            }
            else
            {
                paths = new string[]
                {
                    Guid.NewGuid().ToString(),
                };
            }
            return EtAlii.Ubigia.Storage.ContainerIdentifier.FromPaths(paths);
        }
    }
}
