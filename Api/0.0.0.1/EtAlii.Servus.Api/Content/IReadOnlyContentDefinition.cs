namespace EtAlii.Servus.Api
{
    using System;
    using System.Collections.Generic;

    public interface IReadOnlyContentDefinition : IBlob
    {
        UInt64 Size { get; }
        UInt64 Checksum { get; }

        IEnumerable<IReadOnlyContentDefinitionPart> Parts { get; }
        //BlobSummary Summary { get; }
    }
}
