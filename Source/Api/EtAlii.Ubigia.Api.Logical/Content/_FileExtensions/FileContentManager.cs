// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class FileContentManagerExtensions
    {
        public static async Task Upload(this IContentManager contentManager, string localDataIdentifier, Identifier identifier)
        {
            try
            {
                var fileInfo = new FileInfo(localDataIdentifier);
                var size = fileInfo.Length;
                using (var stream = File.OpenRead(localDataIdentifier))
                {
                    await contentManager
                        .Upload(stream, (uint)size, identifier)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Unable to upload file", e);
            }
        }

        public static async Task Download(this IContentManager contentManager, string localDataIdentifier, Identifier identifier, bool validateChecksum = false)
        {
            try
            {
                using (var stream = File.Create(localDataIdentifier))
                {
                    await contentManager
                        .Download(stream, identifier, validateChecksum)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Unable to download file", e);
            }
        }
    }
}
