namespace EtAlii.Ubigia.Api.Functional.Context
{
    /// <summary>
    /// The Query class contains all information needed to execute graph traversal actions on the current infrastructureClient.
    /// </summary>
#pragma warning disable CA1724// This class really has a purpose.
    public class Schema
#pragma warning restore CA1724
    {
        public StructureFragment Structure { get; }

        public Schema(StructureFragment structure)
        {
            Structure = structure;
        }
    }
}
