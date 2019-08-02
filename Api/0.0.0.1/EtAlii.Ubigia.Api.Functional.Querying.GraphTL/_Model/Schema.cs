namespace EtAlii.Ubigia.Api.Functional
{
    /// <summary>
    /// The Query class contains all information needed to execute graph traversal actions on the current infrastructureClient.
    /// </summary>
    public class Schema
    {
        public Fragment Structure { get; }

        public Schema(Fragment structure)
        {
            Structure = structure;
        }
    }
}
