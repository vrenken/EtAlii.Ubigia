namespace EtAlii.Ubigia
{
    using System.Collections.Generic;

    public interface IReadOnlyContentDefinition : IBlob
    {
        ulong Size { get; }
        ulong Checksum { get; }

        IEnumerable<IReadOnlyContentDefinitionPart> Parts { get; }
    }
}
