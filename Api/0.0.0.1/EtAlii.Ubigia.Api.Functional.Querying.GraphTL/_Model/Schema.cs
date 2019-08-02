namespace EtAlii.Ubigia.Api.Functional
{
    /// <summary>
    /// The Query class contains all information needed to execute graph traversal actions on the current infrastructureClient.
    /// </summary>
    public class Schema
    {
        public StructureFragment Structure { get; }

        public Schema(StructureFragment structure)
        {
            Structure = structure;
        }
    }
}
