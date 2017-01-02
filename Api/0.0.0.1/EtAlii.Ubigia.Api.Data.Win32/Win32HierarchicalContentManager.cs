namespace EtAlii.Ubigia.Api
{
    using System;
    using EtAlii.Ubigia.Api.Logical;

    public static class Win32HierarchicalContentManagerExtensions
    {
        public static void Upload(this IHierarchicalContentManager hierarchicalContentManager, string localDataIdentifier, Identifier identifier)
        {
            try
            {
            //    var fileInfo = new FileInfo(localDataIdentifier);
            //    var size = fileInfo.Length;
            //    using (var stream = File.OpenRead(localDataIdentifier))
            //    {
            //        base.Upload(stream, (UInt32)size, identifier);
            //    }
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Unable to upload folder", e);
            }
        }

        public static void Download(this IHierarchicalContentManager hierarchicalContentManager, string localDataIdentifier, Identifier identifier, bool validateChecksum = false)
        {
            try
            {
            //    using (var stream = File.Create(localDataIdentifier))
            //    {
            //        base.Download(stream, identifier, validateChecksum);
            //    }
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Unable to download folder", e);
            }
        }

        public static void Sync(this IHierarchicalContentManager hierarchicalContentManager, string localDataIdentifier, Identifier identifier)
        {
            try
            {
            //    using (var stream = File.Create(localDataIdentifier))
            //    {
            //        base.Download(stream, identifier, validateChecksum);
            //    }
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Unable to sync folder", e);
            }
        }
    }
}
