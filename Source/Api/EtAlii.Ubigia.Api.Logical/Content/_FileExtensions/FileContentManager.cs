// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class FileContentManagerExtensions
    {
        public static async Task<bool> Upload(this IContentManager contentManager, string localDataIdentifier, Identifier identifier)
        {
            try
            {
                var fileInfo = new FileInfo(localDataIdentifier);
                var size = fileInfo.Length;
#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
                await using var stream = File.OpenRead(localDataIdentifier);
#pragma warning restore CA2007

                await contentManager
                    .Upload(stream, (uint)size, identifier)
                    .ConfigureAwait(false);

                return true;
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
#pragma warning disable CA2007 // REMOVE WHEN .NET 6 IS STABLE
                await using var stream = File.Create(localDataIdentifier);
#pragma warning restore CA2007
                await contentManager
                    .Download(stream, identifier, validateChecksum)
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Unable to download file", e);
            }
        }
    }
}
