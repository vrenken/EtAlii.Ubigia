namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.IO;

    public interface IContentManager
    {
        void Upload(Stream localDataStream, UInt64 sizeInBytes, Identifier identifier);
        void Download(Stream localDataStream, Identifier identifier, bool validateChecksum = false);

            //void Upload(string localDataIdentifier, Identifier identifier);
        //void Upload(Stream stream, UInt64 size, Identifier identifier);

        //void Download(string localDataIdentifier, Identifier identifier, bool validateChecksum = false);
        //void Download(Stream stream, Identifier identifier, bool validateChecksum = false);
    }
}
