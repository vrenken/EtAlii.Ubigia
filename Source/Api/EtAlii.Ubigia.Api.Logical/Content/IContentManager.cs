// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Facade that hides away complex logical Content operations.
    /// </summary>
    public interface IContentManager
    {
        Task Upload(Stream stream, ulong sizeInBytes, Identifier identifier);
        Task Download(Stream stream, Identifier identifier, bool validateChecksum = false);

        Task<bool> HasContent(Identifier identifier);

        //void Upload(string localDataIdentifier, Identifier identifier)
        //void Upload(Stream stream, UInt64 size, Identifier identifier)

        //void Download(string localDataIdentifier, Identifier identifier, bool validateChecksum = false)
        //void Download(Stream stream, Identifier identifier, bool validateChecksum = false)
    }
}
