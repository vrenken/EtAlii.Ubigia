namespace EtAlii.Ubigia.Api.Functional
{
    /// <summary>
    /// The Query class contains all information needed to execute graph traversal actions on the current infrastructureClient.
    /// </summary>
    public class Query
    {
        public Fragment Structure { get; }

        public Query(Fragment structure)
        {
            Structure = structure;
        }
    }
}
