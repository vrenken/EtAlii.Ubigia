// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;

    public static class FileHierarchicalContentManagerExtensions
    {
        public static bool Upload(this IHierarchicalContentManager hierarchicalContentManager, string localDataIdentifier, in Identifier identifier)
        {
            try
            {
            //    var fileInfo = new FileInfo(localDataIdentifier)
            //    var size = fileInfo.Length
            //    using (var stream = File.OpenRead(localDataIdentifier))
            //    [
            //        base.Upload(stream, (UInt32)size, identifier)
            //    ]
            return true;
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Unable to upload folder", e);
            }
        }

        public static void Download(this IHierarchicalContentManager hierarchicalContentManager, string localDataIdentifier, in Identifier identifier, bool validateChecksum = false)
        {
            try
            {
            //    using (var stream = File.Create(localDataIdentifier))
            //    [
            //        base.Download(stream, identifier, validateChecksum)
            //    ]
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Unable to download folder", e);
            }
        }

        public static void Sync(this IHierarchicalContentManager hierarchicalContentManager, string localDataIdentifier, in Identifier identifier)
        {
            try
            {
            //    using (var stream = File.Create(localDataIdentifier))
            //    [
            //        base.Download(stream, identifier, validateChecksum)
            //    ]
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Unable to sync folder", e);
            }
        }
    }
}
